using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

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

            grdViewCustomers.DataSource = dName.selectDiseasesNames();
            grdViewCustomers.DataBind();
        }
        //ListItem loginLI = this.Master.Page.FindControl("bsr") as ListItem;
        //loginLI.Visible = false; 
    }

    public void showDiseaseDetails(object sender, EventArgs e)
    {
        grdViewCustomers.DataSource = dName.selectDiseasesNames();
        grdViewCustomers.DataBind();

        //DiseasesDetailsGrid.Columns[0].Visible = false;
        //DiseasesDetailsGrid.Columns[1].Visible = false;
    }
    protected void grdViewCustomers_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string Table_Name = grdViewCustomers.DataKeys[e.Row.RowIndex].Value.ToString();
            GridView grdViewOrdersOfCustomer = (GridView)e.Row.FindControl("grdViewOrdersOfCustomer");
            grdViewOrdersOfCustomer.DataSource = dDetails.selectDiseasesNames(Table_Name);
            grdViewOrdersOfCustomer.DataBind();
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
}