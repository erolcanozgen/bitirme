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
            dName.selectDiseasesNames();
            
            //bu bir gridviewe atanacak
            grdViewUnapprovedDiseases.DataSource = dt = dDetails.selectUnapprovedDiseaseDetails(dName.tableNames);
            grdViewUnapprovedDiseases.DataBind();

            if (dt.Rows.Count <= 0)
            {
                buttonApprove.Visible = false;
                nullWarningLabel.Visible = true;
            }
        
        }

    }
    protected void buttonApprove_Click(object sender, EventArgs e)
    {
        int selectedRowCounts = 0;
        List<int> diseaseId = new List<int>();
        List<string> diseaseName = new List<string>();

        foreach (GridViewRow row in grdViewUnapprovedDiseases.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkRow = (row.Cells[0].FindControl("chkRow") as CheckBox);
                if (chkRow.Checked)
                {
                    selectedRowCounts++;
                    diseaseId.Add(Convert.ToInt32(grdViewUnapprovedDiseases.DataKeys[row.RowIndex].Value.ToString()));
                    diseaseName.Add(row.Cells[2].Text);
                }
            }
        }
        if (selectedRowCounts > 0)
        {
            notifier.Dispose();
            try
            {
                dDetails.approveSelectedPublication(diseaseId, diseaseName);
                Notifier.AddSuccessMessage("Selected publicaton(s) has been approved.");
                Response.Redirect("AddedResearches.aspx");
            }
            catch (Exception ex)
            {
                Notifier.AddErrorMessage("An error occured! Please try again later.");
                //Alert.Show("An error occured!Please try again later.");
            }
        }
        else
        {
            Notifier.AddWarningMessage("Please select at least one publication to approve!");
            //Alert.Show("Please select at least one publication to approve!");
        }
    }

}