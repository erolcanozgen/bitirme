<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="SignUp.aspx.cs" Inherits="SignUp" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="headContent" runat="server">
    <link href="css/SignUp.css" type="text/css" rel="stylesheet" />
    <style type="text/css">
        .auto-style2 {
            width: 256px;
            height: 92px;
        }

        .auto-style3 {
            height: 92px;
        }

        .auto-style4 {
            width: 511px;
        }

        .auto-style5 {
            width: 50%;
        }

        .auto-style6 {
            height: 26px;
            width: 50%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <div id="signIn">
    <div id="login-box">
                <h2>Login</h2>
                <div id="login-box-name" style="margin-top: 20px;">Email:</div>
                <div id="login-box-field" style="margin-top: 20px;">
                    <input name="q" class="form-login" title="Username" value="" size="30" maxlength="2048" /></div>
                <div id="Div1">Password:</div>
                <div id="Div2">
                    <input name="q" type="password" class="form-login" title="Password" value="" size="30" maxlength="2048" /></div>
                <br />
                <span class="login-box-options">
                    <input type="checkbox" name="1" value="1">
                    Remember Me <a href="#" style="margin-left: 30px;">Forgot password?</a></span>
                <br />
                <br />
                <a href="#">
                    <img src="images/login-btn.png" width="103" height="42" style="margin-left: 90px;" /></a>
    </div>

</div>

    <div id="psdgraphics-com-table">
        <form id="form2">
            <div id="psdg-header">
                <span class="psdg-bold">User Registration Form</span><br />
            </div>
            <br />
            <table cellpadding="1" cellspacing="1" style="width: 60%" align="center" class="Table">
                <tr>
                    <td colspan="2" valign="top" class="auto-style4">
                        <table style="margin-top: 0px; width: 402px;">
                            <tr>
                                <td align="left" style="text-decoration-color: white; color: #FFFFFF;" width="30%" class="style1">First Name:</td>
                                <td align="left" class="auto-style5">
                                    <asp:TextBox ID="txtFirstName" runat="server" MaxLength="50" CssClass="TextinputText"></asp:TextBox></td>
                                <td align="left" width="20%">&nbsp;</td>
                            </tr>
                            <tr>
                                <td align="left" width="30%" class="FieldCaptionTD" style="color: #FFFFFF">Last Name:</td>
                                <td align="left" class="auto-style5">
                                    <asp:TextBox ID="txtLastName" runat="server" MaxLength="50" CssClass="TextinputText"></asp:TextBox></td>
                                <td align="left" width="20%">&nbsp;</td>
                            </tr>
                            <tr>
                                <td align="left" width="30%" style="height: 26px; color: #FFFFFF;" class="FieldCaptionTD">UserName:</td>
                                <td align="left" class="auto-style6">
                                    <asp:TextBox ID="txtUserName" runat="server" MaxLength="12" CssClass="TextinputText"></asp:TextBox></td>
                                <td align="left" width="20%" style="height: 26px">&nbsp;</td>
                            </tr>
                            <tr>
                                <td align="left" style="color: #FFFFFF;" width="30%" class="FieldCaptionTD">Password:</td>
                                <td align="left" class="auto-style5">
                                    <asp:TextBox ID="txtPwd" runat="server" MaxLength="15" CssClass="TextinputText"
                                        TextMode="Password"></asp:TextBox></td>
                                <td align="left" width="20%">&nbsp;</td>
                            </tr>
                            <tr>
                                <td align="left" width="30%" style="color: #FFFFFF;height: 26px" class="FieldCaptionTD">Confirm Password:</td>
                                <td align="left" class="auto-style6">
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
                                <td align="left" style="color: #FFFFFF;" width="30%" class="FieldCaptionTD">E-mail:</td>
                                <td align="left" class="auto-style5">
                                    <asp:TextBox ID="txtEmailID" runat="server" MaxLength="70" CssClass="TextinputText"></asp:TextBox></td>
                                <td align="left" width="20%">&nbsp;</td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" class="auto-style4">
                        <table style="width: 101%; height: 102px;" class="Table">
                            <tr>
                                <td class="auto-style3">
                                    <asp:Label ID="lblMsg" runat="server" CssClass="Label" Font-Bold="True"
                                        ForeColor="Red"></asp:Label></td>
                                <td class="auto-style2">
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                        ShowSummary="False" Width="145px" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right" colspan="2">
                                    <asp:Button ID="btnSave" runat="server" Text="Sign Up" CssClass="Button"
                                        OnClick="btnSave_Click" /></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>

        </form>
    </div>

</asp:Content>

