using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetCoreTemplate.Models;

[Table("budgetacte")]
public class BudgetActe
{
    [Key]
    [Column("id")]
    public int Id { get; set; }
    
    [Column("id_typeacte")]
    public int IdTypeActe { get; set; }
    
    [Column("annee")]
    public int Annee { get; set; }
    
    [Column("budget")]
    public Double Budget { get; set; }
    
    /*----------------------------------*/
    
    [ForeignKey("IdTypeActe")]
    public virtual TypeActe? TypeActe { get; set; }
}