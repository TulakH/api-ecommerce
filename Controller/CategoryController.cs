using Mapster;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class CategoryController(ICategoryRepository categoryRepository) : ControllerBase
{

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] PageParameters pageParameters)
    {
        try
        {
            var categoriesQuery = categoryRepository.GetCategorysAsQueyrable();
            var pagedCategories = await PagedList<CategoryDto>.CreateAsync<Category, CategoryDto>(categoriesQuery, pageParameters.PageNumber, pageParameters.PageSize);
            return Ok(pagedCategories);
        }
        catch (System.Exception)
        {
        //TODO : Gestion d'erreur
           return Problem("Internal error contact support");
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategory([FromBody] CreateCategory createCategory)
    {
        
        try
        {
            var category = createCategory.Adapt<Category>();
            await categoryRepository.InsertCategory(category);
            await categoryRepository.Save();
            return Created();
        }
        catch (Exception ex)
        {
            return Problem("Internal error contact support");
        }
    }
}