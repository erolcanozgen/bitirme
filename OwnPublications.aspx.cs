using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class OwnPublications : System.Web.UI.Page
{
    Users user = null;
    static int id;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (this.Session["user"] == null)
            {
                Notifier.AddInfoMessage("You should sign in to see your publication(s) ");
                Response.Redirect("~/HomePage.aspx");
            }
            else
                getApprovedGridData();
            getUnapprovedGridData();
        }
    }
    private void getApprovedGridData()
    {
       user = this.Session["user"] as Users;
        DataTable dt =  user.getOwnPublication();
       grdViewPublications.DataSource =dt;
       grdViewPublications.DataBind();
       setReferenceColumn(dt,grdViewPublications);
    }
    private void getUnapprovedGridData()
    {
         user = this.Session["user"] as Users;
         UnapprovedPublications uap = new UnapprovedPublications();
         uap.ownerOfPublication = user.username;
         DataTable dt = uap.getOwnUnapprovedPub();
         grdUnapprovedView.DataSource =dt;
         grdUnapprovedView.DataBind();
         setReferenceColumn(dt,grdUnapprovedView);
        
    }
    private void setReferenceColumn(DataTable dt, GridView grd)
    {
        HyperLink link_ref, link_gene; LinkButton seeDetailsBtn;



        for (int i = 0; i < grd.Rows.Count; i++)
        {
            link_gene = grd.Rows[i].FindControl("Gene_Name") as HyperLink;
            link_gene.NavigateUrl = String.Format("{0}?gene={1}", ConfigurationManager.AppSettings["GeneCardsLink"], link_gene.Text);
            link_gene.Target = "_blank";

            switch (dt.Rows[i]["Reference_Type"].ToString())
            {
                case "1":
                    link_ref = grd.Rows[i].FindControl("Link") as HyperLink;
                    link_ref.NavigateUrl = String.Format("{0}/{1}", ConfigurationManager.AppSettings["PubmedLink"], link_ref.Text);
                    link_ref.Target = "_blank";
                    link_ref.Text = String.Format("PMID: {0}", link_ref.Text);
                    link_ref.Visible = true;
                    break;

                case "2":
                    link_ref = grd.Rows[i].FindControl("Link") as HyperLink;
                    link_ref.Target = "_blank";
                    link_ref.NavigateUrl = link_ref.Text;
                    link_ref.Text = "External Links";
                    link_ref.Visible = true;
                    break;

                case "3":
                default:
                    seeDetailsBtn = grd.Rows[i].FindControl("seeDetailsBtn") as LinkButton;
                    seeDetailsBtn.Visible = true;
                    break;
            }
        }
    }
    public void ShowPopup(object sender, EventArgs e)
    {
        Label lbl_ref;
        LinkButton btn = (LinkButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        int i = Convert.ToInt32(row.RowIndex);

        lbl_ref = (row.NamingContainer as GridView).Rows[i].FindControl("lbl_reference") as Label;
        referenceLbl.Text = lbl_ref.Text;

        this.Button1_ModalPopupExtender.Show();
    }
    protected void grdViewPublications_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView grd = (sender as GridView);
        grd.PageIndex = e.NewPageIndex;
        if (grd.ID == "grdViewPublications")
            getApprovedGridData();
        if (grd.ID == "grdUnapprovedView")
            getUnapprovedGridData();
    }


    protected void update_button_Click(object sender, ImageClickEventArgs e)
    {
        int case_count, control_count;
        double case_freq, control_freq;
        ImageButton ibtn1 = sender as ImageButton;
        int rowIndex = Convert.ToInt32(ibtn1.Attributes["RowIndex"]);
        id = Convert.ToInt32(grdUnapprovedView.DataKeys[rowIndex]["ID"]);
        SelectDisease_TextBox.Text = grdUnapprovedView.DataKeys[rowIndex]["Disease_Name"].ToString();
        SNP_TextBox.Text = grdUnapprovedView.DataKeys[rowIndex]["SNP"].ToString();
        GeneName_TextBox.Text = grdUnapprovedView.DataKeys[rowIndex]["Gene_Name"].ToString();
        P_TextBox.Text = grdUnapprovedView.DataKeys[rowIndex]["P_Value"].ToString();
        Reference_DropDown.SelectedValue = grdUnapprovedView.DataKeys[rowIndex]["Reference_Type"].ToString();
        Reference_TextBox.Text = grdUnapprovedView.DataKeys[rowIndex]["Reference"].ToString();

        control_freq = Convert.ToDouble(grdUnapprovedView.DataKeys[rowIndex]["Frequency_Control"]);
        case_freq = Convert.ToDouble(grdUnapprovedView.DataKeys[rowIndex]["Frequency_Patient"]);
        case_count = Convert.ToInt32(grdUnapprovedView.DataKeys[rowIndex]["Case_Count"]);
        control_count = Convert.ToInt32(grdUnapprovedView.DataKeys[rowIndex]["Control_Count"]);


        CaseYes_TextBox.Text = Math.Round((case_count * case_freq),MidpointRounding.AwayFromZero).ToString();
        CaseNo_TextBox.Text = (case_count-Convert.ToInt32(CaseYes_TextBox.Text)).ToString();

        ControlYes_TextBox.Text = Math.Round((control_count * control_freq),MidpointRounding.AwayFromZero).ToString();
        ControlNo_TextBox.Text = (control_count-Convert.ToInt32(ControlYes_TextBox.Text)).ToString();


        UpdatePopupExtender1.Show();

    }

    protected void update_Pub_Click(object sender, EventArgs e)
    {
        UnapprovedPublications unAppPub = new UnapprovedPublications();

        unAppPub.id = id;
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
            unAppPub.ownerOfPublication = ((Users)Session["user"]).username;
        else
            unAppPub.ownerOfPublication = "Guest";

        try
        {
            unAppPub.updateUnapprovedPub();
            getUnapprovedGridData();
            Notifier.AddSuccessMessage("Publication has been updated.");
        }
        catch (MySql.Data.MySqlClient.MySqlException myException)
        {
            Notifier.AddErrorMessage("An error was occured while updating the publication! Please try again later.");
            //MessageBox.Show("An error was occured while adding the publication! Please try again later..");
        }
    }
    protected void delete_button_Click(object sender, ImageClickEventArgs e)
    {
        
        try
        {
            ImageButton ibtn1 = sender as ImageButton;
            int rowIndex = Convert.ToInt32(ibtn1.Attributes["RowIndex"]);
            id = Convert.ToInt32(grdUnapprovedView.DataKeys[rowIndex]["ID"]);
            UnapprovedPublications unAppPub = new UnapprovedPublications();
            unAppPub.id = Convert.ToInt32(grdUnapprovedView.DataKeys[rowIndex]["ID"]);
            unAppPub.deleteSelectedPublication();
            getUnapprovedGridData();
            Notifier.AddSuccessMessage("Publication has been deleted.");
        }
        catch (MySql.Data.MySqlClient.MySqlException myException)
        {
            Notifier.AddErrorMessage("An error was occured while deleting the publication! Please try again later.");
            //MessageBox.Show("An error was occured while adding the publication! Please try again later..");
        }
    }
}