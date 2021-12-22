using System.Reflection;
using Application.Common.Interfaces;
using Application.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Infrastructure.Persistance;

public class AppDbContext : DbContext, IAppDbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<UrlItem> UrlItems => Set<UrlItem>();
    public DbSet<UrlGroup> UrlGroups => Set<UrlGroup>();

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
}