using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreTemplate.Models.VIEWS;

[Keyless]
[Table("v_budgetaire")]
public class VBudgetaire
{
    [Column("annee")]
    public int Annee { get; set; }
    
    [Column("mois")]
    public int Mois { get; set; }
    
    [Column("sum_reel")]
    public Double SumReel { get; set; }
    
    [Column("sum_budget")]
    public Double SumBudget { get; set; }
    
    [Column("sum_realisation")]
    public Double SumRealisation { get; set; }
}