using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

/// <summary>
/// Summary description for UnapprovedPublications
/// </summary>
public class UnapprovedPublications
{
    public int id;
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
    public int reference_type;
    public int isApproved;
    public string ownerOfPublication;
	public UnapprovedPublications()
	{
        this.case_count = -1;
        this.control_count = -1;
        this.disease_name = String.Empty;
        this.Gene_Name = String.Empty;
        this.snp = String.Empty;
        this.freq_control = String.Empty;
        this.freq_Patient = String.Empty;
        this.p_value = String.Empty;
        this.or_value = String.Empty;
        this.reference = String.Empty;
        this.reference_type = -1;
        this.isApproved = 0;
        this.ownerOfPublication = String.Empty;
	}
    public void insertDiseaseDetails(UnapprovedPublications unAppPub)
    {
        Connection newCon = new Connection();
        string query = String.Format("INSERT INTO unapprovedpublications"
            + " (Case_Count, Control_Count, Disease_Name, Gene_Name, SNP, Frequency_Control, Frequency_Patient, P_Value, OR_Value ,Reference, isApproved, Reference_Type, ownerOfPublication)"
            + " VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}')"
            ,unAppPub.case_count, unAppPub.control_count, unAppPub.disease_name, unAppPub.Gene_Name, unAppPub.snp, unAppPub.freq_control,
            unAppPub.freq_Patient, unAppPub.p_value, unAppPub.or_value, unAppPub.reference, this.isApproved, this.reference_type, ownerOfPublication);
        MySqlCommand command = new MySqlCommand(query, newCon.conn);
        command.ExecuteNonQuery();
        newCon.conn.Close();
    }
    public DataTable selectUnapprovedDiseaseDetails()
    {
        DataTable dt = new DataTable();
        DataTable dt2 = new DataTable();
       
            Connection newCon = new Connection();
            dt.Clear();        
            string query = String.Format("select ID,Disease_Name,Gene_Name,SNP,Case_Count, Control_Count, Frequency_Control, Frequency_Patient, P_Value, OR_Value, Reference,Reference_Type, ownerOfPublication from unapprovedpublications where isApproved = {0} ", this.isApproved);
            MySqlCommand command = new MySqlCommand(query, newCon.conn);
            MySqlDataAdapter dr = new MySqlDataAdapter(command);
            dr.Fill(dt);
            newCon.conn.Close();

            dt2 = dt.Clone();
            dt2.Columns["Case_Count"].DataType = System.Type.GetType("System.Double");
            dt2.Columns["Control_Count"].DataType = System.Type.GetType("System.Double");
            dt2.Columns["Frequency_Patient"].DataType = System.Type.GetType("System.Double");
            dt2.Columns["Frequency_Control"].DataType = System.Type.GetType("System.Double");
            dt2.Columns["P_Value"].DataType = System.Type.GetType("System.Double");
            dt2.Columns["OR_Value"].DataType = System.Type.GetType("System.Double");

            foreach (DataRow row in dt.Rows)
            {
                dt2.ImportRow(row);
            }
        
        return dt2;
    }
}