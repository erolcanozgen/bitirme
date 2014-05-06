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
        DataView sortedView = new DataView(dDetails.selectMetaAnalysis(DiseasesList.SelectedValue));
        sortedView.Sort = "OR_Value Desc";
        grdViewCustomers.DataSource = sortedView;
        grdViewCustomers.DataBind();
        foreach (GridViewRow row in grdViewCustomers.Rows)
        {
            if (row.RowIndex % 2 == 1) row.CssClass = "even";
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

        DataView sortedView = new DataView(dDetails.selectMetaAnalysis(DiseasesList.SelectedValue));
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
            DataTable dt = dDetails.selectSNPDetails(DiseasesList.SelectedValue, SNP_Name, tmpId);
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
        HyperLink link_ref, link_gene; Label lbl_ref;

        for (int i = 0; i < grd.Rows.Count; i++)
        {
            link_gene = grd.Rows[i].FindControl("Gene_Name") as HyperLink;
            link_gene.Text = dt.Rows[i]["Gene_Name"].ToString();
            link_gene.NavigateUrl = String.Format("{0}?gene={1}", ConfigurationManager.AppSettings["GeneCardsLink"], dt.Rows[i]["Gene_Name"]);
            link_gene.Target = "_blank";


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
        DataTable dt = new DataTable();
        DiseaseDetails dd = new DiseaseDetails();
        dd.disease_name = DiseasesList.SelectedValue;
        dt = dd.GetSnpList();

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (dt.Rows[i]["SNP"] == null || dt.Rows[i]["SNP"].ToString() == String.Empty) continue;
            MetaAnalaysis ma = new MetaAnalaysis(DiseasesList.SelectedValue.ToString(), dt.Rows[i]["SNP"].ToString());
            ma.DoMetaAnalysis();
        }
    }
}
