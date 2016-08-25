<%@ Page Title="" Language="C#" MasterPageFile="~/Yeni.master" AutoEventWireup="true" CodeFile="UtilityCalculators.aspx.cs" Inherits="UtilityCalculators" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/Notifier.ascx" TagPrefix="uc1" TagName="Notifier" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="headContent" runat="server">
    <link href="css/style.css" type="text/css" rel="stylesheet" />
    <link href="css/UtilityTable.css" type="text/css" rel="stylesheet" />
    <link href="css/Button.css" type="text/css" rel="stylesheet" />
      <script src="Scripts/ErrorSuccessNotifier.js"></script>
       <link href="Styles/ErrorSuccessNotifier.css" rel="stylesheet" />


</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:Notifier runat="server" ID="notifier" />
    <div>
        <asp:TabContainer runat="server">

            <asp:TabPanel runat="server" BorderColor="Black"  ScrollBars="Auto">
                <HeaderTemplate>
                    <asp:Label runat="server" ForeColor="Black" Text="Odds Ratio - Risk Ratio - Chi-Square" Font-Bold="true" Font-Italic="true" Font-Size="Small"></asp:Label>
                </HeaderTemplate>
                
                <ContentTemplate>
                <table style="width:100%">
                <tr>
                    <td style="vertical-align:top">
                        <div style="float: left; width: 345px;">

                        <table id="filterTable">
                             
                            <thead>
                                <tr>
                                    <th></th>
                                    <th>Allele1</th>
                                    <th>Allele2</th>
                                </tr>
                            </thead>
                            <tbody>

                                <tr>
                                    <td>
                                        <asp:Label ID="lblCaseCount" runat="server" Text="Number of Cases"></asp:Label>
                                    </td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtCaseYes" runat="server" Width="30px" autocomplete="off"></asp:TextBox>
                                        <asp:RegularExpressionValidator Display="Dynamic" ID="revNumericValidator" runat="server" ValidationGroup="result1"
                                            ValidationExpression="^[0-9]*$" ControlToValidate="txtCaseYes" ErrorMessage="Must be Numeric" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                            ControlToValidate="txtCaseYes" Display="Dynamic"
                                            ErrorMessage="** required field." ValidationGroup="result1"
                                            ForeColor="Red">
                                        </asp:RequiredFieldValidator>
                                    </td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtCaseNo" runat="server" Width="30px" autocomplete="off"></asp:TextBox>
                                        <asp:RegularExpressionValidator Display="Dynamic" ID="RegularExpressionValidator1" runat="server" ValidationGroup="result1"
                                            ValidationExpression="^[0-9]*$" ControlToValidate="txtCaseNo" ErrorMessage="Must be Numeric" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                            ControlToValidate="txtCaseNo" Display="Dynamic"
                                            ErrorMessage="** required field." ValidationGroup="result1"
                                            ForeColor="Red">
                                        </asp:RequiredFieldValidator>
                                    </td>



                                </tr>


                                <tr>
                                    <td>
                                        <asp:Label ID="lblControlCount" runat="server" Text="Number of Controls"></asp:Label>
                                    </td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtControlYes" runat="server" Width="30px" autocomplete="off"></asp:TextBox>
                                        <asp:RegularExpressionValidator Display="Dynamic" ID="RegularExpressionValidator2" runat="server" ValidationGroup="result1"
                                            ValidationExpression="^[0-9]*$" ControlToValidate="txtControlYes" ErrorMessage="Must be Numeric" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                            ControlToValidate="txtControlYes" Display="Dynamic"
                                            ErrorMessage="** required field." ValidationGroup="result1"
                                            ForeColor="Red">
                                        </asp:RequiredFieldValidator>
                                    </td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtControlNo" runat="server" Width="30px" autocomplete="off"></asp:TextBox>
                                        <asp:RegularExpressionValidator Display="Dynamic" ID="RegularExpressionValidator3" runat="server" ValidationGroup="result1"
                                            ValidationExpression="^[0-9]*$" ControlToValidate="txtControlNo" ErrorMessage="Must be Numeric" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                                            ControlToValidate="txtControlNo" Display="Dynamic"
                                            ErrorMessage="** required field." ValidationGroup="result1"
                                            ForeColor="Red">
                                        </asp:RequiredFieldValidator>
                                    </td>
                                </tr>


                            </tbody>
                            <tfoot>
                                <tr>
                                    <td style="text-align: right" colspan="3">
                                        <asp:Button runat="server" ID="btnCalculate" CssClass="buttonCss" Text=" Calculate " OnClick="btnCalculate_Click" ValidationGroup="result1" />
                                    </td>
                                </tr>
                            </tfoot>
                        </table>

                    </div>
                    </td>
                    <td style="border-left-style: solid; border-left-width: 1px; border-left-color: #C0C0C0">
                    <asp:Panel  ID="pnl_Odds_results" runat="server">
                    <div style=" float:right; width: 350px" aria-disabled="False">

                        <table>
                            <thead>
                                <tr>
                                    <th>Odds Ratio </th>
                                </tr>

                            </thead>

                            <tbody>
                                <tr>
                                    <td>
                                        <asp:Label ID="lbl_or_value" runat="server" Text="Odds Ratio : "></asp:Label>
                                    </td>
                                    <td style="text-align: left">
                                        <asp:Label ID="result_or_value" Width="45px" runat="server" Font-Bold="true" ForeColor="Red" Font-Size="13px"></asp:Label>
                                    </td>
                                </tr>

                                <tr>
                                    <td>
                                        <asp:Label ID="lbl_or_variance" runat="server" Text=" Variance (OR): "></asp:Label>
                                    </td>
                                    <td style="text-align: left">
                                        <asp:Label ID="result_or_variance" Width="45px" runat="server" Font-Bold="true" ForeColor="Red" Font-Size="13px"></asp:Label>
                                    </td>
                                </tr>

                                <tr>
                                    <td>
                                        <asp:Label ID="lbl_ln_or" runat="server" Text=" ln (OR): "></asp:Label>
                                    </td>
                                    <td style="text-align: left">
                                        <asp:Label ID="result_ln_or" Width="45px" runat="server" Font-Bold="true" ForeColor="Red" Font-Size="13px"></asp:Label>
                                    </td>
                                </tr>

                                <tr>
                                    <td>
                                        <asp:Label ID="lbl_CI_or" runat="server" Text=" %95 Confidence Interval: "></asp:Label>
                                    </td>
                                    <td style="text-align: left">
                                        <asp:Label ID="result_CI_or"  Width="105px" runat="server" Font-Bold="true" ForeColor="Red" Font-Size="13px"></asp:Label>
                                    </td>
                                </tr>


                            </tbody>


                        </table>
                    </div>
                    <div style="float: right; width: 350px" aria-disabled="False">
                        <table>
                            <thead>
                                <tr>
                                    <th>Risk Ratio </th>
                                </tr>

                            </thead>

                            <tbody>
                                <tr>
                                    <td>
                                        <asp:Label ID="lbl_rr_value" runat="server" Text="Risk Ratio: "></asp:Label>
                                    </td>
                                    <td style="text-align: left">
                                        <asp:Label ID="result_rr_value" Width="45px" runat="server" Font-Bold="true" ForeColor="Red" Font-Size="13px"></asp:Label>
                                    </td>
                                </tr>

                                <tr>
                                    <td>
                                        <asp:Label ID="lbl_rr_variance" runat="server" Text=" Variance (RR): "></asp:Label>
                                    </td>
                                    <td style="text-align: left">
                                        <asp:Label ID="result_rr_variance" Width="45px" runat="server" Font-Bold="true" ForeColor="Red" Font-Size="13px"></asp:Label>
                                    </td>
                                </tr>

                                <tr>
                                    <td>
                                        <asp:Label ID="lbl_ln_rr" runat="server" Text=" ln (RR): "></asp:Label>
                                    </td>
                                    <td style="text-align: left">
                                        <asp:Label ID="result_ln_rr" Width="45px" runat="server" Font-Bold="true" ForeColor="Red" Font-Size="13px"></asp:Label>
                                    </td>
                                </tr>

                                <tr>
                                    <td>
                                        <asp:Label ID="lbl_CI_rr" runat="server" Text=" %95 Confidence Interval: "></asp:Label>
                                    </td>
                                    <td style="text-align: left">
                                        <asp:Label ID="result_CI_rr" Width="105px" runat="server" Font-Bold="true" ForeColor="Red" Font-Size="13px"></asp:Label>
                                    </td>
                                </tr>


                            </tbody>
                        </table>

                    </div>
                    <div style="width: 450px;left: 200px; position: relative; top: 24px;" aria-disabled="False">
                    <table>
                        <thead>
                            <th>Chi-Square</th>

                        </thead>

                        <tbody>

                            <tr>
                                <td>
                                    <asp:Label ID="lblpValue" runat="server" Text=" P Value "></asp:Label>
                                </td>
                                <td style="text-align: left">
                                    <asp:Label ID="respValue" runat="server" Font-Bold="true" ForeColor="Red" Font-Size="13px"></asp:Label>
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <asp:Label ID="lbl_chi_square_without_Yates" runat="server" Text=" Chi-Square Wwithout Yates Correction: "></asp:Label>
                                </td>
                                <td style="text-align: left">
                                    <asp:Label ID="result_chi_square_without_Yates" Width="45px" runat="server" Font-Bold="true" ForeColor="Red" Font-Size="13px"></asp:Label>
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <asp:Label ID="lbl_chi_square_with_Yates" runat="server" Text=" Chi-Square with Yates Correction: "></asp:Label>
                                </td>
                                <td style="text-align: left">
                                    <asp:Label ID="result_chi_square_with_Yates" Width="45px" runat="server" Font-Bold="true" ForeColor="Red" Font-Size="13px"></asp:Label>
                                </td>
                            </tr>




                        </tbody>

                    </table>
                   </div>
                    </asp:Panel>
                    </td>
                </tr>
                </table>
               </ContentTemplate>

            </asp:TabPanel>
            <asp:TabPanel runat="server"  ScrollBars="Auto">

                <HeaderTemplate>
                    <asp:Label runat="server" ForeColor="Black" Text="Hardy-Weinberg Equilibrium" Font-Bold="true" Font-Italic="true" Font-Size="Small"></asp:Label>
                </HeaderTemplate>
                
                <ContentTemplate>
                  <table style="width:100%">
                      <tr> 
                          <td  style="vertical-align:top">
                                   <div style="float: left; width: 345px">
                        <table>
                            <thead>
                                <th>Hardy-Weinberg Equilibrium</th>
                            </thead>

                            <tbody>

                                <tr>
                                    <td>
                                        <asp:Label ID="lbl_com_homozygotes" runat="server" Text="Common Homozygotes (p<sup>2</sup>): "></asp:Label>

                                    </td>

                                    <td style="text-align: left">
                                        <asp:TextBox ID="txt_com_homozygotes" Width="45px" runat="server" autocomplete="off"></asp:TextBox>
                                        <asp:RegularExpressionValidator Display="Dynamic" ID="RegularExpressionValidator4" runat="server" ValidationGroup="HardyWeinberg"
                                            ValidationExpression="^[0-9]*$" ControlToValidate="txt_com_homozygotes" ErrorMessage="Must be Numeric" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ValidationGroup="HardyWeinberg"
                                            ControlToValidate="txt_com_homozygotes" Display="Dynamic"
                                            ErrorMessage="** required field."
                                            ForeColor="Red">
                                        </asp:RequiredFieldValidator>
                                    </td>


                                </tr>

                                <tr>
                                    <td>
                                        <asp:Label ID="lbl_heterozygotes" runat="server" Text="Heterozygotes (2pq): "></asp:Label>
                                    </td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txt_heterozygotes" Width="45px" runat="server" autocomplete="off"></asp:TextBox>
                                        <asp:RegularExpressionValidator Display="Dynamic" ID="RegularExpressionValidator5" runat="server" ValidationGroup="HardyWeinberg"
                                            ValidationExpression="^[0-9]*$" ControlToValidate="txt_heterozygotes" ErrorMessage="Must be Numeric" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ValidationGroup="HardyWeinberg"
                                            ControlToValidate="txt_heterozygotes" Display="Dynamic"
                                            ErrorMessage="** required field."
                                            ForeColor="Red">
                                        </asp:RequiredFieldValidator>
                                    </td>

                                </tr>

                                <tr>
                                    <td>
                                        <asp:Label ID="lbl_rare_homozygotes" runat="server" Text="Rare Homozygotes (q<sup>2</sup>): "></asp:Label>
                                    </td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txt_rare_homozygotes" Width="45px" runat="server" autocomplete="off"></asp:TextBox>
                                        <asp:RegularExpressionValidator Display="Dynamic" ID="RegularExpressionValidator6" runat="server" ValidationGroup="HardyWeinberg"
                                            ValidationExpression="^[0-9]*$" ControlToValidate="txt_rare_homozygotes" ErrorMessage="Must be Numeric" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ValidationGroup="HardyWeinberg"
                                            ControlToValidate="txt_rare_homozygotes" Display="Dynamic"
                                            ErrorMessage="** required field."
                                            ForeColor="Red">
                                        </asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                            </tbody>

                            <tfoot>
                                <tr>
                                    <td style="text-align: right" colspan="3">
                                        <asp:Button runat="server" ID="btn_hardy_weinberg" CssClass="buttonCss" Text=" Calculate " OnClick="btn_hardy_weinberg_Click" ValidationGroup="HardyWeinberg" />
                                    </td>
                                </tr>
                            </tfoot>

                        </table>

                    </div>
                          </td>
                      
                          <td style="border-left-style: solid; border-left-width: 1px; border-left-color: #C0C0C0">
         
                             <div style="float: right; width: 650px">

                                 <table>
                                    <thead>
                                        <th>Results</th>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>
                                                 <asp:Label ID="lbl_expected_common" runat="server" Text="Expected Common Homozygotes"></asp:Label>
                                            </td>
                                            <td style="text-align: left">
                                                <asp:Label ID="result_expected_common" runat="server" Font-Bold="true" ForeColor="Red" Font-Size="13px"></asp:Label>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td>
                                                <asp:Label ID="lbl_expected_heterozgt" runat="server" Text="Expected Heterozygotes"></asp:Label>
                                            </td>
                                             <td style="text-align: left">
                                                <asp:Label ID="result_expected_heterozgt" runat="server" Font-Bold="true" ForeColor="Red" Font-Size="13px"></asp:Label>
                                            </td>
                                        </tr>

                                        <tr>
                                           <td>
                                                <asp:Label ID="lbl_expected_rare" runat="server" Text="Expected Rare Homozygotes"></asp:Label>
                                           </td>
                                            <td style="text-align: left">
                                                <asp:Label ID="result_expected_rare" runat="server" Font-Bold="true" ForeColor="Red" Font-Size="13px"></asp:Label>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td>
                                                <asp:Label ID="lbl_p_allele" runat="server" Text="p Allele Frequency "></asp:Label>
                                            </td>
                                            <td style="text-align: left">
                                                <asp:Label ID="result_p_allele"  Width="110px" runat="server" Font-Bold="true" ForeColor="Red" Font-Size="13px"></asp:Label>
                                            </td>
                                        </tr>

                                        <tr>
                                             <td>
                                                <asp:Label ID="lbl_q_allele" runat="server" Text="q Allele Frequency "></asp:Label>
                                            </td>
                                            <td style="text-align: left">
                                                <asp:Label ID="result_q_allele" runat="server" Font-Bold="true" ForeColor="Red" Font-Size="13px"></asp:Label>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td>
                                                <asp:Label ID="lbl_x_square" runat="server" Text="X<sup>2"></asp:Label>
                                            </td>
                                            <td style="text-align: left">
                                                <asp:Label ID="result_x_square" runat="server" Font-Bold="true" ForeColor="Red" Font-Size="13px"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lbl_p" runat="server" Text="P value"></asp:Label>
                                            </td>
                                            <td style="text-align: left">
                                                <asp:Label ID="result_p" runat="server" Font-Bold="true" ForeColor="Red" Font-Size="13px"></asp:Label>
                                            </td>
                                        </tr>
                                 </tbody>
                             </table>
                        </div>

                      </td>
                   </tr>
               
            </table>
                </ContentTemplate>

            </asp:TabPanel>
            <asp:TabPanel runat="server" ID="TabPanel1"  ScrollBars="Auto">
                <HeaderTemplate>
                    <asp:Label runat="server" Text="Post-hoc Power" ForeColor="Black" Font-Bold="true" Font-Italic="true" Font-Size="Small"></asp:Label>
                    
                </HeaderTemplate>
                <ContentTemplate>
                    <table>
                        <tr>
                            <td> 
                               <div style="float: left; width: 345px">
                                    <table id="Table1">
                                        <thead>
                                            <tr>
                                                <th colspan="2"></th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>
                                                <td colspan="2" style="background-color:-webkit-gradient(linear, left top, left bottom, from(#ededed), to(#ebebeb))">Study Incidence</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label1" runat="server" Text="Group 1"></asp:Label>
                                                </td>
                                                <td style="text-align: left">
                                                    <asp:TextBox ID="postHocPowerTxt1" runat="server" Width="30px" autocomplete="off"></asp:TextBox>
                                                    <asp:Label ID="Label3" runat="server" Text=" %"></asp:Label>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server"
                                                        ControlToValidate="postHocPowerTxt1" Display="Dynamic"
                                                        ErrorMessage="** required field." ValidationGroup="postHocPower" ForeColor="Red">
                                                    </asp:RequiredFieldValidator>
                                                    <asp:RangeValidator ID="RangeValidator1" Display="Dynamic" runat="server" ControlToValidate="postHocPowerTxt1" ForeColor="Red"
                                                        ErrorMessage="Value must be between 0 and 100" ValidationGroup="postHocPower" MaximumValue="99.999" MinimumValue="0.0" SetFocusOnError="True" Type="Double"></asp:RangeValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label2" runat="server" Text="Group 2"></asp:Label>
                                                </td>
                                                <td style="text-align: left">
                                                    <asp:TextBox ID="postHocPowerTxt2" runat="server" Width="30px" autocomplete="off"></asp:TextBox>
                                                    <asp:Label ID="Label4" runat="server" Text=" %"></asp:Label>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="postHocPowerTxt2" Display="Dynamic"
                                                        ErrorMessage="** required field." ValidationGroup="postHocPower" ForeColor="Red">
                                                    </asp:RequiredFieldValidator>
                                                    <asp:RangeValidator ID="RangeValidator2" Display="Dynamic" runat="server" ControlToValidate="postHocPowerTxt2" ForeColor="Red"
                                                        ErrorMessage="Value must be between 0 and 100"  MaximumValue="99.99" MinimumValue="0.0" ValidationGroup="postHocPower" Type="Double" SetFocusOnError="True"></asp:RangeValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" style="background-color:-webkit-gradient(linear, left top, left bottom, from(#ededed), to(#ebebeb))">Number of Subjects</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label5" runat="server" Text="Group 1"></asp:Label>
                                                </td>
                                                <td style="text-align: left">
                                                    <asp:TextBox ID="postHocPowerTxt3" runat="server" Width="30px" autocomplete="off"></asp:TextBox>
                                                    <asp:Label ID="Label6" runat="server" Text=""></asp:Label>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator7" Display="Dynamic" runat="server" ForeColor="Red"
                                                        ValidationExpression="^[0-9]*$" ControlToValidate="postHocPowerTxt3" ErrorMessage="Must be Numeric" ValidationGroup="postHocPower" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server"
                                                        ControlToValidate="postHocPowerTxt3" Display="Dynamic"
                                                        ErrorMessage="** required field." ValidationGroup="postHocPower"
                                                        ForeColor="Red">
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label7" runat="server" Text="Group 2"></asp:Label>
                                                </td>
                                                <td style="text-align: left">
                                                    <asp:TextBox ID="postHocPowerTxt4" runat="server" Width="30px" autocomplete="off"></asp:TextBox>
                                                    <asp:Label ID="Label8" runat="server" Text=""></asp:Label>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator12" Display="Dynamic" runat="server" ForeColor="Red"
                                                        ValidationExpression="^[0-9]*$" ControlToValidate="postHocPowerTxt4" ErrorMessage="Must be Numeric" ValidationGroup="postHocPower" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="postHocPowerTxt4" 
                                                        Display="Dynamic" ErrorMessage="** required field." ValidationGroup="postHocPower" ForeColor="Red">
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="alpha" runat="server" Text="Alpha"></asp:Label>
                                                </td>
                                                <td style="text-align: left" colspan="2">
                                                    <asp:TextBox ID="postHocPowerTxt5" runat="server" Width="30px" Text="0.05" autocomplete="off"></asp:TextBox>
                                                    <asp:RegularExpressionValidator Display="Dynamic" ID="RegularExpressionValidator8" runat="server" ValidationGroup="postHocPower"
                                                        ValidationExpression="^0.[0-9]*$" ControlToValidate="postHocPowerTxt5" ErrorMessage="Must be Numeric" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server"
                                                        ControlToValidate="postHocPowerTxt5" Display="Dynamic"
                                                        ErrorMessage="** required field." ValidationGroup="postHocPower"
                                                        ForeColor="Red">
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                        </tbody>
                                        <tfoot>
                                            <tr>
                                                <td style="text-align: right" colspan="3">
                                                    <asp:Button runat="server" ID="postHocPowerBtn1" CssClass="buttonCss" ValidationGroup="postHocPower" Text=" Calculate " OnClick="postHocPowerBtn1_Click" />
                                                </td>
                                            </tr>
                                        </tfoot>
                                    </table>
                                </div>
                            </td>
                        
                            <td>
                               <div style="float: left; width: 300px" aria-disabled="False">
                                    <table>
                                        <thead>
                                            <tr>
                                                <th>Post-hoc Power</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>
                                                <td style="text-align: center">
                                                    <asp:Label ID="postHocPowerLbl" runat="server" Font-Bold="true" ForeColor="Red" Font-Size="25px"></asp:Label>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>

                            </td>
                        </tr>
     
                    </table>
                </ContentTemplate>
            </asp:TabPanel>
            <asp:TabPanel ID="Bonferroni_Correction" runat="server" ScrollBars="Auto">

                <HeaderTemplate>
                    <asp:Label runat="server" ForeColor="Black" Text="Bonferroni Correction" Font-Bold="true" Font-Italic="true" Font-Size="Small"></asp:Label>
                </HeaderTemplate>

                <ContentTemplate>

                      <table style="width:100%">    
                         <tr>
                             <td>
                                <div style="float: left; width: 345px">
                                                    <table>
                                                        <thead>
                                                            <th>Bonferroni Correction</th>
                                                        </thead>

                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lbl_alpha" runat="server" Text="Alpha: "></asp:Label>
                                                                </td>
                                                                <td style="text-align: left">
                                                                    <asp:TextBox ID="txt_alpha" Width="45px" runat="server" autocomplete="off"></asp:TextBox>
                                                                        <asp:RegularExpressionValidator Display="Dynamic" ID="RegularExpressionValidator9" runat="server" ValidationGroup="Bonferroni"
                                                                        ValidationExpression="^(0*\.[0-9]*)|0|1|(1\.0)$" ControlToValidate="txt_alpha" ErrorMessage="Must be between 0-1" />
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ValidationGroup="Bonferroni"
                                                                        ControlToValidate="txt_alpha" Display="Dynamic"
                                                                        ErrorMessage="** required field."
                                                                        ForeColor="Red">
                                                                    </asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lbl_test_number" runat="server" Text="Number of Tests: "></asp:Label>
                                                                </td>
                                                                <td style="text-align: left">
                                                                    <asp:TextBox ID="txt_test_number" Width="45px" runat="server" autocomplete="off"></asp:TextBox>
                                                                        <asp:RegularExpressionValidator Display="Dynamic" ID="RegularExpressionValidator10" runat="server" ValidationGroup="Bonferroni"
                                                                        ValidationExpression="^[0-9]*$" ControlToValidate="txt_test_number" ErrorMessage="Must be Numeric" />
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ValidationGroup="Bonferroni"
                                                                        ControlToValidate="txt_test_number" Display="Dynamic"
                                                                        ErrorMessage="** required field."
                                                                        ForeColor="Red">
                                                                    </asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lbl_corelation" runat="server" Text="Correlation: "></asp:Label>
                                                                </td>
                                                                <td style="text-align: left">
                                                                    <asp:TextBox ID="txt_corelation" Width="45px" runat="server" autocomplete="off"></asp:TextBox>
                                                                        <asp:RegularExpressionValidator Display="Dynamic" ID="RegularExpressionValidator11" runat="server" ValidationGroup="Bonferroni"
                                                                        ValidationExpression="^(0*\.[0-9]*)|0|1|(1\.0)$" ControlToValidate="txt_corelation" ErrorMessage="Must be between 0-1" />
                                     
                                                                </td>
                                                            </tr>
                                                        </tbody>

                                                        <tfoot>
                                                            <tr>
                                                                <td style="text-align: right" colspan="3">
                                                                    <asp:Button runat="server" ID="BonferroniCalcBtn" CssClass="buttonCss" ValidationGroup="Bonferroni" Text=" Calculate " OnClick="BonferroniCalcBtn_Click" />
                                                                </td>
                                                            </tr>
                                                        </tfoot>

                                                        </table>
                                                    </div>
                             </td>        

                             <td style="border-left-style: solid; border-left-width: 1px; border-left-color: #C0C0C0">
                                        <asp:Panel ID="pnl_Bonferroni_results" runat="server">
                               <div style="float: right;">
                                <table>
                                    <thead>
                                        <th>Results</th>
                                    </thead>

                                    <tbody>
                                        <tr>
                                            <td>
                                                <asp:Label  ID="lbl_no_correction" runat="server" Text="With no correction the chance of finding one or more significant differences in given tests:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label_no_correction" runat="server" Font-Bold="true" ForeColor="Red" Font-Size="13px"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lbl_bonferroni_factor" runat="server" Text="With Bonferroni Correction alpha is between:"></asp:Label>
                                            </td>
                                            <td style="text-align: left">
                                                <asp:Label ID="Label_bonferroni_factor" runat="server" Font-Bold="true" ForeColor="Red" Font-Size="13px"></asp:Label>
                                            </td>
                                        </tr>
                                            <tr>
                                            <td>
                                                <asp:Label ID="lbl_z_score_one" runat="server" Text="Z-score for one tailed test:"></asp:Label>
                                            </td>
                                            <td style="text-align: left">
                                                <asp:Label ID="Label_z_score_one" runat="server" Font-Bold="true" ForeColor="Red" Font-Size="13px"></asp:Label>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td>
                                                <asp:Label ID="lbl_z_score_two" runat="server" Text="Z-score for two tailed test:"></asp:Label>
                                            </td>
                                            <td style="text-align: left">
                                                <asp:Label ID="Label_z_score_two" runat="server" Font-Bold="true" ForeColor="Red" Font-Size="13px"></asp:Label>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>

                               </div>
                            </asp:Panel>
                             
                             </td>
                       
                         </tr>
                    </table>
                </ContentTemplate>
            </asp:TabPanel>

        </asp:TabContainer>
    </div>
</asp:Content>
