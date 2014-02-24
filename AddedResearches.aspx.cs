using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AddedResearches : System.Web.UI.Page
{
    DiseasesNames dName = new DiseasesNames();
    DiseaseDetails dDetails = new DiseaseDetails();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            dName.selectDiseasesNames();
            
            //bu bir gridviewe atanacak
            grdViewUnapprovedDiseases.DataSource = dDetails.selectUnapprovedDiseaseDetails(dName.tableNames);
            grdViewUnapprovedDiseases.DataBind();
            
            
        }

    }
}