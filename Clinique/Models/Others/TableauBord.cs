using AspNetCoreTemplate.Data;
using AspNetCoreTemplate.Models.VIEWS;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreTemplate.Models.Others;

public class TableauBord
{
    private readonly ApplicationDbContext _context;
    
    public List<VSommeActeParAnsMoisTypeActeWithBudget> Recettes { get; set; }
    public List<VSommeDepenseParAnsMoisTypeDepenseWithBudget> Depenses{ get; set; }
    
    public decimal ReelRecetteTotal{ get; set; }
    public decimal BudgetRecetteTotal{ get; set; }
    public decimal RealisationRecetteTotal{ get; set; }
    
    public decimal ReelDepenseTotal{ get; set; }
    public decimal BudgetDepenseTotal{ get; set; }
    public decimal RealisationDepenseTotal{ get; set; }
    
    public decimal ReelBeneficeTotal{ get; set; }
    public decimal BudgetBeneficeTotal{ get; set; }
    public decimal RealisationBeneficeTotal{ get; set; }


    public TableauBord(ApplicationDbContext context, int? annee , int? mois)
    {
        _context = context;
        Recettes = GetRecettes(annee, mois);
        Depenses = GetDepenses(annee, mois);
        AddTotalFromRecette(annee, mois);
        AddTotalFromDepense(annee, mois);
        AddResultFromBenefice(annee, mois);
    }

    void AddTotalFromRecette(int? annee , int? mois)
    {
        var L = Recettes;
        ReelRecetteTotal = 0;
        BudgetRecetteTotal = 0;
        RealisationRecetteTotal = 0;
        foreach (var q in L)
        {
            ReelRecetteTotal = Math.Round(ReelRecetteTotal+ q.TotalMontant);
            BudgetRecetteTotal = Math.Round(BudgetRecetteTotal+ q.Budget);
        }
        if(L.Count>0)
            RealisationRecetteTotal = Math.Round((ReelRecetteTotal * 100) / BudgetRecetteTotal);
    }
    
    void AddTotalFromDepense(int? annee , int? mois)
    {
        var L = GetDepenses(annee, mois);
        ReelDepenseTotal = 0;
        BudgetDepenseTotal = 0;
        RealisationDepenseTotal = 0;
        foreach (var q in L)
        {
            ReelDepenseTotal = Math.Round(ReelDepenseTotal+ q.TotalMontant);
            BudgetDepenseTotal = Math.Round(BudgetDepenseTotal+ q.Budget);
        }
        if(L.Count>0)
            RealisationDepenseTotal = Math.Round((ReelDepenseTotal * 100) / BudgetDepenseTotal);
    }

    void AddResultFromBenefice(int? annee, int? mois)
    {
        ReelBeneficeTotal = Math.Round(ReelRecetteTotal - ReelDepenseTotal);
        BudgetBeneficeTotal = Math.Round(BudgetRecetteTotal - BudgetDepenseTotal);
        if(BudgetBeneficeTotal!=0)
            RealisationBeneficeTotal = Math.Round((ReelBeneficeTotal * 100) / BudgetBeneficeTotal);
    }

    List<VSommeActeParAnsMoisTypeActeWithBudget> GetRecettes(int? annee , int? mois)
    {
        var actes = _context.VSommeActeParAnsMoisTypeActeWithBudget
            .Include( q => q.TypeActe)
            .Where(q => q.Annee == annee && q.Mois == mois)
            .OrderBy( q => q.IdTypeActe)
            .ToList();

        var actesF = actes;

        var typeactes = _context.VTypeActeAll.Where(q => q.Annee == annee)
            .OrderBy(q => q.IdTypeActe)
            .ToList();
        for (int i = 0; i < typeactes.Count; i++)
        {
            try
            {
                int verif = actes[i].IdTypeActe;
            }
            catch (Exception e)
            {
                VSommeActeParAnsMoisTypeActeWithBudget V = new VSommeActeParAnsMoisTypeActeWithBudget
                {
                    Annee = (int)annee,
                    Mois = (int)mois,
                    IdTypeActe = typeactes[i].IdTypeActe,
                    TotalMontant = 0,
                    Budget = Math.Round((decimal)typeactes[i].Budget/12),
                    Realisation = 0
                };
                V.TypeActe = _context.TypeActe.First(q => q.Id == typeactes[i].IdTypeActe);
                actesF.Add(V);
            }
        }

        return actesF;
    }
    
    List<VSommeDepenseParAnsMoisTypeDepenseWithBudget> GetDepenses(int? annee , int? mois)
    {
        var depenses = _context.VSommeDepenseParAnsMoisTypeDepenseWithBudget
            .Include( q => q.TypeDepense)
            .Where(q => q.Annee == annee && q.Mois == mois)
            .OrderBy( q => q.IdTypeDepense)
            .ToList();

        var depenseF = depenses;

        var typeDepenses = _context.VTypeDepenseAll.Where(q => q.Annee == annee)
            .OrderBy(q => q.IdTypeDepense)
            .ToList();
        for (int i = 0; i < typeDepenses.Count; i++)
        {
            try
            {
                int verif = depenses[i].IdTypeDepense;
            }
            catch (Exception e)
            {
                VSommeDepenseParAnsMoisTypeDepenseWithBudget V = new VSommeDepenseParAnsMoisTypeDepenseWithBudget
                {
                    Annee = (int)annee,
                    Mois = (int)mois,
                    IdTypeDepense = typeDepenses[i].IdTypeDepense,
                    TotalMontant = 0,
                    Budget = Math.Round((decimal)typeDepenses[i].Budget/12),
                    Realisation = 0
                };
                V.TypeDepense = _context.TypeDepense.First(q => q.Id == typeDepenses[i].IdTypeDepense);
                depenseF.Add(V);
            }
        }

        return depenseF;
    }
    
    


}