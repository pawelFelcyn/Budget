namespace Budget.Data.Entities;

public class Category
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Color { get; set; }
    public CategoryType Type { get; set; }
}