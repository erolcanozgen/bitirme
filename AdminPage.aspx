<%@ Page Title="" Language="C#" MasterPageFile="~/Yeni.master" AutoEventWireup="true" CodeFile="AdminPage.aspx.cs" Inherits="AdminPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" Runat="Server">  
        <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1"> 
        <meta name="viewport" content="width=device-width, initial-scale=1.0">
        <meta name="keywords" content="menu, navigation, animation, transition, transform, rotate, css3, web design, component, icon, slide" />
        <link rel="shortcut icon" href="../favicon.ico"> 
        <link rel="stylesheet" type="text/css" href="css/VerticalNav/demo.css" />
        <link rel="stylesheet" type="text/css" href="css/VerticalNav/style1.css" />
        <link href='http://fonts.googleapis.com/css?family=Terminal+Dosis' rel='stylesheet' type='text/css' />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <div class="container">
            <table style="width:100%">
                <tr>
                    <td style="width:220px">
                        <div >
                            <ul class="ca-menu">
                                <li>
                                    <a href="#">
                                        
                                        <div class="ca-content">
                                            <h2 class="ca-main">Added Researches</h2>
                                            <h3 class="ca-sub">See New Researches</h3>
                                        </div>
                                    </a>
                                </li>
                                <li>
                                    <a href="#">
                                        
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
                    &nbsp;
                </td>
            </tr>
        </table>
        </div>
        <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.6.4/jquery.min.js"></script>
</asp:Content>
