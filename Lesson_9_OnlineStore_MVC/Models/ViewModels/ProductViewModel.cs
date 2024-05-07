using Lesson_9_OnlineStore_Domain.Entities.Concretes;

namespace Lesson_9_OnlineStore_MVC.Models.ViewModels
{
	public class ProductViewModel
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		public string? Description { get; set; }
		public byte[]? Image { get; set; }
		public double? Price { get; set; }
		public  string? CategoryName { get; set; }
		public  List<Tag>? Tags { get; set; }
	}
}
