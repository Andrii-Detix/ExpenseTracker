using ExpenseTracker.Domain.Abstractions;
using ExpenseTracker.Domain.Currencies.ValueObjects.Codes;
using ExpenseTracker.Domain.Currencies.ValueObjects.CurrencyNames;
using Shared.Results;

namespace ExpenseTracker.Domain.Currencies;

public class Currency : BaseEntity
{
    private Currency(Guid id, CurrencyCode code, CurrencyName name) 
        : base(id)
    {
        Code = code;
        Name = name;
    }
    
    private Currency() { }
    
    public CurrencyCode Code { get; }
    public CurrencyName Name { get; }

    public static Result<Currency> Create(Guid id, string code, string name)
    {
        Result<CurrencyCode> codeResult = CurrencyCode.Create(code);
        if (codeResult.IsFailure)
        {
            return codeResult.Error;
        }
        
        Result<CurrencyName> nameResult = CurrencyName.Create(code);
        if (nameResult.IsFailure)
        {
            return nameResult.Error;
        }

        return new Currency(id, codeResult.Value!, nameResult.Value!);
    }
}