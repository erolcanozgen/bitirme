using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Configuration;

public partial class Disasters : System.Web.UI.Page
{
    DiseasesNames dName = new DiseasesNames();
    DiseaseDetails dDetails = new DiseaseDetails();

    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (!IsPostBack)
        {
            DiseasesList.DataTextField = "Name";
            DiseasesList.DataValueField = "Table_Name";
            DiseasesList.DataSource = dName.selectDiseasesNames();
            DiseasesList.DataBind();
            GetGridData();

            if (Session["user"] != null)
            {
                excelAktar.Visible = Visible;
            }
        }
        //ListItem loginLI = this.Master.Page.FindControl("bsr") as ListItem;
        //loginLI.Visible = false; 
    }

    private void GetGridData()
    {
        HyperLink link_gene;
        DataView sortedView = new DataView(dDetails.getMetaAnalysis(DiseasesList.SelectedValue));
        sortedView.Sort = "OR_Value Desc";
        grdViewCustomers.DataSource = sortedView;
        grdViewCustomers.DataBind();
        foreach (GridViewRow row in grdViewCustomers.Rows)
        {
            if (row.RowIndex % 2 == 1) row.CssClass = "even";
        }
        for (int i = 0; i < grdViewCustomers.Rows.Count; i++)
        {
            link_gene = grdViewCustomers.Rows[i].FindControl("Gene_Name") as HyperLink;
            link_gene.NavigateUrl = String.Format("{0}?gene={1}", ConfigurationManager.AppSettings["GeneCardsLink"], link_gene.Text);
            link_gene.Target = "_blank";
        }
    }

    public void showDiseaseDetails(object sender, EventArgs e)
    {
        GetGridData();  
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
    protected void gvDetails_Sorting(object sender, GridViewSortEventArgs e)
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

        DataView sortedView = new DataView(dDetails.getMetaAnalysis(DiseasesList.SelectedValue));
        sortedView.Sort = e.SortExpression + " " + sortingDirection;
        grdViewCustomers.DataSource = sortedView;
        grdViewCustomers.DataBind();
    }

    protected void grdViewCustomers_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string SNP_Name = grdViewCustomers.DataKeys[e.Row.RowIndex]["SNP"].ToString();
            string tmpId = grdViewCustomers.DataKeys[e.Row.RowIndex]["tmpId"].ToString();
            GridView grdViewOrdersOfCustomer = (GridView)e.Row.FindControl("grdViewOrdersOfCustomer");
            DataTable dt = dDetails.getSNPDetails(DiseasesList.SelectedValue, SNP_Name, tmpId);
            grdViewOrdersOfCustomer.DataSource = dt;
            grdViewOrdersOfCustomer.DataBind();
            setReferenceColumn(dt, grdViewOrdersOfCustomer);
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
    }
    protected void excelAktar_Click(object sender, ImageClickEventArgs e)
    {
        Response.Clear();
        string fileName = "attachment; filename=" + DiseasesList.SelectedValue + ".xls";
        Response.AddHeader("content-disposition", fileName);
        Response.ContentEncoding = System.Text.Encoding.GetEncoding("Windows-1254");
        Response.Charset = "UTF-8";
        Response.ContentType = "application/ms-excel";

        StringWriter stringWrite = new StringWriter();
        HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);


        grdViewCustomers.RenderControl(htmlWrite);

        Response.Write(stringWrite.ToString());
        Response.End();
    }

    private void setReferenceColumn(DataTable dt, GridView grd)
    {
        HyperLink link_ref; Label lbl_ref;
        for (int i = 0; i < grd.Rows.Count; i++)
        {
            switch (dt.Rows[i]["Reference_Type"].ToString())
            {
                case "1":
                    link_ref = grd.Rows[i].FindControl("Link") as HyperLink;
                    link_ref.Text = "External Links";
                    link_ref.NavigateUrl = String.Format("{0}/{1}", ConfigurationManager.AppSettings["PubmedLink"], dt.Rows[i]["Reference"].ToString());
                    link_ref.Target = "_blank";
                    lbl_ref = grd.Rows[i].FindControl("lbl_reference") as Label;
                    lbl_ref.Visible = false;
                    break;

                case "2":
                    link_ref = grd.Rows[i].FindControl("Link") as HyperLink;
                    link_ref.Text = "External Links";
                    link_ref.Target = "_blank";
                    link_ref.NavigateUrl = dt.Rows[i]["Reference"].ToString();
                    lbl_ref = grd.Rows[i].FindControl("lbl_reference") as Label;
                    lbl_ref.Visible = false;
                    break;

                case "3":
                default:
                    link_ref = grd.Rows[i].FindControl("Link") as HyperLink;
                    link_ref.Visible = false;
                    lbl_ref = grd.Rows[i].FindControl("lbl_reference") as Label;
                    lbl_ref.Text = dt.Rows[i]["Reference"].ToString();
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
 
        lbl_ref = row.FindControl("lbl_reference") as Label;
        referenceTxt.Text = lbl_ref.Text;

        this.Button1_ModalPopupExtender.Show();
    }
    protected void grdViewCustomers_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdViewCustomers.PageIndex = e.NewPageIndex;
        GetGridData();
    }

    protected void btnMetaAnalysis_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = new DataTable();
            DiseaseDetails dd = new DiseaseDetails();
            dd.disease_name = DiseasesList.SelectedValue;
            dt = dd.GetSnpList();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["SNP"].ToString() == "null" || dt.Rows[i]["SNP"].ToString() == String.Empty) continue;
                MetaAnalaysis ma = new MetaAnalaysis(DiseasesList.SelectedValue.ToString(), dt.Rows[i]["SNP"].ToString());
                ma.DoMetaAnalysis();
            }
            GetGridData();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Meta analysis for " + DiseasesList.SelectedValue + "  has been done successfully.');", true);
            
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Meta analysis for " + DiseasesList.SelectedValue + " could not be done.');", true);
        }
    }
    protected void btnEnrichment_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "", "ShowProgress();", true);
        //string script = "$(document).ready(function () { $('[id*=btnEnrichment]').click(); });";
        //ClientScript.RegisterStartupScript(this.GetType(), "load", script, true);

        int selectedRowCounts = 0; string genes = String.Empty;
        foreach (GridViewRow row in grdViewCustomers.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkRow = (row.Cells[0].FindControl("chkRow") as CheckBox);
                if (chkRow.Checked)
                {
                    selectedRowCounts++;
                    genes += (row.Cells[3].FindControl("Gene_Name") as HyperLink).Text + ":";
                }
            }
        }
        if (selectedRowCounts <= 0)
        {
            Notifier.AddErrorMessage("Please select one publication at least!");
        }
        else 
        {
            Response.Redirect(String.Format("~/Enrichment.aspx?genes={0}",genes));
        }
    }
}
