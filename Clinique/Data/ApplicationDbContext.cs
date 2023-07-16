using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AspNetCoreTemplate.Models;

namespace AspNetCoreTemplate.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<AspNetCoreTemplate.Models.Test> Test { get; set; } = default!;
    public DbSet<AspNetCoreTemplate.Models.Utilisateur> Utilisateur { get; set; } = default!;

}