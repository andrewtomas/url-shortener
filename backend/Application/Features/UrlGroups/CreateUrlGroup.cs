using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Entities;
using MediatR;

namespace Application.Features.URLGroups;

public record CreateUrlGroupCommand(string Name) : IRequest<CQRSResponse>;

public class CreateUrlGroupValidator : IValidator<CreateUrlGroupCommand>
{
    public Task<ValidationResult> Validate(CreateUrlGroupCommand request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
            return Task.FromResult(ValidationResult.Error("Group name is invalid. Enter a valid group name."));

        return Task.FromResult(ValidationResult.Success);
    }
}

public class CreateUrlGroupCommandHandler : IRequestHandler<CreateUrlGroupCommand, CQRSResponse>
{
    private readonly IAppDbContext _context;

    public CreateUrlGroupCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<CQRSResponse> Handle(CreateUrlGroupCommand request, CancellationToken cancellationToken)
    {
        var urlGroup = new UrlGroup
        {
            Name = request.Name
        };

        await _context.UrlGroups.AddAsync(urlGroup, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return CQRSResponse.Success(urlGroup);
    }
}