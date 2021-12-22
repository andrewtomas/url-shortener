using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;

namespace Application.Features.URLGroups;

public class UpdateUrlGroupCommand : IRequest<CQRSResponse>
{
    public string Id { get; set; }
    public string Name { get; set; }
}

public class UpdateUrlGroupCommandHandler : IRequestHandler<UpdateUrlGroupCommand, CQRSResponse>
{
    private readonly IAppDbContext _context;

    public UpdateUrlGroupCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<CQRSResponse> Handle(UpdateUrlGroupCommand request, CancellationToken cancellationToken)
    {
        var id = Guid.Parse(request.Id);
        var group = await _context.UrlGroups
            .FindAsync(new object[] {id}, cancellationToken);
        if (group == null)
            return CQRSResponse.NotFound<string>($"Url group with id: {request.Id} was not found.");

        group.Name = request.Name;
        await _context.SaveChangesAsync(cancellationToken);

        return CQRSResponse.Success(group);
    }
}