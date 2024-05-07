using Lesson_9_OnlineStore_Domain.Entities.Concretes;

namespace Lesson_9_OnlineStore_MVC.Models.ViewModels
{
	public class NewProductViewModel
	{
		public string? Name { get; set; }
		public string? Description { get; set; }
		public byte[]? Image { get; set; }
		public string? ImageContentType { get; set; }
		public double? Price { get; set; }
		public List<int>? Tags { get; set; }
		// Foreign Key
		public int CategoryId { get; set; }
	}
}
