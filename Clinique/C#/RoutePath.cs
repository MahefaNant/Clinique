namespace AspNetCoreTemplate.C_;

public class RoutePath
{
    public static string HtmlTemplate()
    {
        return "~/Views/Shared/_HtmlTemplate.cshtml";
    }

    public static string NavAdmin()
    {
        return "~/Views/Admin/template/_Nav.cshtml";
    }
    
    public static string AsideAdmin()
    {
        return "~/Views/Admin/template/_Aside.cshtml";
    }

    public static string ContentAdmin()
    {
        return "~/Views/Admin/template/_Content.cshtml";
    }
    
    /*-------------------------------*/
    
    public static string NavEmployer()
    {
        return "~/Views/Employer/template/_Nav.cshtml";
    }
    
    public static string AsideEmployer()
    {
        return "~/Views/Employer/template/_Aside.cshtml";
    }

    public static string ContentEmployer()
    {
        return "~/Views/Employer/template/_Content.cshtml";
    }
}