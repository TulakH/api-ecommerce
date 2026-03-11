using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateCategory()
    {
        return Ok();
    }
}