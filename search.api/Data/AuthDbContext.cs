using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
public class AuthDbContext: IdentityDbContext<UserDetails>
{
    public AuthDbContext(DbContextOptions<AuthDbContext> options): base(options)
    {   
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}