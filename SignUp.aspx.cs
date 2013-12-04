using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MySql.Data.MySqlClient;

public partial class SignUp : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {

        Connection newCon = new Connection();
        string insertSql = "INSERT INTO Member (Name,Surname,Username,Password,Email,roleId) values (@FirstName,@LastName,@UserName,@Password,@Email,@RolId)";
                
        MySqlCommand cmd = new MySqlCommand();
        cmd.Connection = newCon.conn;
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = insertSql;

        MySqlParameter firstName = new MySqlParameter("@FirstName", MySqlDbType.VarChar, 120);
        firstName.Value = txtFirstName.Text.ToString();
        cmd.Parameters.Add(firstName);

        MySqlParameter lastName = new MySqlParameter("@LastName", MySqlDbType.VarChar, 120);
        lastName.Value = txtLastName.Text.ToString();
        cmd.Parameters.Add(lastName);

        MySqlParameter userName = new MySqlParameter("@UserName", MySqlDbType.VarChar, 50);
        userName.Value = txtUserName.Text.ToString();
        cmd.Parameters.Add(userName);

        MySqlParameter pwd = new MySqlParameter("@Password", MySqlDbType.VarChar, 50);
        pwd.Value = txtPwd.Text.ToString();
        cmd.Parameters.Add(pwd);

        MySqlParameter email = new MySqlParameter("@Email", MySqlDbType.VarChar, 50);
        email.Value = txtEmailID.Text.ToString();
        cmd.Parameters.Add(email);

        MySqlParameter rolId = new MySqlParameter("@RolId", MySqlDbType.Int32);
        rolId.Value = 2;
        cmd.Parameters.Add(rolId);

        try
        {
            cmd.ExecuteNonQuery();
            lblMsg.Text = "User Registration successful";
            ClearControls(Page);
        }
        catch (MySqlException ex)
        {
            string errorMessage = "Error in registring user";
            errorMessage += ex.Message;
            throw new Exception(errorMessage);

        }
        finally
        {
            newCon.conn.Close();
        }
    }

    public static void ClearControls(Control Parent)
    {
        foreach (Control c in Parent.Controls)
            ClearControls(c);
    }
}