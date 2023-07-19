using System.Globalization;
using System.Net.Mime;
using AspNetCoreTemplate.C_;
using AspNetCoreTemplate.Data;
using AspNetCoreTemplate.Models;
using AspNetCoreTemplate.Models.CSV;
using CsvHelper;
using CsvHelper.Configuration;
using GrapeCity.Documents.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace AspNetCoreTemplate.Controllers;

public class EmployerController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly string _empType = "employer";



    public EmployerController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Home()
    {
        
        Utilisateur U = JsonConvert.DeserializeObject<Utilisateur>(Request.Cookies[_empType]);
        var model = new
        {
            utilisateur = U
        };
        return View(model);
    }
    
    [HttpGet]
    public async Task<IActionResult> AfficheFacture(int id)
    {
        var facturePatient = await _context.FacturePatient
            .Include(f => f.Patient)
            .FirstOrDefaultAsync(m => m.Id == id);

        var factureDetails = await _context.VFactureActe.Include(a => a.TypeActe)
            .Include(a => a.FacturePatient)
            .Include(a => a.Patient)
            .Where(a => a.IdFacture == id)
            .ToListAsync();

        var sommeFacture = await _context.VSommeFacture.Where(a => a.IdFacture == id).FirstOrDefaultAsync();

        ViewBag.factureDetails = factureDetails;
        ViewBag.sommeFacture = sommeFacture.TotalMontant;
        return View(facturePatient);
    }
    
    [HttpGet]
    public async Task<IActionResult> PdfFacture(int id)
    {
        var tmp = Path.GetTempFileName();

        var req = HttpContext.Request;

        var uri = new Uri($"{req.Scheme}://{req.Host}{req.PathBase}/Employer/AfficheFacture/{id}");
        // var uri = new Uri("http://localhost:8080/global/pdfTest");
        

        var browserPath = BrowserFetcher.GetSystemChromePath();
        using var browser = new GcHtmlBrowser(browserPath);

        using var htmlPage = browser.NewPage(uri);
        
        PdfOptions pdfOption = new PdfOptions()
        {
            PageRanges = "1-100",
            Margins = new PdfMargins(0.2f),
            Landscape = false,
            PreferCSSPageSize = true
        };

        htmlPage.SaveAsPdf(tmp, pdfOption);
        var stream = new MemoryStream();
        using (var ts = System.IO.File.OpenRead(tmp))
            ts.CopyTo(stream);
        System.IO.File.Delete(tmp);
        return File(stream.ToArray(), MediaTypeNames.Application.Pdf, "Facture.pdf");
    }

    public int IdTypeDepenseViaCode(string code)
    {
        TypeDepense typeDepense = _context.TypeDepense.First(q => q.Code == code);
        return typeDepense.Id;
    }

    public IActionResult Csv(IFormFile fichier)
    {
        using var reader = new StreamReader(fichier.OpenReadStream());
        using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = ";" // Spécifier le point-virgule comme séparateur
        });        
        var depenseCsv = csv.GetRecords<DepenseCSV>().ToList();
        var depense = depenseCsv.Select(depenseCsv => new Depense()
        {
            IdTypeDepense = IdTypeDepenseViaCode(depenseCsv.Code),
            Date = DateTimeToUTC.Make(depenseCsv.Date),
            Montant = depenseCsv.Montant
        }).ToList();

        _context.AddRange(depense);
        _context.SaveChanges();
        return Ok();
    }
}