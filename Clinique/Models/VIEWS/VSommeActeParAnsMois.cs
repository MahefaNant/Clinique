using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreTemplate.Models.VIEWS;

[Keyless]
[Table("v_somme_acte_paransmois")]
public class VSommeActeParAnsMois
{
    
    [Column("id_typeacte")]
        public int IdTypeActe { get; set; }
        
        [Column("annee")]
        public int Annee { get; set; }
        
        [Column("mois")]
        public int Mois { get; set; }
        
        [Column("total_montant")]
        public Double TotalMontant { get; set; }
        
        /*---------------------------------------------------------*/
        
        [ForeignKey("IdTypeActe")]
        public virtual TypeActe? TypeActe { get; set; }
}