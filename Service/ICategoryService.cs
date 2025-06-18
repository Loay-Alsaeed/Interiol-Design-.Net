using Backend_.Net.DTO;
using Backend_.Net.Entities;

public interface ICategoryService
{
    Task<Category> AddCategoryAsync(CategoryDTO request);
    Task<List<Category>> GetAllCategoriesAsync();
    Task<Category?> GetCategoryByIdAsync(Guid id);
    Task<Category?> UpdateCategoryAsync(Guid id, CategoryDTO request);
    Task<bool> DeleteCategoryAsync(Guid id);
}
