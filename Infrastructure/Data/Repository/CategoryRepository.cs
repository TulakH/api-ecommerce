using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public class CategoryRepository(ApplicationDbContext dbContext) : ICategoryRepository
{

    private bool _disposed = false;

    public async Task DeleteCategory(Guid id)
    {
        var category = await dbContext.Categories.FindAsync(id);
        if (category == null) return;
        dbContext.Categories.Remove(category);
    }

    public IQueryable<Category> GetCategorysAsQueyrable() => dbContext.Categories.AsQueryable();

    public async Task<Category?> GetCategoryById(Guid id) => await dbContext.Categories.FindAsync(id);

    public async Task<Category?> GetCategoryByName(string name) => await dbContext.Categories.FirstOrDefaultAsync(c => c.Name == name);

    public IEnumerable<Category> GetCategorys() => dbContext.Categories.AsEnumerable();

    public async Task InsertCategory(Category category) => await dbContext.AddAsync(category);

    public async Task Save() => await dbContext.SaveChangesAsync();

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