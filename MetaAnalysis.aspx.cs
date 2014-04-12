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

            if (Session["user"] != null)
            {
                excelAktar.Visible = Visible;
            }


        }
        //ListItem loginLI = this.Master.Page.FindControl("bsr") as ListItem;
        //loginLI.Visible = false; 
    }

    public void showDiseaseDetails(object sender, EventArgs e)
    {
        DataView sortedView = new DataView(dDetails.selectMetaAnalysis(DiseasesList.SelectedValue));
        sortedView.Sort = "OR_Value Desc";
        grdViewCustomers.DataSource = sortedView;
        grdViewCustomers.DataBind();
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

     private void setReferenceColumn(DataTable dt ,GridView grd)
    {
        HyperLink link_ref; Label lbl_ref;

        for (int i = 0; i < grd.Rows.Count; i++)
            switch (dt.Rows[i].ItemArray[8].ToString())
            {
                case "1":
                    link_ref = grd.Rows[i].FindControl("Link") as HyperLink;
                    link_ref.Text = "External Links";
                    link_ref.NavigateUrl = String.Format("{0}/{1}", ConfigurationManager.AppSettings["PubmedLink"], dt.Rows[i].ItemArray[7]); // ItemArray[7] <-- Reference  alanı
                    link_ref.Target = "_blank";
                    lbl_ref = grd.Rows[i].FindControl("lbl_reference") as Label;
                    lbl_ref.Visible = false;
                    break;

                case "2":
                    link_ref = grd.Rows[i].FindControl("Link") as HyperLink;
                    link_ref.Text = "External Links";
                    link_ref.Target = "_blank";
                    link_ref.NavigateUrl = dt.Rows[i].ItemArray[7].ToString();
                    lbl_ref = grd.Rows[i].FindControl("lbl_reference") as Label;
                    lbl_ref.Visible = false;
                    break;

                case "3":
                default:
                    link_ref = grd.Rows[i].FindControl("Link") as HyperLink;
                    link_ref.Visible = false;
                    lbl_ref = grd.Rows[i].FindControl("lbl_reference") as Label;
                    lbl_ref.Text = dt.Rows[i].ItemArray[7].ToString();
                    break;
            }
    }
}
