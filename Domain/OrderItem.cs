namespace Domain;

public class OrderItem
{
    public required Guid Id {get; set;} = Guid.NewGuid();
    public required Guid OrderId {get; set;}
    public required Guid ProductId {get; set;}
    public int Quantity {get; set;}
    public decimal UnitPrice {get; set;}
    public decimal TotalPrice {get; set;}
    public Product? Product {get; set;}
    public Order? Order {get; set;}
}