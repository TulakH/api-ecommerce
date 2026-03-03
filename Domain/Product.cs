namespace Domain;

public class Product
{
    public Guid Id {get; set;} = Guid.NewGuid();
    public required string Name {get; set;}
    public required string Description {get; set;}
    public required decimal Price {get; set;}
    public int StockQuantity {get; set;}
    public Guid CategoryId {get; set;}
    public Category? Category {get; set;}
    public ICollection<OrderItem>? OrderItems {get; set;}  
    public DateTimeOffset CreatedAt {get; set;}
    public DateTimeOffset UpdatedAt{get; set;}
}