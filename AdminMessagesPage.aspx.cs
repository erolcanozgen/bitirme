using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminMessagesPage : System.Web.UI.Page
{
    Messages message = new Messages();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ListView1.DataSource = message.selectComingMessages();
            ListView1.DataBind();
        }
    }
    protected void ButtonDelete_Click(object sender, EventArgs e)
    {
        //dDetails.case_count = Convert.ToInt32(CaseCount_TextBox.Text);
        //dDetails.control_count = Convert.ToInt32(ControlCount_TextBox.Text);
        //dDetails.disease_name = Dropdown_SelectDisease.SelectedValue;
        //dDetails.Gene_Name = GeneName_TextBox.Text;
        //dDetails.snp = DropDown_SNP.SelectedValue;
        //dDetails.freq_control = FC_TextBox.Text;
        //dDetails.freq_Patient = FP_TextBox.Text;
        //dDetails.p_value = P_TextBox.Text;
        //dDetails.or_value = OR_TextBox.Text;
        //dDetails.reference = Reference_TextBox.Text;
        //try
        //{
        //    dDetails.insertDiseaseDetails(dDetails);
        //    MessageBox.Show("Publication was added!");
        //    Response.Redirect("HomePage.aspx");
        //}
        //catch (MySql.Data.MySqlClient.MySqlException myException)
        //{
        //    MessageBox.Show("An error was occured while adding the publication! Please try again later..");
        //}
    }
}