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
    public string author;
	
    public DiseaseDetails()
	{
        this.diseaseId = -1;
        this.case_count = -1;
        this.control_count = -1;
        this.disease_name = String.Empty;
        this.Gene_Name = String.Empty;
        this.snp = String.Empty;
        this.freq_control = String.Empty;
        this.freq_Patient = String.Empty;
        this.p_value = String.Empty;
        this.or_value = String.Empty;
        this.author = String.Empty;
	}
    public DataTable selectDiseaseDetails(string name)
    {
        Connection newCon = new Connection();
        string query = String.Format("select Id,DiseaseId, Case_Count, Control_Count, Disease_Name, Gene_Name, SNP, Frequency_Control, Frequency_Patient, P_Value, OR_Value ,Reference from {0} ", name);
        MySqlCommand command = new MySqlCommand(query, newCon.conn);
        MySqlDataAdapter dr = new MySqlDataAdapter(command);
        DataTable dt = new DataTable();
        dt.Clear();
        dr.Fill(dt);
        newCon.conn.Close();
        return dt;
    }

}