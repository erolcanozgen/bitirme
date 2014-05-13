<%@ Page Title="" Language="C#" MasterPageFile="~/Yeni.master" AutoEventWireup="true" CodeFile="DeleteMember.aspx.cs" Inherits="DeleteMember" %>
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
      <script src="Scripts/ErrorSuccessNotifier.js"></script>
       <link href="Styles/ErrorSuccessNotifier.css" rel="stylesheet" />
        
        <link href='http://fonts.googleapis.com/css?family=Terminal+Dosis' rel='stylesheet' type='text/css' />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
                                <a href="DeleteMember.aspx">
                                        
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
                                    <asp:GridView ID="grdViewMembers" runat="server" AutoGenerateColumns="False" DataKeyNames="ID"
                                        CellPadding="3" ForeColor="Black" CssClass="table2" AllowSorting="True" AllowPaging="True">
			                            <Columns>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkRow" runat="server" Width="20px" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField  DataField="ID" HeaderText="Id" Visible="false"/>
                                            <asp:BoundField  DataField="Username" HeaderText="User Name" />
                                            <asp:BoundField  DataField="Name" HeaderText="Name" />
                                            <asp:BoundField  DataField="Surname" HeaderText="Surname" />
                                            <asp:BoundField  DataField="Email" HeaderText="E-Mail" />
			                            </Columns>
		                                <RowStyle HorizontalAlign="Left" VerticalAlign="Middle" />
		                            </asp:GridView>
                              
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align:left">
                                    <asp:Button Style="margin-left:20px" ID="deleteMembersBtn" runat="server" Text=" Delete Selected Members " class="buttonCss" OnClick="buttonDeleteMembers_Click" />
                                </td>
                            </tr>
                        </table>
                </td>
            </tr>
        </table>
        </asp:Panel> 
    </div>
</asp:Content>

