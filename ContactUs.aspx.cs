using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    protected void SendMessageBtn_Click(object sender, EventArgs e)
    {
        MailMessage eMail = new MailMessage();
        eMail.From = new MailAddress("tusnp.database@gmail.com");
        eMail.To.Add("tusnp.database@gmail.com");
        eMail.Subject = subjectTxt.Text;
        eMail.Body = "From:\t" + emailTxt.Text + "\n" + "Name:\t" + nameTxt.Text + "\n" + messageTxt.Text;
        SmtpClient smtp = new SmtpClient();
        smtp.Credentials = new System.Net.NetworkCredential("tusnp.database@gmail.com", "tusnp12345");
        smtp.Port = 587;
        smtp.Host = "smtp.gmail.com";
        smtp.EnableSsl = true;
        try
        {
            smtp.SendAsync(eMail, (object)eMail);
            Notifier.AddSuccessMessage("Mail has been sent..");
        }
        catch (SmtpException ex)
        {
            Notifier.AddErrorMessage("An error occured.Please try again later.");
        }
    }
}