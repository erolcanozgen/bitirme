using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Diseases : System.Web.UI.Page
{
    DiseasesNames dName = new DiseasesNames();
    DiseaseDetails dDetails = new DiseaseDetails();
    List<KeyValuePair<string, string>> param = new List<KeyValuePair<string, string>>();
    static string gene = String.Empty;
    static string snp = String.Empty;
    static string min_sample_size = String.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
          ScriptManager.RegisterStartupScript(this, GetType(),"", "Reload();", true);
   

            if (!IsPostBack)
            {
                cmbDiseaseName.DataTextField = "Name";
                cmbDiseaseName.DataValueField = "Table_Name";
                cmbDiseaseName.DataSource = dName.selectDiseasesNames();
                cmbDiseaseName.DataBind();
                cmbDiseaseName.SelectedValue = Request.QueryString["disease"];
                getGridData();
                     
            }
        }
        catch(Exception ex)
        {
            Notifier.AddErrorMessage("An error was occured while getting the researches!");
            //Alert.Show(ex.Message);
        }
    }

    private void getGridData()
    {

        param.Clear();

        if (gene != String.Empty) param.Add(new KeyValuePair<string, string>("Gene_Name", gene));

        if (min_sample_size != String.Empty) param.Add(new KeyValuePair<string, string>("Case_Count+Control_Count", min_sample_size));

        if (snp != String.Empty) param.Add(new KeyValuePair<string, string>("SNP", snp));

        DataTable dt = new DataTable();
        dt = dDetails.getFilteredDiseaseDetails(param,cmbDiseaseName.SelectedValue);
        grdViewDiseases.DataSource = dt;
        grdViewDiseases.DataBind();
        setReferenceColumn(dt);
        foreach (GridViewRow row in grdViewDiseases.Rows)
        {
            if (row.RowIndex % 2 == 1) row.CssClass = "even";
        }
    }


    protected void btnFilter_Click(object sender, EventArgs e)
    {
        try
        {
            gene = txtGene.Text;
            snp = txtSNP.Text;
            min_sample_size = txtMinimumSample.Text;
            getGridData();

           ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>disp_confirm();</script>", false);
        }
        catch (Exception ex)
        {
            Notifier.AddErrorMessage("An error was occured while filtering the researches!");
            
            //Alert.Show(ex.Message);
        }
    }

    private void setReferenceColumn(DataTable dt)
    {
        HyperLink link_ref, link_gene; Label lbl_ref; LinkButton seeDetailsBtn;



        for (int i = 0; i < grdViewDiseases.Rows.Count; i++)
        {
            link_gene = grdViewDiseases.Rows[i].FindControl("Gene_Name") as HyperLink;
            link_gene.NavigateUrl = String.Format("{0}?gene={1}", ConfigurationManager.AppSettings["GeneCardsLink"], link_gene.Text);
            link_gene.Target = "_blank";

            switch (dt.Rows[i]["Reference_Type"].ToString())
            {
                case "1":
                    link_ref = grdViewDiseases.Rows[i].FindControl("Link") as HyperLink;
                    link_ref.NavigateUrl = String.Format("{0}/{1}", ConfigurationManager.AppSettings["PubmedLink"], link_ref.Text);
                    link_ref.Target = "_blank";
                    link_ref.Text = "External Links";
                    seeDetailsBtn = grdViewDiseases.Rows[i].FindControl("seeDetailsBtn") as LinkButton;
                    seeDetailsBtn.Visible = false;
                    break;

                case "2":
                    link_ref = grdViewDiseases.Rows[i].FindControl("Link") as HyperLink;
                    link_ref.Target = "_blank";
                    link_ref.NavigateUrl = link_ref.Text;
                    link_ref.Text = "External Links";
                    seeDetailsBtn = grdViewDiseases.Rows[i].FindControl("seeDetailsBtn") as LinkButton;
                    seeDetailsBtn.Visible = false;
                    break;

                case "3":
                default:
                    link_ref = grdViewDiseases.Rows[i].FindControl("Link") as HyperLink;
                    link_ref.Visible = false;
                    lbl_ref = grdViewDiseases.Rows[i].FindControl("lbl_reference") as Label;
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

        lbl_ref = grdViewDiseases.Rows[i].FindControl("lbl_reference") as Label;
        referenceLbl.Text = lbl_ref.Text;
        
        this.Button1_ModalPopupExtender.Show();
    }

    public SortDirection dir
    {
        get
        {
            if (ViewState["dirState"] == null)
            {
                ViewState["dirState"] = SortDirection.Ascending;
            }
            return (SortDirection)ViewState["dirState"];
        }
        set
        {
            ViewState["dirState"] = value;
        }
    }
    protected void grdViewDiseases_Sorting(object sender, GridViewSortEventArgs e)
    {
        string sortingDirection = string.Empty;
        if (dir == SortDirection.Ascending)
        {
            dir = SortDirection.Descending;
            sortingDirection = "Desc";
        }
        else
        {
            dir = SortDirection.Ascending;
            sortingDirection = "Asc";
        }

        DataView sortedView = new DataView(dDetails.getDiseaseDetails(cmbDiseaseName.SelectedValue));
        sortedView.Sort = e.SortExpression + " " + sortingDirection;
        grdViewDiseases.DataSource = sortedView;
        grdViewDiseases.DataBind();
        setReferenceColumn(sortedView.ToTable()); 
    }
    protected void grdViewDiseases_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdViewDiseases.PageIndex = e.NewPageIndex;
        getGridData();
    }
}