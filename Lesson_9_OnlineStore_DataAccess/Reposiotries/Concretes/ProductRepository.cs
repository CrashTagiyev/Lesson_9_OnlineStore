using Lesson_9_OnlineStore_DataAccess.Contexts;
using Lesson_9_OnlineStore_DataAccess.Reposiotries.Abstracts;
using Lesson_9_OnlineStore_Domain.Entities.Concretes;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.Arm;

namespace Lesson_9_OnlineStore_DataAccess.Reposiotries.Concretes;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
	public ProductRepository(AppDbContext context) : base(context)
	{
	}

	public async Task<List<Product>> GetAllProductsLazyAsync()
	{
		return await _context.Set<Product>().Include(p => p.Tags).ToListAsync();

		//var products = from p in _context.Products.Include(p => p.Tags)
		//			   select p;
	}
	public async Task<Product> GetByIdLazyAsync(int id)
	{
		return await _context.Products.Include(p => p.Tags).FirstOrDefaultAsync(x => x.Id == id);
	}
}
