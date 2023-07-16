using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetCoreTemplate.Models;

[Table("utilisateur")]
public class Utilisateur
{
    [Key]
    [Column("id")]
    public int Id { get; set; }
    
    [Column("nom")]
    public string Nom { get; set; }
    
    [Column("prenom")]
    public string Prenom { get; set; }
    
    [Column("mail")]
    public  string Mail { get; set; }
    
    [Column("code")]
    // [DataType(DataType.Password)]
    public string Code { get; set; }
    
    [Column("type")]
    public string Type { get; set; }
    
    /*---------------------------------------------*/

}