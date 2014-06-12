using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ChangePassword : System.Web.UI.Page
{
    Users user = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.Session["user"] == null)
        {
            Notifier.AddInfoMessage("You should sign in");
            Response.Redirect("~/HomePage.aspx");
        }
        else
        user = this.Session["user"] as Users;
    }
    protected void btnChangePwd_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtOldPwd.Text == user.passwd)
            {
                if (txtNewPwdAgain.Text == txtNewPwd.Text)
                {
                    user.passwd = txtNewPwdAgain.Text;
                    user.ChangePassword();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Password has been changed successfuly.');", true);
                }
                else ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('New passwords do not match.');", true);
            }
            else ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Wrong old password is entered.');", true);
        }
        catch (Exception ex) { ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('An error is ocurred while changing password.');", true); }
    }
   
}