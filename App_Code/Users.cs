using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Users
/// </summary>
public class Users
{
    public int id;
    public string username;
    public string name;
    public string surname;
    public string email;
    public string passwd;
    public int rolId;
	public Users()
	{
        this.id = -1;
        this.rolId = -1;
        this.username = String.Empty;
        this.name = String.Empty;
        this.surname = String.Empty;
        this.email = String.Empty;
        this.passwd = String.Empty;
	}

    public Users(string usrname, string name, string surname, string email, string passwd, int rolId)
    {
        this.username = usrname;
        this.name = name;
        this.surname = surname;
        this.email = email;
        this.passwd = passwd;
        this.rolId = rolId;
    }

    public bool checkUser(string username, string password)
    {
        Connection newCon = new Connection();
        string query = "select id,Username,Name,Surname,Email,roleId,Password from member where Username = @usrname and Password = @paswrd";
        MySqlCommand command = new MySqlCommand(query, newCon.conn);
        command.Parameters.AddWithValue("@usrname", username);
        command.Parameters.AddWithValue("@paswrd", password);
        MySqlDataAdapter dr = new MySqlDataAdapter(command);
        DataTable dt = new DataTable();
        dt.Clear();
        dr.Fill(dt);

        if (dt.Rows.Count == 0)
        {
            newCon.conn.Close();
            return false;
        }
        else
        {
            this.username = username;
            this.id = Convert.ToInt32(dt.Rows[0]["id"].ToString());
            this.name = dt.Rows[0]["Name"].ToString();
            this.surname = dt.Rows[0]["Surname"].ToString();
            this.email = dt.Rows[0]["Email"].ToString();
            this.rolId = Convert.ToInt32(dt.Rows[0]["roleId"]);
            this.passwd = dt.Rows[0]["Password"].ToString();
            
            newCon.conn.Close();
            return true;
        }
        
    }

    public void AddUser()
    {

        try
        {
        Connection newCon = new Connection();
        string insertSql = "INSERT INTO Member (Username,Name,Surname,Email,roleId,Password) values (@Username,@Name,@Surname,@Email,@RolId,@Password)";

        MySqlCommand cmd = new MySqlCommand();
        cmd.Connection = newCon.conn;
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = insertSql;


        MySqlParameter userName = new MySqlParameter("@Username", MySqlDbType.VarChar, 50);
        userName.Value = this.username;
        cmd.Parameters.Add(userName);

        MySqlParameter firstName = new MySqlParameter("@Name", MySqlDbType.VarChar, 120);
        firstName.Value = this.name;
        cmd.Parameters.Add(firstName);

        MySqlParameter lastName = new MySqlParameter("@Surname", MySqlDbType.VarChar, 120);
        lastName.Value = this.surname;
        cmd.Parameters.Add(lastName);


        MySqlParameter email = new MySqlParameter("@Email", MySqlDbType.VarChar, 50);
        email.Value = this.email;
        cmd.Parameters.Add(email);

        MySqlParameter pwd = new MySqlParameter("@Password", MySqlDbType.VarChar, 50);
        pwd.Value = this.passwd;
        cmd.Parameters.Add(pwd);

        MySqlParameter rolId = new MySqlParameter("@RolId", MySqlDbType.Int32);
        rolId.Value = 2 ;
        cmd.Parameters.Add(rolId);

        cmd.ExecuteNonQuery();
        newCon.conn.Close();
        }
        catch (MySqlException ex)
        {
            string errorMessage = "Error in registring user";
            errorMessage += ex.Message;
            throw new Exception(errorMessage);

        }
   
    }

    public DataTable getUsers() 
    {
        Connection newCon = new Connection();
        string query = "select id,Username,Name,Surname,Email,roleId from member where roleId = 2";
        MySqlCommand command = new MySqlCommand(query, newCon.conn);
        MySqlDataAdapter dr = new MySqlDataAdapter(command);
        DataTable dt = new DataTable();
        dt.Clear();
        dr.Fill(dt);
    
        newCon.conn.Close();
        return dt;
    }

    public void UpdateUser()
    {
        try
        {
            Connection newCon = new Connection();
            string query = "UPDATE member SET Name = @Name ,Surname = @SurName, Email = @Email where id = @id";
            MySqlCommand command = new MySqlCommand(query, newCon.conn);
            command.Parameters.Add("@Name", this.name);
            command.Parameters.Add("@SurName", this.surname);
            command.Parameters.Add("@Email", this.email);
            command.Parameters.Add("@id", this.id);
            command.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    public void ChangePassword()
    {
        try
        {
            Connection newCon = new Connection();
            string query = "UPDATE member SET Password = @Pwd where id = @id";
            MySqlCommand command = new MySqlCommand(query, newCon.conn);
            command.Parameters.Add("@Pwd", this.passwd);
            command.Parameters.Add("@id", this.id);
            command.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable getOwnPublication()
    {
        DataTable dt = new DataTable();
        Connection newCon = new Connection();
        MySqlCommand command = new MySqlCommand("getOwnPublications", newCon.conn);
        MySqlDataAdapter dr = new MySqlDataAdapter(command);
        command.CommandType = System.Data.CommandType.StoredProcedure;
        command.Parameters.AddWithValue("?ownr", (object)this.username);
        dt.Clear();
        dr.Fill(dt);  
        newCon.conn.Close();
        return dt;
    }

    public void deleteUser()
    {
        Connection newCon = new Connection();
        string query = String.Format("delete from member where Id = {0}", this.id);
        MySqlCommand command = new MySqlCommand(query, newCon.conn);
        command.ExecuteNonQuery();
        newCon.conn.Close();
    }

}