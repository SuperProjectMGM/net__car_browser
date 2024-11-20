using Microsoft.EntityFrameworkCore;
using search.api.Models;

namespace search.api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    
    public DbSet<UserDetails> UsersDetails { get; set; }
    
    public DbSet<Rental> Rentals { get; set; }
    
    public DbSet<RentalFirm> Firms { get; set; }
}