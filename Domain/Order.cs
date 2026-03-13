namespace Domain;

public class Order
{
    public required Guid Id {get; set;} = Guid.NewGuid();
    public required Guid ClientId {get; set;}
    public DateTimeOffset CreatedAt {get; set;}
    public required ICollection<OrderItem> OrderItems {get; set;}
}
