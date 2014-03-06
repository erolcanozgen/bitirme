using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;

/// <summary>
/// Summary description for AutoComplete
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class AutoComplete : System.Web.Services.WebService {

    public AutoComplete () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    [System.Web.Script.Services.ScriptMethod]
    public string[] GetSNPs(string prefixText, int count)
    {
        string[] returnValue = null;
        DiseasesNames dName = new DiseasesNames();
        DiseaseDetails dDetails = new DiseaseDetails();

        DataTable dtDiseases = dName.selectDiseasesNames();
        DataTable dtSNPs = dDetails.selectAllSNPs(dName.tableNames);

        returnValue = dtSNPs.AsEnumerable().Select(row => row.Field<string>("SNP")).ToArray();

        return (from rV in returnValue where rV.StartsWith(prefixText, StringComparison.CurrentCultureIgnoreCase) select rV).Take(100).ToArray();
    }

    [WebMethod]
    [System.Web.Script.Services.ScriptMethod]
    public string[] GetDiseaseNames(string prefixText, int count)
    {
        string[] returnValue = null;
        DiseasesNames dName = new DiseasesNames();

        DataTable dtDiseases = dName.selectDiseasesNames();

        returnValue = dtDiseases.AsEnumerable().Select(row => row.Field<string>("Name")).ToArray();

        return (from rV in returnValue where rV.StartsWith(prefixText, StringComparison.CurrentCultureIgnoreCase) select rV).Take(100).ToArray();
    }
}
