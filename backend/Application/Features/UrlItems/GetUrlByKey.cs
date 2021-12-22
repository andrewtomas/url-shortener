using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.UrlItems;

public class GetUrlByKeyQuery : IRequest<CQRSResponse<string>>
{
    public string Key { get; set; }
}

public class GetUrlByKeyQueryValidator : IValidator<GetUrlByKeyQuery>
{
    public Task<ValidationResult> Validate(GetUrlByKeyQuery request)
    {
        if (string.IsNullOrWhiteSpace(request.Key))
            return Task.FromResult(ValidationResult.Error("Please enter a valid key."));

        return Task.FromResult(ValidationResult.Success);
    }
}

public class GetUrlByKeyQueryHandler : IRequestHandler<GetUrlByKeyQuery, CQRSResponse<string>>
{
    private readonly IAppDbContext _context;

    public GetUrlByKeyQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<CQRSResponse<string>> Handle(GetUrlByKeyQuery request, CancellationToken cancellationToken)
    {
        var url = await _context.UrlItems
            .FirstOrDefaultAsync(u => u.ShortUrl.Equals(request.Key), cancellationToken);

        return url == null
            ? CQRSResponse.NotFound<string>($"{request.Key} is not found.")
            : CQRSResponse.Success(url.Url);
    }
}