﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class UtilityCalculators : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
 
    protected void btnCalculate_Click(object sender, EventArgs e)
    {

        try
        {
            Double or_value = 0.0f; Double ln_or = 0.0f; Double or_variance = 0.0f;
            Double rr_value = 0.0f; Double rr_variance = 0.0f; Double ln_rr = 0.0f; Double p_value = 0.0f;
           
    
            #region or_calculation
            or_value =  Utility.CalculateOrValue(Convert.ToInt32(txtCaseYes.Text), Convert.ToInt32(txtCaseNo.Text),
                                                             Convert.ToInt32(txtControlYes.Text), Convert.ToInt32(txtControlNo.Text));
            txt_or_value.Text = or_value.ToString("0.0000");

            ln_or = (Math.Log(Convert.ToDouble(txt_or_value.Text)));
            txt_ln_or.Text = ln_or.ToString("0.0000");

            or_variance = Utility.CalculateOrVariance(Convert.ToInt32(txtCaseYes.Text), Convert.ToInt32(txtCaseNo.Text),
                            
                             Convert.ToInt32(txtControlYes.Text), Convert.ToInt32(txtControlNo.Text));


            

            txt_or_variance.Text = or_variance.ToString("0.0000");
            txt_CI_or.Text = (Math.Exp( ln_or - (1.96 * Math.Sqrt(or_variance)))).ToString("0.0000")
                              + "  -  "
                              + (Math.Exp( ln_or + (1.96 * Math.Sqrt(or_variance)))).ToString("0.0000");

            txt_chi_square_without_Yates.Text = Utility.CalculateChiSquareWithoutYates(Convert.ToInt32(txtCaseYes.Text), Convert.ToInt32(txtCaseNo.Text),
                                                             Convert.ToInt32(txtControlYes.Text), Convert.ToInt32(txtControlNo.Text)).ToString("0.0000");

            txt_chi_square_with_Yates.Text = Utility.CalculateChiSquareWithYates(Convert.ToInt32(txtCaseYes.Text), Convert.ToInt32(txtCaseNo.Text),
                                                             Convert.ToInt32(txtControlYes.Text), Convert.ToInt32(txtControlNo.Text)).ToString("0.0000");


            #endregion

            #region rr_calculation

            rr_value =  Utility.CalculateRRValue(Convert.ToInt32(txtCaseYes.Text), Convert.ToInt32(txtCaseNo.Text),
                                                          Convert.ToInt32(txtControlYes.Text), Convert.ToInt32(txtControlNo.Text));
            txt_rr_value.Text = rr_value.ToString("0.0000");
            rr_variance = Utility.CalculateRrVariance(Convert.ToInt32(txtCaseYes.Text), (Convert.ToInt32(txtCaseNo.Text) + Convert.ToInt32(txtCaseYes.Text)),
                                                       Convert.ToInt32(txtControlYes.Text), (Convert.ToInt32(txtControlNo.Text) + Convert.ToInt32(txtControlYes.Text)));
            txt_rr_variance.Text = rr_variance.ToString("0.0000");

            ln_rr = (Math.Log(Convert.ToDouble(txt_rr_value.Text)));
            txt_ln_rr.Text = ln_rr.ToString("0.0000");

            txt_CI_rr.Text = (Math.Exp(ln_rr - (1.96 * Math.Sqrt(rr_variance)))).ToString("0.0000")
                               + "  -  "
                               + (Math.Exp(ln_rr + (1.96 * Math.Sqrt(rr_variance)))).ToString("0.0000");
            #endregion


            p_value = Utility.CalculatePValue(Convert.ToInt32(txtCaseYes.Text), Convert.ToInt32(txtCaseNo.Text),
                                                       Convert.ToInt32(txtControlYes.Text), Convert.ToInt32(txtControlNo.Text));
            double tmp = p_value; string format ="0.";
            while (tmp < 1 || format.Length<6)
            {
                format += "0";
                tmp *= 10;
            }
            txtpValue.Text = p_value.ToString(format);
            
            setTextBoxLength(results_panel.Controls[0]);
           
        }

        catch (Exception ex)
        {
            Alert.Show(ex.Message);
        }
    
    }

    private void setTextBoxLength(Control ctrl)
    {
        
        foreach (Control s in ctrl.Controls)
        {
            if (s is TextBox)
            {
                int size = 7;
                int finalWidth = size * ((TextBox)s).Text.Length;
                ((TextBox)s).Width = new Unit(finalWidth);
            }
        }
    }

    protected void postHocPowerBtn1_Click(object sender, EventArgs e)
    {
        try
        {
            double phPower = 0;
            phPower = Utility.CalculatePostHocPower(Convert.ToInt32(postHocPowerTxt1.Text), Convert.ToInt32(postHocPowerTxt2.Text),
                                                          Convert.ToInt32(postHocPowerTxt3.Text), Convert.ToInt32(postHocPowerTxt4.Text), Convert.ToDouble(postHocPowerTxt5.Text));
            postHocPowerTxt.Text = phPower.ToString("0.00") + " %";      
        }

        catch (Exception ex)
        {
            Alert.Show(ex.Message);
        }
    
    }   

    protected void btn_hardy_weinberg_Click(object sender, EventArgs e)
    {
         Dictionary<string, double> result = new Dictionary<string, double>();
         result = Utility.CalculateHardyWeingbergEquilibrium(Convert.ToInt32(txt_com_homozygotes.Text),Convert.ToInt32(txt_heterozygotes.Text),Convert.ToInt32(txt_rare_homozygotes.Text) );
         txt_expected_common.Text = result["Expected_CH"].ToString("0.000");
         txt_expected_heterozgt.Text = result["Expected_H"].ToString("0.000");
         txt_expected_rare.Text = result["Expected_RH"].ToString("0.000");
         txt_p_allele.Text = result["P_allele"].ToString("0.000");
         txt_q_allele.Text = result["Q_allele"].ToString("0.000");
         txt_x_square.Text = result["X_square"].ToString("0.000");
    }

}