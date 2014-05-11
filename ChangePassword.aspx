<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Yeni.master" CodeFile="ChangePassword.aspx.cs" Inherits="ChangePassword" %>
<%@ Register Src="~/MyAccountLeftPanel.ascx" TagName="leftPanel" TagPrefix="UserControl"%>

<asp:Content ID="Content2" ContentPlaceHolderID="headContent" Runat="Server">  
        <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1"> 
        <meta name="viewport" content="width=device-width, initial-scale=1.0">
        <meta name="keywords" content="menu, navigation, animation, transition, transform, rotate, css3, web design, component, icon, slide" />
        <link rel="shortcut icon" href="../favicon.ico"> 
        <link rel="stylesheet" type="text/css" href="css/VerticalNav/demo.css" />
        <link rel="stylesheet" type="text/css" href="css/VerticalNav/style1.css" />
        <link href='http://fonts.googleapis.com/css?family=Terminal+Dosis' rel='stylesheet' type='text/css' />
        <link rel="stylesheet" type="text/css" href="css/Table.css" />
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server" >

    <asp:Panel ID="leftPanel" runat="server" CssClass="MyAccountLeft">
        <UserControl:leftPanel ID="LeftPanel1" runat="server" />
    </asp:Panel>


     <asp:Panel runat="server" ID="mainContent" CssClass="MyAccountMain"> 
         <table style="margin: 25px auto;">
                <thead>
                    <th colspan="2"> Change Password </th>
                </thead>
                <tbody>
                    <tr>
                        <td><asp:Label runat="server" ID="lblOldPwd"  Text="Old Password: "></asp:Label></td>
                        <td style="padding:15px 18px 15px 18px"><asp:TextBox runat="server" ID="txtOldPwd" TextMode="Password" CssClass="twitterStyleTextbox" ></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                                            ControlToValidate ="txtOldPwd" Display="Dynamic"
                                            ErrorMessage="** required field." ValidationGroup="changePwd"
                                            ForeColor="Red">
                          </asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td><asp:Label runat="server" ID="lblNewPwd" Text="New Password: "></asp:Label></td>
                        <td style="padding:15px 18px 15px 18px"><asp:TextBox runat="server" ID="txtNewPwd" TextMode="Password" CssClass="twitterStyleTextbox"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                            ControlToValidate ="txtNewPwd" Display="Dynamic"
                                            ErrorMessage="** required field." ValidationGroup="changePwd"
                                            ForeColor="Red">
                          </asp:RequiredFieldValidator></td>
                    </tr>
                     <tr>
                        <td><asp:Label runat="server" ID="lblNewPwdAgain" Text="New Password (Again): "></asp:Label></td>
                        <td style="padding:15px 18px 15px 18px"><asp:TextBox runat="server" ID="txtNewPwdAgain" TextMode="Password" CssClass="twitterStyleTextbox"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                            ControlToValidate ="txtNewPwdAgain" Display="Dynamic"
                                            ErrorMessage="** required field." ValidationGroup="changePwd"
                                            ForeColor="Red">
                          </asp:RequiredFieldValidator></td>
                    </tr>
                </tbody>
                <tfoot>
                     <tr>
                       <td style="text-align: right" colspan="2">
                         <asp:Button runat="server" ID="btnChangePwd" CssClass="buttonCss" Text=" Update " OnClick="btnChangePwd_Click" ValidationGroup="changePwd" />
                         </td>
                     </tr>
               </tfoot>
         </table>

     </asp:Panel>



</asp:Content>