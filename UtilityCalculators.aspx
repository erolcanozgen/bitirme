<%@ Page  Title="" Language="C#" MasterPageFile="~/Yeni.master" AutoEventWireup="true" CodeFile="UtilityCalculators.aspx.cs" Inherits="UtilityCalculators" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="ContentHead" ContentPlaceHolderID="headContent" runat="server">
      <link href="css/style.css" type="text/css" rel="stylesheet"/>
     <link href="css/Table.css" type="text/css" rel="stylesheet"/>
    <link href="css/Button.css" type="text/css" rel="stylesheet"/>

     
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server" >

<div>
   
    <div style="float:left;width:345px">
                
         <table id="filterTable">
      
             <thead>
		<tr>
			<th colspan="2"> </th>
		</tr>
	</thead>
             <tbody>
                    
                 <tr>
                        <td>
                            <asp:Label ID="lblCaseCount" runat="server" Text="Case Count"></asp:Label>
                        </td>
                         <td style="text-align:left">
                             <asp:TextBox ID="txtCaseCount" runat="server" Width="30px" ></asp:TextBox>
                         </td>
                 </tr>

                 <tr>
                        <td>
                            <asp:Label ID="lblCaseYes" runat="server" Text="The number of people who carry genes in Case "></asp:Label>
                        </td>
                         <td style="text-align:left">
                             <asp:TextBox ID="txtCaseYes" runat="server" Width="30px"></asp:TextBox>
                         </td>
                 </tr>

                   <tr>
                        <td>
                            <asp:Label ID="lblControlCount" runat="server" Text="Control Count "></asp:Label>
                        </td>
                         <td style="text-align:left">
                             <asp:TextBox ID="txtControlCount" runat="server" Width="30px"></asp:TextBox>
                         </td>
                 </tr>

                  <tr>
                        <td>
                            <asp:Label ID="lblControlYes" runat="server" Text="The number of people who carry genes in Control "></asp:Label>
                        </td>
                         <td style="text-align:left">
                             <asp:TextBox ID="txtControlYes" runat="server" Width="30px"></asp:TextBox>
                         </td>
                 </tr>

              
             </tbody>
             <tfoot>
           <tr>
           <td style="text-align:right" colspan="2">
               <asp:Button runat="server" ID="btnCalculate" CssClass="buttonCss" Text=" Calculate " OnClick="btnCalculate_Click"/>
           </td>
           </tr>
             </tfoot>
          </table>

    </div>

      <div id="DiseasesTable" style="float:right;width:625px" aria-disabled="False">
            
          <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto">
          
          </asp:Panel>
         </div>

   </div>



 
       </asp:Content>