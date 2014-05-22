using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PathwayImage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            if (!IsPostBack)
            {
                string pathway = Request.QueryString["pathway"];
                System.Drawing.Image img = KeggApi.getImage(pathway);
                var ms = new MemoryStream();
                img.Save(ms, ImageFormat.Png);
                System.IO.BinaryReader br = new System.IO.BinaryReader(ms);
                Byte[] bytes = br.ReadBytes((Int32)ms.Length);
                string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                Image1.ImageUrl = "data:image/png;base64," + base64String;
                Image1.Visible = true; 
            }
        }
        catch (Exception ex)
        {
            Notifier.AddErrorMessage("An error was occured.Please try again later.");
            //Alert.Show(ex.Message);
        }
    }
}