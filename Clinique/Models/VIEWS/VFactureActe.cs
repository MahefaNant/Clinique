using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreTemplate.Models.VIEWS;

[Keyless]
[Table("v_facture_acte")]
public class VFactureActe
{
    [Column("id_typeacte")]
    public int IdTypeActe { get; set; }
    
    [Column("id_facture")]
    public int IdFacture { get; set; }
    
    [Column("id_patient")]
    public int IdPatient { get; set; }
    
    [Column("montant")]
    public Double Montant { get; set; }
    
    [Column("facture_date")]
    public DateTime FactureDate { get; set; }
    
    /*----------------------------------------*/
    [ForeignKey("IdTypeActe")]
    public virtual TypeActe? TypeActe { get; set; }
    
    [ForeignKey("IdFacture")]
    public virtual FacturePatient? FacturePatient { get; set; }
    
    [ForeignKey("IdPatient")]
    public virtual Patient? Patient { get; set; }
    
}