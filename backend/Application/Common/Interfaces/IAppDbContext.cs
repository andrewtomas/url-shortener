using Application.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces;

public interface IAppDbContext
{
    DbSet<UrlItem> UrlItems { get; }
    DbSet<UrlGroup> UrlGroups { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}