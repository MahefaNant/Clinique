using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreTemplate.Models.VIEWS;

[Keyless]
[Table("v_somme_depense_paransmoistypeacte_withbudget")]
public class VSommeDepenseParAnsMoisTypeDepenseWithBudget
{
    [Column("id_typedepense")]
    public int IdTypeDepense { get; set; }
    
    [Column("annee")]
    public int Annee { get; set; }
    
    [Column("mois")]
    public int Mois { get; set; }
    
    [Column("montant_total")]
    public decimal TotalMontant { get; set; }
    
    [Column("budget")]
    public decimal Budget { get; set; }
    
    [Column("realisation")]
    public decimal Realisation { get; set; }
    
    /*--------------------------------------------*/
    
    [ForeignKey("IdTypeDepense")]
    public virtual TypeDepense? TypeDepense { get; set; }
}