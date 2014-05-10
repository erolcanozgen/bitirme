using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MyAccount : System.Web.UI.Page
{
    Users user = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        user = this.Session["user"] as Users;
        if (!IsPostBack)
        {
            txtUsername.Text = user.username;
            txtName.Text = user.name;
            txtSurname.Text = user.surname;
            txtEmail.Text = user.email;
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            if ((txtName.Text == user.name) && (txtSurname.Text == user.surname)
                && (txtEmail.Text == user.email)) ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('No change has been made.');", true);
            else
            {
                user.name = txtName.Text;
                user.surname = txtSurname.Text;
                user.email = txtEmail.Text;
                user.UpdateUser();
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Update operation has been done successfuly.');", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('An error was occured while updating');", true);
        }
    }
}