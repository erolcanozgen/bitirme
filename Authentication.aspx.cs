using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Authentication : System.Web.UI.Page
{
    string UserName;
    string Password;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        UserName = Request["Username"];
        Password = Request["Password"];

        Connection newConnection = new Connection();
        Users newUser = new Users();

        if (newUser.checkUser(UserName, Password))
            Session.Add("user", newUser);
        else      
            Response.Write("Wrong username and password combinations.");

        Response.Redirect("HomePage.aspx");
 
      
 

    }
}