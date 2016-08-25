<%@ Page  Async="true" Title="" Language="C#" MasterPageFile="~/Yeni.master" AutoEventWireup="true" CodeFile="ContactUs.aspx.cs" Inherits="_Default" %>

<%@ Register Src="~/Notifier.ascx" TagPrefix="uc1" TagName="Notifier" %>

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
      <script src="Scripts/ErrorSuccessNotifier.js"></script>
       <link href="Styles/ErrorSuccessNotifier.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server" >
        &nbsp;<uc1:Notifier runat="server" ID="notifier" />
        <table class="searchPanel" style="width:550px">
            <tr>
                <td colspan="2">
                    <img style="float: left; vertical-align: middle; margin: 0 10px 0 0;" src="images/contact.png" alt="contact" />
                    <h2 style="margin: 15px 0 0 0;">Contact Us</h2>
                    &nbsp;<br />
                </td>
            </tr>
            <tr>
                <td style="vertical-align:middle" class="auto-style2"><span>Name</span></td>
                <td style="vertical-align:middle"><asp:TextBox CssClass="contact" ID="nameTxt" runat="server" autocomplete="off"></asp:TextBox>
                    <asp:RequiredFieldValidator SetFocusOnError="true" ID="ReqName" runat="server" ErrorMessage="Please Enter Name!" 
                        ControlToValidate="nameTxt" ForeColor="Red" Height="16px" Width="190px" Display="Dynamic"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td style="vertical-align:middle" class="auto-style2"><span>Email Address</span></td>
                <td style="vertical-align:middle"><asp:TextBox CssClass="contact" ID="emailTxt" runat="server" autocomplete="off"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="ReqEmail" runat="server" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"  
                        ControlToValidate="emailTxt" ForeColor="Red" Height="16px" Width="190px" Display="Dynamic" ErrorMessage="Invalid E-mail address!">
                    </asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td style="vertical-align:middle" class="auto-style2"><span>Subject</span></td>
                <td style="vertical-align:middle"><asp:TextBox CssClass="contact" ID="subjectTxt" runat="server" autocomplete="off"></asp:TextBox>
                    <asp:RequiredFieldValidator SetFocusOnError="true" ID="ReqSubject" runat="server" ErrorMessage="Please Enter Subject!" 
                        ControlToValidate="subjectTxt" ForeColor="Red" Height="16px" Width="190px" Display="Dynamic"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td style="vertical-align:middle" class="auto-style2"><span>Message</span></td>
                <td style="vertical-align:middle"><asp:TextBox CssClass="contact" ID="messageTxt" Width="98%" runat="server" Rows="5" TextMode="MultiLine"></asp:TextBox>
                    <asp:RequiredFieldValidator SetFocusOnError="true" ID="ReqMessage" runat="server" ErrorMessage="Please Enter Message!" 
                        ControlToValidate="messageTxt" ForeColor="Red" Height="16px" Width="190px" Display="Dynamic"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr style="text-align:right">
                <td colspan="2" style="text-align:right; vertical-align:middle" class="auto-style2">
                    &nbsp;<asp:Button CssClass="buttonCss" ID="SendMessageBtn" runat="server" Text=" Send " Height="33px" OnClick="SendMessageBtn_Click" />
                </td>
            </tr>
        </table>
      
</asp:Content>