<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Yeni.master" CodeFile="MyAccount.aspx.cs" Inherits="MyAccount" %>
<%@ Register Src="~/MyAccountLeftPanel.ascx" TagName="leftPanel" TagPrefix="UserControl"%>
<%@ Register Src="~/Notifier.ascx" TagPrefix="uc1" TagName="Notifier" %>
<asp:Content ID="Content2" ContentPlaceHolderID="headContent" Runat="Server">  
        <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1"> 
        <meta name="viewport" content="width=device-width, initial-scale=1.0">
        <meta name="keywords" content="menu, navigation, animation, transition, transform, rotate, css3, web design, component, icon, slide" />
        <link rel="shortcut icon" href="../favicon.ico"> 
        <link rel="stylesheet" type="text/css" href="css/VerticalNav/demo.css" />
        <link rel="stylesheet" type="text/css" href="css/VerticalNav/style1.css" />
        <link href='http://fonts.googleapis.com/css?family=Terminal+Dosis' rel='stylesheet' type='text/css' />
      <script src="Scripts/ErrorSuccessNotifier.js"></script>
       <link href="Styles/ErrorSuccessNotifier.css" rel="stylesheet" />
        <link rel="stylesheet" type="text/css" href="css/Table.css" />
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server" >
    <uc1:Notifier runat="server" ID="notifier" />
    <asp:Panel ID="leftPanel" runat="server" CssClass="MyAccountLeft">
        <UserControl:leftPanel runat="server" />
    </asp:Panel>


     <asp:Panel runat="server" ID="mainContent" CssClass="MyAccountMain"> 
         <table style="margin: 25px auto;">
                <thead>
                    <th colspan="2"> Profile Details </th>
                </thead>
                <tbody>
                    <tr>
                        <td><asp:Label runat="server" ID="lblUsername" Text="User Name: "></asp:Label></td>
                        <td style="padding:15px 18px 15px 18px"><asp:TextBox runat="server" ID="txtUsername" CssClass="twitterStyleTextbox" ReadOnly="true" ></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td><asp:Label runat="server" ID="lblName" Text="Name: "></asp:Label></td>
                        <td style="padding:15px 18px 15px 18px"><asp:TextBox runat="server" ID="txtName" CssClass="twitterStyleTextbox"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                            ControlToValidate ="txtName" Display="Dynamic"
                                            ErrorMessage="** required field." ValidationGroup="account_update"
                                            ForeColor="Red">
                          </asp:RequiredFieldValidator></td>
                    </tr>
                     <tr>
                        <td><asp:Label runat="server" ID="lblSurname" Text="Surname: "></asp:Label></td>
                        <td style="padding:15px 18px 15px 18px"><asp:TextBox runat="server" ID="txtSurname" CssClass="twitterStyleTextbox"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                            ControlToValidate ="txtSurname" Display="Dynamic"
                                            ErrorMessage="** required field." ValidationGroup="account_update"
                                            ForeColor="Red">
                          </asp:RequiredFieldValidator></td>
                    </tr>
                     <tr>
                        <td><asp:Label runat="server" ID="lblEmail" Text="Email: "></asp:Label></td>
                        <td style="padding:15px 18px 15px 18px"><asp:TextBox runat="server" ID="txtEmail" CssClass="twitterStyleTextbox"></asp:TextBox>
                          <asp:RegularExpressionValidator Display="Dynamic" ID="revEmailValidator" runat="server" ValidationGroup="account_update"
                                            ValidationExpression="[-0-9a-zA-Z.+_]+@[-0-9a-zA-Z.+_]+\.[a-zA-Z]{2,4}" ControlToValidate="txtEmail" ErrorMessage="Must be correct" />
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                            ControlToValidate ="txtEmail" Display="Dynamic"
                                            ErrorMessage="** required field." ValidationGroup="account_update"
                                            ForeColor="Red">
                          </asp:RequiredFieldValidator></td>
                    </tr>

                </tbody>
                <tfoot>
                     <tr>
                       <td style="text-align: right" colspan="2">
                         <asp:Button runat="server" ID="btnUpdate" CssClass="buttonCss" Text=" Update " OnClick="btnUpdate_Click" ValidationGroup="account_update" />
                         </td>
                     </tr>
               </tfoot>
         </table>

     </asp:Panel>



</asp:Content>