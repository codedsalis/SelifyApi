using Microsoft.EntityFrameworkCore;
using SelifyApi.Entities;

namespace SelifyApi.Data;

public class DataContext: DbContext
{
    public DataContext(DbContextOptions<DataContext> options) 
        : base(options)
    {
        
    }

    public DbSet<Food> Foods { get; set; } = null!;
}