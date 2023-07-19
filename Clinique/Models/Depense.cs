using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetCoreTemplate.Models;

[Table("depense")]
public class Depense
{
    [Key]
    [Column("id")]
    public int Id { get; set; }
    
    [Column("id_typedepense")]
    public int IdTypeDepense { get; set; }
    
    
    [Column("montant")]
    public Double Montant { get; set; }
    
    [Column("date")]
    public DateTime Date { get; set; }
    
    /*--------------------------------------*/
    
    [ForeignKey("IdTypeDepense")]
    public virtual TypeDepense? TypeDepense { get; set; }
    
}