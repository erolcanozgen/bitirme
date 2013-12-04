using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;

/// <summary>
/// Summary description for DiseasesNames
/// </summary>
public class DiseasesNames
{
    public int id;
    public string name;

	public DiseasesNames()
	{
	
	}
    public DataTable selectDiseasesNames()
    {
        Connection newCon = new Connection();
        string query = "select id,Name,Table_Name from diseases";
        MySqlCommand command = new MySqlCommand(query, newCon.conn);
        MySqlDataAdapter dr = new MySqlDataAdapter(command);
        DataTable dt = new DataTable();
        dt.Clear();
        dr.Fill(dt);
        newCon.conn.Close();

        return dt;
    }
}