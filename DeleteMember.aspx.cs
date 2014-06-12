using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DeleteMember : System.Web.UI.Page
{
    Users members = new Users();
    DataTable dt = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (this.Session["user"] == null)
            {
                Notifier.AddInfoMessage("You should sign in ");
                Response.Redirect("~/HomePage.aspx");
            }
            else
            {
                notifier.Dispose();

                getMembers();
                if (dt.Rows.Count <= 0)
                {
                    deleteMembersBtn.Visible = false;
                    Notifier.AddWarningMessage("There are no member!");
                }
            }
        }
    }

    private void getMembers()
    {
        grdViewMembers.DataSource = dt = members.getUsers();
        grdViewMembers.DataBind();
    }

    protected void buttonDeleteMembers_Click(object sender, EventArgs e)
    {
        notifier.Dispose();

        int selectedRowCounts = 0;
        List<int> memberId = new List<int>();

        foreach (GridViewRow row in grdViewMembers.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkRow = (row.Cells[0].FindControl("chkRow") as CheckBox);
                if (chkRow.Checked)
                {
                    selectedRowCounts++;
                    memberId.Add(Convert.ToInt32(grdViewMembers.DataKeys[row.RowIndex]["ID"].ToString()));
                }
            }
        }
        if (selectedRowCounts > 0)
        {
            notifier.Dispose();
            try
            {
                for (int i = 0; i < memberId.Count; i++)
                {
                    members.id = memberId[i];
                    members.deleteUser();
                }
                Notifier.AddSuccessMessage("Selected member(s) has been deleted.");
               

                getMembers();
                if (dt.Rows.Count <= 0)
                {   
                    deleteMembersBtn.Visible = false;
                    Notifier.AddErrorMessage("There are no member!");
                }

            }
            catch (Exception ex)
            {
                Notifier.AddErrorMessage("An error occured! Please try again later.");
            }
        }
        else
        {
            Notifier.AddWarningMessage("Please select at least one member to delete!");
        }
    }
}