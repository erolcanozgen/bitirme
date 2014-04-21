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
                                      +"Frequency_Control VARCHAR(200) NULL DEFAULT NULL ,"
                                      +"Frequency_Patient VARCHAR(200) NULL DEFAULT NULL ,"
                                      +"P_Value VARCHAR(45) NULL DEFAULT NULL ,"
                                      +"OR_Value VARCHAR(45) NULL DEFAULT NULL ,"
                                      +"Reference VARCHAR(2000) NOT NULL ,"
                                      + "isApproved INT(1) NOT NULL ,"
                                      + "Reference_Type INT(11) NOT NULL ,"
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
        string query = String.Format("insert into {0} (DiseaseId, Disease_Name, Gene_Name, SNP, Case_Count, Control_Count, Frequency_Control, Frequency_Patient, P_Value, OR_Value ,Reference, isApproved,Reference_Type, ownerOfPublication) "
                                      + "select d.ID, Disease_Name,Gene_Name,SNP,Case_Count, Control_Count, Frequency_Control, Frequency_Patient, P_Value, OR_Value, Reference,1,Reference_Type, ownerOfPublication from unapprovedpublications u,diseases d where d.Table_Name = u.Disease_Name and  u.ID = {1} ", this.disease_name, this.id);
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