using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Enrichment : System.Web.UI.Page
{
    static DataView stable = new DataView();
    DataTable table = new DataTable();
    static KeggApi kegg = new KeggApi();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                string notFoundGenes = String.Empty;
                string notFoundPaths = String.Empty;
                kegg.clear();
                string[] genes = Request.QueryString["genes"].Split(':');
                for (int i = 0; i < genes.Length; i++)
                {
                    if (genes[i] == "") continue;
                    if (!kegg.findGenes(genes[i]))
                        notFoundGenes += genes[i] + " ";
                }
                if (notFoundGenes != String.Empty)
                    Notifier.AddWarningMessage(notFoundGenes + "gene(s) are not found!");
                else
                    grdEnrichment.EmptyDataText = "There are no pathway for selected gene(s)";
                kegg.linkGenesToPathway();

                table.Clear();
                table.Columns.Clear();
                table.Columns.Add("keggPathways", typeof(string));
                table.Columns.Add("searchedGenes", typeof(string));
                table.Columns.Add("significantScore", typeof(float));

                List<string> coveredPathways = new List<string>();
                foreach (string[] s in kegg.Pathways)
                {
                    if ((coveredPathways.Find(p => p == s[1])) == null)
                    {
                        coveredPathways.Add(s[1]);
                        string[] tmpGenes = kegg.getGenes(s[1]).ToArray();
                        string searchedGenes = String.Empty;
                        foreach (string str in tmpGenes)
                        {
                            if (str == tmpGenes[tmpGenes.Length - 1])
                                searchedGenes += str;
                            else
                                searchedGenes += str + ",";
                        }
                        table.Rows.Add(s[1], searchedGenes, ((float)tmpGenes.Length / kegg.genesCountInPathway(s[1])));
                    }
                }
                DataView sortedView = new DataView(table);
                sortedView.Sort = "significantScore Desc";
                grdEnrichment.DataSource = sortedView;
                grdEnrichment.DataBind();

                stable = sortedView;

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
                    link_image.NavigateUrl = String.Format("http://www.kegg.jp/kegg-bin/show_pathway?{0}{1}", link_image.Text, createParameters(genesOfPath,kegg.Genes));
                    link_image.Target = "_blank";

                    string[] colors = { "yellow", "orange", "pink", "purple", "brown", "green", "red", "black" };
                    for (int j = 0; j < genesOfPath.Length; j++ )
                        grdEnrichment.Rows[i].Cells[1].Text += "   <span style=\"border: 1px solid blue;color:Blue;background-color:" + (j > colors.Length - 1 ? colors[colors.Length - 1] : colors[j]) + ";\">" + genesOfPath[j] + "</span>";
                }
            }
        }
        catch (Exception ex)
        {
            Notifier.AddErrorMessage("An error was occured.Please try again later.");
            //Alert.Show(ex.Message);
        }
    }
    private string createParameters(string[] genesOfPath,List<string[]> s)
    {
        List<string[]> syn = new List<string[]>(); 
        string param = String.Empty;
        string[] colors = { "%09yellow,blue", "%09orange,blue", "%09pink,blue", "%09purple,blue", "%09brown,blue", "%09green,blue", "%09red,blue", "%09black,blue" };
        for (int i = 0; i < genesOfPath.Length; i++) 
        {
            int j=0;
            if(i>colors.Length) 
                j = colors.Length-1;
            else 
                j = i;
            string[] synonims = s.Where(p => p[0] == genesOfPath[i]).Select(p => p[2]).ToArray();
            foreach (string synonym in synonims)
            {
                foreach (string str in synonym.Split(','))
                    param += '/' + str + colors[j];
            }
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
            link_image.NavigateUrl = String.Format("http://www.kegg.jp/kegg-bin/show_pathway?{0}{1}", link_image.Text, createParameters(genesOfPath, kegg.Genes));
            link_image.Target = "_blank";

            string[] colors = { "yellow", "orange", "pink", "purple", "brown", "green", "red", "black" };
            for (int j = 0; j < genesOfPath.Length; j++)
                grdEnrichment.Rows[i].Cells[1].Text += "   <span style=\"border: 1px solid blue;color:Blue;background-color:" + (j > colors.Length - 1 ? colors[colors.Length - 1] : colors[j]) + ";\">" + genesOfPath[j] + "</span>";
        }
    }
    protected void grdEnrichment_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdEnrichment.PageIndex = e.NewPageIndex;
        stable.Sort = "significantScore Desc";
        grdEnrichment.DataSource = stable;
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
            link_image.NavigateUrl = String.Format("http://www.kegg.jp/kegg-bin/show_pathway?{0}{1}", link_image.Text, createParameters(genesOfPath, kegg.Genes));
            link_image.Target = "_blank";

            string[] colors = { "yellow", "orange", "pink", "purple", "brown", "green", "red", "black" };
            for (int j = 0; j < genesOfPath.Length; j++)
                grdEnrichment.Rows[i].Cells[1].Text += "   <span style=\"border: 1px solid blue;color:Blue;background-color:" + (j > colors.Length - 1 ? colors[colors.Length - 1] : colors[j]) + ";\">" + genesOfPath[j] + "</span>";
        }
    }
}