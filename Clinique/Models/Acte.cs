using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetCoreTemplate.Models;

[Table("acte")]
public class Acte
{
    [Key]
    [Column("id")]
    public int Id { get; set; }
    
    [Column("id_typeacte")]
    public int IdTypeActe { get; set; }
    
    [Column("id_facture")]
    public int IdFacture { get; set; }

    [Column("montant")]
    public Double Montant { get; set; }

    /*--------------------------------------*/
    
    [ForeignKey("IdTypeActe")]
    public virtual TypeActe? TypeActe { get; set; }
    
    [ForeignKey("IdFacture")]
    public virtual FacturePatient? FacturePatient { get; set; }
}