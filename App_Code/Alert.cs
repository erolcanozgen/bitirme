using System.Web;
using System.Text;
using System.Web.UI;
public static class Alert
{
    public static void Show(string message)
    {
        // Buradaki tek tırnak mesaj silmek için izin ister.
        string cleanMessage = message.Replace("'", "\\'");
        string script = "<script type=\"text/javascript\">alert('" + cleanMessage + "');</script>";
        // Yüklenecek webformu alır.
        Page page = HttpContext.Current.CurrentHandler as Page;
        // Sayfa üzerinde allready olup olmadığını kontrol eder.
        if (page != null && !page.ClientScript.IsClientScriptBlockRegistered("alert"))
        {
            page.ClientScript.RegisterClientScriptBlock(typeof(Alert), "alert", script);
        }
    }
}