using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetCoreTemplate.Models;

[Table("facture_patient")]
public class FacturePatient
{
    [Key]
    [Column("id")]
    public int Id { get; set; }
    
    [Column("id_patient")]
    public int IdPatient { get; set; }
    
    [Column("date")]
    public DateTime Date { get; set; } 
        
    /*--------------------------------------*/

    [ForeignKey("IdPatient")]
    public virtual Patient? Patient { get; set; }
}