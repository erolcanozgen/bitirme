<%@ Page Title="" Language="C#" MasterPageFile="~/Yeni.master" AutoEventWireup="true" CodeFile="AddNewPublication.aspx.cs" Inherits="_Default" %>
<asp:Content ID="ContentHead" ContentPlaceHolderID="headContent" runat="server">
      <link href="css/SignUp.css" type="text/css" rel="stylesheet"/>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server" >
       

        <img style="float: left; vertical-align: middle; margin: 0 10px 0 0;" src="images/addPub.png" alt="contact" />
        <h3 style="margin: 15px 0 0 0;">Add New <br />Publication</h3>
        <br /><br />
        <br /><br />
        
        <div class="form_settings">
            <p><span>Select The Disease</span><asp:DropDownList ID="Dropdown_SelectDisease" runat="server"></asp:DropDownList></p>
            <p><span>SNP</span><asp:DropDownList ID="DropDown_SNP" runat="server"></asp:DropDownList></p>        
        </div>
        <div class="form_settings">
            <p><span>Case Count</span><asp:TextBox CssClass="contact" ID="CaseCount_TextBox" runat="server" ></asp:TextBox></p>
            <p><span>Control Count</span><asp:TextBox CssClass="contact" ID="ControlCount_TextBox" runat="server"></asp:TextBox></p>
            <p><span>Gene Name</span><asp:TextBox CssClass="contact" ID="GeneName_TextBox" runat="server"></asp:TextBox></p>
            <p><span>Frequency Control</span><asp:TextBox CssClass="contact" ID="FC_TextBox" runat="server"></asp:TextBox></p>
            <p><span>Frequency Patient</span><asp:TextBox CssClass="contact" ID="FP_TextBox" runat="server"></asp:TextBox></p>
            <p><span>P Value</span><asp:TextBox CssClass="contact" ID="P_TextBox" runat="server"></asp:TextBox></p>
            <p><span>OR Value</span><asp:TextBox CssClass="contact" ID="OR_TextBox" runat="server"></asp:TextBox></p>
            <p><span>Reference</span><asp:TextBox CssClass="contact" ID="Reference_TextBox" runat="server" Rows="5" TextMode="MultiLine"></asp:TextBox></p>
            <p style="padding-top: 15px"><span>&nbsp; &nbsp;</span><asp:Button CssClass="submit" ID="AddButton" runat="server" Text="Add" OnClick="AddButton_Click" /></p>
        </div>
       

</asp:Content>