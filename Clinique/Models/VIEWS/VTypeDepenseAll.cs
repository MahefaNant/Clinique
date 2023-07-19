using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreTemplate.Models.VIEWS;

[Keyless]
[Table("v_typedepense_all")]
public class VTypeDepenseAll
{
    [Column("id_typedepense")]
    public int IdTypeDepense { get; set; }
    
    [Column("annee")]
    public int Annee { get; set; }
    
    [Column("budget")]
    public double Budget { get; set; }
    
    [Column("nom")]
    public string Nom { get; set; }
    
    [Column("code")]
    public string Code { get; set; }
    
    /*---------------------------------------------------------*/
    
    [ForeignKey("IdTypeDepense")]
    public virtual TypeDepense? TypeDepense { get; set; }
    
    /*----------------------------------------------------------*/
    
    [NotMapped]
    public decimal Realisation { get; set; }
}