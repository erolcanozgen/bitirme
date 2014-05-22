using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for MetaAnalysisDB
/// </summary>
public class MetaAnalysisDB
{
 
    public string disease_name;
    public string snp;
    public string p_value;
    public decimal or_value;
    public string I2;
    public int numOfPublications;


	public MetaAnalysisDB(string dsease_name, string snp, decimal or_val, string p_val,string i2,int numpub)
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
            + " (Disease_Name, SNP,OR_Value, P_Value,I2,NumOfPublications,Gene_Name)"
            + " VALUES('{0}','{1}','{2}','{3}','{4}',{5}, (select DISTINCT Gene_Name from {0} where SNP = '{1}') )"
            , this.disease_name, this.snp, this.or_value, this.p_value, this.I2, this.numOfPublications);
        MySqlCommand command = new MySqlCommand(query, newCon.conn);
        command.ExecuteNonQuery();
        newCon.conn.Close();
    }

    public void updateMetaAnalysis()
    {
        Connection newCon = new Connection();
        string query = String.Format("UPDATE metaanalysis SET"
            + " OR_Value='{0}', P_Value='{1}', I2='{2}', NumOfPublications='{3}' ,Gene_Name = (select DISTINCT Gene_Name from {4} where SNP = '{5}')"
            + " WHERE Disease_Name='{4}' AND SNP='{5}' "
            , this.or_value, this.p_value, this.I2, this.numOfPublications, this.disease_name, this.snp);
        MySqlCommand command = new MySqlCommand(query, newCon.conn);
        command.ExecuteNonQuery();
        newCon.conn.Close();
    }

    public static bool isMetaAnalysisDone(string diseaseName, string SNP)
    {
        Connection newCon = new Connection();
        string query = String.Format("select COUNT(*) from metaanalysis WHERE Disease_Name='{0}' AND SNP='{1}' ", diseaseName, SNP);
        MySqlCommand command = new MySqlCommand(query, newCon.conn);
        int count = int.Parse(command.ExecuteScalar().ToString());
        newCon.conn.Close();

        if (count > 0)
            return true;
        else
            return false;
    }

}