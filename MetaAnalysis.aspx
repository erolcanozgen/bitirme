<%@ Page Title="" Language="C#" MasterPageFile="~/Yeni.master" AutoEventWireup="true" CodeFile="MetaAnalysis.aspx.cs" Inherits="Disasters" EnableEventValidation="false"%>
<%@ Register Src="~/Notifier.ascx" TagPrefix="uc1" TagName="Notifier" %>
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
        .modal
        {
            position: fixed;
            top: 0;
            left: 0;
            background-color: black;
            z-index: 99;
            opacity: 0.8;
            filter: alpha(opacity=80);
            -moz-opacity: 0.8;
            min-height: 100%;
            width: 100%;
        }
        .loading
        {
            font-family: Arial;
            font-size: 10pt;
            border: 5px solid #67CFF5;
            width: 120px;
            height: 110px;
            display: none;
            position: fixed;
            background-color: White;
            z-index: 999;
            text-align:center;
            vertical-align:middle;
        }
        .modalBackground {
            background-color:white;
            filter:alpha(opacity=70);
            opacity:0.8;
        }
    </style>
    <link rel="stylesheet" type="text/css" href="css/Table.css" />
    <link href="css/Button.css" rel="stylesheet" />
    <script src="Scripts/ErrorSuccessNotifier.js"></script>
    <link href="Styles/ErrorSuccessNotifier.css" rel="stylesheet" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        function ShowProgress() {
            setTimeout(function () {
                var modal = $('<div />');
                modal.addClass("modal");
                $('body').append(modal);
                var loading = $(".loading");
                loading.show();
                var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
                var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
                loading.css({ top: top, left: left });
            }, 200);
        }
        function btnEnrichment_Click() {
            ShowProgress();
        }
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:Notifier runat="server" ID="notifier" />
    <div>
        <table>
            <thead>
            <tr>
                <th>
                    <asp:Label ID="Label1" runat="server" Text="Select a Disease to Examine Researches:"></asp:Label>              
                </th>
                 <th> <asp:DropDownList ID="DiseasesList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="showDiseaseDetails" > </asp:DropDownList></th>
                <th><asp:Button runat="server" CssClass="buttonCss" Text=" Meta Analysis " ID="btnMetaAnalysis" OnClick="btnMetaAnalysis_Click" Visible="false" /></th>
                <th><asp:Button runat="server" CssClass="buttonCss" Text=" Pathway Analysis " ID="btnEnrichment" OnClick="btnEnrichment_Click" OnClientClick="btnEnrichment_Click()" /></th>
            </tr>
                </thead>
         </table>

        <div id="excelButton">
                        <asp:ImageButton Visible="false" ID="excelAktar" runat="server" 
                            ImageUrl="~/images/excelAktar.jpg" BorderColor="#333333"
                            BorderStyle="Ridge" onclick="excelAktar_Click" ImageAlign="Right" />
        </div>

        <asp:Panel runat="server">
        <asp:GridView ID="grdViewCustomers" runat="server" AutoGenerateColumns="False" DataKeyNames="SNP,tmpId" onsorting="gvDetails_Sorting" RowStyle-Wrap="false"
            OnRowDataBound="grdViewCustomers_OnRowDataBound" BorderStyle="Solid" BorderWidth="1px" ForeColor="Black" GridLines="Both" AllowSorting="True" AllowPaging="True" OnPageIndexChanging="grdViewCustomers_PageIndexChanging">
			           
			<Columns>
				<asp:TemplateField ItemStyle-Width="20px">
					<ItemTemplate>
						<a href="JavaScript:divexpandcollapse('div<%# Eval("tmpId") %>');">
							<img alt="Details" id='imgdiv<%# Eval("tmpId") %>' src="images/plus.png" />
						</a>
						<div id='div<%# Eval("tmpId") %>' style="display: none;">
							<asp:GridView ID="grdViewOrdersOfCustomer" runat="server" AutoGenerateColumns="false"
								DataKeyNames="SNP" AllowPaging="False" RowStyle-Wrap="false">
								<Columns>
                                    <asp:BoundField  DataField="Case_Count" HeaderText="Case Count" />
                                    <asp:BoundField  DataFormatString="{0:F2}" DataField="Frequency_Patient" HeaderText="Frequency In Case" />
                                    <asp:BoundField  DataField="Control_Count" HeaderText="Control Count" />
                                    <asp:BoundField  DataFormatString="{0:F2}" DataField="Frequency_Control" HeaderText="Frequency In Control" />
                                    <asp:BoundField  DataFormatString="{0:F2}" DataField="P_Value" HeaderText="P Value" />
                                    <asp:BoundField  DataFormatString="{0:F2}" DataField="OR_Value" HeaderText="OR Value" />
                                    <asp:BoundField  DataField="CI" HeaderText="95% CI" SortExpression="CI"/>
                                    <asp:TemplateField HeaderText="Reference">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="Link" Visible="false" runat="server"></asp:HyperLink>
                                            <asp:Label runat="server" ID="lbl_reference" Visible="false"></asp:Label>
                                            <asp:LinkButton ID="seeDetailsBtn" Visible="false" runat="server" OnClick="ShowPopup">See Details</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField  DataField="SNP" HeaderText="SNP" Visible="false" />
                                    <asp:BoundField  DataField="tmpId" HeaderText="tmpId" Visible="false" />
                                </Columns>
                                <RowStyle Height="40px" />
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
                <asp:TemplateField HeaderText="Gene Name">
                    <ItemTemplate>
                            <asp:HyperLink ID="Gene_Name" runat="server" Text='<%# Bind("Gene_Name") %>'></asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
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
        </asp:Panel>

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
    <div class="loading">
        <img src="images/loader.gif" alt=""/>
    </div>
</asp:Content>

