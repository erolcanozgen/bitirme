<%@ Page Title="" Language="C#" MasterPageFile="~/Yeni.master" AutoEventWireup="true" CodeFile="MetaAnalysis.aspx.cs" Inherits="Disasters" EnableEventValidation="false"%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
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

	    function html_entity_decode(str) {
	        var p = document.createElement("p");
	        p.innerHTML = str.replace(/</g, "&lt;").replace(/>/g, "&gt;");
	        return p.innerHTML;
	    }


	</script>
    <style type="text/css">
        .modalBackground {
            background-color:white;
            filter:alpha(opacity=70);
            opacity:0.8;
        }
    </style>
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

    
        <asp:GridView ID="grdViewCustomers" runat="server" AutoGenerateColumns="False" DataKeyNames="SNP,tmpId" onsorting="gvDetails_Sorting"
            OnRowDataBound="grdViewCustomers_OnRowDataBound" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Both" AllowSorting="True" AllowPaging="True">
			           
			<Columns>
				<asp:TemplateField ItemStyle-Width="20px">
					<ItemTemplate>
						<a href="JavaScript:divexpandcollapse('div<%# Eval("tmpId") %>');">
							<img alt="Details" id='imgdiv<%# Eval("tmpId") %>' src="images/plus.png" />
						</a>
						<div id='div<%# Eval("tmpId") %>' style="display: none;">
							<asp:GridView ID="grdViewOrdersOfCustomer" runat="server" AutoGenerateColumns="false"
								DataKeyNames="SNP" CssClass="ChildGrid" AllowPaging="False">
								<Columns>
                                        <asp:TemplateField HeaderText="Gene Name">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="Gene_Name" runat="server"></asp:HyperLink>
                                            </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField  DataFormatString="{0:F2}" DataField="Case_Count" HeaderText="Case Count" />
                                    <asp:BoundField  DataFormatString="{0:F2}" DataField="Frequency_Patient" HeaderText="Frequency In Case" />
                                    <asp:BoundField  DataFormatString="{0:F2}" DataField="Control_Count" HeaderText="Control Count" />
                                    <asp:BoundField  DataFormatString="{0:F2}" DataField="Frequency_Control" HeaderText="Frequency In Control" />
                                    <asp:BoundField  DataFormatString="{0:F2}" DataField="P_Value" HeaderText="P Value" />
                                    <asp:BoundField  DataFormatString="{0:F2}" DataField="OR_Value" HeaderText="OR Value" />
                                    <asp:TemplateField HeaderText="Reference">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="Link" runat="server"></asp:HyperLink>
                                            <asp:Label runat="server" ID="lbl_reference" Visible="false"></asp:Label>
                                            <asp:LinkButton ID="seeDetailsBtn" runat="server" OnClick="ShowPopup">See Details</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField  DataField="SNP" HeaderText="SNP" Visible="false" />
                                    <asp:BoundField  DataField="tmpId" HeaderText="tmpId" Visible="false" />
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
				<asp:BoundField  DataField="SNP" HeaderText="SNP" SortExpression="SNP"/>
                <asp:BoundField  DataFormatString="{0:F2}" DataField="OR_Value" HeaderText="Meta OR Value" SortExpression="OR_Value"/>
                <asp:BoundField  DataFormatString="{0:F2}" DataField="P_Value" HeaderText="Z Value" SortExpression="P_Value"/>
                <asp:BoundField  DataFormatString="{0:F2}" DataField="I2" HeaderText="I2" SortExpression="I2"/>
                <asp:BoundField  DataField="NumOfPublications" HeaderText="No Of Publications" SortExpression="NumOfPublications"/>
                <asp:BoundField  DataField="tmpId" HeaderText="tmpId" Visible="false" />
			</Columns>
            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#808080" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#383838" />
		</asp:GridView>

        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
        <asp:Button ID="Button1" runat="server" Text="Button" style="display:none" />
        <asp:ModalPopupExtender ID="Button1_ModalPopupExtender"
            runat="server"  Enabled="True" 
            PopupControlID="popUpTable" TargetControlID="Button1" 
            CancelControlID="Button3" BackgroundCssClass="modalBackground">
        </asp:ModalPopupExtender>
        <table id="popUpTable" class="table2" style="padding:0px 0px 0px 0px; width:550px; height:200px">
                <thead>
		            <tr>
			            <th>Reference</th>
		            </tr>
	            </thead>
                <tr style="height:180px; padding:0px 0px 0px 0px;">
                    <td  style="padding:0px 0px 0px 0px;">
                        <asp:TextBox ID="referenceTxt" Height="180px" Width="550px" runat="server" TextMode="MultiLine" ReadOnly="True"></asp:TextBox>
                    </td>
                </tr>
                <tfoot  style="padding:0px 0px 0px 0px;">
                    <tr style="float:right; padding:0px 0px 0px 0px;">
                        <td  style="padding:0px 0px 0px 0px;">
                            <asp:Button ID="Button3"  style="padding:0px 0px 0px 0px;" runat="server" Text=" Close " />
                        </td>
                    </tr>
                </tfoot>
        </table>
	</div>
</asp:Content>

