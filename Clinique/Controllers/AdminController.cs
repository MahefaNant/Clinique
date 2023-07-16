using AspNetCoreTemplate.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AspNetCoreTemplate.Controllers;

public class AdminController : Controller
{
    private readonly string _adminType = "admin";
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
}