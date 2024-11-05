using Microsoft.EntityFrameworkCore;
using search.api.Models;

namespace search.api.Data;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    
    public DbSet<Rental> Rentals { get; set; }
    
    public DbSet<RentalFirm> Firms { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder contextOptionsBuilder)
    {
        var connectionString = "server=localhost;user=root;password=sm2NdiT7KGVoKE;database=test2";
        var serverVersion = new MySqlServerVersion(new Version(9,1,0));
        contextOptionsBuilder.UseMySql(connectionString, serverVersion);
    }
}