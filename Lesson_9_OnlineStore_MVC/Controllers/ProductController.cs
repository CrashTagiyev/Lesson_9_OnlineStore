using Lesson_9_OnlineStore_DataAccess.Reposiotries.Abstracts;
using Lesson_9_OnlineStore_Domain.Entities.Concretes;
using Lesson_9_OnlineStore_MVC.Models;
using Lesson_9_OnlineStore_MVC.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Lesson_9_OnlineStore_MVC.Controllers
{
	public class ProductController : Controller
	{
		private readonly IProductRepository _productRepo;
		private readonly ICategoryRepository _categoryRepo;
		private readonly ITagRepository _tagsRepo;

		public ProductController(IProductRepository productRepo, ICategoryRepository categoryRepo, ITagRepository tagRepository)
		{
			_productRepo = productRepo;
			_categoryRepo = categoryRepo;
			_tagsRepo = tagRepository;
		}

		public async Task<IActionResult> GetProducts(int? pageNumber)
		{

			List<ProductViewModel> products = new();
			foreach (Product product in await _productRepo.GetAllAsync())
			{
				products.Add(new()
				{
					Id = product.Id,
					Name = product.Name,
					Description = product.Description,
					Price = product.Price,
					CategoryName = (await _categoryRepo.GetByIdAsync(product.CategoryId)).Name
				});
			}
			int pageSize = 6;
			return View(PaginatedList<ProductViewModel>.Create(products, pageNumber ?? 1, pageSize));
		}


		[HttpGet]
		public async Task<IActionResult> ProductDetail(int id)
		{
			var product = await _productRepo.GetByIdLazyAsync(id);
			var ProductDetail = new ProductViewModel()
			{
				Id = product.Id,
				Name = product.Name,
				Description = product.Description,
				Price = product.Price,
				CategoryName = (await _categoryRepo.GetByIdAsync(product.CategoryId)).Name,
				Tags = product.Tags.ToList()
			};
			return View(ProductDetail);
		}


		[HttpGet]
		public async Task<IActionResult> AddProduct()
		{
			var tags = await _tagsRepo.GetAllAsync();
			var selectItems = new List<SelectListItem>();
			foreach (Tag tag in tags)
			{
				selectItems.Add(new SelectListItem { Value = tag.Id.ToString(), Text = tag.Name });
			}
			ViewBag.tags = new MultiSelectList(selectItems, "Value", "Text");
			var categories = await _categoryRepo.GetAllAsync();
			ViewBag.categories = new SelectList(categories, "Id", "Name");
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> AddProduct(NewProductViewModel product, IFormFile image)
		{
			if (image == null || image.Length == 0)
				return BadRequest("No file uploaded.");

			if (!image.ContentType.StartsWith("image/"))
				return BadRequest("Uploaded file is not an image.");


			// You might want to restrict the file size as well
			if (image.Length > 10 * 1024 * 1024) // 10 MB limit
				return BadRequest("Uploaded file exceeds the maximum allowed size.");

			using (var memoryStream = new MemoryStream())
			{
				await image.CopyToAsync(memoryStream);

				product.Image = memoryStream.ToArray();
				product.ImageContentType = image.ContentType;
			}

			Product newProduct = new()
			{
				Name = product.Name,
				Description = product.Description,
				Price = product.Price,
				CategoryId = product.CategoryId,
				Image = product.Image,
				ImageContentType = product.ImageContentType,
				Tags = new List<Tag>()
			};

			foreach (int tagId in product.Tags)
			{
				var tag = await _tagsRepo.GetByIdAsync(tagId);
				if (tag != null)
					newProduct.Tags.Add(tag);
			}

			await _productRepo.AddAsync(newProduct);

			return RedirectToAction("GetProducts");
		}

		public async Task<IActionResult> GetImage(int id)
		{
			var product = await _productRepo.GetByIdAsync(id);

			if (product == null || product.Image == null)
			{
				return NotFound();
			}

			return File(product.Image, product.ImageContentType);
		}

	}
}
