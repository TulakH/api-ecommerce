public interface ICategoryRepository
{
    IEnumerable<Category> GetCategorys();
    IQueryable<Category> GetCategorysAsQueyrable();
    Task<Category?> GetCategoryById(Guid id);
    Task<Category?> GetCategoryByName(string name);
    Task InsertCategory(Category Category);
    void UpdateCategory(Category Category);
    Task DeleteCategory(Guid id);
    Task Save();
}