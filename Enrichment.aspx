<%@ Page Title="" Language="C#" MasterPageFile="~/Yeni.master" AutoEventWireup="true" CodeFile="Enrichment.aspx.cs" Inherits="Enrichment" %>

<%@ Register Src="~/Notifier.ascx" TagPrefix="uc1" TagName="Notifier" %>


<asp:Content ID="Content1" ContentPlaceHolderID="headContent" Runat="Server">
    <link href="css/Table.css" rel="stylesheet" />
    <link href="css/Button.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DiseaseList" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:Notifier runat="server" ID="notifier" />

    <asp:GridView ID="grdEnrichment" runat="server" AutoGenerateColumns="False" OnSorting="grdEnrichment_Sorting" OnPageIndexChanging="grdEnrichment_PageIndexChanging"
             BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Both" AllowSorting="True" AllowPaging="True">
			           
			<Columns>
                <asp:TemplateField HeaderText="Pathway Id">
                    <ItemTemplate>
                            <asp:HyperLink ID="pathId" runat="server" Text='<%# Bind("pathId") %>'></asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField  DataFormatString="{0:F2}" DataField="pathName" HeaderText="Pathway Name" SortExpression="pathName"/>
                <asp:TemplateField HeaderText="Searched Genes">
                    <ItemTemplate>
                            <asp:Label ID="searchedGenes" runat="server" Text='<%# Bind("searchedGenes") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField  DataFormatString="{0:F2}" DataField="significantScore" HeaderText="Significant Score" SortExpression="significantScore"/>
			</Columns>
            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#808080" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#383838" />
		</asp:GridView>
   
</asp:Content>

