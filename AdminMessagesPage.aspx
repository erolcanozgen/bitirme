<%@ Page Title="" Language="C#" MasterPageFile="~/Yeni.master" AutoEventWireup="true" CodeFile="AdminMessagesPage.aspx.cs" Inherits="AdminMessagesPage" %>

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
<asp:Content ID="Content2" ContentPlaceHolderID="DiseaseList" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container">
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
                <td style="border-left-style: solid; border-left-width: 1px; border-left-color: #C0C0C0; vertical-align:top; margin-left:10px">
                    <asp:DataPager ID="MessagesPager" runat="server"
                       PagedControlID="ListView1" PageSize="5">
                       <Fields>
                          <asp:NextPreviousPagerField ButtonType="Button" ButtonCssClass="buttonCss" ShowFirstPageButton="True" 
                                      ShowLastPageButton="True"/>
                       </Fields>
                    </asp:DataPager>

                   <asp:ListView ID="ListView1" runat="server" GroupItemCount="1" GroupPlaceholderID="groupPlaceHolder1"
                        ItemPlaceholderID="itemPlaceHolder1">
                        <LayoutTemplate>
                            <table>
                                <asp:PlaceHolder runat="server" ID="groupPlaceHolder1"></asp:PlaceHolder>
                            </table>
                        </LayoutTemplate>
                        <GroupTemplate>
                            <tr>
                                <asp:PlaceHolder runat="server" ID="itemPlaceHolder1"></asp:PlaceHolder>
                            </tr>
                        </GroupTemplate>
                        <ItemTemplate>
                            <tr>
                                <table border="1" style="padding: 2px; width: 800px; height: 100px;
                                    border: 1px dashed #000000; background-color: #B0E2F5">
                                    <tr>
                                        <td>
                                            <asp:Label ID="LabelName" runat="server" Text="Name:" Font-Bold="True"></asp:Label><span><%# Eval("name") %></span><br />
                                            <asp:Label ID="LabelEmail" runat="server" Text="E-mail:" Font-Bold="True"></asp:Label><span><%# Eval("email") %></span><br />
                                            <asp:Label ID="LabelSubject" runat="server" Text="Subject:" Font-Bold="True"></asp:Label><span><%# Eval("subject")%></span><br />
                                            <asp:Label ID="LabelDate" runat="server" Text="Date:" Font-Bold="True"></asp:Label><span><%# Eval("messageDate")%></span><br />                                           
                                        </td>
                                        <td style="text-align:center">
                                            <asp:TextBox ID="txtMessage" runat="server" TextMode="MultiLine" ReadOnly="true" Text='<%#Eval("message")%>' Width="550px" Height="80px"></asp:TextBox>
                                        </td>
                                        <td style="text-align:center">
                                            <asp:Button ID="ButtonDelete" runat="server" Text="Delete" CssClass="buttonCss" OnClick="ButtonDelete_Click"/>
                                        </td>
                                    </tr>
                                </table>
                            </tr>
                        </ItemTemplate>
                    </asp:ListView>
                </td>
            </tr>
        </table>
        </div>
        <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.6.4/jquery.min.js"></script>
</asp:Content>

