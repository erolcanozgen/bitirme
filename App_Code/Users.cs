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
    public int rolId;
	public Users()
	{
        this.id = -1;
        this.rolId = -1;
        this.username = String.Empty;
        this.name = String.Empty;
        this.surname = String.Empty;
        this.email = String.Empty;
	}

    public bool checkUser(string username, string password)
    {
        Connection newCon = new Connection();
        string query = "select id,Username,Name,Surname,Email,roleId from member where Username = @usrname and Password = @paswrd";
        MySqlCommand command = new MySqlCommand(query, newCon.conn);
        command.Parameters.AddWithValue("@usrname", username);
        command.Parameters.AddWithValue("@paswrd", password);
        MySqlDataAdapter dr = new MySqlDataAdapter(command);
        DataTable dt = new DataTable();
        dt.Clear();
        dr.Fill(dt);

        if (dt.Rows.Count == 0) return false;
        else
        {
            this.username = username;
            this.id = Convert.ToInt32(dt.Rows[0].ItemArray[0]);
            this.name = dt.Rows[0].ItemArray[2].ToString();
            this.surname = dt.Rows[0].ItemArray[3].ToString();
            this.email = dt.Rows[0].ItemArray[4].ToString() ;
            this.rolId = Convert.ToInt32(dt.Rows[0].ItemArray[5]);
            return true;
            
        }




        newCon.conn.Close();

    }
 
}