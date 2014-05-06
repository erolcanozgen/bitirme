using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;

public partial class _Default : System.Web.UI.Page
{
    DiseasesNames dName = new DiseasesNames();
    DiseaseDetails dDetails = new DiseaseDetails();
    int referenceType = 0;
 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Dropdown_SelectDisease.DataTextField = "Name";
            //Dropdown_SelectDisease.DataValueField = "Table_Name";
            //Dropdown_SelectDisease.DataSource =dName.selectDiseasesNames();
            //Dropdown_SelectDisease.DataBind();

            //DropDown_SNP.DataTextField = "SNP";
            //DropDown_SNP.DataValueField = "SNP";
            //DropDown_SNP.DataSource = dDetails.selectAllSNPs(dName.tableNames);// buraya Distinct snp metodu yazılacak!!
            //DropDown_SNP.DataBind();
        }
    }

    protected void AddButton_Click(object sender, EventArgs e)
    {
        UnapprovedPublications unAppPub = new UnapprovedPublications();

        unAppPub.disease_name = SelectDisease_TextBox.Text.Replace(' ', '_');
        unAppPub.snp = SNP_TextBox.Text;
        unAppPub.Gene_Name = GeneName_TextBox.Text;
        unAppPub.case_count = Convert.ToInt32(CaseYes_TextBox.Text) + Convert.ToInt32(CaseNo_TextBox.Text);
        unAppPub.control_count = Convert.ToInt32(ControlYes_TextBox.Text) + Convert.ToInt32(ControlNo_TextBox.Text);
        unAppPub.freq_control = (Convert.ToDecimal(ControlYes_TextBox.Text) / unAppPub.control_count);
        unAppPub.freq_Patient = (Convert.ToDecimal(CaseYes_TextBox.Text) / unAppPub.case_count);
        unAppPub.p_value = P_TextBox.Text;
        unAppPub.or_value = (decimal)Utility.CalculateOrValue(Convert.ToInt32(CaseYes_TextBox.Text), Convert.ToInt32(CaseNo_TextBox.Text), 
                                                     Convert.ToInt32(ControlYes_TextBox.Text), Convert.ToInt32(ControlNo_TextBox.Text));
        unAppPub.reference = Reference_TextBox.Text;
        unAppPub.reference_type = Convert.ToInt32(Reference_DropDown.SelectedValue);

        double or_variance = Utility.CalculateOrVariance(Convert.ToInt32(CaseYes_TextBox.Text), Convert.ToInt32(CaseNo_TextBox.Text),
                                                            Convert.ToInt32(ControlYes_TextBox.Text), Convert.ToInt32(ControlNo_TextBox.Text));
        unAppPub.CI = Utility.CalculateCI((double)unAppPub.or_value, or_variance);
        
        if (Session["user"] != null)
            unAppPub.ownerOfPublication = ((Users)Session["user"]).name;
        else
            unAppPub.ownerOfPublication = "Guest";

        try
        {
            unAppPub.insertDiseaseDetails(unAppPub);
            Notifier.AddSuccessMessage("Publication was added.");
            //MessageBox.Show("Publication was added!");
            Response.Redirect("HomePage.aspx");
        }
        catch (MySql.Data.MySqlClient.MySqlException myException)
        {
            Notifier.AddErrorMessage("An error was occured while adding the publication! Please try again later.");
            //MessageBox.Show("An error was occured while adding the publication! Please try again later..");
        }
    }
}