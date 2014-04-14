<%@ Page Title="" Language="C#" MasterPageFile="~/Yeni.master" AutoEventWireup="true" CodeFile="AddedResearches.aspx.cs" Inherits="AddedResearches" %>

<%@ Register Src="~/Notifier.ascx" TagPrefix="uc1" TagName="Notifier" %>


<asp:Content ID="Content1" ContentPlaceHolderID="headContent" Runat="Server">  
        <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1"> 
        <meta name="viewport" content="width=device-width, initial-scale=1.0">
        <meta name="keywords" content="menu, navigation, animation, transition, transform, rotate, css3, web design, component, icon, slide" />
        <link rel="shortcut icon" href="../favicon.ico"> 
        <link rel="stylesheet" type="text/css" href="css/VerticalNav/demo.css" />
        <link rel="stylesheet" type="text/css" href="css/VerticalNav/style1.css" />
        <link rel="stylesheet" type="text/css" href="css/Button.css" />
        <link href='http://fonts.googleapis.com/css?family=Terminal+Dosis' rel='stylesheet' type='text/css' />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <div class="container">
          <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto">
            <table style="width:100%">
                <tr>
                    <td style="width:200px; vertical-align:top">
                        <div >
                            <ul class="ca-menu">
                                <li>
                                    <a href="AddedResearches.aspx" >
                                        
                                        <div class="ca-content">
                                            <h2 class="ca-main">Added Researches</h2>
                                            <h3 class="ca-sub">See New Researches</h3>
                                        </div>
                                    </a>
                                </li>
                                <li>
                                    <a href="AdminMessagesPage.aspx">
                                        
                                        <div class="ca-content">
                                            <h2 class="ca-main">Messages</h2>
                                            <h3 class="ca-sub">See Coming Messages</h3>
                                        </div>
                                    </a>
                                </li>
                                <li>
                                    <a href="#">
                                        
                                        <div class="ca-content">
                                            <h2 class="ca-main">Delete Member</h2>
                                            <h3 class="ca-sub">Delete A Member</h3>
                                        </div>
                                    </a>
                                </li>
                            </ul>
                        </div><!-- content -->
            
                </td>
                <td style="border-left-style: solid; border-left-width: 1px; border-left-color: #C0C0C0">
                    <uc1:Notifier runat="server" ID="notifier" />
                    <table class="searchPanel">
                        <tr>
                            <td colspan="2">
                                
                                <asp:GridView ID="grdViewUnapprovedDiseases" runat="server" AutoGenerateColumns="False" DataKeyNames="ID,Disease_Name,SNP"
                                    BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" AllowSorting="True" AllowPaging="True">
			                        <AlternatingRowStyle BackColor="#CCCCCC" />
			                        <Columns>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkRow" runat="server" Width="20px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField  DataField="ID" HeaderText="Id" Visible="false"/>
                                        <asp:BoundField  DataField="Disease_Name" HeaderText="Disease Name" />
				                        <asp:BoundField  DataField="Gene_Name" HeaderText="Gene Name" />
                                        <asp:BoundField  DataField="SNP" HeaderText="SNP" />
                                        <asp:BoundField  DataFormatString="{0:F2}" DataField="Case_Count" HeaderText="Case Count" />
                                        <asp:BoundField  DataFormatString="{0:F2}" DataField="Control_Count" HeaderText="Control Count" />
                                        <asp:BoundField  DataFormatString="{0:F2}" DataField="Frequency_Control" HeaderText="Frequency Control" />
                                        <asp:BoundField  DataFormatString="{0:F2}" DataField="Frequency_Patient" HeaderText="Frequency Patient" />
                                        <asp:BoundField  DataFormatString="{0:F2}" DataField="OR_Value" HeaderText="OR Value" />
                                        <asp:BoundField  DataFormatString="{0:F2}" DataField="P_Value" HeaderText="P Value" />
                                        <asp:BoundField  DataField="Reference" HeaderText="Reference" />
                                        <asp:BoundField  DataField="ownerOfPublication" HeaderText="Owner" />
			                        </Columns>
		                            <FooterStyle BackColor="#CCCCCC" />
                                    <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                    <RowStyle Height="20px" />
                                    <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" BorderColor="#3399FF" BorderStyle="Solid" />
                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                    <SortedAscendingHeaderStyle BackColor="#808080" />
                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                    <SortedDescendingHeaderStyle BackColor="#383838" />
		                        </asp:GridView>
                              
                            </td>
                        </tr>
                        <tr>
                           
                            <td style="text-align:right">
                                <asp:Button ID="buttonApprove" runat="server" Text=" Approve Selected Publications " class="buttonCss" OnClick="buttonApprove_Click" />
                            </td>
                            <td style="text-align:left">
                                <asp:Button ID="buttonReject" runat="server" Text=" Delete Selected Publications " class="buttonCss" OnClick="buttonApprove_Click" />
                            </td>
                        </tr>
                    </table>
                    
                </td>
            </tr>
        </table>
              </asp:Panel>
        </div>
        <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.6.4/jquery.min.js"></script>
</asp:Content>


