<%@ Page Title="" Language="C#" MasterPageFile="~/Yeni.master" AutoEventWireup="true" CodeFile="AddNewPublication.aspx.cs" Inherits="_Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/Notifier.ascx" TagPrefix="uc1" TagName="Notifier" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="headContent" runat="server">
      <link href="css/SignUp.css" type="text/css" rel="stylesheet"/>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server" >
        <uc1:Notifier runat="server" ID="notifier" />
        <img style="border-style: ridge; border-width: 2px; float: left; vertical-align: middle; margin: 0 10px 0 0;" src="images/addPub.png" alt="contact" />
        <h3 style="margin: 15px 0 0 0;">Add New <br />Publication</h3>
        <br />
        <br />
        <br />
        <br />
        <br />
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
        <table style="vertical-align:middle" class="form_settings">
            <tr>
                <td><span>Disease Name</span></td>
                <td><asp:TextBox ID="SelectDisease_TextBox" runat="server"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" TargetControlID="SelectDisease_TextBox" MinimumPrefixLength="1" CompletionInterval="100" 
                    EnableCaching="false" ServicePath="AutoComplete.asmx" ServiceMethod="GetDiseaseNames" Enabled="true" ></asp:AutoCompleteExtender>
                </td>
                <td>
                    <asp:RequiredFieldValidator SetFocusOnError="true" ID="ReqSelectDisease" runat="server" ErrorMessage="Enter Disease Name!" 
                        ControlToValidate="SelectDisease_TextBox" ForeColor="Red" Height="16px" Width="190px" Display="Dynamic"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td><span>SNP</span></td>
                <td><asp:TextBox ID="SNP_TextBox" runat="server"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="SNP_TextBox" MinimumPrefixLength="1" CompletionInterval="100" 
                    EnableCaching="false" ServicePath="AutoComplete.asmx" ServiceMethod="GetSNPs" Enabled="true" ></asp:AutoCompleteExtender>
                </td>
                <td>
                    <asp:RequiredFieldValidator SetFocusOnError="true" ID="ReqSNP" runat="server" ErrorMessage="Enter SNP!" 
                        ControlToValidate="SNP_TextBox" Font-Bold="False" ForeColor="Red" Height="16px" Width="190px" Display="Dynamic"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td><span>Gene Name</span></td>
                <td><asp:TextBox CssClass="contact" ID="GeneName_TextBox" runat="server"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" TargetControlID="GeneName_TextBox" MinimumPrefixLength="1" CompletionInterval="100" 
                    EnableCaching="false" ServicePath="AutoComplete.asmx" ServiceMethod="GetGeneNames" Enabled="true" ></asp:AutoCompleteExtender>
                </td>
                <td>
                    <asp:RequiredFieldValidator SetFocusOnError="true" ID="ReqGeneName" runat="server" ErrorMessage="Enter Gene Name!" 
                        ControlToValidate="GeneName_TextBox" ForeColor="Red" Height="16px" Width="190px" Display="Dynamic"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td><span>Case Exposed Count (Yes)</span></td>
                <td><asp:TextBox CssClass="contact" ID="CaseYes_TextBox" runat="server" autocomplete="off" ></asp:TextBox></td>
                <td>
                    <asp:RequiredFieldValidator SetFocusOnError="true" ID="ReqCaseYes" runat="server" ErrorMessage="Enter Case Exposed Count!" 
                        ControlToValidate="CaseYes_TextBox" ForeColor="Red" Height="16px" Width="190px" Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValCaseYes" Operator="DataTypeCheck" Type="Integer" runat="server" ControlToValidate="CaseYes_TextBox"
                        ErrorMessage="Value must be number!" SetFocusOnError="true" ForeColor="Red" Display="Dynamic"></asp:CompareValidator>
                    <asp:RangeValidator ID="RangeValCaseYes" runat="server" ErrorMessage="Value should not be negative!" ForeColor="Red" 
                         MinimumValue="0" MaximumValue="999999999999" SetFocusOnError="True" Display="Dynamic" ControlToValidate="CaseYes_TextBox"></asp:RangeValidator>
                </td>
            </tr>
            <tr>
                <td><span>Case Unexposed Count (No)</span></td>
                <td><asp:TextBox CssClass="contact" ID="CaseNo_TextBox" runat="server" autocomplete="off"></asp:TextBox></td>
                <td>
                    <asp:RequiredFieldValidator SetFocusOnError="true" ID="ReqCaseNo" runat="server" ErrorMessage="Enter Case Unxposed Count!" 
                        ControlToValidate="CaseNo_TextBox" ForeColor="Red" Height="16px" Width="190px" Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValCaseNo" Operator="DataTypeCheck" Type="Integer" runat="server" ControlToValidate="CaseNo_TextBox"
                        ErrorMessage="Value must be number!" SetFocusOnError="true" ForeColor="Red" Display="Dynamic"></asp:CompareValidator>
                   <asp:RangeValidator ID="RangeValCaseNo" runat="server" ErrorMessage="Value should not be negative!" ForeColor="Red" 
                         MinimumValue="0" MaximumValue="999999999999" SetFocusOnError="True" Display="Dynamic" ControlToValidate="CaseNo_TextBox"></asp:RangeValidator>
                </td>
            </tr>
            <tr>
                <td><span>Control Exposed Count (Yes)</span></td>
                <td><asp:TextBox CssClass="contact" ID="ControlYes_TextBox" runat="server" autocomplete="off"></asp:TextBox></td>
                <td>
                    <asp:RequiredFieldValidator SetFocusOnError="true" ID="ReqControlYes" runat="server" ErrorMessage="Enter Control Exposed Count!" 
                        ControlToValidate="ControlYes_TextBox" ForeColor="Red" Height="16px" Width="190px" Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValControlYes" Operator="DataTypeCheck" Type="Integer" runat="server" ControlToValidate="ControlYes_TextBox"
                        ErrorMessage="Value must be number!" SetFocusOnError="true" ForeColor="Red" Display="Dynamic"></asp:CompareValidator>
                   <asp:RangeValidator ID="RangeValControlYes" runat="server" ErrorMessage="Value should not be negative!" ForeColor="Red" 
                         MinimumValue="0" MaximumValue="999999999999"  SetFocusOnError="True" Display="Dynamic" ControlToValidate="ControlYes_TextBox"></asp:RangeValidator>
                </td>
            </tr>
            <tr>
                <td><span>Control Unexposed Count (No)</span></td>
                <td><asp:TextBox CssClass="contact" ID="ControlNo_TextBox" runat="server" autocomplete="off"></asp:TextBox></td>
                <td>
                    <asp:RequiredFieldValidator SetFocusOnError="true" ID="ReqControlNo" runat="server" ErrorMessage="Enter Control Unexposed Count!" 
                        ControlToValidate="ControlNo_TextBox" ForeColor="Red" Height="16px" Width="190px" Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValControlNo" Operator="DataTypeCheck" Type="Integer" runat="server" ControlToValidate="ControlNo_TextBox"
                        ErrorMessage="Value must be number!" SetFocusOnError="true" ForeColor="Red" Display="Dynamic"></asp:CompareValidator>
                    <asp:RangeValidator ID="RangeValControlNo" runat="server" ErrorMessage="Value should not be negative!" ForeColor="Red" 
                         MinimumValue="0" MaximumValue="999999999999"  SetFocusOnError="True" Display="Dynamic" ControlToValidate="ControlNo_TextBox"></asp:RangeValidator>
                </td>
            </tr>
            <tr>
                <td><span>P Value</span></td>
                <td><asp:TextBox CssClass="contact" ID="P_TextBox" runat="server" autocomplete="off"></asp:TextBox></td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td><span><asp:DropDownList ID="Reference_DropDown" runat="server" Height="25px" Width="191px">
                                <asp:ListItem Text="Select the Reference Type" Value="0" />  
                                <asp:ListItem Value="1">PubMed Id</asp:ListItem>
                                <asp:ListItem Value="2">Link</asp:ListItem>
                                <asp:ListItem Value="3">Diğer</asp:ListItem>
                          </asp:DropDownList></span>
                </td>
                <td><asp:TextBox CssClass="contact" ID="Reference_TextBox" runat="server" Rows="5" TextMode="MultiLine"></asp:TextBox></td>
                <td>
                    <asp:RequiredFieldValidator SetFocusOnError="true" ID="ReqDropDown" InitialValue="0" runat="server" ErrorMessage="Select a Reference Type!" 
                        ControlToValidate="Reference_DropDown" ForeColor="Red" Height="16px" Width="190px" Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator SetFocusOnError="true" ID="ReqReferenceTxt" runat="server" ErrorMessage="Enter Reference Type!" 
                        ControlToValidate="Reference_TextBox" ForeColor="Red" Height="16px" Width="190px" Display="Dynamic"></asp:RequiredFieldValidator>
                </td>
            </tr> 
            <tr>
                <td>&nbsp;</td>
                <td> <p style="padding-top: 15px"><asp:Button CssClass="submit" ID="AddButton" runat="server" Text="Add" OnClick="AddButton_Click" /></p></td>
                <td>&nbsp;</td>
            </tr>
        </table>
</asp:Content>