using Microsoft.EntityFrameworkCore.ChangeTracking;

public interface ICategoryRepository
{
    IEnumerable<Category> GetCategorys();
    IQueryable<Category> GetCategorysAsQueyrable();
    ValueTask<Category?> GetCategoryById(Guid id);
    Task<Category?> GetCategoryByName(string name);
    ValueTask<EntityEntry<Category>> InsertCategory(Category Category);
    void UpdateCategory(Category Category);
    Task DeleteCategory(Guid id);
    Task Save();
}