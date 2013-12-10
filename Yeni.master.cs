using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Yeni : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user"] != null)
        {
            login_signUp.Visible = false;
            session_name.Text = ((Users)Session["user"]).name;
            logout.Visible = true;
        }

        else
        {
            login_signUp.Visible = true;
            session_name.Text = "Hello Guest!";
            logout.Visible = false;
        }
    }


    protected void Login_Click(object sender, EventArgs e)
    {
        Connection newConnection = new Connection();
        Users newUser = new Users();

        if (newUser.checkUser(L_UserName.Text, L_passwd.Text))
        {
            Session.Add("user", newUser);
            Response.Redirect("HomePage.aspx");
        }
        else Alert.Show("Wrong username and password combinations.");
    }
}
