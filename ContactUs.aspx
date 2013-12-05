<%@ Page Title="" Language="C#" MasterPageFile="~/Yeni.master" AutoEventWireup="true" CodeFile="ContactUs.aspx.cs" Inherits="_Default" %>
<asp:Content ID="ContentHead" ContentPlaceHolderID="headContent" runat="server">
      <link href="css/SignUp.css" type="text/css" rel="stylesheet"/>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server" >
          
    <form id="contact" runat="server">
    <table>
              <tr>
                  <td colspan="2"><img style="float: left; vertical-align: middle; margin: 0 10px 0 0;" src="images/contact.png" alt="contact" /><h1 style="margin: 15px 0 0 0;">Contact Us</h1></td>
              </tr>
              <tr>
                  <td><span>Name</span></td>
                  <td><input class="contact" type="text" name="your_name" value="" /></td>
              </tr>
              <tr>
                  <td><span>Email Address</span></td>
                  <td><input class="contact" type="text" name="your_email" value="" /></td>
              </tr>
              <tr>
                  <td><span>Message</span></td>
                  <td><textarea class="contact textarea" rows="5" cols="50" name="your_message"></textarea></td>
              </tr>
              <tr style="text-align: right; vertical-align: middle" >
                  <td colspan="2"><asp:Button ID="contact_submitted" runat="server" Text="Button" />
                  </td>
              </tr>
          </table>
        </form>
</asp:Content>