<%@ Page Title="" Language="C#" MasterPageFile="~/Yeni.master" AutoEventWireup="true" CodeFile="Diseases.aspx.cs" Inherits="Disasters" %>
<asp:Content ID="ContentHead" ContentPlaceHolderID="headContent" runat="server">
	<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
	<script type="text/javascript">
	    function divexpandcollapse(divname) {
	        var img = "img" + divname;
	        if ($("#" + img).attr("src") == "images/plus.png") {
	            $("#" + img)
				.closest("tr")
				.after("<tr><td></td><td colspan = '100%'>" + $("#" + divname)
				.html() + "</td></tr>");
	            $("#" + img).attr("src", "images/minus.png");
	        } else {
	            $("#" + img).closest("tr").next().remove();
	            $("#" + img).attr("src", "images/plus.png");
	        }
	    }
	</script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
        <asp:Label ID="Label1" runat="server" Text="Select a disease to examine researches:"></asp:Label>
        <br />&nbsp;&nbsp;
        <asp:DropDownList ID="DiseasesList" runat="server" 
            AutoPostBack="True" onselectedindexchanged="showDiseaseDetails"> </asp:DropDownList>
                                
        <asp:ImageButton Visible="false" ID="excelAktar" runat="server" 
            ImageUrl="~/images/excelAktar.jpg" BorderColor="#93A985" 
            BorderStyle="Ridge" onclick="excelAktar_Click" ImageAlign="Right" />
                
    </div>
    <div>
		<asp:GridView ID="grdViewCustomers" runat="server" AutoGenerateColumns="False" DataKeyNames="Table_Name"
            OnRowDataBound="grdViewCustomers_OnRowDataBound" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical">
			<AlternatingRowStyle BackColor="#CCCCCC" />
			<Columns>
				<asp:TemplateField ItemStyle-Width="20px">
					<ItemTemplate>
						<a href="JavaScript:divexpandcollapse('div<%# Eval("Id") %>');">
							<img alt="Details" id='imgdiv<%# Eval("Id") %>' src="images/plus.png" />
						</a>
						<div id='div<%# Eval("Id") %>' style="display: none;">
							<asp:GridView ID="grdViewOrdersOfCustomer" runat="server" AutoGenerateColumns="false"
								DataKeyNames="Disease_Name" CssClass="ChildGrid">
								<Columns>
                                    <asp:BoundField  DataField="Id" HeaderText="Id" Visible="false" />
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
							</asp:GridView>
						</div>
					</ItemTemplate>

                    <ItemStyle Width="20px"></ItemStyle>
				</asp:TemplateField>
				<asp:BoundField  DataField="Table_Name" HeaderText="Disease Name" />
			</Columns>
		    <FooterStyle BackColor="#CCCCCC" />
            <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#808080" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#383838" />
		</asp:GridView>
	</div>
<%--    <div>
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
    </div>--%>
</asp:Content>

