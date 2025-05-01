using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ValueMateApi.Data;

public class ValuemateDbContext: IdentityDbContext<IdentityUser, IdentityRole, string>
{
    public ValuemateDbContext(DbContextOptions<ValuemateDbContext> options) : base(options) { }

    // Add other DbSets here if needed
}