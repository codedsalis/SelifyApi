using Microsoft.EntityFrameworkCore;
using SelifyApi.Entities;

namespace SelifyApi.Data;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<Food> Foods { get; init; }
    public DbSet<User> Users { get; init; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Food>()
            .HasOne(f => f.User);
    }

}