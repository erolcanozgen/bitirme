using System;
using System.Collections.Generic;
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
            dName.selectDiseasesNames();
            
            //bu bir gridviewe atanacak
            grdViewUnapprovedDiseases.DataSource = dt = dDetails.selectUnapprovedDiseaseDetails(dName.tableNames);
            grdViewUnapprovedDiseases.DataBind();

            if (dt.Rows.Count <= 0)
            {
                buttonApprove.Visible = false;
                Notifier.AddErrorMessage("No new added publication!");
            }
        
        }

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
                
                for (int i = 0; i < diseaseName.Count; i++)
                {
                    dDetails.approveSelectedPublication(diseaseId[i], diseaseName[i]);
                    MetaAnalaysis mt = new MetaAnalaysis(diseaseName[i], SNP[i]);
                    mt.DoMetaAnalysis();
                    Notifier.AddSuccessMessage("Selected publicaton(s) has been approved.");
                    
                }
            }
            catch (Exception ex)
            {
                Notifier.AddErrorMessage("An error occured! Please try again later.");
                //Alert.Show("An error occured!Please try again later.");
            }
            Response.Redirect("~/AddedResearches.aspx");
        }
        else
        {
            Notifier.AddWarningMessage("Please select at least one publication to approve!");
            //Alert.Show("Please select at least one publication to approve!");
        }
    }

}