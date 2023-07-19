using CsvHelper.Configuration.Attributes;

namespace AspNetCoreTemplate.Models.CSV;

public class DepenseCSV
{
    [Name("date")]
    [Format("dd/MM/yyyy")]
    public DateTime Date { get; set; }
    
    [Name("code")]
    public string Code { get; set; }
    
    [Name("montant")]
    public string Montant { get; set; }
    
}