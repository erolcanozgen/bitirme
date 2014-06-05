<%@ Page Title="" Language="C#" MasterPageFile="~/Yeni.master" AutoEventWireup="true" CodeFile="HomePage.aspx.cs" Inherits="_Default" %>
<%@ MasterType VirtualPath="~/Yeni.master"  %>
<%@ Register Src="~/Notifier.ascx" TagPrefix="uc1" TagName="Notifier" %>
<asp:Content ID="Content2" ContentPlaceHolderID="headContent" Runat="Server">  
        <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1"> 
        <meta name="viewport" content="width=device-width, initial-scale=1.0">
        <meta name="keywords" content="menu, navigation, animation, transition, transform, rotate, css3, web design, component, icon, slide" />
        <link rel="shortcut icon" href="../favicon.ico"> 
        <link href="css/SignUp.css" type="text/css" rel="stylesheet"/>
        <link rel="stylesheet" type="text/css" href="css/Button.css" />
        <link href="css/Table2.css" type="text/css" rel="stylesheet"/>
        <link href='http://fonts.googleapis.com/css?family=Terminal+Dosis' rel='stylesheet' type='text/css' />
      <script src="Scripts/ErrorSuccessNotifier.js"></script>
       <link href="Styles/ErrorSuccessNotifier.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:Notifier runat="server" ID="notifier" />
        <table class="table2"  >
            <tr>
                <td style="width:620px;text-align:justify">
                    <h2 style="margin: 15px 0 0 0; font-size:40px">TUSNP</h2>
                    <p style="font-size:20px"> The Turkish SNP Database</p><br />
                </td>
            </tr>
            <tr >
                <td style="width:620px;text-align:justify">
                    <p style="font-size:15px">
                        TUSNP presents single nucleotide polymorphisms (SNPs) that have been previously associated with common diseases in Turkish population. 
                        We represent a freely accessible, updated, and gathered data of association studies showing risk or protective allelic SNP variants 
                        related to 21 common disorders. TUSNP provides the information about allelic frequencies in study groups with statistical significances 
                        (P values) and strengths of the relationships in terms of odds ratio (OR). The website also comprises other calculation tools allowing 
                        the users to obtain the maximum efficiency from the data. Registered users can publish their genetic data in TUSNP.
                    </p>
                </td>
            </tr>
            <tr>
                <td style="width:620px;text-align:left; font-size:15px">
                    Please <a href="ContactUs.aspx" style="color:#09D4FF">contact us</a> to report possible errors in the study details and to inform us of mistakenly left out studies.
                </td>
            </tr>
        </table>
        
</asp:Content>


