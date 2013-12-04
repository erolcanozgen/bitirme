using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user"] != null)
        {
            this.Master.FindControl("welcomeLabel").Visible = true;
            ((Label)this.Master.FindControl("welcomeLabel")).Text = "Welcome " + ((Users)Session["user"]).name;
            this.Master.FindControl("LoginLink").Visible = false;
            this.Master.FindControl("SignupLink").Visible = false;
            this.Master.FindControl("LogoutLink").Visible = true;

        }
    }
}