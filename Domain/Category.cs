public class Category
{
    public required Guid Id {get; set;} = Guid.NewGuid();
    public required string Name {get; set;}
    public string? Description {get; set;}
    public ICollection<Product>? Products {get; set;}
}