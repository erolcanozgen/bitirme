<%@ Page Title="" Language="C#" MasterPageFile="~/Yeni.master" AutoEventWireup="true" CodeFile="UtilityCalculators.aspx.cs" Inherits="UtilityCalculators" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="ContentHead" ContentPlaceHolderID="headContent" runat="server">
    <link href="css/style.css" type="text/css" rel="stylesheet" />
    <link href="css/Table.css" type="text/css" rel="stylesheet" />
    <link href="css/Button.css" type="text/css" rel="stylesheet" />


</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div>

        <asp:ToolkitScriptManager runat="server"></asp:ToolkitScriptManager>

        <asp:TabContainer runat="server">

            <asp:TabPanel runat="server" ID="results_panel" HeaderText="Or-RR Ratio-Chi Square - P Value" ScrollBars="Auto">

                <ContentTemplate>

                    <div style="float: left; width: 345px">

                        <table id="filterTable">

                            <thead>
                                <tr>
                                    <th>Experement </th>
                                    <th>Yes </th>
                                    <th>No </th>
                                </tr>
                            </thead>
                            <tbody>

                                <tr>
                                    <td>
                                        <asp:Label ID="lblCaseCount" runat="server" Text="Case"></asp:Label>
                                    </td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtCaseYes" runat="server" Width="30px"></asp:TextBox>
                                        <asp:RegularExpressionValidator Display="Dynamic" ID="revNumericValidator" runat="server" ValidationGroup="result1"
                                            ValidationExpression="^[0-9]*$" ControlToValidate="txtCaseYes" ErrorMessage="Must be Numeric" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                            ControlToValidate="txtCaseYes" Display="Dynamic"
                                            ErrorMessage="** required field." ValidationGroup="result1"
                                            ForeColor="Red">
                                        </asp:RequiredFieldValidator>
                                    </td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtCaseNo" runat="server" Width="30px"></asp:TextBox>
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
                                        <asp:Label ID="lblControlCount" runat="server" Text="Control"></asp:Label>
                                    </td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtControlYes" runat="server" Width="30px"></asp:TextBox>
                                        <asp:RegularExpressionValidator Display="Dynamic" ID="RegularExpressionValidator2" runat="server" ValidationGroup="result1"
                                            ValidationExpression="^[0-9]*$" ControlToValidate="txtControlYes" ErrorMessage="Must be Numeric" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                            ControlToValidate="txtControlYes" Display="Dynamic"
                                            ErrorMessage="** required field." ValidationGroup="result1"
                                            ForeColor="Red">
                                        </asp:RequiredFieldValidator>
                                    </td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtControlNo" runat="server" Width="30px"></asp:TextBox>
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
                                        <asp:Button runat="server" ID="btnCalculate" CssClass="buttonCss" Text=" Calculate " OnClick="btnCalculate_Click" />
                                    </td>
                                </tr>
                            </tfoot>
                        </table>

                    </div>


                        <div style="float: left; width: 300px" aria-disabled="False">



                            <table>
                                <thead>
                                    <tr>
                                        <th>Odds Ratio </th>
                                    </tr>

                                </thead>

                                <tbody>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lbl_or_value" runat="server" Text="Or Value: "></asp:Label>
                                        </td>
                                        <td style="text-align: left">
                                            <asp:TextBox ID="txt_or_value" Width="45px" runat="server" ReadOnly="true"></asp:TextBox>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <asp:Label ID="lbl_or_variance" runat="server" Text=" Variance(OR): "></asp:Label>
                                        </td>
                                        <td style="text-align: left">
                                            <asp:TextBox ID="txt_or_variance" Width="45px" runat="server" ReadOnly="true"></asp:TextBox>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <asp:Label ID="lbl_ln_or" runat="server" Text=" ln(OR): "></asp:Label>
                                        </td>
                                        <td style="text-align: left">
                                            <asp:TextBox ID="txt_ln_or" Width="45px" runat="server" ReadOnly="true"></asp:TextBox>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <asp:Label ID="lbl_CI_or" runat="server" Text=" %95 Confidence Interval: "></asp:Label>
                                        </td>
                                        <td style="text-align: left">
                                            <asp:TextBox ID="txt_CI_or" Width="105px" runat="server" ReadOnly="true"></asp:TextBox>
                                        </td>
                                    </tr>


                                </tbody>


                            </table>
                        </div>

                        <div style="float: left; width: 300px" aria-disabled="False">
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
                                            <asp:TextBox ID="txt_rr_value" Width="45px" runat="server" ReadOnly="true"></asp:TextBox>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <asp:Label ID="lbl_rr_variance" runat="server" Text=" Variance(RR): "></asp:Label>
                                        </td>
                                        <td style="text-align: left">
                                            <asp:TextBox ID="txt_rr_variance" Width="45px" runat="server" ReadOnly="true"></asp:TextBox>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <asp:Label ID="lbl_ln_rr" runat="server" Text=" ln(RR): "></asp:Label>
                                        </td>
                                        <td style="text-align: left">
                                            <asp:TextBox ID="txt_ln_rr" Width="45px" runat="server" ReadOnly="true"></asp:TextBox>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <asp:Label ID="lbl_CI_rr" runat="server" Text=" %95 Confidence Interval: "></asp:Label>
                                        </td>
                                        <td style="text-align: left">
                                            <asp:TextBox ID="txt_CI_rr" Width="105px" runat="server" ReadOnly="true"></asp:TextBox>
                                        </td>
                                    </tr>


                                </tbody>
                            </table>

                        </div>


                        <table style="position: relative; left: 450px;">
                            <thead>
                                <th>Chi-Square - P Value</th>

                            </thead>

                            <tbody>

                                <tr>
                                    <td>
                                        <asp:Label ID="lblpValue" runat="server" Text=" P Value (Hypergeometric Probability) "></asp:Label>
                                    </td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtpValue" Width="45px" runat="server" ReadOnly="true"></asp:TextBox>
                                    </td>
                                </tr>

                                <tr>
                                    <td>
                                        <asp:Label ID="lbl_chi_square_without_Yates" runat="server" Text=" Chi-Square Without Yates Correction: "></asp:Label>
                                    </td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txt_chi_square_without_Yates" Width="45px" runat="server" ReadOnly="true"></asp:TextBox>
                                    </td>
                                </tr>

                                <tr>
                                    <td>
                                        <asp:Label ID="lbl_chi_square_with_Yates" runat="server" Text=" Chi-Square With Yates Correction: "></asp:Label>
                                    </td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txt_chi_square_with_Yates" Width="45px" runat="server" ReadOnly="true"></asp:TextBox>
                                    </td>
                                </tr>




                            </tbody>

                        </table>


                </ContentTemplate>

            </asp:TabPanel>


            <asp:TabPanel runat="server" HeaderText="Hardy Weinberg calculator" ScrollBars="Auto">
                    <ContentTemplate>

                        <table>
                               <thead></thead>

                                <tbody>

                                    <tr>
                                          <td>
                                               <asp:Label ID="lbl_com_homozygotes" runat="server" Text="Common Homozygotes: "></asp:Label>

                                          </td>


                                    </tr>

                                     <tr>
                                          <td>
                                               <asp:Label ID="lbl_heterozygotes" runat="server" Text="Heterozygotes: "></asp:Label>

                                          </td>


                                    </tr>




                                </tbody>




                        </table>




                    </ContentTemplate>




            </asp:TabPanel>
            <asp:TabPanel runat="server" ID="TabPanel1" HeaderText="Post-hoc Power Calculator" ScrollBars="Auto">
                <ContentTemplate>
                    <div style="float: left; width: 345px">
                        <table id="Table1">

                            <thead>
                                <tr>
                                    <th>Experement </th>
                                    <th>Yes </th>
                                    <th>No </th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label1" runat="server" Text="Case"></asp:Label>
                                    </td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="postHocPowerTxt1" runat="server" Width="30px"></asp:TextBox>
                                        <asp:RegularExpressionValidator Display="Dynamic" ID="RegularExpressionValidator4" runat="server"
                                            ValidationExpression="^[0-9]*$" ControlToValidate="postHocPowerTxt1" ErrorMessage="Must be Numeric" ValidationGroup="postHocPower" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                                            ControlToValidate="postHocPowerTxt1" Display="Dynamic"
                                            ErrorMessage="** required field." ValidationGroup="postHocPower"
                                            ForeColor="Red">
                                        </asp:RequiredFieldValidator>
                                    </td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="postHocPowerTxt2" runat="server" Width="30px"></asp:TextBox>
                                        <asp:RegularExpressionValidator Display="Dynamic" ID="RegularExpressionValidator5" runat="server" ValidationGroup="postHocPower"
                                            ValidationExpression="^[0-9]*$" ControlToValidate="postHocPowerTxt2" ErrorMessage="Must be Numeric" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                                            ControlToValidate="postHocPowerTxt2" Display="Dynamic"
                                            ErrorMessage="** required field." ValidationGroup="postHocPower"
                                            ForeColor="Red">
                                        </asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label2" runat="server" Text="Control"></asp:Label>
                                    </td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="postHocPowerTxt3" runat="server" Width="30px"></asp:TextBox>
                                        <asp:RegularExpressionValidator Display="Dynamic" ID="RegularExpressionValidator6" runat="server" ValidationGroup="postHocPower"
                                            ValidationExpression="^[0-9]*$" ControlToValidate="postHocPowerTxt3" ErrorMessage="Must be Numeric" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server"
                                            ControlToValidate="postHocPowerTxt3" Display="Dynamic"
                                            ErrorMessage="** required field." ValidationGroup="postHocPower"
                                            ForeColor="Red">
                                        </asp:RequiredFieldValidator>
                                    </td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="postHocPowerTxt4" runat="server" Width="30px"></asp:TextBox>
                                        <asp:RegularExpressionValidator Display="Dynamic" ID="RegularExpressionValidator7" runat="server" ValidationGroup="postHocPower"
                                            ValidationExpression="^[0-9]*$" ControlToValidate="postHocPowerTxt4" ErrorMessage="Must be Numeric" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server"
                                            ControlToValidate="postHocPowerTxt4" Display="Dynamic"
                                            ErrorMessage="** required field." ValidationGroup="postHocPower"
                                            ForeColor="Red">
                                        </asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="alpha" runat="server" Text="Alpha"></asp:Label>
                                    </td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="postHocPowerTxt5" runat="server" Width="30px" Text="0.05"></asp:TextBox>
                                        <asp:RegularExpressionValidator Display="Dynamic" ID="RegularExpressionValidator8" runat="server" ValidationGroup="postHocPower"
                                            ValidationExpression="^0.[0-9]*$" ControlToValidate="postHocPowerTxt5" ErrorMessage="Must be Numeric" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server"
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
                     <div style="float: left; width: 300px" aria-disabled="False">
                            <table>
                                <thead>
                                    <tr>
                                        <th>Post Hoc Power</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td style="text-align: left">
                                            <asp:TextBox ID="postHocPowerTxt" Width="45px" runat="server" ReadOnly="true"></asp:TextBox>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                </ContentTemplate>
            </asp:TabPanel>

        </asp:TabContainer>
    </div>


</asp:Content>
