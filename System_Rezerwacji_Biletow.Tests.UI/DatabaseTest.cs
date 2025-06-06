using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System_Rezerwacji_Biletów.Data;
using System_Rezerwacji_Biletów.Models;
using Xunit;
using Microsoft.EntityFrameworkCore.InMemory;
public class SystemReservationContextTests
{
    private SystemReservationContext GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<SystemReservationContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new SystemReservationContext(options);
    }
    [Fact]
    public async Task CanAddUserToDatabase()
    {
        var context = GetInMemoryDbContext();
        var user = new User
        {
            Name = "Jan",
            LastName = "Kowalski",
            BirthDate = new DateTime(1990, 1, 1),
            email = "jan.kowalski@example.com"
        };

        context.Users.Add(user);
        await context.SaveChangesAsync();

        var userFromDb = await context.Users.FirstOrDefaultAsync(u => u.email == "jan.kowalski@example.com");

        Assert.NotNull(userFromDb);
        Assert.Equal("Jan", userFromDb.Name);
        Assert.Equal("Kowalski", userFromDb.LastName);
    }
    [Fact]
    public async Task CanDeleteUserFromDatabase()
    {
        var context = GetInMemoryDbContext();
        var user = new User
        {
            Name = "Marek",
            LastName = "Zielinski",
            BirthDate = new DateTime(1970, 12, 12),
            email = "marek.zielinski@example.com"
        };
        context.Users.Add(user);
        await context.SaveChangesAsync();

        var userToDelete = await context.Users.FirstAsync(u => u.email == "marek.zielinski@example.com");
        context.Users.Remove(userToDelete);
        await context.SaveChangesAsync();

        var deletedUser = await context.Users.FirstOrDefaultAsync(u => u.email == "marek.zielinski@example.com");

        Assert.Null(deletedUser);
    }
}
