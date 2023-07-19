namespace AspNetCoreTemplate.C_;

public class Function
{
    public static string[] Mois()
    {
        string[] res = new string[12];
        res[0] = "Janvier";
        res[1] = "Fevrier";
        res[2] = "Mars";
        res[3] = "Avril";
        res[4] = "Mai";
        res[5] = "Juin";
        res[6] = "Juillet";
        res[7] = "Aout";
        res[8] = "Septembre";
        res[9] = "Octobre";
        res[10] = "Novembre";
        res[11] = "Decembre";
        return res;
    }

    public static string findMois(int mois)
    {
        string[] M = Mois();
        for (int i = 0; i < M.Length; i++)
        {
            if (mois == i - 1)
            {
                return M[i - 1];
            }
        }

        return "Janvier";
    }
}