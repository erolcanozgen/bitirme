using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MySql.Data.MySqlClient;

/// <summary>
/// Summary description for DiseaseDetails
/// </summary>
public class DiseaseDetails
{
    public int[] id;
    public int diseaseId;
    public int case_count;
    public int control_count;
    public string disease_name;
    public string Gene_Name;
    public string snp;
    public decimal freq_control;
    public decimal freq_Patient;
    public string p_value;
    public decimal or_value;
    public string reference;
    public int reference_type;
    public string ownerOfPublication;


    public DiseaseDetails()
	{
        this.diseaseId = -1;
        this.case_count = -1;
        this.control_count = -1;
        this.disease_name = String.Empty;
        this.Gene_Name = String.Empty;
        this.snp = String.Empty;
        this.freq_control = 0;
        this.freq_Patient = 0;
        this.p_value = String.Empty;
        this.or_value = 0;
        this.reference = String.Empty;
        this.reference_type = -1;
        this.ownerOfPublication = String.Empty;
	}

    public DataTable selectDiseaseDetails(string name)
    {
        Connection newCon = new Connection();
        string query = String.Format("select Id,DiseaseId, Case_Count, Control_Count, Disease_Name, Gene_Name, SNP, Frequency_Control, Frequency_Patient, P_Value, OR_Value ,Reference,Reference_Type, ownerOfPublication,CI from {0}", name);
        MySqlCommand command = new MySqlCommand(query, newCon.conn);
        MySqlDataAdapter dr = new MySqlDataAdapter(command);
        DataTable dt = new DataTable();
        dt.Clear();
        dr.Fill(dt);
        newCon.conn.Close();
        return dt;
    }
    public DataTable selectMetaAnalysis(string diseaseName)
    {
        Connection newCon = new Connection();
        string query = String.Format("select  SNP, OR_Value, P_Value, I2, NumOfPublications from MetaAnalysis where Disease_Name = '{0}' ", diseaseName);
        MySqlCommand command = new MySqlCommand(query, newCon.conn);
        MySqlDataAdapter dr = new MySqlDataAdapter(command);
        DataTable dt = new DataTable();
        DataTable dt2 = new DataTable();
        dt.Clear();
        dr.Fill(dt);
        newCon.conn.Close();

        dt2 = dt.Clone();
        dt2.Columns["P_Value"].DataType = System.Type.GetType("System.Double");
        dt2.Columns["OR_Value"].DataType = System.Type.GetType("System.Double");
        dt2.Columns["I2"].DataType = System.Type.GetType("System.Double");

        int tempId = 1;
        System.Data.DataColumn newColumn = new System.Data.DataColumn("tmpId", typeof(System.String));
        dt2.Columns.Add(newColumn);
        newColumn.DefaultValue = tempId++.ToString();
        foreach (DataRow row in dt.Rows)
        {
            dt2.ImportRow(row);
            newColumn.DefaultValue = tempId++.ToString(); //datatable'a gecici bir id değeri atanıyor, her snp icin (divexpandcollapse icin)
        }
        return dt2;
    }
    public DataTable selectAllGenes(string[] diseaseName)
    {
        Connection newCon = new Connection();
        DataTable dt = new DataTable();
        dt.Clear();
        for (int i = 0; i < diseaseName.Length; i++)
        {
            string query = String.Format("select DISTINCT Gene_Name from {0} ", diseaseName[i]);
            MySqlCommand command = new MySqlCommand(query, newCon.conn);
            MySqlDataAdapter dr = new MySqlDataAdapter(command);
            dr.Fill(dt);
        }
        newCon.conn.Close();
        return dt;
    }
    public DataTable selectAllSNPs(string[] diseaseName)
    {
        Connection newCon = new Connection();
        DataTable dt = new DataTable();
        dt.Clear();
        for (int i = 0; i < diseaseName.Length; i++)
        {
            string query = String.Format("select DISTINCT SNP from {0} ", diseaseName[i]);
            MySqlCommand command = new MySqlCommand(query, newCon.conn);
            MySqlDataAdapter dr = new MySqlDataAdapter(command);
            dr.Fill(dt);
        }
        newCon.conn.Close();
        return dt;   
    }
    public DataTable selectSNPDetails(string diseaseName, string SnpName, string tempId)
    {
        Connection newCon = new Connection();
        string query = String.Format("select  Gene_Name,Case_Count, Control_Count, Frequency_Control, Frequency_Patient, P_Value, OR_Value ,Reference,Reference_Type, SNP,CI from {0} where SNP = '{1}'", diseaseName, SnpName);
        MySqlCommand command = new MySqlCommand(query, newCon.conn);
        MySqlDataAdapter dr = new MySqlDataAdapter(command);
        DataTable dt = new DataTable();
        dt.Clear();
        dr.Fill(dt);
        newCon.conn.Close();

        return dt;
    }
    public DataTable SelectWithSnp(string disease_name,string SNP)
    {
        DataTable ret = new DataTable();
        Connection newCon = new Connection();
        string query = String.Format("select Id,DiseaseId, Case_Count, Control_Count, Disease_Name, Gene_Name, SNP, Frequency_Control, Frequency_Patient, P_Value, OR_Value ,Reference from {0} ", disease_name); 
        query += "WHERE SNP = @SNP";
        MySqlCommand command = new MySqlCommand(query, newCon.conn);
        command.Parameters.AddWithValue("@SNP", SNP);
        MySqlDataAdapter dr = new MySqlDataAdapter(command);
        ret.Clear();
        dr.Fill(ret);
        newCon.conn.Close();
        return ret;
    }
    public DataTable SelectFilteredDiseaseDetails(List<KeyValuePair<string, string>> param, string name)
    {
        try{
         DataTable ret = new DataTable();
        Connection newCon = new Connection();
        string query = String.Format("select Id,DiseaseId, Case_Count, Control_Count, Disease_Name, Gene_Name, SNP, Frequency_Control, Frequency_Patient, P_Value, OR_Value ,Reference,Reference_Type, ownerOfPublication,CI from {0} ", name);
        query = query+GetWhereCondition(param);
        MySqlCommand command = new MySqlCommand(query, newCon.conn);
        MySqlDataAdapter dr = new MySqlDataAdapter(command);
        ret.Clear();
        dr.Fill(ret);
        newCon.conn.Close();
        return ret;
        } 
   
        catch(Exception ex)
        {
            throw(ex);
        }     
    }
    public string GetWhereCondition(List<KeyValuePair<string, string>> param){
        string whereCondition = String.Empty;

        try
        {
            whereCondition = "WHERE "; 
            if (param.Count == 0) return whereCondition;

           
            for (int i = 0; i < param.Count; i++)
            {
                if (param[i].Key == "Case_Count+Control_Count")
                {
                    whereCondition = whereCondition + "AND " + param[i].Key + " >= " + param[i].Value + " ";
                }
                else
                whereCondition = whereCondition + "AND " + param[i].Key + " LIKE " + "'%" + param[i].Value +"%' ";
            }

            return whereCondition;
        }

        catch(Exception ex)
        {
            throw(ex);
        }
    }

    public DataTable GetSnpList()
    {
        try
        {
            DataTable ret = new DataTable();
            Connection newCon = new Connection();
            string query = String.Format("select distinct SNP from {0} ", this.disease_name);
            MySqlCommand command = new MySqlCommand(query, newCon.conn);
            MySqlDataAdapter dr = new MySqlDataAdapter(command);
            ret.Clear();
            dr.Fill(ret);
            newCon.conn.Close();
            return ret;
        }

        catch (Exception ex)
        {
            throw (ex);
        }
    }

}