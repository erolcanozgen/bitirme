<%@ Page Title="" Language="C#" MasterPageFile="~/Yeni.master" AutoEventWireup="true" CodeFile="ContactUs.aspx.cs" Inherits="_Default" %>
<asp:Content ID="ContentHead" ContentPlaceHolderID="headContent" runat="server">
      <link href="css/SignUp.css" type="text/css" rel="stylesheet"/>
      <style type="text/css">
          .auto-style2
          {
              width: 87px;
          }
      </style>
    <link href="css/Button.css" rel="stylesheet" />
    <link href="css/Table.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server" >

        <table class="searchPanel" style="width:550px">
            <tr>
                <td colspan="2">
                    <img style="float: left; vertical-align: middle; margin: 0 10px 0 0;" src="images/contact.png" alt="contact" />
                    <h2 style="margin: 15px 0 0 0;">Contact Us</h2>
                    <p>For ask anything</p><br />
                </td>
            </tr>
            <tr>
                <td style="vertical-align:middle" class="auto-style2"><span>Name</span></td>
                <td style="vertical-align:middle"><input class="contact" type="text" name="your_name" value="" /></td>
            </tr>
            <tr>
                <td style="vertical-align:middle" class="auto-style2"><span>Email Address</span></td>
                <td style="vertical-align:middle"><input class="contact" type="text" name="your_email" value="" /></td>
            </tr>
            <tr>
                <td style="vertical-align:middle" class="auto-style2"><span>Subject</span></td>
                <td style="vertical-align:middle"><input class="contact" type="text" name="your_email" value="" /></td>
            </tr>
            <tr>
                <td style="vertical-align:middle" class="auto-style2"><span>Message</span></td>
                <td style="vertical-align:middle"><textarea class="contact textarea" rows="5" cols="50" name="your_message"></textarea></td>
            </tr>
            <tr style="text-align:right">
                <td colspan="2" style="text-align:right; vertical-align:middle" class="auto-style2">
                    &nbsp;<input style="width:70px" class="buttonCss" type="submit" name="contact_submitted" value="send" /></td>
            </tr>
        </table>
      
</asp:Content>