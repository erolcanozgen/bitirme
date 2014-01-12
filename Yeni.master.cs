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
        Page.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        if (!IsPostBack)
        {
            if (Session["user"] != null)
            {
                AddNewPublicationLink.Visible = true;
                login_signUp.Visible = false;
                session_name.Text = "Welcome " + ((Users)Session["user"]).name;
                logout.Visible = true;
            }

            else
            {
                AddNewPublicationLink.Visible = false;
                login_signUp.Visible = true;
                session_name.Text = "Welcome Guest!";
                logout.Visible = false;
            }
        }

        else
        {
            Response.ClearHeaders();

            Response.AddHeader("Cache-Control", "no-cache, no-store, max-age=0, must-revalidate");

            Response.AddHeader("Pragma", "no-cache");

        }

    }


    protected void Login_Click(object sender, EventArgs e)
    {
        Connection newConnection = new Connection();
        Users newUser = new Users();

        if (newUser.checkUser(L_UserName.Text, L_passwd.Text))
        {
            Session.Add("user", newUser);
            Response.Redirect(Request.RawUrl);
        }
        else Alert.Show("Wrong username and password combinations.");
    }
    protected void Register_Click(object sender, EventArgs e)
    {
        if (R_Passwd.Text != R_ConfirmPasswd.Text) Alert.Show("Entered passwords are not matched each other");
        else
        {
            try
            {
                Users newUser = new Users(R_Username.Text, FirstName.Text, LastName.Text, Email.Text, R_Passwd.Text, 1);
                newUser.AddUser();
                Alert.Show("Registration is successfull");
            }
            catch (Exception ex)
            {
                Alert.Show(ex.Message);
            }
        }
    }
 }
