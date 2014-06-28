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
            if (txtOldPwd.Text == Encryption.SifreyiCozAES(user.passwd))
            {
                if (txtNewPwdAgain.Text == txtNewPwd.Text)
                {
                    user.passwd = Encryption.SifreleAES(txtNewPwdAgain.Text);
                    user.ChangePassword();
                    Notifier.AddSuccessMessage("Password has been changed successfuly.");
                }
                else Notifier.AddErrorMessage("New passwords do not match.");

            }
            else Notifier.AddErrorMessage("Wrong old password is entered.");

        }
        catch (Exception ex) { Notifier.AddErrorMessage("An error was occured while registration"); }
    }
   
}