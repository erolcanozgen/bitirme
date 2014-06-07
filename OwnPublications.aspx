<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Yeni.master"  CodeFile="OwnPublications.aspx.cs" Inherits="OwnPublications" EnableEventValidation="false"  %>
<%@ Register Src="~/MyAccountLeftPanel.ascx" TagName="leftPanel" TagPrefix="UserControl"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/Notifier.ascx" TagPrefix="uc1" TagName="Notifier" %>


<asp:Content ID="Content2" ContentPlaceHolderID="headContent" Runat="Server">  
        <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1"> 
        <meta name="viewport" content="width=device-width, initial-scale=1.0">
        <meta name="keywords" content="menu, navigation, animation, transition, transform, rotate, css3, web design, component, icon, slide" />
        <link rel="shortcut icon" href="../favicon.ico"> 
        <link rel="stylesheet" type="text/css" href="css/VerticalNav/demo.css" />
        <link rel="stylesheet" type="text/css" href="css/VerticalNav/style1.css" />
        <link href='http://fonts.googleapis.com/css?family=Terminal+Dosis' rel='stylesheet' type='text/css' />
        <link rel="stylesheet" type="text/css" href="css/Table.css" />
        <script src="Scripts/ErrorSuccessNotifier.js"></script>
        <link href="Styles/ErrorSuccessNotifier.css" rel="stylesheet" />
        <style type="text/css">
            .modalBackground {
                background-color:white;
                filter:alpha(opacity=70);
                opacity:0.8;
        }
        </style>
   <script type="text/javascript">
    function ShowOptions(control, args)
    {
        control._completionListElement.style.zIndex = 100009 ;
    }

   </script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server" >


    <div style="float: left">
        <UserControl:leftPanel ID="LeftPanel1" runat="server" />
    </div>

    <div>
         <uc1:Notifier runat="server" ID="notifier" />
        <asp:TabContainer ID="TabContainer1" runat="server">
           
            <asp:TabPanel ID="TabPanel1" runat="server" BorderColor="Black" ScrollBars="Both">
                <HeaderTemplate>
                    <asp:Label ID="Label1" runat="server" ForeColor="Black" Text="Approved" Font-Bold="true" Font-Italic="true" Font-Size="Small"></asp:Label>
                </HeaderTemplate>

                <ContentTemplate>
                    <div style="overflow-x: auto; overflow-y: auto">
                        <asp:GridView ID="grdViewPublications" runat="server" AutoGenerateColumns="false" GridLines="Both"
                            DataKeyNames="SNP" CssClass="ChildGrid" AllowPaging="true" HorizontalAlign="Center" AllowSorting="True" OnPageIndexChanging="grdViewPublications_PageIndexChanging">
                            <Columns>
                                <asp:BoundField DataField="Disease_Name" HeaderText="Disease Name" SortExpression="Disease_Name" />
                                <asp:TemplateField HeaderText="Gene Name">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="Gene_Name" runat="server" Text='<%# Bind("Gene_Name") %>'></asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="SNP" HeaderText="SNP" SortExpression="SNP" />
                                <asp:BoundField DataField="Case_Count" HeaderText="Case Count" SortExpression="Case_Count" />
                                <asp:BoundField DataFormatString="{0:F3}" DataField="Frequency_Patient" HeaderText="Frequency In Case" SortExpression="Frequency_Patient" />
                                <asp:BoundField DataField="Control_Count" HeaderText="Control Count" SortExpression="Control_Count" />
                                <asp:BoundField DataFormatString="{0:F3}" DataField="Frequency_Control" HeaderText="Frequency In Control" SortExpression="Frequency_Control" />
                                <asp:BoundField DataField="P_Value" HeaderText="P Value" SortExpression="P_Value" />
                                <asp:BoundField DataFormatString="{0:F2}" DataField="OR_Value" HeaderText="OR Value" SortExpression="OR_Value" />
                                <asp:BoundField DataField="CI" HeaderText="95% CI" SortExpression="CI" />
                                <asp:TemplateField HeaderText="Reference">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="Link" runat="server" Visible="false" Text='<%# Bind("Reference") %>'></asp:HyperLink>
                                        <asp:Label runat="server" ID="lbl_reference" Visible="false" Text='<%# Bind("Reference") %>'></asp:Label>
                                        <asp:LinkButton ID="seeDetailsBtn" runat="server" Visible="false" OnClick="ShowPopup">See Details</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField Visible="false" DataField="ownerOfPublication" HeaderText="Owner Of Publication" />
                            </Columns>
                            <RowStyle HorizontalAlign="Left" VerticalAlign="Middle" Height="10px" />
                        </asp:GridView>
                    </div>
                </ContentTemplate>
            </asp:TabPanel>

            <asp:TabPanel ID="TabPanel2" runat="server" BorderColor="Black" ScrollBars="Both">
                <HeaderTemplate>
                    <asp:Label ID="Label2" runat="server" ForeColor="Black" Text="Unapproved" Font-Bold="true" Font-Italic="true" Font-Size="Small"></asp:Label>
                </HeaderTemplate>
                <ContentTemplate>
                    <div style="overflow-x: auto; overflow-y: auto">
                        <asp:GridView ID="grdUnapprovedView" runat="server" AutoGenerateColumns="false" GridLines="Both" OnPageIndexChanging="grdViewPublications_PageIndexChanging"
                            DataKeyNames="ID,Disease_Name,Gene_Name,SNP,Case_Count,Frequency_Patient,Frequency_Control,Control_Count,P_Value,OR_Value,CI,Reference,Reference_Type" CssClass="ChildGrid" AllowPaging="true" HorizontalAlign="Center" AllowSorting="True">
                                                        
                            <Columns >

                                 <asp:TemplateField HeaderText="Update-Delete">
                                    <ItemTemplate>
                                       <asp:ImageButton  RowIndex='<%# Container.DisplayIndex %>' OnClick="update_button_Click"  Height="15px" ID="update_button" runat="server" ImageUrl="images/Update_Button.png"/> 
                                       <asp:ImageButton  RowIndex='<%# Container.DisplayIndex %>' OnClientClick ="return confirm('Do you want to delete the publication?')" OnClick="delete_button_Click"  Height="15px" ID="delete_button" runat="server" ImageUrl="images/delete_button.png"/> 
                                    </ItemTemplate>
                                </asp:TemplateField>
                            

                                <asp:BoundField DataField="Disease_Name"  HeaderText="Disease Name" SortExpression="Disease_Name" />
                                <asp:TemplateField HeaderText="Gene Name">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="Gene_Name" runat="server" Text='<%# Bind("Gene_Name") %>'></asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="ID" Visible="false" />
                                <asp:BoundField DataField="SNP" HeaderText="SNP" SortExpression="SNP" />
                                <asp:BoundField DataField="Case_Count" HeaderText="Case Count" SortExpression="Case_Count" />
                                <asp:BoundField DataFormatString="{0:F3}" DataField="Frequency_Patient" HeaderText="Frequency In Case" SortExpression="Frequency_Patient" />
                                <asp:BoundField DataField="Control_Count" HeaderText="Control Count" SortExpression="Control_Count" />
                                <asp:BoundField DataFormatString="{0:F3}" DataField="Frequency_Control" HeaderText="Frequency In Control" SortExpression="Frequency_Control" />
                                <asp:BoundField DataField="P_Value" HeaderText="P Value" SortExpression="P_Value" />
                                <asp:BoundField DataFormatString="{0:F2}" DataField="OR_Value" HeaderText="OR Value" SortExpression="OR_Value" />
                                <asp:BoundField DataField="CI" HeaderText="95% CI" SortExpression="CI" />
                                 <asp:BoundField DataField="Reference_Type" Visible="false" />
                                <asp:TemplateField HeaderText="Reference">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="Link" runat="server" Visible="false" Text='<%# Bind("Reference") %>'></asp:HyperLink>
                                        <asp:Label runat="server" ID="lbl_reference" Visible="false" Text='<%# Bind("Reference") %>'></asp:Label>
                                        <asp:LinkButton ID="seeDetailsBtn" runat="server" Visible="false" OnClick="ShowPopup">See Details</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField Visible="false" DataField="ownerOfPublication" HeaderText="Owner Of Publication" />
                            </Columns>
                            <RowStyle HorizontalAlign="Left" VerticalAlign="Middle" Height="10px" />



                        </asp:GridView>
                    </div>
                </ContentTemplate>
            </asp:TabPanel>
        </asp:TabContainer>
    </div>



    <asp:Button ID="Button1" runat="server" Text="Button" style="display:none" />
    <asp:Button ID="Button4" runat="server" Text="Button" style="display:none" />
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

    
     <asp:ModalPopupExtender ID="UpdatePopupExtender1"
            runat="server"  Enabled="True" 
            PopupControlID="updateTable" TargetControlID="Button4"  
            DropShadow="true" 
            CancelControlID="Button2" BackgroundCssClass="modalBackground">
        </asp:ModalPopupExtender>

    <table id="updateTable" class="table2" style="padding:0px 0px 0px 0px; width:550px; height:400px">
             
              <thead>
		            <tr>
			            <th colspan="2">Update Publication</th>
		            </tr>
	            </thead>
               <tbody>
                   <tr>
                <td><span>Disease Name</span></td>
                <td style="text-align:left"><asp:TextBox ID="SelectDisease_TextBox" runat="server"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender2" OnClientShown="ShowOptions" runat="server" TargetControlID="SelectDisease_TextBox" MinimumPrefixLength="1" CompletionInterval="100" 
                    EnableCaching="false" ServicePath="AutoComplete.asmx" ServiceMethod="GetDiseaseNames" Enabled="true" ></asp:AutoCompleteExtender>
                    <asp:RequiredFieldValidator SetFocusOnError="true"  ValidationGroup="updatePub" ID="ReqSelectDisease" runat="server" ErrorMessage="Enter Disease Name!" 
                        ControlToValidate="SelectDisease_TextBox" ForeColor="Red" Height="16px" Width="190px" Display="Dynamic"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td><span>SNP</span></td>
                <td style="text-align:left"><asp:TextBox ID="SNP_TextBox" runat="server"></asp:TextBox>
                    <asp:AutoCompleteExtender OnClientShown="ShowOptions"  ID="AutoCompleteExtender1" runat="server" TargetControlID="SNP_TextBox" MinimumPrefixLength="1" CompletionInterval="100" 
                    EnableCaching="false" ServicePath="AutoComplete.asmx" ServiceMethod="GetSNPs" Enabled="true" ></asp:AutoCompleteExtender>
                    <asp:RequiredFieldValidator SetFocusOnError="true" ValidationGroup="updatePub" ID="ReqSNP" runat="server" ErrorMessage="Enter SNP!" 
                        ControlToValidate="SNP_TextBox" Font-Bold="False" ForeColor="Red" Height="16px" Width="190px" Display="Dynamic"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td><span>Gene Name</span></td>
                <td style="text-align:left"><asp:TextBox CssClass="contact" ID="GeneName_TextBox" runat="server"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender3" OnClientShown="ShowOptions" runat="server" TargetControlID="GeneName_TextBox" MinimumPrefixLength="1" CompletionInterval="100" 
                    EnableCaching="false" ServicePath="AutoComplete.asmx" ServiceMethod="GetGeneNames" Enabled="true" ></asp:AutoCompleteExtender>
                    <asp:RequiredFieldValidator SetFocusOnError="true" ValidationGroup="updatePub" ID="ReqGeneName" runat="server" ErrorMessage="Enter Gene Name!" 
                        ControlToValidate="GeneName_TextBox" ForeColor="Red" Height="16px" Width="190px" Display="Dynamic"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td><span>Case Exposed Count (Yes)</span></td>
                <td style="text-align:left"><asp:TextBox CssClass="contact" ID="CaseYes_TextBox" runat="server" autocomplete="off" ></asp:TextBox>
                    <asp:RequiredFieldValidator SetFocusOnError="true" ValidationGroup="updatePub" ID="ReqCaseYes" runat="server" ErrorMessage="Enter Case Exposed Count!" 
                        ControlToValidate="CaseYes_TextBox" ForeColor="Red" Height="16px" Width="190px" Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValCaseYes" Operator="DataTypeCheck" ValidationGroup="updatePub" Type="Integer" runat="server" ControlToValidate="CaseYes_TextBox"
                        ErrorMessage="Value must be number!" SetFocusOnError="true" ForeColor="Red" Display="Dynamic"></asp:CompareValidator>
                    <asp:RangeValidator ID="RangeValCaseYes" runat="server" ValidationGroup="updatePub" ErrorMessage="Value should not be negative!" ForeColor="Red" 
                         MinimumValue="0" MaximumValue="999999999999" SetFocusOnError="True" Display="Dynamic" ControlToValidate="CaseYes_TextBox"></asp:RangeValidator>
                </td>
            </tr>
            <tr>
                <td><span>Case Unexposed Count (No)</span></td>
                <td style="text-align:left"><asp:TextBox CssClass="contact" ID="CaseNo_TextBox" runat="server" autocomplete="off"></asp:TextBox>
                    <asp:RequiredFieldValidator SetFocusOnError="true" ValidationGroup="updatePub" ID="ReqCaseNo" runat="server" ErrorMessage="Enter Case Unxposed Count!" 
                        ControlToValidate="CaseNo_TextBox" ForeColor="Red" Height="16px" Width="190px" Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValCaseNo" ValidationGroup="updatePub" Operator="DataTypeCheck" Type="Integer" runat="server" ControlToValidate="CaseNo_TextBox"
                        ErrorMessage="Value must be number!" SetFocusOnError="true" ForeColor="Red" Display="Dynamic"></asp:CompareValidator>
                   <asp:RangeValidator ID="RangeValCaseNo" ValidationGroup="updatePub" runat="server" ErrorMessage="Value should not be negative!" ForeColor="Red" 
                         MinimumValue="0" MaximumValue="999999999999" SetFocusOnError="True" Display="Dynamic" ControlToValidate="CaseNo_TextBox"></asp:RangeValidator>
                </td>
            </tr>
            <tr>
                <td><span>Control Exposed Count (Yes)</span></td>
                <td style="text-align:left"><asp:TextBox CssClass="contact" ID="ControlYes_TextBox" runat="server" autocomplete="off"></asp:TextBox>
                    <asp:RequiredFieldValidator ValidationGroup="updatePub" SetFocusOnError="true" ID="ReqControlYes" runat="server" ErrorMessage="Enter Control Exposed Count!" 
                        ControlToValidate="ControlYes_TextBox" ForeColor="Red" Height="16px" Width="190px" Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ValidationGroup="updatePub" ID="CompareValControlYes" Operator="DataTypeCheck" Type="Integer" runat="server" ControlToValidate="ControlYes_TextBox"
                        ErrorMessage="Value must be number!" SetFocusOnError="true" ForeColor="Red" Display="Dynamic"></asp:CompareValidator>
                   <asp:RangeValidator ValidationGroup="updatePub" ID="RangeValControlYes" runat="server" ErrorMessage="Value should not be negative!" ForeColor="Red" 
                         MinimumValue="0" MaximumValue="999999999999"  SetFocusOnError="True" Display="Dynamic" ControlToValidate="ControlYes_TextBox"></asp:RangeValidator>
                </td>
            </tr>
            <tr>
                <td><span>Control Unexposed Count (No)</span></td>
                <td style="text-align:left"><asp:TextBox CssClass="contact" ID="ControlNo_TextBox" runat="server" autocomplete="off"></asp:TextBox>
                    <asp:RequiredFieldValidator ValidationGroup="updatePub" SetFocusOnError="true" ID="ReqControlNo" runat="server" ErrorMessage="Enter Control Unexposed Count!" 
                        ControlToValidate="ControlNo_TextBox" ForeColor="Red" Height="16px" Width="190px" Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ValidationGroup="updatePub" ID="CompareValControlNo" Operator="DataTypeCheck" Type="Integer" runat="server" ControlToValidate="ControlNo_TextBox"
                        ErrorMessage="Value must be number!" SetFocusOnError="true" ForeColor="Red" Display="Dynamic"></asp:CompareValidator>
                    <asp:RangeValidator ValidationGroup="updatePub" ID="RangeValControlNo" runat="server" ErrorMessage="Value should not be negative!" ForeColor="Red" 
                         MinimumValue="0" MaximumValue="999999999999"  SetFocusOnError="True" Display="Dynamic" ControlToValidate="ControlNo_TextBox"></asp:RangeValidator>
                </td>
            </tr>
            <tr>
                <td><span>P Value</span></td>
                <td style="text-align:left">
                    <asp:TextBox CssClass="contact" ID="P_TextBox" runat="server" autocomplete="off"></asp:TextBox>
                    <asp:RequiredFieldValidator ValidationGroup="updatePub" SetFocusOnError="true" ID="ReqPValue" runat="server" ErrorMessage="Enter P Value!" 
                        ControlToValidate="P_TextBox" ForeColor="Red" Height="16px" Width="190px" Display="Dynamic"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td><span><asp:DropDownList ID="Reference_DropDown" runat="server" Height="25px" Width="191px">
                                <asp:ListItem Text="Select the Reference Type" Value="0" />  
                                <asp:ListItem Value="1">PubMed Id</asp:ListItem>
                                <asp:ListItem Value="2">Link</asp:ListItem>
                                <asp:ListItem Value="3">Other</asp:ListItem>
                          </asp:DropDownList></span>
                </td>
                <td style="text-align:left"><asp:TextBox CssClass="contact" ID="Reference_TextBox" runat="server" Rows="5" TextMode="MultiLine"></asp:TextBox>
                    <asp:RequiredFieldValidator ValidationGroup="updatePub" SetFocusOnError="true" ID="ReqDropDown" InitialValue="0" runat="server" ErrorMessage="Select a Reference Type!" 
                        ControlToValidate="Reference_DropDown" ForeColor="Red" Height="16px" Width="190px" Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ValidationGroup="updatePub" SetFocusOnError="true" ID="ReqReferenceTxt" runat="server" ErrorMessage="Enter Reference Type!" 
                        ControlToValidate="Reference_TextBox" ForeColor="Red" Height="16px" Width="190px" Display="Dynamic"></asp:RequiredFieldValidator>
                </td>
            </tr> 
               </tbody>
                <tfoot>
                    <tr>
                        <td style="padding:0px 0px 0px 0px;text-align:right" colspan="2">
                            <asp:Button ID="Button2" style="padding:0px 0px 0px 0px;" runat="server" Text=" close"  />
                            <asp:Button ID="update_Pub" ValidationGroup="updatePub"  style="padding:0px 0px 0px 0px;" runat="server" Text=" Update " OnClientClick ="return confirm('Do you want to accept the changes?')" OnClick="update_Pub_Click"  />
                        </td>
                    </tr>
                </tfoot>
        </table>





</asp:Content>