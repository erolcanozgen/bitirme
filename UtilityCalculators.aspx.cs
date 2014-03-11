using System;
using System.Collections.Generic;
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
            Double rr_value = 0.0f; Double rr_variance = 0.0f; Double ln_rr = 0.0f;
           
            results_panel.Visible = true;
    
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

        }

        catch (Exception ex)
        {
            Alert.Show(ex.Message);
        }
    
    }
}