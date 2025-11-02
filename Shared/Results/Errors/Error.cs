using Shared.Results.Exceptions;

namespace Shared.Results.Errors;

public record Error
{
    private const string Separator = "||";
    private Error(string code, string message, ErrorType type)
    {
        Code = code.Trim();
        Message = message.Trim();
        Type = type;
    }
    
    public string Code { get; }
    public string Message { get; }
    public ErrorType Type { get; }

    public string Serialize()
    {
        return string.Join(Separator, Code, Message, Type);
    }

    public static Error Deserialize(string serialized)
    {
        if (string.IsNullOrWhiteSpace(serialized))
            throw new InvalidSerializedErrorException();

        string[] parts = serialized.Split(Separator);

        if (parts.Length != 3 || 
            !Enum.TryParse(parts[2], ignoreCase: true, out ErrorType type))
            throw new InvalidSerializedErrorException();


        return new Error(parts[0], parts[1], type);
    }

    
    public static Error Create(string code, string message, ErrorType type) =>
        new(code, message, type);
    
    public static Error Validation(string code, string message) =>
        new(code, message, ErrorType.Validation);
    
    public static Error Unauthorized(string code, string message) =>
        new(code, message, ErrorType.Unauthorized);
    
    public static Error NotFound(string code, string message) =>
        new(code, message, ErrorType.NotFound);
    
    public static Error Conflict(string code, string message) =>
        new(code, message, ErrorType.Conflict);
    
    public static Error Failure(string code, string message) =>
        new(code, message, ErrorType.Failure);

    public static readonly Error None =
        new(string.Empty, string.Empty, ErrorType.None);
}