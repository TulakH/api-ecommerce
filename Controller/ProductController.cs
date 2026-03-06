using Controller.Model;
using Domain;
using Infrastructure.Data.Repository.Abstraction;
using Mapster;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class ProductController(IProductRepository productRepository) : ControllerBase
{
    [HttpGet]
    public ActionResult<List<ProductDto>> Get()
    {
        
        try
        {
            return productRepository.GetProducts().Select(p => p.Adapt<ProductDto>()).ToList();
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

    [HttpPost]
    public async Task<ActionResult> CreateProduct([FromBody] CreateProduct product)
    {
        try
        {
            await productRepository.InsertProduct(product.Adapt<Product>());
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
            productRepository.UpdateProduct(product.Adapt<Product>());
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