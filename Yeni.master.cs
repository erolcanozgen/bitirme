using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Yeni : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        DiseasesNames diseaseNames = new DiseasesNames();
        DataTable dt = diseaseNames.selectDiseasesNames();


        for (int i = 0; i < dt.Rows.Count; i++)
        {
            HyperLink DynLink = new HyperLink();
            DynLink.ID = dt.Rows[i]["Table_Name"].ToString();
            DynLink.Text = dt.Rows[i]["Name"].ToString();
            DynLink.NavigateUrl = string.Format("~/Diseases.aspx?disease={0}", dt.Rows[i]["Table_Name"].ToString());
            DiseaseList.Controls.Add(DynLink);
        }

        if (!IsPostBack)
        {


            if (Session["user"] != null)
            {
                AddNewPublicationLink.Visible = true;
                login_signUp.Visible = false;
                session_name.Text = "Welcome " + ((Users)Session["user"]).name;
                logout.Visible = true;
                MyAccount.Visible = true;
                if(((Users)Session["user"]).rolId==1)
                    AdminPage.Visible = true;
                else
                    AdminPage.Visible = false;
            }

            else
            {
                AddNewPublicationLink.Visible = false;
                AdminPage.Visible = false;
                login_signUp.Visible = true;
                session_name.Text = "Welcome Guest!";
                logout.Visible = false;
                MyAccount.Visible = false;
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
            Notifier.AddSuccessMessage("Success Login.");
            Session.Add("user", newUser);
            Response.Redirect(Request.RawUrl);
        }
        else
            Notifier.AddErrorMessage("Wrong username and password combinations.");
            //Alert.Show("Wrong username and password combinations.");
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
                FindAllTextBox(this);
            }
            catch (Exception ex)
            {

                Alert.Show("An error was occured while registration");
            }
        }
    }

    public void FindAllTextBox(Control ctrl)
    {
        if (ctrl != null)
        {
            foreach (Control c in ctrl.Controls)
            {
                if (c is TextBox)
                    ((TextBox)c).Text = String.Empty;
                FindAllTextBox(c);
            }
        }
    }

 }
