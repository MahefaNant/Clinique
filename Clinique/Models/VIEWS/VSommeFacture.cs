using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreTemplate.Models.VIEWS;

[Keyless]
[Table("v_somme_facture")]
public class VSommeFacture
{
    [Column("id_facture")]
    public int IdFacture { get; set; }
    
    [Column("total_montant")]
    public Double TotalMontant { get; set; }
    
    /*------------------------------------*/
    
    [ForeignKey("IdFacture")]
    public virtual FacturePatient? FacturePatient { get; set; }
}