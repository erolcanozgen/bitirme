using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Enrichment : System.Web.UI.Page
{
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

                 DataTable table = new DataTable();
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
                 grdEnrichment.DataSource = table;
                 grdEnrichment.DataBind();

                 HyperLink link_image;
                 for (int i = 0; i < grdEnrichment.Rows.Count; i++)
                 {
                     link_image = grdEnrichment.Rows[i].FindControl("keggPathways") as HyperLink;
                     link_image.NavigateUrl = String.Format("~/PathwayImage.aspx?pathway={0}", link_image.Text);
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
}