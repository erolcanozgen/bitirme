<%@ Page  Title="" Language="C#" MasterPageFile="~/Yeni.master" AutoEventWireup="true" CodeFile="Diseases.aspx.cs" Inherits="Diseases" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="headContent" runat="server">
      <link href="css/style.css" type="text/css" rel="stylesheet"/>
     <link href="css/Button.css" type="text/css" rel="stylesheet"/>

     
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
                
         <table id="filterTable" class="searchPanel">
      
         <tr>
          <td><label id="lblDiseaseName">Disease Name: </label></td>
          <td colspan="3"><asp:DropDownList runat="server" ID="cmbDiseaseName" Width="100px"></asp:DropDownList></td>
         </tr> 

         <tr>
          <td><label id="Label1">Snp : </label></td>
          <td> <asp:DropDownList runat="server" ID="cmbSNP" Width="105px"/></td>
          <td>  <asp:CheckBox runat="server" ID="chkSNP"  OnCheckedChanged="chkSNP_CheckedChanged" AutoPostBack="true" /></td>
          <td><asp:TextBox runat="server" ID="txtSNP" Visible="false" Width="100px"></asp:TextBox></td>
         </tr>
          
          <tr>
          <td><label id="lblGene">Gen Name: </label></td>
          <td colspan="3"><asp:TextBox runat="server" ID="txtGene" Width="100px"></asp:TextBox></td>
          </tr> 
         
          <tr>
          <td><label id="lblCaseCount">Min Case Count: </label></td>
          <td colspan="3"><asp:TextBox runat="server" ID="txtCaseCount" Width="100px"></asp:TextBox></td>
          </tr>

          <tr>
          <td><label id="lblControlCount">Min Control Count: </label></td>
          <td colspan="3"><asp:TextBox runat="server" ID="txtCotrolCount" Width="100px"></asp:TextBox></td>
          </tr>
              
           <tr>
           <td></td>
           <td style="text-align:right">
               <asp:Button runat="server" ID="btnFilter" CssClass="buttonCss" Text=" Filter " OnClick="btnFilter_Click" />
           </td>
           </tr>

          </table>

    </div>

      <div id="DiseasesTable" style="float:right;width:625px" aria-disabled="False">
            <asp:GridView ID="grdViewDiseases" runat="server" AutoGenerateColumns="false"
								            DataKeyNames="SNP" CssClass="ChildGrid" AllowPaging="False" HorizontalAlign="Center" Width="100%">

               <Columns>
                                                <asp:BoundField  DataField="Gene_Name" HeaderText="Gene Name" />
                                                <asp:BoundField  DataField="SNP" HeaderText="SNP" />
                                                <asp:BoundField  DataField="Case_Count" HeaderText="Case Count" />
                                                <asp:BoundField  DataField="Frequency_Patient" HeaderText="Frequency Patient" />
                                                <asp:BoundField  DataField="Control_Count" HeaderText="Control Count" />
                                                <asp:BoundField  DataField="Frequency_Control" HeaderText="Frequency Control" />
                                                <asp:BoundField  DataField="P_Value" HeaderText="P Value" />
                                                <asp:BoundField  DataField="OR_Value" HeaderText="OR Value" />
                                                <asp:BoundField  DataField="Reference" HeaderText="Reference" />
 

                   </Columns>
                <RowStyle HorizontalAlign="Left" VerticalAlign="Middle" />
          </asp:GridView>

         </div>

   </div>

     

<script src="http://ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js"></script>
<script>
    $(document).ready(function () {
        $("#show").hide();
        

        $("#hide").click(function () {
            $("#filterTable").hide();
            $("#hide").hide();
            $("#show").show();
            $("#DiseasesTable").width(960, 0);

        });
        $("#show").click(function () {
            $("table").show();
            $("#show").hide();
            $("#hide").show();
            $("#DiseasesTable").width(625, 0);
        });
    });
</script>
 
       </asp:Content>