<%@ Page Title="" Language="C#" MasterPageFile="~/Yeni.master" AutoEventWireup="true" CodeFile="AddedResearches.aspx.cs" Inherits="AddedResearches" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/Notifier.ascx" TagPrefix="uc1" TagName="Notifier" %>


<asp:Content ID="Content1" ContentPlaceHolderID="headContent" Runat="Server">  
        <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1"> 
        <meta name="viewport" content="width=device-width, initial-scale=1.0">
        <meta name="keywords" content="menu, navigation, animation, transition, transform, rotate, css3, web design, component, icon, slide" />
        <link rel="shortcut icon" href="../favicon.ico"> 
        <link rel="stylesheet" type="text/css" href="css/VerticalNav/demo.css" />
        <link rel="stylesheet" type="text/css" href="css/VerticalNav/style1.css" />
        <link rel="stylesheet" type="text/css" href="css/Button.css" />
        <link href="css/Table2.css" type="text/css" rel="stylesheet"/>
        <style type="text/css">
        .modalBackground {
            background-color:white;
            filter:alpha(opacity=70);
            opacity:0.8;
        }
    </style>
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
                        <table class="Table2">
                            <tr>
                                <td >
                                
                                    <asp:GridView ID="grdViewUnapprovedDiseases" runat="server" AutoGenerateColumns="False" DataKeyNames="ID,Disease_Name,SNP"
                                        CellPadding="3" ForeColor="Black" CssClass="table2" AllowSorting="True" AllowPaging="True" OnPageIndexChanging="grdViewUnapprovedDiseases_PageIndexChanging">
			                            <Columns>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkRow" runat="server" Width="20px" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField  DataField="ID" HeaderText="Id" Visible="false"/>
                                            <asp:BoundField  DataField="Disease_Name" HeaderText="Disease Name" />
				                            <asp:TemplateField HeaderText="Gene Name">
                                                <ItemTemplate>
                                                      <asp:HyperLink ID="Gene_Name" runat="server" Text='<%# Bind("Gene_Name") %>'></asp:HyperLink>
                                                </ItemTemplate>
                                           </asp:TemplateField>
                                            <asp:BoundField  DataField="SNP" HeaderText="SNP" />
                                            <asp:BoundField  DataFormatString="{0:F2}" DataField="Case_Count" HeaderText="Case Count" />
                                            <asp:BoundField  DataFormatString="{0:F2}" DataField="Control_Count" HeaderText="Control Count" />
                                            <asp:BoundField  DataFormatString="{0:F2}" DataField="Frequency_Control" HeaderText="Frequency Control" />
                                            <asp:BoundField  DataFormatString="{0:F2}" DataField="Frequency_Patient" HeaderText="Frequency Patient" />
                                            <asp:BoundField  DataFormatString="{0:F2}" DataField="OR_Value" HeaderText="OR Value" />
                                            <asp:BoundField  DataFormatString="{0:F2}" DataField="P_Value" HeaderText="P Value" />
                                            <asp:TemplateField HeaderText="Reference">
                                                 <ItemTemplate>
                                                    <asp:HyperLink ID="Link" runat="server" Text='<%# Bind("Reference") %>' ></asp:HyperLink>
                                                    <asp:Label runat="server" ID="lbl_reference" Visible="false" Text='<%# Bind("Reference") %>'></asp:Label>
                                                    <asp:LinkButton ID="seeDetailsBtn" runat="server" OnClick="ShowPopup">See Details</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField  DataField="ownerOfPublication" HeaderText="Owner" />
			                            </Columns>
		                                <RowStyle HorizontalAlign="Left" VerticalAlign="Middle" />
		                            </asp:GridView>
                              
                                </td>
                            </tr>
                            <tr>
                           
                                <td style="text-align:left">
                                    <asp:Button Style="margin-left:20px" ID="buttonApprove" runat="server" Text=" Approve Selected Publications " class="buttonCss" OnClick="buttonApprove_Click" />
                                    <asp:Button Style="margin-left:20px" ID="buttonReject" runat="server" Text=" Delete Selected Publications " class="buttonCss" OnClick="buttonApprove_Click" />
                                </td>
                            </tr>
                        </table>
                </td>
            </tr>
        </table>
        </asp:Panel> 

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
                        <asp:TextBox ID="referenceLbl" Height="180px" Width="550px" runat="server" TextMode="MultiLine" ReadOnly="True"></asp:TextBox>
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
        <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.6.4/jquery.min.js"></script>
</asp:Content>


