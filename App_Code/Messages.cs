using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

/// <summary>
/// Summary description for Messages
/// </summary>
public class Messages
{
    public int[] id;
    public string[] name;
    public string[] email;
    public string[] subject;
    public string[] message;
    public DateTime[] messageDate;

	public Messages()
	{
         this.id = null;
         this.name = null;
         this.email = null;
         this.subject = null;
         this.message = null;
         this.messageDate = null;
	
	}
    public DataTable selectComingMessages()
    {
        Connection newCon = new Connection();
        string query = "select id, name, email, subject, message, messageDate from messages";
        MySqlCommand command = new MySqlCommand(query, newCon.conn);
        MySqlDataAdapter dr = new MySqlDataAdapter(command);
        DataTable dt = new DataTable();
        dt.Clear();
        dr.Fill(dt);
        newCon.conn.Close();


        id = dt.AsEnumerable().Select(row => row.Field<int>("id")).ToArray();
        name = dt.AsEnumerable().Select(row => row.Field<string>("name")).ToArray();
        email = dt.AsEnumerable().Select(row => row.Field<string>("email")).ToArray();
        subject = dt.AsEnumerable().Select(row => row.Field<string>("subject")).ToArray();
        message = dt.AsEnumerable().Select(row => row.Field<string>("message")).ToArray();
        messageDate = dt.AsEnumerable().Select(row => row.Field<DateTime>("messageDate")).ToArray();

        return dt;
    }
}