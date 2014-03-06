<%@ Page Title="" Language="C#" MasterPageFile="~/Yeni.master" AutoEventWireup="true" CodeFile="MetaAnalysis.aspx.cs" Inherits="Disasters" EnableEventValidation="false"%>
<asp:Content ID="ContentHead" ContentPlaceHolderID="headContent" runat="server">
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
	<script type="text/javascript">
	    function divexpandcollapse(divname) {
	        var img = "img" + divname;
	        if ($("#" + img).attr("src") == "images/plus.png") {
	            $("#" + img).closest("tr")
				.after("<tr><td></td><td colspan = '100%'>" + $("#" + divname)
				.html() + "</td></tr>");
	            $("#" + img).attr("src", "images/minus.png");
	        } else {
	            $("#" + img).closest("tr").next().remove();
	            $("#" + img).attr("src", "images/plus.png");
	        }
	    }
	</script>
  
    <link rel="stylesheet" type="text/css" href="css/Table.css" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div>
        <table>
            <thead>
            <tr>
                <th style="width:50%">
                    <asp:Label ID="Label1" runat="server" Text="Select a Disease to Examine Researches:"></asp:Label>              
                </th>
                 <th> <asp:DropDownList ID="DiseasesList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="showDiseaseDetails" > </asp:DropDownList></th>

            </tr>
                </thead>
         </table>

        <div id="excelButton">
                        <asp:ImageButton Visible="false" ID="excelAktar" runat="server" 
                            ImageUrl="~/images/excelAktar.jpg" BorderColor="#333333" 
                            BorderStyle="Ridge" onclick="excelAktar_Click" ImageAlign="Right" />
        </div>

    
                    <asp:GridView ID="grdViewCustomers" runat="server" AutoGenerateColumns="False" DataKeyNames="SNP"
                        OnRowDataBound="grdViewCustomers_OnRowDataBound" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Both" AllowSorting="True">
			           
			            <Columns>
				            <asp:TemplateField ItemStyle-Width="20px">
					            <ItemTemplate>
						            <a href="JavaScript:divexpandcollapse('div<%# Eval("SNP") %>');">
							            <img alt="Details" id='imgdiv<%# Eval("SNP") %>' src="images/plus.png" />
						            </a>
						            <div id='div<%# Eval("SNP") %>' style="display: none;">
							            <asp:GridView ID="grdViewOrdersOfCustomer" runat="server" AutoGenerateColumns="false"
								            DataKeyNames="SNP" CssClass="ChildGrid" AllowPaging="False">
								            <Columns>
                                                <asp:BoundField  DataField="Gene_Name" HeaderText="Gene Name" />
                                                <asp:BoundField  DataField="Case_Count" HeaderText="Case Count" />
                                                <asp:BoundField  DataField="Frequency_Patient" HeaderText="Frequency Patient" />
                                                <asp:BoundField  DataField="Control_Count" HeaderText="Control Count" />
                                                <asp:BoundField  DataField="Frequency_Control" HeaderText="Frequency Control" />
                                                <asp:BoundField  DataField="P_Value" HeaderText="P Value" />
                                                <asp:BoundField  DataField="OR_Value" HeaderText="OR Value" />
                                                <asp:BoundField  DataField="Reference" HeaderText="Reference" />
                                                <asp:BoundField  DataField="SNP" HeaderText="SNP" Visible="false" />

                                            </Columns>
							            </asp:GridView>
                                    </div>
					            </ItemTemplate>
                                <ItemStyle Width="20px"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkRow" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
				            <asp:BoundField   DataField="SNP" HeaderText="SNP" />
                            <asp:BoundField  DataField="OR_Value" HeaderText="OR Value" />
                            <asp:BoundField  DataField="P_Value" HeaderText="Z Value" />
                            <asp:BoundField  DataField="I2" HeaderText="I2" />
                            <asp:BoundField  DataField="NumOfPublications" HeaderText="No Of Publications" />
			            </Columns>
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

