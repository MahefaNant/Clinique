using AspNetCoreTemplate.Data;
using AspNetCoreTemplate.Models;
using AspNetCoreTemplate.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AspNetCoreTemplate.Controllers;

public class AuthentificationController : Controller
{
    private readonly string _adminType = "admin";
    private readonly string _empType = "employer";
    private readonly ApplicationDbContext db;

    public AuthentificationController(ApplicationDbContext db)
    {
        this.db = db;
    }

    [HttpPost]
    public async Task<IActionResult> Authentification(string mail, string code, bool remember, bool type)
    {
        Utilisateur? utilisateur = await UtilisateurService.Login(db, mail);
        bool isPasswordValid = UtilisateurService.IsValidPassword(utilisateur, code);
        if (isPasswordValid)
        {
            string serializeUtilisateur = JsonConvert.SerializeObject(utilisateur);
            if (utilisateur.Type.Equals(_adminType) && type)
            {
                Response.Cookies.Append( _adminType, serializeUtilisateur);
                return RedirectToAction("Home", "Admin");
            }

            if (utilisateur.Type.Equals(_empType) && type == false)
            {
                Response.Cookies.Append(_empType, serializeUtilisateur);
                return RedirectToAction("Home", "Employer");
            }

            return RedirectToAction("Login", "Admin");
        }

        return RedirectToAction("Login", "Admin");
    }

    public IActionResult SignOut()
    {
        if (Request.Cookies[_adminType] != null) Response.Cookies.Delete(_adminType);
        if (Request.Cookies[_empType] != null) Response.Cookies.Delete(_empType);
        return RedirectToAction("Login", "Admin");
    }
}
