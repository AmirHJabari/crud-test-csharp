using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Persistence;

public static class ApplicationDbContextSeed
{
    public static async Task SeedSampleDataAsync(ApplicationDbContext context)
    {
        if (!context.Customers.Any())
        {
            context.Customers.Add(new()
            {
                FirstName = "Amir H.",
                LastName = "Jabari",
                Email = "amirhamzehjabari@gmail.com",
                DateOfBirth = new DateTime(2002, 12, 22, 0, 0, 0, DateTimeKind.Utc),
                PhoneNumber = "+989051877561",
                BankAccountNumber = "1234123412341234"
            });

            await context.SaveChangesAsync();
        }
    }
}
