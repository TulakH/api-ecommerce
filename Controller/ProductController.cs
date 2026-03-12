using Controller.Model;
using Domain;
using Infrastructure.Data.Repository.Abstraction;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class ProductController(IProductRepository productRepository) : ControllerBase
{

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] PageParameters pageParameters)
    {
        
        try
        {
            var productQueryable = productRepository.GetProductsAsQueryable();
            var pagedProduct = await PagedList<ProductDto>.CreateAsync<Product, ProductDto>(productQueryable, pageParameters.PageNumber, pageParameters.PageSize);
            return Ok(pagedProduct);
        }
        catch 
        {
            return Problem("Internal error contact support");
        }
    }

    [HttpGet]
    [Route("{id:guid}")]
    public async Task<ActionResult<ProductDto>> Get(Guid id)
    {
        
        try
        {
            var product = await productRepository.GetProductById(id);
            if (product is null) return NotFound();
            var productDTO = product.Adapt<ProductDto>();
            return  Ok(productDTO);
        }
        catch (Exception ex)
        {
            return Problem("Internal error contact support");
        }
    }

    [HttpGet]
    [Route("{keyword}")]
    public async Task<IActionResult> Search(string keyword, [FromQuery] PageParameters pageParameters)
    {

        if (keyword.Length < 3) return BadRequest("need more than 2 caracter");

        try
        {
            var productsQuery = productRepository.GetProductsByNameKeyword(keyword.ToLower());
            var pagedProduct = await PagedList<ProductDto>.CreateAsync<Product, ProductDto>(productsQuery, pageParameters.PageNumber, pageParameters.PageSize);
            return  Ok(pagedProduct);
        }
        catch (Exception ex)
        {
            return Problem("Internal error contact support");
        }
    }

    [HttpGet]
    [Route("by-category/{categoryId:guid}")]

    public async Task<IActionResult> GetProductByCategory([FromQuery] Guid categoryId, [FromQuery] PageParameters pageParameters)
    {
        try
        {
            var productsQuery = productRepository.GetProductsByCategoryId(categoryId);
            var pagedProduct = await PagedList<ProductDto>.CreateAsync<Product, ProductDto>(productsQuery, pageParameters.PageNumber, pageParameters.PageSize);
            return Ok(pagedProduct);
        }
        catch (Exception)
        {
            return Problem("internal error, contact support");
        }
    }

    [HttpPut]
    [Route("add-category")]
    public async Task<IActionResult> AddCategory(Guid productId, Guid categoryId, [FromServices] ICategoryRepository categoryRepository)
    {
        try
        {
            if (!await categoryRepository.GetCategorysAsQueyrable().AnyAsync(c => c.Id == categoryId)) return BadRequest($"{nameof(categoryId)} don't exist");
            var product = await productRepository.GetProductById(productId);
            if (product is null) return BadRequest($"{nameof(productId)} don't exist");
            product.CategoryId = categoryId;
            await productRepository.Save();

            return NoContent();
        }
        catch (System.Exception)
        {
            return Problem("internal error, contact support");
        }
    }

    [HttpPost]
    public async Task<ActionResult> CreateProduct([FromBody] CreateProduct product)
    {
        try
        {
            var productDb = product.Adapt<Product>();
            await productRepository.InsertProduct(productDb);
            await productRepository.Save();
            return Created();
        }
        catch (Exception ex)
        {
            return Problem("Internal error contact support");
        }
    }

    [HttpPut]
    public async Task<ActionResult> ModifyProduct([FromBody] ProductDto product)
    {
        try
        {
            var productDb = product.Adapt<Product>();
            productRepository.UpdateProduct(productDb);
            await productRepository.Save();
            return NoContent();
        }
        catch (Exception ex)
        {
            return Problem("Internal error contact support");
        }
    }

    [HttpDelete]
    public async Task<ActionResult> ModifyProduct(Guid id)
    {
        try
        {
            await productRepository.DeleteProduct(id);
            await productRepository.Save();
            return NoContent();
        }
        catch (Exception ex)
        {
            return Problem("Internal error contact support");
        }
    }
}