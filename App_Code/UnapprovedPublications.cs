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
    public decimal freq_control;
    public decimal freq_Patient;
    public string p_value;
    public decimal or_value;
    public string reference;
    public int reference_type;
    public int isApproved;
    public string ownerOfPublication;
    public string CI;

	public UnapprovedPublications()
	{
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
        this.isApproved = 0;
        this.ownerOfPublication = String.Empty;
        this.CI = String.Empty;
	}
    public void insertDiseaseDetails(UnapprovedPublications unAppPub)
    {
        Connection newCon = new Connection();
        string query = String.Format("INSERT INTO unapprovedpublications"
            + " (Case_Count, Control_Count, Disease_Name, Gene_Name, SNP, Frequency_Control, Frequency_Patient, P_Value, OR_Value ,Reference, isApproved, Reference_Type, ownerOfPublication,CI)"
            + " VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}')"
            ,unAppPub.case_count, unAppPub.control_count, unAppPub.disease_name, unAppPub.Gene_Name, unAppPub.snp, unAppPub.freq_control,
            unAppPub.freq_Patient, unAppPub.p_value, unAppPub.or_value, unAppPub.reference, this.isApproved, this.reference_type, ownerOfPublication,this.CI);
        MySqlCommand command = new MySqlCommand(query, newCon.conn);
        command.ExecuteNonQuery();
        newCon.conn.Close();
    }

    public DataTable getUnapprovedDiseaseDetails()
    {
        DataTable dt = new DataTable();
        DataTable dt2 = new DataTable();
       
            Connection newCon = new Connection();
            dt.Clear();        
            string query = String.Format("select ID,Disease_Name,Gene_Name,SNP,Case_Count, Control_Count, Frequency_Control, Frequency_Patient, P_Value, OR_Value, Reference,Reference_Type, ownerOfPublication,CI from unapprovedpublications where isApproved = {0} ", this.isApproved);
            MySqlCommand command = new MySqlCommand(query, newCon.conn);
            MySqlDataAdapter dr = new MySqlDataAdapter(command);
            dr.Fill(dt);
            newCon.conn.Close();

            dt2 = dt.Clone();
            dt2.Columns["P_Value"].DataType = System.Type.GetType("System.Double");

            foreach (DataRow row in dt.Rows)
            {
                dt2.ImportRow(row);
            }
        
        return dt2;
    }


    public void updateUnapprovedPub()
    {
        Connection newCon = new Connection();
        string query = String.Format("UPDATE unapprovedpublications"
            + " SET Case_Count = {0}, Control_Count = {1}, Disease_Name = '{2}', Gene_Name = '{3}', SNP ='{4}', Frequency_Control = {5}, Frequency_Patient ={6}, P_Value = '{7}', OR_Value = '{8}' ,Reference = '{9}', Reference_Type = {10}, CI = '{11}' Where id = '{12}' "
           , this.case_count, this.control_count, this.disease_name, this.Gene_Name, this.snp, this.freq_control,
             this.freq_Patient, this.p_value, this.or_value, this.reference, this.reference_type, this.CI,this.id);
        MySqlCommand command = new MySqlCommand(query, newCon.conn);
        command.ExecuteNonQuery();
        newCon.conn.Close();
    }

    public DataTable getOwnUnapprovedPub()
    {
        DataTable dt = new DataTable();
        DataTable dt2 = new DataTable();

        Connection newCon = new Connection();
        dt.Clear();
        string query = String.Format("select ID,Disease_Name,Gene_Name,SNP,Case_Count, Control_Count, Frequency_Control, Frequency_Patient, P_Value, OR_Value, Reference,Reference_Type,CI from unapprovedpublications where isApproved = 0 and ownerOfPublication = '{0}'",this.ownerOfPublication);
        MySqlCommand command = new MySqlCommand(query, newCon.conn);
        MySqlDataAdapter dr = new MySqlDataAdapter(command);
        dr.Fill(dt);
        newCon.conn.Close();

        dt2 = dt.Clone();
        dt2.Columns["P_Value"].DataType = System.Type.GetType("System.Double");

        foreach (DataRow row in dt.Rows)
        {
            dt2.ImportRow(row);
        }

        return dt2;
    }

    public void SetAsApproved()
    {
        Connection newCon = new Connection();

        string query = String.Format("UPDATE unapprovedpublications SET isApproved='1' WHERE ID={0}",this.id);
        MySqlCommand command = new MySqlCommand(query, newCon.conn);
        command.ExecuteNonQuery();

        newCon.conn.Close();
    }

    public void CreateNewDiseases()
    {
        Connection newCon = new Connection();
        string query = String.Format("CREATE  TABLE IF NOT EXISTS bitirmeprojesi.{0} ("
                                      +"ID INT(11) NOT NULL AUTO_INCREMENT ,"
                                      + "DiseaseId INT(11) NOT NULL ,"
                                      +"Disease_Name VARCHAR(200) NOT NULL ,"
                                      + "Case_Count INT(11) NOT NULL ,"
                                      + "Control_Count INT(11) NOT NULL ,"
                                      +"Gene_Name VARCHAR(100) NOT NULL ,"
                                      +"SNP VARCHAR(100) NOT NULL ,"
                                      +"Frequency_Control DECIMAL(11,10) NULL DEFAULT NULL ,"
                                      + "Frequency_Patient DECIMAL(11,10) NULL DEFAULT NULL ,"
                                      +"P_Value VARCHAR(45) NULL DEFAULT NULL ,"
                                      + "OR_Value DECIMAL(20,10) NULL DEFAULT NULL ,"
                                      +"Reference VARCHAR(2000) NOT NULL ,"
                                      + "Reference_Type INT(11) NOT NULL ,"
                                      + "CI VARCHAR(100) ,"
                                      +"ownerOfPublication VARCHAR(120) NULL DEFAULT NULL ,"
                                      +"PRIMARY KEY (ID) ,"
                                      +"CONSTRAINT {1}"
                                      +" FOREIGN KEY (DiseaseId )"
                                      +"  REFERENCES bitirmeprojesi.diseases (Id )"
                                      +"  ON DELETE CASCADE"
                                      +"  ON UPDATE CASCADE)",this.disease_name,String.Format("fk_{0}_Disease",this.disease_name) );
        MySqlCommand command = new MySqlCommand(query, newCon.conn);
        command.ExecuteNonQuery();
        query = String.Format("insert into diseases (Table_Name,Name) values('{0}','{1}')",this.disease_name, this.disease_name.Replace('_', ' '));
        command = new MySqlCommand(query, newCon.conn);
        command.ExecuteNonQuery();
    }

    private void insertToTable()
    {
        Connection newCon = new Connection();
        string query = String.Format("insert into {0} (DiseaseId, Disease_Name, Gene_Name, SNP, Case_Count, Control_Count, Frequency_Control, Frequency_Patient, P_Value, OR_Value ,Reference,Reference_Type, ownerOfPublication,CI) "
                                      + "select d.ID, Disease_Name,Gene_Name,SNP,Case_Count, Control_Count, Frequency_Control, Frequency_Patient, P_Value, OR_Value, Reference,Reference_Type, ownerOfPublication,CI from unapprovedpublications u,diseases d where d.Table_Name = u.Disease_Name and  u.ID = {1} ", this.disease_name, this.id);
        MySqlCommand command = new MySqlCommand(query, newCon.conn);
        command.ExecuteNonQuery();

        this.SetAsApproved();
        newCon.conn.Close();
    }

    public void deleteSelectedPublication()
    {
        Connection newCon = new Connection();

        string query = String.Format("DELETE FROM unapprovedpublications WHERE ID={0}", this.id);
        MySqlCommand command = new MySqlCommand(query, newCon.conn);
        command.ExecuteNonQuery();

        newCon.conn.Close();
    }

    public void approveSelectedPublication(bool isExist)
    {
       
        if (!isExist)
        {
            CreateNewDiseases();   

        }

        insertToTable();
        
    }
}