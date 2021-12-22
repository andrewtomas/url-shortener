using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;

namespace Application.Features.UrlItems;

public class DeleteUrlItemCommand : IRequest<CQRSResponse>
{
    public string Id { get; set; }
}

public class DeleteUrlItemCommandHandler : IRequestHandler<DeleteUrlItemCommand, CQRSResponse>
{
    private readonly IAppDbContext _context;

    public DeleteUrlItemCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<CQRSResponse> Handle(DeleteUrlItemCommand request, CancellationToken cancellationToken)
    {
        var id = Guid.Parse(request.Id);
        var item = await _context.UrlItems.FindAsync(new object[] {id}, cancellationToken);

        if (item == null)
            return CQRSResponse.NotFound<string>($"Item with Id: {request.Id} has not been found.");

        _context.UrlItems.Remove(item);
        await _context.SaveChangesAsync(cancellationToken);

        return CQRSResponse.Success<string>("Item has been deleted successfully.");
    }
}