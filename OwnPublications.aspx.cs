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
    protected void Page_Load(object sender, EventArgs e)
    {
        getGridData();
    }

    private void getGridData()
    {
       user = this.Session["user"] as Users;
        DataTable dt =  user.getOwnPublication();
       grdViewPublications.DataSource =dt;
       grdViewPublications.DataBind();
       setReferenceColumn(dt);
    }

    private void setReferenceColumn(DataTable dt)
    {
        HyperLink link_ref, link_gene; Label lbl_ref; LinkButton seeDetailsBtn;



        for (int i = 0; i < grdViewPublications.Rows.Count; i++)
        {
            link_gene = grdViewPublications.Rows[i].FindControl("Gene_Name") as HyperLink;
            link_gene.NavigateUrl = String.Format("{0}?gene={1}", ConfigurationManager.AppSettings["GeneCardsLink"], link_gene.Text);
            link_gene.Target = "_blank";

            switch (dt.Rows[i]["Reference_Type"].ToString())
            {
                case "1":
                    link_ref = grdViewPublications.Rows[i].FindControl("Link") as HyperLink;
                    link_ref.NavigateUrl = String.Format("{0}/{1}", ConfigurationManager.AppSettings["PubmedLink"], link_ref.Text);
                    link_ref.Target = "_blank";
                    link_ref.Text = String.Format("PMID: {0}", link_ref.Text);
                    link_ref.Visible = true;
                    break;

                case "2":
                    link_ref = grdViewPublications.Rows[i].FindControl("Link") as HyperLink;
                    link_ref.Target = "_blank";
                    link_ref.NavigateUrl = link_ref.Text;
                    link_ref.Text = "External Links";
                    link_ref.Visible = true;
                    break;

                case "3":
                default:
                    seeDetailsBtn = grdViewPublications.Rows[i].FindControl("seeDetailsBtn") as LinkButton;
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

        lbl_ref = grdViewPublications.Rows[i].FindControl("lbl_reference") as Label;
        referenceLbl.Text = lbl_ref.Text;

        this.Button1_ModalPopupExtender.Show();
    }

    protected void grdViewPublications_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdViewPublications.PageIndex = e.NewPageIndex;
        getGridData();
    }
}