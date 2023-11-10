namespace Budget.Data.Entities;

public  class Transaction
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal Amount { get; set; }
    public DateTime TransactionDate { get; set; }

    public Guid CategoryId { get; set; }
    public virtual Category? Category { get; set; }
}