<%@ Page Title="" Language="C#" MasterPageFile="~/Yeni.master" AutoEventWireup="true" CodeFile="AddNewPublication.aspx.cs" Inherits="_Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="ContentHead" ContentPlaceHolderID="headContent" runat="server">
      <link href="css/SignUp.css" type="text/css" rel="stylesheet"/>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server" >
       

        <img style="float: left; vertical-align: middle; margin: 0 10px 0 0;" src="images/addPub.png" alt="contact" />
        <h3 style="margin: 15px 0 0 0;">Add New <br />Publication</h3>
        <br /><br />
        <br />
        
        
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
        <div class="form_settings">
            <p>
                <span>Disease Name</span><asp:TextBox ID="SelectDisease_TextBox" runat="server"></asp:TextBox>
               <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" TargetControlID="SelectDisease_TextBox" MinimumPrefixLength="1" CompletionInterval="100" 
                    EnableCaching="false" ServicePath="AutoComplete.asmx" ServiceMethod="GetDiseaseNames" Enabled="true" ></asp:AutoCompleteExtender>
            </p>
            <p>
                <span>SNP</span><asp:TextBox ID="SNP_TextBox" runat="server"></asp:TextBox>
                <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="SNP_TextBox" MinimumPrefixLength="1" CompletionInterval="100" 
                    EnableCaching="false" ServicePath="AutoComplete.asmx" ServiceMethod="GetSNPs" Enabled="true" ></asp:AutoCompleteExtender>
            </p>        

            <p>
                <span>Gene Name</span><asp:TextBox CssClass="contact" ID="GeneName_TextBox" runat="server"></asp:TextBox>
                <asp:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" TargetControlID="GeneName_TextBox" MinimumPrefixLength="1" CompletionInterval="100" 
                    EnableCaching="false" ServicePath="AutoComplete.asmx" ServiceMethod="GetGeneNames" Enabled="true" ></asp:AutoCompleteExtender>
            </p>

        </div>
        <div class="form_settings">
            <p><span>Case Count</span><asp:TextBox CssClass="contact" ID="CaseCount_TextBox" runat="server" ></asp:TextBox></p>
            <p><span>Control Count</span><asp:TextBox CssClass="contact" ID="ControlCount_TextBox" runat="server"></asp:TextBox></p>
            <p><span>Frequency Control</span><asp:TextBox CssClass="contact" ID="FC_TextBox" runat="server"></asp:TextBox></p>
            <p><span>Frequency Patient</span><asp:TextBox CssClass="contact" ID="FP_TextBox" runat="server"></asp:TextBox></p>
            <p><span>P Value</span><asp:TextBox CssClass="contact" ID="P_TextBox" runat="server"></asp:TextBox></p>
            <p><span>OR Value</span><asp:TextBox CssClass="contact" ID="OR_TextBox" runat="server"></asp:TextBox></p>
            <p><span><asp:DropDownList ID="Reference_DropDown" runat="server" Height="25px" Width="191px">
                <asp:ListItem Text="Select the Reference Type" Value="" />  
                <asp:ListItem Value="1">PubMed Id</asp:ListItem>
                <asp:ListItem Value="2">Link1</asp:ListItem>
                <asp:ListItem Value="3">Link2</asp:ListItem>
                </asp:DropDownList></span>
                <asp:TextBox CssClass="contact" ID="Reference_TextBox" runat="server" Rows="5" TextMode="MultiLine"></asp:TextBox>
            </p>
            <p style="padding-top: 15px"><span>&nbsp; &nbsp;</span><asp:Button CssClass="submit" ID="AddButton" runat="server" Text="Add" OnClick="AddButton_Click" /></p>
        </div>
       
    
</asp:Content>