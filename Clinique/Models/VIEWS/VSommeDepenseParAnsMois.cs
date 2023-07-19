using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreTemplate.Models.VIEWS;

[Keyless]
[Table("v_somme_depense_paransmois")]
public class VSommeDepenseParAnsMois
{
    [Column("id_typedepense")]
    public int IdTypeDepense { get; set; }
    
    [Column("annee")]
    public int Annee { get; set; }
    
    [Column("mois")]
    public int Mois { get; set; }
    
    [Column("montant_total")]
    public Double TotalMontant { get; set; }
    
    /*---------------------------------------------------------*/
    
    [ForeignKey("IdTypeDepense")]
    public virtual TypeDepense? TypeDepense { get; set; }
    
}