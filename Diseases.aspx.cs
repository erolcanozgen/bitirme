using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Diseases : System.Web.UI.Page
{
    DiseasesNames dName = new DiseasesNames();
    DiseaseDetails dDetails = new DiseaseDetails();

    protected void Page_Load(object sender, EventArgs e)
    {
       
        try
        {

            if (!IsPostBack)
            {
                cmbDiseaseName.DataTextField = "Name";
                cmbDiseaseName.DataValueField = "Table_Name";
                cmbDiseaseName.DataSource = dName.selectDiseasesNames();
                cmbDiseaseName.DataBind();
                cmbDiseaseName.SelectedValue = Request.QueryString["disease"];

                grdViewDiseases.DataSource = dDetails.selectDiseaseDetails(cmbDiseaseName.SelectedValue);
                grdViewDiseases.DataBind();
            }
        }

        catch(Exception ex)
        {
            Alert.Show(ex.Message);
        }
    }
 
   
    protected void btnFilter_Click(object sender, EventArgs e)
    {
        try
        {
            List<KeyValuePair<string, string>> param = new List<KeyValuePair<string, string>>();
            
            if(txtGene.Text != String.Empty) param.Add(new KeyValuePair<string,string>("Gene_Name",txtGene.Text));

            if (txtCaseCount.Text != String.Empty) param.Add(new KeyValuePair<string, string>("Case_Count", txtCaseCount.Text));

            if (txtCotrolCount.Text != String.Empty) param.Add(new KeyValuePair<string, string>("Control_Count", txtCotrolCount.Text));

            if (txtSNP.Text != String.Empty) param.Add(new KeyValuePair<string, string>("SNP", txtSNP.Text));   

            grdViewDiseases.DataSource =  dDetails.SelectFilteredDiseaseDetails(param, cmbDiseaseName.SelectedValue.ToString());
            grdViewDiseases.DataBind();


  

        
        }

        catch (Exception ex)
        {
            Alert.Show(ex.Message);
        }
        

    
    }
}