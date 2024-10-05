using Microsoft.EntityFrameworkCore;
using StackLab.Survey.Domain.Entities;

namespace StackLab.Survey.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    public DbSet<User> Users { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
