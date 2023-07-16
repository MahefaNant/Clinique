using AspNetCoreTemplate.C_;
using AspNetCoreTemplate.Data;
using AspNetCoreTemplate.Models;
using AspNetCoreTemplate.Services.Repo;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace AspNetCoreTemplate.Services;

public class UtilisateurService: ServiceRepo<Utilisateur>
{
    public override Pagination<Utilisateur> Pagination { get; set; }
    public override ApplicationDbContext _context { get; set; }

    public UtilisateurService(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public static async Task<Utilisateur?> Login(ApplicationDbContext _context, string mail)
    {
        var utilisateur = await _context.Utilisateur.FirstOrDefaultAsync(u => u.Mail.Contains(mail));
        return utilisateur;
    }

    public static bool IsValidPassword(Utilisateur? utilisateur, string code)
    {
        // bool isPasswordValid = utilisateur != null && BCrypt.Net.BCrypt.Verify(code, utilisateur.Code);
        bool isPasswordValid = utilisateur != null && (utilisateur.Code.Equals(code));
        return isPasswordValid;
    }

    public static Utilisateur? GetByCookies(string cookies)
    {
        return JsonConvert.DeserializeObject<Utilisateur>(cookies);
    }
}