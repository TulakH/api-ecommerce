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

    [HttpPost]
    public async Task<ActionResult> CreateProduct([FromBody] ProductDto product)
    {
        try
        {
            await productRepository.InsertProduct(product.Adapt<Product>());
            return Created();
        }
        catch 
        {
            return Problem("Internal error contact support");
        }
    }


}