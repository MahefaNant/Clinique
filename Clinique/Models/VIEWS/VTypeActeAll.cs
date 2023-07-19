using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreTemplate.Models.VIEWS;

[Keyless]
[Table("v_typeacte_all")]
public class VTypeActeAll
{
    [Column("id_typeacte")]
    public int IdTypeActe { get; set; }
    
    [Column("annee")]
    public int Annee { get; set; }
    
    [Column("budget")]
    public double Budget { get; set; }
    
    [Column("nom")]
    public string Nom { get; set; }
    
    [Column("code")]
    public string Code { get; set; }
    
    /*---------------------------------------------------------*/
    
    [ForeignKey("IdTypeActe")]
    public virtual TypeActe? TypeActe { get; set; }
    
    /*----------------------------------------------------------*/
    
    [NotMapped]
    public decimal Realisation { get; set; }
}