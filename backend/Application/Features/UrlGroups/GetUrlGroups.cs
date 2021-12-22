using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.URLGroups;

public record GetAllGroupsQuery : IRequest<CQRSResponse>;

public class GroupsVm
{
    public IList<UrlGroup> Groups { get; set; }
}

public class GetAllGroupsQueryHandler : IRequestHandler<GetAllGroupsQuery, CQRSResponse>
{
    private readonly IAppDbContext _context;

    public GetAllGroupsQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<CQRSResponse> Handle(GetAllGroupsQuery request, CancellationToken cancellationToken)
    {
        var groups = new GroupsVm
        {
            Groups = await _context.UrlGroups
                .Include(g => g.URLItems)
                .AsSplitQuery()
                .ToListAsync(cancellationToken)
        };

        return CQRSResponse.Success(groups);
    }
}