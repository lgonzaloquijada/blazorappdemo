using blazorappdemo.Models;

namespace blazorappdemo.Services;

public interface ICategoryService
{
    Task<IEnumerable<Category>?> GetCategories();
    Task<Category?> GetCategory(int id);
    Task AddCategory(Category category);
    Task UpdateCategory(Category category);
    Task DeleteCategory(int id);
}
