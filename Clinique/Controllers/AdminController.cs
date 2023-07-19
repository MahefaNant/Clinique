using AspNetCoreTemplate.Data;
using AspNetCoreTemplate.Models;
using AspNetCoreTemplate.Models.Others;
using AspNetCoreTemplate.Models.VIEWS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace AspNetCoreTemplate.Controllers;

public class AdminController : Controller
{
    
    private readonly ApplicationDbContext _context;


    private readonly string _adminType = "admin";

    public AdminController(ApplicationDbContext context)
    {
        _context = context;
    }

    // Admin/Login
    public IActionResult Login()
    {
        HttpContext.Session.Clear();
        foreach (var cookie in Request.Cookies.Keys)
        {
            Response.Cookies.Delete(cookie);
        }
        return View();
    }
    
    // Admin/Home
    public IActionResult Home()
    {
        Utilisateur U = JsonConvert.DeserializeObject<Utilisateur>(Request.Cookies[_adminType]);
        var model = new
        {
            utilisateur = U
        };
        return View(model);
    }

    public IActionResult Budgetaire(int? annee , int? mois)
    {
        TableauBord tableauBord = new TableauBord(_context, annee, mois);
        ViewBag.tableauBord = tableauBord;
        return View();
    }

}