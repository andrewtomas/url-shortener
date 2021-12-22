namespace Application.Common.Models;

public class ValidationResult
{
    public ValidationResult()
    {
    }

    public ValidationResult(string errorMessage)
    {
        ErrorMessage = errorMessage;
    }

    public bool IsSuccess => string.IsNullOrWhiteSpace(ErrorMessage);
    public string ErrorMessage { get; set; }

    public static ValidationResult Success => new();

    public static ValidationResult Error(string message)
    {
        return new(message);
    }
}