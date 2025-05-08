using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace System_Rezerwacji_Biletow.Data;

public class System_Rezerwacji_BiletowContext : IdentityDbContext<IdentityUser>
{
    public System_Rezerwacji_BiletowContext(DbContextOptions<System_Rezerwacji_BiletowContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}
