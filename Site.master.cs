using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SiteMaster : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    protected void LogoutLink_Click(object sender, EventArgs e)
    {
        Session.Remove("user");
        LoginLink.Visible = true;
        SignupLink.Visible = true;
        LogoutLink.Visible = false;
    }
}
