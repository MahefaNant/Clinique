using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetCoreTemplate.Models;

[Table("patient")]
public class Patient
{
    [Key]
    [Column("id")]
    public int Id { get; set; }
    
    [Column("nom")]
    public string Nom { get; set; }
    
    [Column("naissance")]
    public DateOnly Naissance { get; set; }
    
    [Column("genre")]
    public int Genre { get; set; }
    
    [Column("remboursement")]
    public bool Remboursement { get; set; }
}