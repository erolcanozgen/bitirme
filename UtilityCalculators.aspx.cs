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
     
        if (Panel1.FindControl("lbl_or_value") == null)
        {
            Label lbl_or_value = new Label();
            lbl_or_value.ID = "lbl_or_value";
            lbl_or_value.Text = "Or Value: ";
            Panel1.Controls.Add(lbl_or_value);
        }

        if (Panel1.FindControl("txt_or_value") == null)
        {
            TextBox txt_or_value = new TextBox();
            txt_or_value.Text = Utility.CalculateOrValue(Convert.ToInt32(txtCaseYes.Text), Convert.ToInt32(txtCaseNo.Text),
                                                        Convert.ToInt32(txtControlYes.Text), Convert.ToInt32(txtControlNo.Text)).ToString("0.000");
            txt_or_value.ID = "txt_or_value";
            txt_or_value.Width = Unit.Pixel(50);
            Panel1.Controls.Add(txt_or_value);
        }

        else ((TextBox)Panel1.FindControl("txt_or_value")).Text = Utility.CalculateOrValue(Convert.ToInt32(txtCaseYes.Text), Convert.ToInt32(txtCaseNo.Text),
                                                       Convert.ToInt32(txtControlYes.Text), Convert.ToInt32(txtControlNo.Text)).ToString("0.000");
    
    
    }
}