using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;

namespace Application.Features.UrlGroups;

public class DeleteUrlGroupCommand : IRequest<CQRSResponse>
{
    public string Id { get; set; }
}

public class DeleteUrlGroupCommandHandler : IRequestHandler<DeleteUrlGroupCommand, CQRSResponse>
{
    private readonly IAppDbContext _context;

    public DeleteUrlGroupCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<CQRSResponse> Handle(DeleteUrlGroupCommand request, CancellationToken cancellationToken)
    {
        var id = Guid.Parse(request.Id);
        var group = await _context.UrlGroups
            .FindAsync(new object[] {id}, cancellationToken);

        if (group == null) return CQRSResponse.NotFound<string>($"Group with {request.Id} was not found.");

        _context.UrlGroups.Remove(group);
        await _context.SaveChangesAsync(cancellationToken);

        return CQRSResponse.Success($"Group: '{group.Name}' was deleted successfully.");
    }
}