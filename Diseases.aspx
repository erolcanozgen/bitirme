<%@ Page  Title="" Language="C#" MasterPageFile="~/Yeni.master" AutoEventWireup="true" CodeFile="Diseases.aspx.cs" Inherits="Diseases" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/Notifier.ascx" TagPrefix="uc1" TagName="Notifier" %>


<asp:Content ID="ContentHead" ContentPlaceHolderID="headContent" runat="server">
    <link href="css/style.css" type="text/css" rel="stylesheet"/>
    <link href="css/Table.css" type="text/css" rel="stylesheet"/>
    <link href="css/Button.css" type="text/css" rel="stylesheet"/>
         

<script src="http://ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js"></script>
<script>
    function Reload() {
        $("#show").hide();
        $("#hide").click(function () {
            $("#filterTable").hide();
            $("#hide").hide();
            $("#show").show();
            $("#DiseasesTable").width(1120, 0);
        });
        $("#show").click(function () {
            $("#filterTable").show();
            $("#show").hide();
            $("#hide").show();
            $("#DiseasesTable").width(830, 0);
        });
 }
</script>
    <style type="text/css">
        .modalBackground {
            background-color:white;
            filter:alpha(opacity=70);
            opacity:0.8;
        }
    </style>
     
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server" >

<div>
    <div style="float:left;width:25px">
        <a style="writing-mode:tb-rl; -webkit-transform:rotate(90deg);-moz-transform:rotate(90deg);-o-transform: rotate(90deg);white-space:nowrap;display:block;bottom:0;width:20px;height:20px;" id="hide">Hide Filtering Panel</a>
     </div>
    <div style="float:left;width:25px">
        <a style="writing-mode:tb-rl; -webkit-transform:rotate(90deg);-moz-transform:rotate(90deg);-o-transform: rotate(90deg);white-space:nowrap;display:block;bottom:0;width:20px;height:20px;" id="show">Show Filtering Panel</a>
    </div>
      
    <div style="float:left;width:345px">
                
         <table id="filterTable">
      
             <thead>
		<tr>
			<th colspan="2"> Advanced Search</th>
		</tr>
	</thead>
             <tbody>
         <tr>
          <td><label id="lblDiseaseName">Disease Name: </label></td>
          <td><asp:DropDownList runat="server" ID="cmbDiseaseName" Width="143px"></asp:DropDownList></td>
         </tr> 

         <tr>
            
          <td><label id="Label1">Snp : </label></td>
          <td><asp:TextBox ID="txtSNP" runat="server"></asp:TextBox>

                <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txtSNP" MinimumPrefixLength="1" CompletionInterval="100" 
                    EnableCaching="false" ServicePath="AutoComplete.asmx" ServiceMethod="GetSNPs" Enabled="true" ></asp:AutoCompleteExtender></td>
         </tr>
          
          <tr>
          <td><label id="lblGene">Gene Name: </label></td>
          <td><asp:TextBox runat="server" ID="txtGene" Width="143px"></asp:TextBox></td>
          </tr> 
         
      
          <tr>
          <td><label id="lblMinimumSample">Minumum sample size: </label></td>
          <td><asp:TextBox runat="server" ID="txtMinimumSample" Width="143px"></asp:TextBox></td>
          </tr>
              
                 </tbody>
             <tfoot>
           <tr>
           <td style="text-align:right" colspan="2">
               <asp:Button runat="server" ID="btnFilter" CssClass="buttonCss" Text=" Filter " OnClick="btnFilter_Click" />
           </td>
           </tr>
             </tfoot>
          </table>

    </div>

      <div id="DiseasesTable" style="float:left; width:830px" aria-disabled="False">
            
                <asp:Panel ID="diseasesTablePanel" runat="server" ScrollBars="Auto">
                    <asp:GridView ID="grdViewDiseases" runat="server" AutoGenerateColumns="false" OnSorting="grdViewDiseases_Sorting"
			        DataKeyNames="SNP" CssClass="ChildGrid" AllowPaging="true" HorizontalAlign="Center" Width="95%" AllowSorting="True" OnPageIndexChanging="grdViewDiseases_PageIndexChanging">

                        <Columns>
                            <asp:TemplateField HeaderText="Gene Name">
                                <ItemTemplate>
                                     <asp:HyperLink ID="Gene_Name" runat="server" Text='<%# Bind("Gene_Name") %>'></asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField  DataField="SNP" HeaderText="SNP" SortExpression="SNP"/>
                            <asp:BoundField  DataField="Case_Count" HeaderText="Case Count" SortExpression="Case_Count"/>
                            <asp:BoundField  DataField="Frequency_Patient" HeaderText="Frequency In Case" SortExpression="Frequency_Patient"/>
                            <asp:BoundField  DataField="Control_Count" HeaderText="Control Count" SortExpression="Control_Count"/>
                            <asp:BoundField  DataField="Frequency_Control" HeaderText="Frequency In Control" SortExpression="Frequency_Control"/>
                            <asp:BoundField  DataField="P_Value" HeaderText="P Value" SortExpression="P_Value"/>
                            <asp:BoundField  DataField="OR_Value" HeaderText="OR Value" SortExpression="OR_Value"/>
                            <asp:BoundField  DataField="CI" HeaderText="95% CI" SortExpression="CI"/>
                            <asp:TemplateField HeaderText="Reference">
                                <ItemTemplate>
                                    <asp:HyperLink ID="Link" runat="server" Text='<%# Bind("Reference") %>' ></asp:HyperLink>
                                    <asp:Label runat="server" ID="lbl_reference" Visible="false" Text='<%# Bind("Reference") %>'></asp:Label>
                                    <asp:LinkButton ID="seeDetailsBtn" runat="server" OnClick="ShowPopup">See Details</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField Visible="false"  DataField="ownerOfPublication" HeaderText="Owner Of Publication" />
                        </Columns>
                        <RowStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                    </asp:GridView>
                </asp:Panel>
                <asp:Button ID="Button1" runat="server" Text="Button" style="display:none" />
                <asp:ModalPopupExtender ID="Button1_ModalPopupExtender"
                    runat="server"  Enabled="True" 
                    PopupControlID="popUpTable" TargetControlID="Button1" 
                    CancelControlID="Button3" BackgroundCssClass="modalBackground">
                </asp:ModalPopupExtender>
                <table id="popUpTable" style="padding:0px 0px 0px 0px; width:550px; height:200px">
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
        </div>

   </div>


 
       </asp:Content>