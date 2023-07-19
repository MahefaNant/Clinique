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
    public DbSet<AspNetCoreTemplate.Models.Patient> Patient { get; set; } = default!;
    public DbSet<AspNetCoreTemplate.Models.TypeActe> TypeActe { get; set; } = default!;
    public DbSet<AspNetCoreTemplate.Models.TypeDepense> TypeDepense { get; set; } = default!;
    public DbSet<AspNetCoreTemplate.Models.Acte> Acte { get; set; } = default!;
    public DbSet<AspNetCoreTemplate.Models.Depense> Depense { get; set; } = default!;
    public DbSet<AspNetCoreTemplate.Models.FacturePatient> FacturePatient { get; set; } = default!;
    public DbSet<AspNetCoreTemplate.Models.VIEWS.VBudgetaire> VBudgetaires { get; set; } = default!;
    public DbSet<AspNetCoreTemplate.Models.VIEWS.VSommeFacture> VSommeFacture { get; set; } = default!;
    public DbSet<AspNetCoreTemplate.Models.VIEWS.VFactureActe> VFactureActe { get; set; } = default!;
    public DbSet<AspNetCoreTemplate.Models.VIEWS.VSommeActeParAnsMois> VSommeActeParAnsMois { get; set; } = default!;
    public DbSet<AspNetCoreTemplate.Models.VIEWS.VSommeDepenseParAnsMois> VSommeDepenseParAnsMois { get; set; } = default!;
    public DbSet<AspNetCoreTemplate.Models.VIEWS.VTypeActeAll> VTypeActeAll { get; set; } = default!;
    public DbSet<AspNetCoreTemplate.Models.VIEWS.VTypeDepenseAll> VTypeDepenseAll { get; set; } = default!;
    public DbSet<AspNetCoreTemplate.Models.VIEWS.VSommeActeParAnsMoisTypeActeWithBudget> VSommeActeParAnsMoisTypeActeWithBudget { get; set; } = default!;
    public DbSet<AspNetCoreTemplate.Models.VIEWS.VSommeDepenseParAnsMoisTypeDepenseWithBudget> VSommeDepenseParAnsMoisTypeDepenseWithBudget { get; set; } = default!;
    public DbSet<AspNetCoreTemplate.Models.BudgetActe> BudgetActe { get; set; } = default!;
    public DbSet<AspNetCoreTemplate.Models.BudgetDepense> BudgetDepense { get; set; } = default!;

}