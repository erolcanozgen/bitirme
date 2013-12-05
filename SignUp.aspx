<%@ Page Title="" Language="C#" MasterPageFile="~/Yeni.master" AutoEventWireup="true" CodeFile="SignUp.aspx.cs" Inherits="SignUp"  %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="headContent" runat="server">
      <link href="SignUp.css" type="text/css" rel="stylesheet"/>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <form id="form1"  runat="server">
    <div>
    <br />
    <table border="1" cellpadding="1" cellspacing="1" style="width: 60%" align="center" class="Table">
            <tr>
                <td colspan="2" class="style2">
                    User Registration Form</td>
            </tr>
            <tr>
                <td colspan="2" valign="top">
                    <table style="width: 100%">
                        <tr>
                            <td align="left" width="30%" class="style1">
                                First Name:</td>
                            <td align="left" width="50%">
                                <asp:TextBox ID="txtFirstName" runat="server" MaxLength="50" CssClass="TextinputText"></asp:TextBox></td>
                            <td align="left" width="20%">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td align="left" width="30%" class="FieldCaptionTD">
                                Last Name:</td>
                            <td align="left" width="50%">
                                <asp:TextBox ID="txtLastName" runat="server" MaxLength="50" CssClass="TextinputText"></asp:TextBox></td>
                            <td align="left" width="20%">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td align="left" width="30%" style="height: 26px" class="FieldCaptionTD">
                                UserName:</td>
                            <td align="left" width="50%" style="height: 26px">
                                <asp:TextBox ID="txtUserName" runat="server" MaxLength="12" CssClass="TextinputText"></asp:TextBox></td>
                            <td align="left" width="20%" style="height: 26px">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td align="left" width="30%" class="FieldCaptionTD">
                                Password:</td>
                            <td align="left" width="50%">
                                <asp:TextBox ID="txtPwd" runat="server" MaxLength="15" CssClass="TextinputText" 
                                    TextMode="Password"></asp:TextBox></td>
                            <td align="left" width="20%">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td align="left" width="30%" style="height: 26px" class="FieldCaptionTD">
                                Confirm Password:</td>
                            <td align="left" width="50%" style="height: 26px">
                                <asp:TextBox ID="txtRePwd" runat="server" MaxLength="15" 
                                    CssClass="TextinputText" TextMode="Password"></asp:TextBox></td>
                            <td align="left" width="20%" style="height: 26px">
                                <asp:CompareValidator ID="CompareValidator1" runat="server" 
                                    ControlToCompare="txtRePwd" ControlToValidate="txtPwd" 
                                    Operator="Equal" ErrorMessage="Password and confirm password do not match" 
                                    SetFocusOnError="True"></asp:CompareValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" width="30%" class="FieldCaptionTD">
                                E-mail:</td>
                            <td align="left" width="50%">
                                <asp:TextBox ID="txtEmailID" runat="server" MaxLength="70" CssClass="TextinputText"></asp:TextBox></td>
                            <td align="left" width="20%">
                                &nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <table style="width: 100%" class="Table">
                        <tr>
                            <td>
                                <asp:Label ID="lblMsg" runat="server" CssClass="Label" Font-Bold="True" 
                                    ForeColor="Red"></asp:Label></td>
                            <td>
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                    ShowSummary="False" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" colspan="2">
                                <asp:Button ID="btnSave" runat="server" Text="Sign Up" CssClass="Button" 
                                    onclick="btnSave_Click" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    </form>
</asp:Content>

