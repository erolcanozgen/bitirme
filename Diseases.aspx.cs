﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Diseases : System.Web.UI.Page
{
    DiseasesNames dName = new DiseasesNames();
    DiseaseDetails dDetails = new DiseaseDetails();

    protected void Page_Load(object sender, EventArgs e)
    {
       
        try
        {

            if (!IsPostBack)
            {
                cmbDiseaseName.DataTextField = "Name";
                cmbDiseaseName.DataValueField = "Table_Name";
                cmbDiseaseName.DataSource = dName.selectDiseasesNames();
                cmbDiseaseName.DataBind();
                cmbDiseaseName.SelectedValue = Request.QueryString["disease"];

                
                DataTable dt = dDetails.selectDiseaseDetails(cmbDiseaseName.SelectedValue);
                grdViewDiseases.DataSource = dt;
                grdViewDiseases.DataBind();
                setReferenceColumn(dt);       
            }
        }

        catch(Exception ex)
        {
            Notifier.AddErrorMessage("An error was occured while getting the researches!");
            //Alert.Show(ex.Message);
        }
    }  

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        try
        {
            List<KeyValuePair<string, string>> param = new List<KeyValuePair<string, string>>();
            
            if(txtGene.Text != String.Empty) param.Add(new KeyValuePair<string,string>("Gene_Name",txtGene.Text));

            if (txtCaseCount.Text != String.Empty) param.Add(new KeyValuePair<string, string>("Case_Count", txtCaseCount.Text));

            if (txtCotrolCount.Text != String.Empty) param.Add(new KeyValuePair<string, string>("Control_Count", txtCotrolCount.Text));

            if (txtSNP.Text != String.Empty) param.Add(new KeyValuePair<string, string>("SNP", txtSNP.Text));


            DataTable dt = dDetails.SelectFilteredDiseaseDetails(param, cmbDiseaseName.SelectedValue.ToString());
            grdViewDiseases.DataSource = dt;
            grdViewDiseases.DataBind();
            setReferenceColumn(dt);

           

        }
        catch (Exception ex)
        {
            Notifier.AddErrorMessage("An error was occured while filtering the researches!");
            //Alert.Show(ex.Message);
        }
    }


    private void setReferenceColumn(DataTable dt)
    {
        HyperLink link_ref; Label lbl_ref; LinkButton seeDetailsBtn;

        for (int i = 0; i < grdViewDiseases.Rows.Count; i++)
            switch (dt.Rows[i].ItemArray[12].ToString())
            {
                case "1":
                    link_ref = grdViewDiseases.Rows[i].FindControl("Link") as HyperLink;
                    link_ref.Text = "External Links";
                    link_ref.NavigateUrl = String.Format("{0}/{1}", ConfigurationManager.AppSettings["PubmedLink"], dt.Rows[i].ItemArray[11]); // ItemArray[11] <-- Reference  alanı
                    link_ref.Target = "_blank";
                    seeDetailsBtn = grdViewDiseases.Rows[i].FindControl("seeDetailsBtn") as LinkButton;
                    seeDetailsBtn.Visible = false;
                    break;

                case "2":
                    link_ref = grdViewDiseases.Rows[i].FindControl("Link") as HyperLink;
                    link_ref.Text = "External Links";
                    link_ref.Target = "_blank";
                    link_ref.NavigateUrl = dt.Rows[i].ItemArray[11].ToString();
                    seeDetailsBtn = grdViewDiseases.Rows[i].FindControl("seeDetailsBtn") as LinkButton;
                    seeDetailsBtn.Visible = false;
                    break;

                case "3":
                default:
                    link_ref = grdViewDiseases.Rows[i].FindControl("Link") as HyperLink;
                    link_ref.Visible = false;
                    lbl_ref = grdViewDiseases.Rows[i].FindControl("lbl_reference") as Label;
                    lbl_ref.Text = dt.Rows[i].ItemArray[11].ToString();
                    break;
            }
    }
    public void ShowPopup(object sender, EventArgs e)
    {
        Label lbl_ref;
        LinkButton btn = (LinkButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        int i = Convert.ToInt32(row.RowIndex);

        lbl_ref = grdViewDiseases.Rows[i].FindControl("lbl_reference") as Label;
        referenceLbl.Text = lbl_ref.Text;
        
        this.Button1_ModalPopupExtender.Show();
    }

}