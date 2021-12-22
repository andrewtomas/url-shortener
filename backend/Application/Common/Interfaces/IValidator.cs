using Application.Common.Models;

namespace Application.Common.Interfaces;

public interface IValidator<TRequest>
{
    Task<ValidationResult> Validate(TRequest request);
}