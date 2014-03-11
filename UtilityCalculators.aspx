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


     <asp:Panel Visible="false" ID="results_panel" runat="server" ScrollBars="Auto">

      <div style="float:left;width:300px" aria-disabled="False">
            
         
          
              <table>
                   <thead>
			             <tr><th> Odds Ratio </th></tr>
            
	                </thead>

                    <tbody>
                        <tr>
                            <td>
                            <asp:Label ID="lbl_or_value" runat="server" Text="Or Value: "></asp:Label>
                            </td>
                             <td style="text-align:left">
                            <asp:TextBox ID="txt_or_value" Width="45px" runat="server" ReadOnly= "true"></asp:TextBox>
                            </td>
                        </tr>

                         <tr>
                            <td>
                            <asp:Label ID="lbl_or_variance" runat="server" Text=" Variance(OR): "></asp:Label>
                            </td>
                             <td style="text-align:left">
                            <asp:TextBox ID="txt_or_variance" Width="45px" runat="server" ReadOnly= "true"></asp:TextBox>
                            </td>
                        </tr>

                             <tr>
                            <td>
                            <asp:Label ID="lbl_ln_or" runat="server" Text=" ln(OR): "></asp:Label>
                            </td>
                             <td style="text-align:left">
                            <asp:TextBox ID="txt_ln_or" Width="45px" runat="server" ReadOnly= "true"></asp:TextBox>
                            </td>
                        </tr>

                            <tr>
                            <td>
                            <asp:Label ID="lbl_CI_or" runat="server" Text=" %95 Confidence Interval: "></asp:Label>
                            </td>
                             <td style="text-align:left">
                            <asp:TextBox ID="txt_CI_or" Width="105px" runat="server" ReadOnly= "true"></asp:TextBox>
                            </td>
                        </tr>

                         
                    </tbody>


              </table>
              </div>

              <div style="float:left;width:300px" aria-disabled="False">
                <table>
                     <thead>
			             <tr><th> Risk Ratio </th></tr>
            
	                </thead>

                    <tbody>
                     <tr>
                            <td>
                            <asp:Label ID="lbl_rr_value" runat="server" Text="Risk Ratio: "></asp:Label>
                            </td>
                             <td style="text-align:left">
                            <asp:TextBox ID="txt_rr_value" Width="45px" runat="server" ReadOnly= "true"></asp:TextBox>
                            </td>
                        </tr>

                         <tr>
                            <td>
                            <asp:Label ID="lbl_rr_variance" runat="server" Text=" Variance(RR): "></asp:Label>
                            </td>
                             <td style="text-align:left">
                            <asp:TextBox ID="txt_rr_variance" Width="45px" runat="server" ReadOnly= "true"></asp:TextBox>
                            </td>
                        </tr>

                            <tr>
                            <td>
                            <asp:Label ID="lbl_ln_rr" runat="server" Text=" ln(RR): "></asp:Label>
                            </td>
                             <td style="text-align:left">
                            <asp:TextBox ID="txt_ln_rr" Width="45px" runat="server" ReadOnly= "true"></asp:TextBox>
                            </td>
                        </tr>

                         <tr>
                            <td>
                            <asp:Label ID="lbl_CI_rr" runat="server" Text=" %95 Confidence Interval: "></asp:Label>
                            </td>
                             <td style="text-align:left">
                            <asp:TextBox ID="txt_CI_rr" Width="105px" runat="server" ReadOnly= "true"></asp:TextBox>
                            </td>
                        </tr>


                        </tbody>
                </table>
                  
              </div>


            <table style="position:relative;left:112px;">
                <thead>
                    <th></th>

                </thead>

                <tbody>

                     <tr>
                            <td>
                            <asp:Label ID="lbl_chi_square_without_Yates" runat="server" Text=" Chi-Square Without Yates Correction: "></asp:Label>
                            </td>
                             <td style="text-align:left">
                            <asp:TextBox ID="txt_chi_square_without_Yates" Width="45px" runat="server" ReadOnly= "true"></asp:TextBox>
                            </td>
                        </tr>

                        <tr>
                            <td>
                            <asp:Label ID="lbl_chi_square_with_Yates" runat="server" Text=" Chi-Square With Yates Correction: "></asp:Label>
                            </td>
                             <td style="text-align:left">
                            <asp:TextBox ID="txt_chi_square_with_Yates" Width="45px" runat="server" ReadOnly= "true"></asp:TextBox>
                            </td>
                        </tr>




                </tbody>

            </table>



          </asp:Panel>
         

   </div>



 
       </asp:Content>