<%@ Page Title="" Language="C#" MasterPageFile="~/Yeni.master" AutoEventWireup="true" CodeFile="Diseases.aspx.cs" Inherits="Disasters" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
        <table class="gridtable">
            <tr>
                <td>
                    &nbsp;&nbsp;
                    <asp:Label ID="Label1" runat="server" Text="Select a disease to examine researches:"></asp:Label>
                    <br />&nbsp;&nbsp;
                    <asp:DropDownList ID="DiseasesList" runat="server" 
                        AutoPostBack="True"
                        onselectedindexchanged="showDiseaseDetails"> </asp:DropDownList>
                </td>
                <td style="width: 328px; text-align:right;" >
                    <asp:ImageButton Visible="false" ID="excelAktar" runat="server" 
                        ImageUrl="~/images/excelAktar.jpg" BorderColor="#93A985" 
                        BorderStyle="Ridge" onclick="excelAktar_Click" />
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    <asp:GridView ID="DiseasesDetailsGrid" runat="server" CellPadding="10" AutoGenerateColumns="False"
                        ForeColor="Black" AllowSorting="True"
                        BackColor="White" BorderColor="#999999" BorderStyle="Solid" 
                        BorderWidth="1px">
                        <EditRowStyle BorderColor="White" HorizontalAlign="Left" 
                            VerticalAlign="Middle" Wrap="False" />
                        <EmptyDataRowStyle BorderColor="Black" BorderStyle="Solid" 
                            HorizontalAlign="Left" VerticalAlign="Middle" />
                        <AlternatingRowStyle BackColor="#CCCCCC" />
                         <Columns>
	                        <asp:BoundField  DataField="Disease_Name" HeaderText="Disease Name" />
                            <asp:BoundField  DataField="Case_Count" HeaderText="Case Count" />
                            <asp:BoundField  DataField="Control_Count" HeaderText="Control Count" />
                            <asp:BoundField  DataField="Gene_Name" HeaderText="Gene Name" />
                            <asp:BoundField  DataField="SNP" HeaderText="SNP" />
                            <asp:BoundField  DataField="Frequency_Control" HeaderText="Frequency Control" />
                            <asp:BoundField  DataField="Frequency_Patient" HeaderText="Frequency Patient" />
                            <asp:BoundField  DataField="P_Value" HeaderText="P Value" />
                            <asp:BoundField  DataField="OR_Value" HeaderText="OR Value" />
                            <asp:BoundField  DataField="Reference" HeaderText="Author" />
                        </Columns>
                        <FooterStyle BackColor="#93A985" Height="10px" />
                        <HeaderStyle BackColor="#93A985" Font-Bold="True" ForeColor="White" 
                            VerticalAlign="Middle" Wrap="False" />
                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                        <SortedAscendingHeaderStyle BackColor="#808080" />
                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                        <SortedDescendingHeaderStyle BackColor="#383837" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

