using CramCrow.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CramCrow.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<User> Users { get; set; } = default!;
    public DbSet<Card> Cards { get; set; } = default!;
}