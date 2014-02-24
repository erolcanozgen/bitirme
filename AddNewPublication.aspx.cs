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
 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Dropdown_SelectDisease.DataTextField = "Name";
            Dropdown_SelectDisease.DataValueField = "Table_Name";
            Dropdown_SelectDisease.DataSource =dName.selectDiseasesNames();
            Dropdown_SelectDisease.DataBind();

            DropDown_SNP.DataTextField = "SNP";
            DropDown_SNP.DataValueField = "SNP";
            DropDown_SNP.DataSource = dDetails.selectAllSNPs(dName.tableNames);// buraya Distinct snp metodu yazılacak!!
            DropDown_SNP.DataBind();
        }
    }
    protected void AddButton_Click(object sender, EventArgs e)
    {
        dDetails.case_count = Convert.ToInt32(CaseCount_TextBox.Text);
        dDetails.control_count = Convert.ToInt32(ControlCount_TextBox.Text);
        dDetails.disease_name = Dropdown_SelectDisease.SelectedValue;
        dDetails.Gene_Name = GeneName_TextBox.Text;
        dDetails.snp = DropDown_SNP.SelectedValue;
        dDetails.freq_control = FC_TextBox.Text;
        dDetails.freq_Patient = FP_TextBox.Text;
        dDetails.p_value = P_TextBox.Text;
        dDetails.or_value = OR_TextBox.Text;
        dDetails.reference = Reference_TextBox.Text;
        try
        {
            dDetails.insertDiseaseDetails(dDetails);
            MessageBox.Show("Publication was added!");
            Response.Redirect("HomePage.aspx");
        }
        catch (MySql.Data.MySqlClient.MySqlException myException)
        {
            MessageBox.Show("An error was occured while adding the publication! Please try again later..");
        }
    }
}