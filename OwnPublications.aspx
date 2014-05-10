<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Yeni.master" CodeFile="OwnPublications.aspx.cs" Inherits="OwnPublications" %>
<%@ Register Src="~/MyAccountLeftPanel.ascx" TagName="leftPanel" TagPrefix="UserControl"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/Notifier.ascx" TagPrefix="uc1" TagName="Notifier" %>


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
    <asp:GridView ID="grdViewPublications" runat="server" AutoGenerateColumns="false" 
			        DataKeyNames="SNP" CssClass="ChildGrid" AllowPaging="true" HorizontalAlign="Center" AllowSorting="True" OnPageIndexChanging="grdViewPublications_PageIndexChanging">

                        <Columns>
                            <asp:TemplateField HeaderText="Gene Name">
                                <ItemTemplate>
                                     <asp:HyperLink ID="Gene_Name" runat="server" Text='<%# Bind("Gene_Name") %>'></asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField  DataField="SNP" HeaderText="SNP" SortExpression="SNP"/>
                            <asp:BoundField  DataField="Case_Count" HeaderText="Case Count" SortExpression="Case_Count"/>
                            <asp:BoundField  DataFormatString="{0:F2}" DataField="Frequency_Patient" HeaderText="Frequency In Case" SortExpression="Frequency_Patient"/>
                            <asp:BoundField  DataField="Control_Count" HeaderText="Control Count" SortExpression="Control_Count"/>
                            <asp:BoundField  DataFormatString="{0:F2}" DataField="Frequency_Control" HeaderText="Frequency In Control" SortExpression="Frequency_Control"/>
                            <asp:BoundField  DataField="P_Value" HeaderText="P Value" SortExpression="P_Value"/>
                            <asp:BoundField  DataFormatString="{0:F2}" DataField="OR_Value" HeaderText="OR Value" SortExpression="OR_Value"/>
                            <asp:BoundField  DataField="CI" HeaderText="95% CI" SortExpression="CI"/>
                            <asp:TemplateField HeaderText="Reference">
                                <ItemTemplate>
                                    <asp:HyperLink ID="Link" runat="server" Text='<%# Bind("Reference") %>' ></asp:HyperLink>
                                    <asp:Label runat="server" ID="lbl_reference" Visible="false" Text='<%# Bind("Reference") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField Visible="false"  DataField="ownerOfPublication" HeaderText="Owner Of Publication" />
                        </Columns>
                        <RowStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                    </asp:GridView>

     <asp:Panel runat="server" ID="mainContent" CssClass="MyAccountMain"> 
         
      </asp:Panel>

</asp:Content>