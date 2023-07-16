using AspNetCoreTemplate.Data;
using AspNetCoreTemplate.Models;
using Microsoft.AspNetCore.Mvc;
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
    
}