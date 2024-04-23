using Microsoft.EntityFrameworkCore;
using SelifyApi.Entities;

namespace SelifyApi.Data;

public class DataContext: DbContext
{
    public DataContext(DbContextOptions<DataContext> options) 
        : base(options)
    {
        
    }

    public DbSet<Food> Foods { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Food>()
            .HasOne(f => f.User);
    }

}