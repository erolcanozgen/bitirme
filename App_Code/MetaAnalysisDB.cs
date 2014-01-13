using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for MetaAnalysisDB
/// </summary>
public class MetaAnalysisDB
{
 
    public string disease_name;
    public string snp;
    public string p_value;
    public string or_value;
    public string I2;
    public int numOfPublications;


	public MetaAnalysisDB(string dsease_name, string snp, string or_val, string p_val,string i2,int numpub)
	{
        this.disease_name = dsease_name;
        this.snp = snp;
        this.p_value = p_val;
        this.or_value = or_val;
        this.I2 = i2;
        this.numOfPublications = numpub;
	}

    
    public void insertMetaAnalysis()
    {
        Connection newCon = new Connection();
        string query = String.Format("INSERT INTO metaanalysis"
            + " (Disease_Name, SNP,OR_Value, P_Value,I2,NumOfPublications)"
            + " VALUES('{0}','{1}','{2}','{3}','{4}',{5})"
            , this.disease_name, this.snp, this.or_value, this.p_value, this.I2, this.numOfPublications);
        MySqlCommand command = new MySqlCommand(query, newCon.conn);
        command.ExecuteNonQuery();
        newCon.conn.Close();
    }

}