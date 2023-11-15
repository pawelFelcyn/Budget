using Budget.Data.Entities;
using Budget.Dtos;

namespace Budget.Mappers;

internal static class TransactionMappers
{
    public static Transaction ToDomain(this CreateTransactionDto dto)
    {
        return new Transaction()
        {
            Name = dto.Name,
            Description = dto.Description,
            Amount = dto.Amount,
            TransactionDate = dto.TransactionDate,
            CategoryId = dto.CategoryId.Value
        };
    }
}
