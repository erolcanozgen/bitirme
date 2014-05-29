using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Enrichment : System.Web.UI.Page
{
    static DataTable table = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                 string[] genes = Request.QueryString["genes"].Split(':');
                 KeggApi kegg = new KeggApi();
                 for (int i = 0; i < genes.Length - 1; i++)
                 {
                     kegg.findGenes(genes[i]);
                 }
                 kegg.linkGenesToPathway();

                 table.Clear();
                 table.Columns.Clear();
                 table.Columns.Add("keggPathways", typeof(string));
                 table.Columns.Add("searchedGenes", typeof(string));
                 table.Columns.Add("significantScore", typeof(float));

                 foreach (string[] s in kegg.Pathways)
                 {
                     string[] tmpGenes = kegg.getGenes(s[1]).ToArray();
                     string searchedGenes = String.Empty;
                         foreach (string str in tmpGenes)
                         {
                             if (str == tmpGenes[tmpGenes.Length - 1])
                                 searchedGenes += str;
                            else
                                 searchedGenes += str + ",";
                        }

                         table.Rows.Add(s[1], searchedGenes, ((float)tmpGenes.Length/kegg.genesCountInPathway(s[1])));

                 }
                 DataView sortedView = new DataView(table);
                 sortedView.Sort = "significantScore Desc";
                 grdEnrichment.DataSource = sortedView;
                 grdEnrichment.DataBind();

                 foreach (GridViewRow row in grdEnrichment.Rows)
                 {
                     if (row.RowIndex % 2 == 1) row.CssClass = "even";
                 }

                 HyperLink link_image;
                 for (int i = 0; i < grdEnrichment.Rows.Count; i++)
                 {
                     link_image = grdEnrichment.Rows[i].FindControl("keggPathways") as HyperLink;
                     if (link_image.Text.Substring(0, 5) == "path:")
                         link_image.Text = link_image.Text.Remove(0, 5);    // remove path: from path:hsa05010 = hsa05010 
                     string[] genesOfPath = (grdEnrichment.Rows[i].FindControl("searchedGenes") as Label).Text.Split(',');
                     link_image.NavigateUrl = String.Format("http://www.kegg.jp/kegg-bin/show_pathway?{0}{1}", link_image.Text, createParameters(genesOfPath));
                     link_image.Target = "_blank";
                 }
            }
        }
        catch (Exception ex)
        {
            Notifier.AddErrorMessage("An error was occured.Please try again later.");
            //Alert.Show(ex.Message);
        }
    }
    private string createParameters(string[] genesOfPath)
    {
        string param = String.Empty;
        string[] colors = { "%09yellow,blue", "%09orange,blue", "%09pink,blue", "%09purple,blue", "%09brown,blue", "%09green,blue", "%09red,blue", "%09black,blue" };
        for (int i = 0; i < genesOfPath.Length; i++) 
        {
            int j=0;
            if(i>colors.Length) 
                j = colors.Length;
            else 
                j = i;
            param += '/' + genesOfPath[i] + colors[j]; 
        }
        return param;
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
    protected void grdEnrichment_Sorting(object sender, GridViewSortEventArgs e)
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

        DataView sortedView = new DataView(table);
        sortedView.Sort = e.SortExpression + " " + sortingDirection;
        grdEnrichment.DataSource = sortedView;
        grdEnrichment.DataBind();

        foreach (GridViewRow row in grdEnrichment.Rows)
        {
            if (row.RowIndex % 2 == 1) row.CssClass = "even";
        }

        HyperLink link_image;
        for (int i = 0; i < grdEnrichment.Rows.Count; i++)
        {
            link_image = grdEnrichment.Rows[i].FindControl("keggPathways") as HyperLink;
            if (link_image.Text.Substring(0, 5) == "path:")
                link_image.Text = link_image.Text.Remove(0, 5);    // remove path: from path:hsa05010 = hsa05010 
            string[] genesOfPath = (grdEnrichment.Rows[i].FindControl("searchedGenes") as Label).Text.Split(',');
            link_image.NavigateUrl = String.Format("http://www.kegg.jp/kegg-bin/show_pathway?{0}{1}", link_image.Text, createParameters(genesOfPath));
            link_image.Target = "_blank";
        }
    }

    protected void grdEnrichment_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdEnrichment.PageIndex = e.NewPageIndex;
        DataView sortedView = new DataView(table);
        sortedView.Sort = "significantScore Desc";
        grdEnrichment.DataSource = sortedView;
        grdEnrichment.DataBind();

        foreach (GridViewRow row in grdEnrichment.Rows)
        {
            if (row.RowIndex % 2 == 1) row.CssClass = "even";
        }

        HyperLink link_image;
        for (int i = 0; i < grdEnrichment.Rows.Count; i++)
        {
            link_image = grdEnrichment.Rows[i].FindControl("keggPathways") as HyperLink;
            if (link_image.Text.Substring(0, 5) == "path:")
                link_image.Text = link_image.Text.Remove(0, 5);    // remove path: from path:hsa05010 = hsa05010 
            string[] genesOfPath = (grdEnrichment.Rows[i].FindControl("searchedGenes") as Label).Text.Split(',');
            link_image.NavigateUrl = String.Format("http://www.kegg.jp/kegg-bin/show_pathway?{0}{1}", link_image.Text, createParameters(genesOfPath));
            link_image.Target = "_blank";
        }
    }
}