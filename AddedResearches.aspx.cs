using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AddedResearches : System.Web.UI.Page
{
    DiseasesNames dName = new DiseasesNames();
    DiseaseDetails dDetails = new DiseaseDetails();
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            notifier.Dispose();

            selectUnapprovedDiseases();

            if (dt.Rows.Count <= 0)
            {
                buttonApprove.Visible = false;
                buttonReject.Visible = false;
                Notifier.AddErrorMessage("No new added publication!");
            }
        
        }

    }
    private void selectUnapprovedDiseases()
    {
        dName.selectDiseasesNames();

        grdViewUnapprovedDiseases.DataSource = dt = dDetails.selectUnapprovedDiseaseDetails(dName.tableNames);
        grdViewUnapprovedDiseases.DataBind();
        setReferenceColumn(dt);
    }
    protected void buttonApprove_Click(object sender, EventArgs e)
    {
        notifier.Dispose();

        int selectedRowCounts = 0;
        List<int> diseaseId = new List<int>();
        List<string> diseaseName = new List<string>();
        List<string> SNP = new List<string>();

        foreach (GridViewRow row in grdViewUnapprovedDiseases.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkRow = (row.Cells[0].FindControl("chkRow") as CheckBox);
                if (chkRow.Checked)
                {
                    selectedRowCounts++;
                    diseaseId.Add(Convert.ToInt32(grdViewUnapprovedDiseases.DataKeys[row.RowIndex]["ID"].ToString()));
                    diseaseName.Add(grdViewUnapprovedDiseases.DataKeys[row.RowIndex]["Disease_Name"].ToString());
                    SNP.Add(grdViewUnapprovedDiseases.DataKeys[row.RowIndex]["SNP"].ToString());
                }
            }
        }
        if (selectedRowCounts > 0)
        {
            notifier.Dispose();
            try
            {
                if (((Button)sender).ID == "buttonApprove")
                {
                    for (int i = 0; i < diseaseName.Count; i++)
                    {
                        
                        dDetails.approveSelectedPublication(diseaseId[i], diseaseName[i]);

                        if (MetaAnalysisDB.isMetaAnalysisDone(diseaseName[i], SNP[i]))
                        {
                            //update existing
                            MetaAnalaysis mt = new MetaAnalaysis(diseaseName[i], SNP[i]);
                            mt.UpdateMetaAnalysis();
                        }
                        else
                        {
                            //insert new
                            MetaAnalaysis mt = new MetaAnalaysis(diseaseName[i], SNP[i]);
                            mt.DoMetaAnalysis();
                        }
                    }
                    Notifier.AddSuccessMessage("Selected publicaton(s) has been approved.");
                }


                else if (((Button)sender).ID == "buttonReject")
                {

                    for (int i = 0; i < diseaseName.Count; i++)
                        dDetails.deleteSelectedPublication(diseaseId[i], diseaseName[i]);
                   
                    Notifier.AddSuccessMessage("Selected publicaton(s) has been deleted.");
                }


                selectUnapprovedDiseases();
                if (dt.Rows.Count <= 0)
                {
                    buttonApprove.Visible = false;
                    buttonReject.Visible = false;
                    Notifier.AddErrorMessage("No new added publication!");
                }

            }
            catch (Exception ex)
            {
                Notifier.AddErrorMessage("An error occured! Please try again later.");
                //Alert.Show("An error occured!Please try again later.");
            }
            //Response.Redirect("~/AddedResearches.aspx");
        }
        else
        {
            Notifier.AddWarningMessage("Please select at least one publication to approve!");
            //Alert.Show("Please select at least one publication to approve!");
        }
    }


    public void ShowPopup(object sender, EventArgs e)
    {
        Label lbl_ref;
        LinkButton btn = (LinkButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        int i = Convert.ToInt32(row.RowIndex);

        lbl_ref = grdViewUnapprovedDiseases.Rows[i].FindControl("lbl_reference") as Label;
        referenceLbl.Text = lbl_ref.Text;

        this.Button1_ModalPopupExtender.Show();
    }

    private void setReferenceColumn(DataTable dt)
    {
        HyperLink link_ref, link_gene; Label lbl_ref; LinkButton seeDetailsBtn;



        for (int i = 0; i < grdViewUnapprovedDiseases.Rows.Count; i++)
        {
            link_gene = grdViewUnapprovedDiseases.Rows[i].FindControl("Gene_Name") as HyperLink;
            link_gene.Text = dt.Rows[i]["Gene_Name"].ToString();
            link_gene.NavigateUrl = String.Format("{0}?gene={1}", ConfigurationManager.AppSettings["GeneCardsLink"], dt.Rows[i]["Gene_Name"]);
            link_gene.Target = "_blank";

            switch (dt.Rows[i]["Reference_Type"].ToString())
            {
                case "1":
                    link_ref = grdViewUnapprovedDiseases.Rows[i].FindControl("Link") as HyperLink;
                    link_ref.Text = "External Links";
                    link_ref.NavigateUrl = String.Format("{0}/{1}", ConfigurationManager.AppSettings["PubmedLink"], dt.Rows[i]["Reference"]);
                    link_ref.Target = "_blank";
                    seeDetailsBtn = grdViewUnapprovedDiseases.Rows[i].FindControl("seeDetailsBtn") as LinkButton;
                    seeDetailsBtn.Visible = false;
                    break;

                case "2":
                    link_ref = grdViewUnapprovedDiseases.Rows[i].FindControl("Link") as HyperLink;
                    link_ref.Text = "External Links";
                    link_ref.Target = "_blank";
                    link_ref.NavigateUrl = dt.Rows[i]["Reference"].ToString();
                    seeDetailsBtn = grdViewUnapprovedDiseases.Rows[i].FindControl("seeDetailsBtn") as LinkButton;
                    seeDetailsBtn.Visible = false;
                    break;

                case "3":
                default:
                    link_ref = grdViewUnapprovedDiseases.Rows[i].FindControl("Link") as HyperLink;
                    link_ref.Visible = false;
                    lbl_ref = grdViewUnapprovedDiseases.Rows[i].FindControl("lbl_reference") as Label;
                    lbl_ref.Text = dt.Rows[i]["Reference"].ToString();
                    break;
            }
        }
    }

}