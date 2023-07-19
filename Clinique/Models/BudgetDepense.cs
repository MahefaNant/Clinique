using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetCoreTemplate.Models;

[Table("budgedepense")]
public class BudgetDepense
{
    [Key]
    [Column("id")]
    public int Id { get; set; }
    
    [Column("id_typedepense")]
    public int IdTypeDepense { get; set; }
    
    [Column("annee")]
    public int Annee { get; set; }
    
    [Column("budget")]
    public Double Budget { get; set; }
    
    /*----------------------------------*/
    
    [ForeignKey("IdTypeDepense")]
    public virtual TypeDepense? TypeDepense { get; set; }
}