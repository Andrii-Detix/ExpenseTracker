using Shared.Results;

namespace ExpenseTracker.Domain.ExpenseRecords.ValueObjects.ExpenseAmounts;

public record ExpenseAmount
{
    private const int DecimalPlaces = 2;
    
    private ExpenseAmount(decimal value)
    {
        Value = value;
    }
    
    public decimal Value { get; }

    public static Result<ExpenseAmount> Create(decimal value)
    {
        if (value < 0)
        {
            return ExpenseAmountErrors.IsNegative;
        }

        value = Math.Round(value, DecimalPlaces, MidpointRounding.AwayFromZero);
        
        return new ExpenseAmount(value);
    }
}