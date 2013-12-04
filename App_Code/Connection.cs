using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Configuration;

/// <summary>
/// Summary description for Connection
/// </summary>
public class Connection
{
    public MySqlConnection conn;

	public Connection()
	{
		try
        {
            conn = new MySql.Data.MySqlClient.MySqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
            conn.Open();
        }
        catch (MySql.Data.MySqlClient.MySqlException ex)
        {
            
        }
	}
}