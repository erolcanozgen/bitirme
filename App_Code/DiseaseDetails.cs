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
    public string freq_control;
    public string freq_Patient;
    public string p_value;
    public string or_value;
    public string reference;
    public bool isApproved;


    public DiseaseDetails()
	{
        this.diseaseId = -1;
        this.case_count = -1;
        this.control_count = -1;
        this.disease_name = String.Empty;
        this.Gene_Name = String.Empty;
        this.snp = String.Empty;
        this.p_value = String.Empty;
        this.or_value = String.Empty;
        this.reference = String.Empty;
	}

    public DataTable selectDiseaseDetails(string name)
    {
        Connection newCon = new Connection();
        string query = String.Format("select Id,DiseaseId, Case_Count, Control_Count, Disease_Name, Gene_Name, SNP, Frequency_Control, Frequency_Patient, P_Value, OR_Value ,Reference, isApproved from {0} ", name);
        MySqlCommand command = new MySqlCommand(query, newCon.conn);
        MySqlDataAdapter dr = new MySqlDataAdapter(command);
        DataTable dt = new DataTable();
        dt.Clear();
        dr.Fill(dt);
        newCon.conn.Close();
        return dt;
    }
    public void insertDiseaseDetails(DiseaseDetails dDetails)
    {
        Connection newCon = new Connection();
        string query = String.Format("INSERT INTO {0}" 
            +" (DiseaseId, Case_Count, Control_Count, Disease_Name, Gene_Name, SNP, Frequency_Control, Frequency_Patient, P_Value, OR_Value ,Reference, isApproved)"
            + " VALUES('{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}')"
            , dDetails.disease_name, 2 ,dDetails.case_count, dDetails.control_count, dDetails.disease_name, dDetails.Gene_Name, dDetails.snp, dDetails.freq_control,
            dDetails.freq_Patient, dDetails.p_value, dDetails.or_value, dDetails.reference, 0);
        MySqlCommand command = new MySqlCommand(query, newCon.conn);
        command.ExecuteNonQuery();
        newCon.conn.Close();
    }
    public DataTable selectMetaAnalysis(string diseaseName)
    {
        Connection newCon = new Connection();
        string query = String.Format("select  SNP, OR_Value, P_Value, I2, NumOfPublications from MetaAnalysis where Disease_Name = '{0}' ", diseaseName);
        MySqlCommand command = new MySqlCommand(query, newCon.conn);
        MySqlDataAdapter dr = new MySqlDataAdapter(command);
        DataTable dt = new DataTable();
        dt.Clear();
        dr.Fill(dt);
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
    public DataTable selectUnapprovedDiseaseDetails(string[] diseaseName)
    {
        DataTable dt = new DataTable();
        if (diseaseName.Length > 0)
        {
            Connection newCon = new Connection();
            dt.Clear();
            for (int i = 0; i < diseaseName.Length; i++)
            {
                string query = String.Format("select  ID,Disease_Name,Gene_Name,SNP,Case_Count, Control_Count, Frequency_Control, Frequency_Patient, P_Value, OR_Value ,Reference from {0} where isApproved = 0 ", diseaseName[i]);
                MySqlCommand command = new MySqlCommand(query, newCon.conn);
                MySqlDataAdapter dr = new MySqlDataAdapter(command);
                dr.Fill(dt);
            }
            newCon.conn.Close();
        }
        return dt;
    }
    public DataTable selectSNPDetails(string diseaseName, string SnpName)
    {
        Connection newCon = new Connection();
        string query = String.Format("select  Gene_Name,Case_Count, Control_Count, Frequency_Control, Frequency_Patient, P_Value, OR_Value ,Reference, SNP from {0} where SNP = '{1}' and isApproved = 1 ", diseaseName, SnpName);
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
        query += "WHERE SNP = @SNP and isApproved = 1";
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
        string query = String.Format("select Id,DiseaseId, Case_Count, Control_Count, Disease_Name, Gene_Name, SNP, Frequency_Control, Frequency_Patient, P_Value, OR_Value ,Reference from {0} ", name);
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
            whereCondition = "WHERE isApproved = 1 "; 
            if (param.Count == 0) return whereCondition;

           
            for (int i = 0; i < param.Count; i++)
            {
                if (param[i].Key == "Control_Count" || param[i].Key == "Case_Count")
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



    public void approveSelectedPublication(List<int> diseaseId, List<string> diseaseName)
    {
        Connection newCon = new Connection();
        try
        {
            for (int i = 0; i < diseaseName.Count; i++)
            {
                string query = String.Format("UPDATE {0} SET isApproved='1' WHERE ID={1}", diseaseName[i], diseaseId[i]);
                MySqlCommand command = new MySqlCommand(query, newCon.conn);
                command.ExecuteNonQuery();
            }
        }
        catch (Exception ex)
        {
            Alert.Show("An error occured!Please try again later.");
        }
        newCon.conn.Close();
    }
}