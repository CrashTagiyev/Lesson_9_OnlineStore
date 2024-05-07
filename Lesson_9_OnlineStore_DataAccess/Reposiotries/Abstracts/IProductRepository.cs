using Lesson_9_OnlineStore_Domain.Entities.Concretes;

namespace Lesson_9_OnlineStore_DataAccess.Reposiotries.Abstracts;

public interface IProductRepository:IGenericRepository<Product>
{
	public  Task<Product> GetByIdLazyAsync(int id);
	public Task<List<Product>> GetAllProductsLazyAsync();
}
