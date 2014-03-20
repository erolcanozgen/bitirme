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
        //grdViewCustomers.Visible = true;
        grdViewCustomers.DataSource = dDetails.selectMetaAnalysis(DiseasesList.SelectedValue);
        grdViewCustomers.DataBind();
    }
    protected void grdViewCustomers_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string SNP_Name = grdViewCustomers.DataKeys[e.Row.RowIndex].Value.ToString();
            GridView grdViewOrdersOfCustomer = (GridView)e.Row.FindControl("grdViewOrdersOfCustomer");
            grdViewOrdersOfCustomer.DataSource = dDetails.selectSNPDetails(DiseasesList.SelectedValue, SNP_Name);
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

    //eklenecek gridview'e
    protected void calculateSelectedMetaAnalysis(object sender, EventArgs e)
    {
        int selectedRowCounts = 0;
        GridView grdViewOrdersOfCustomer = (GridView)FindControl("grdViewOrdersOfCustomer");
        foreach (GridViewRow row in grdViewOrdersOfCustomer.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkRow = (row.Cells[0].FindControl("chkRow") as CheckBox);
                if (chkRow.Checked)
                {
                    selectedRowCounts++;
                }
            }
        }
        if (selectedRowCounts > 2)
        {
            //calculate meta analysis
        }
        else {
            Alert.Show("You must minimum 2 research to calculate meta-analysis!");
        }
    }
}
