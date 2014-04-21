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
    public int[] id;
    public string[] names;
    public string[] tableNames;

	public DiseasesNames()
	{
	
	}
    public DataTable selectDiseasesNames()
    {
        try
        {
            Connection newCon = new Connection();
            string query = "select id,Name,Table_Name from diseases";
            MySqlCommand command = new MySqlCommand(query, newCon.conn);
            MySqlDataAdapter dr = new MySqlDataAdapter(command);
            DataTable dt = new DataTable();
            dt.Clear();
            dr.Fill(dt);
            newCon.conn.Close();


            id = dt.AsEnumerable().Select(row => row.Field<int>("id")).ToArray();
            names = dt.AsEnumerable().Select(row => row.Field<string>("Name")).ToArray();
            tableNames = dt.AsEnumerable().Select(row => row.Field<string>("Table_Name")).ToArray();

            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public bool isDiseaseTableExist(string tableName)
    {
        bool ret = false;
        Connection newCon = new Connection();
        string query = String.Format("select count(*) from diseases where Table_Name= '{0}'", tableName);
        MySqlCommand command = new MySqlCommand(query, newCon.conn);
        int count = int.Parse(command.ExecuteScalar().ToString());
        if (count > 0)
            ret = true;
        newCon.conn.Close();

        return ret;
    }
}