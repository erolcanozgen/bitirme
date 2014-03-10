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
			<th> Experement </th>
            <th> Yes </th>
            <th> No </th>
		</tr>
	</thead>
             <tbody>
                    
                 <tr>
                        <td>
                            <asp:Label ID="lblCaseCount" runat="server" Text="Case"></asp:Label>
                        </td>
                         <td style="text-align:left">
                             <asp:TextBox ID="txtCaseYes" runat="server" Width="30px" ></asp:TextBox>
                             <asp:RegularExpressionValidator Display="Dynamic" ID="revNumericValidator" runat="server" 
                             ValidationExpression="^[0-9]*$" ControlToValidate="txtCaseYes" ErrorMessage="Must be Numeric" />
                             <asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server"
                            ControlToValidate="txtCaseYes" Display="Dynamic"
                            ErrorMessage="** required field."
                            ForeColor="Red">
                            </asp:RequiredFieldValidator>
                         </td>
                          <td style="text-align:left">
                             <asp:TextBox ID="txtCaseNo" runat="server" Width="30px"></asp:TextBox>
                             <asp:RegularExpressionValidator Display="Dynamic" ID="RegularExpressionValidator1" runat="server" 
                             ValidationExpression="^[0-9]*$" ControlToValidate="txtCaseNo" ErrorMessage="Must be Numeric" />
                             <asp:RequiredFieldValidator id="RequiredFieldValidator2" runat="server"
                            ControlToValidate="txtCaseNo" Display="Dynamic"
                            ErrorMessage="** required field."
                            ForeColor="Red">
                            </asp:RequiredFieldValidator>
                            </td>



                 </tr>

            
                   <tr>
                        <td>
                            <asp:Label ID="lblControlCount" runat="server" Text="Control"></asp:Label>
                        </td>
                         <td style="text-align:left">
                             <asp:TextBox ID="txtControlYes" runat="server" Width="30px"></asp:TextBox>
                             <asp:RegularExpressionValidator Display="Dynamic" ID="RegularExpressionValidator2" runat="server" 
                             ValidationExpression="^[0-9]*$" ControlToValidate="txtControlYes" ErrorMessage="Must be Numeric" />
                             <asp:RequiredFieldValidator id="RequiredFieldValidator3" runat="server"
                            ControlToValidate="txtControlYes" Display="Dynamic"
                            ErrorMessage="** required field."
                            ForeColor="Red">
                            </asp:RequiredFieldValidator>
                         </td>
                        <td style="text-align:left">
                             <asp:TextBox ID="txtControlNo" runat="server" Width="30px"></asp:TextBox>
                             <asp:RegularExpressionValidator Display="Dynamic" ID="RegularExpressionValidator3" runat="server" 
                             ValidationExpression="^[0-9]*$" ControlToValidate="txtControlNo" ErrorMessage="Must be Numeric" />
                             <asp:RequiredFieldValidator id="RequiredFieldValidator4" runat="server"
                            ControlToValidate="txtControlNo" Display="Dynamic"
                            ErrorMessage="** required field."
                            ForeColor="Red">
                            </asp:RequiredFieldValidator>
                         </td>
                 </tr>

              
             </tbody>
             <tfoot>
           <tr>
           <td style="text-align:right" colspan="3">
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