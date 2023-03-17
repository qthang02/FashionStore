using API.Entities;

namespace API.Repositories;

public interface ICategoryRepository
{
    Task<IEnumerable<Category>> GetCategories();
    Task<Category> GetCategoryById(int id);
    void AddCategory(Category category);
    Task<bool> SaveAllAsync();
}