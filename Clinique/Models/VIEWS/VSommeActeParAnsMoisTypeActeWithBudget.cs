using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreTemplate.Models.VIEWS;


[Keyless]
[Table("v_somme_acte_paransmoistypeacte_withbudget")]
public class VSommeActeParAnsMoisTypeActeWithBudget
{
    [Column("id_typeacte")]
    public int IdTypeActe { get; set; }
    
    [Column("annee")]
    public int Annee { get; set; }
    
    [Column("mois")]
    public int Mois { get; set; }
    
    [Column("total_montant")]
    public decimal TotalMontant { get; set; }
    
    [Column("budget")]
    public decimal Budget { get; set; }
    
    [Column("realisation")]
    public decimal Realisation { get; set; }
    
    /*--------------------------------------------*/
    
    [ForeignKey("IdTypeActe")]
    public virtual TypeActe? TypeActe { get; set; }
    
}