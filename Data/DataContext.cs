using dotnetAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace dotnetAPI.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        
    }
    public DbSet<SuperHero> SuperHeroes { get; set; }
    public DbSet<SideKick> SideKicks { get; set; }
}