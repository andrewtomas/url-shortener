using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Application.Features.UrlItems;

public class CreateUrlItemCommand : IRequest<CQRSResponse>
{
    public string Url { get; set; }
    public string GroupId { get; set; }
    public string? Name { get; set; }
}

public class CreateUrlItemCommandValidator : IValidator<CreateUrlItemCommand>
{
    public Task<ValidationResult> Validate(CreateUrlItemCommand request)
    {
        if (string.IsNullOrEmpty(request.Url))
            return Task.FromResult(ValidationResult.Error("URL is empty."));

        if (!IsUrlValid(request.Url))
            return Task.FromResult(ValidationResult.Error("URL is not valid, please enter a valid URL."));

        return Task.FromResult(ValidationResult.Success);
    }

    private static bool IsUrlValid(string url)
    {
        return Uri.TryCreate(url, UriKind.Absolute, out var uriResult)
               && uriResult != null
               && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
    }
}

public class CreateUrlItemCommandHandler : IRequestHandler<CreateUrlItemCommand, CQRSResponse>
{
    private readonly IAppDbContext _context;

    public CreateUrlItemCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<CQRSResponse> Handle(CreateUrlItemCommand request, CancellationToken cancellationToken)
    {
        var urlItem = new UrlItem
        {
            Url = request.Url,
            GroupId = Guid.Parse(request.GroupId)
        };

        if (string.IsNullOrEmpty(request.Name))
        {
            urlItem.ShortUrl = GenerateShortUrl();
        }
        else
        {
            var urlExists = _context.UrlItems.Any(i => i.ShortUrl.Equals(request.Name));

            if (urlExists)
                return CQRSResponse.AlreadyExists<string>(
                    $"{request.Name} already exsits, please choose another name.");

            urlItem.ShortUrl = request.Name;
        }

        await _context.UrlItems.AddAsync(urlItem, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return CQRSResponse.Success(urlItem);
    }

    private string GenerateShortUrl(int length = 5)
    {
        Random random = new();
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
    }
}