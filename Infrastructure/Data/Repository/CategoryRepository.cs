using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

public class CategoryRepository(ApplicationDbContext dbContext) : ICategoryRepository
{

    private bool _disposed = false;

    public async Task DeleteCategory(Guid id)
    {
        var category = await dbContext.Categories.FindAsync(id);
        if (category == null) return;
        dbContext.Categories.Remove(category);
    }

    public IQueryable<Category> GetCategorysAsQueyrable() => dbContext.Categories;

    public ValueTask<Category?> GetCategoryById(Guid id) => dbContext.Categories.FindAsync(id);

    public Task<Category?> GetCategoryByName(string name) => dbContext.Categories.FirstOrDefaultAsync(c => c.Name == name);

    public IEnumerable<Category> GetCategorys() => dbContext.Categories.AsEnumerable();

    public ValueTask<EntityEntry<Category>> InsertCategory(Category category) => dbContext.AddAsync(category);

    public Task Save() => dbContext.SaveChangesAsync();

    public void UpdateCategory(Category category)
    {
        dbContext.Categories.Entry(category).State = EntityState.Modified;
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                dbContext.Dispose();
            }
        }
        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

}