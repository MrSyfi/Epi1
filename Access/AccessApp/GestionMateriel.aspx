<%@ Page Title="EpiCMBD" MasterPageFile="~/Site.Master" Language="C#" AutoEventWireup="true" CodeBehind="GestionMateriel.aspx.cs" Inherits="AccessApp.GestionMateriel" enableEventValidation="false" MaintainScrollPositionOnPostback="true" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
   
    <div class="page-header" style="text-align:center;">
        <h2>EpiCMBD</h2>
    </div> 
   
    <asp:TextBox ID="TB_recherche" CssClass="form-control" runat="server" OnTextChanged="TB_recherche_TextChanged" placeholder="EPIiD..." name="Recherche" AutoPostBack="true" TextMode="Search" style="margin: auto"></asp:TextBox>

    <hr />
    

    <div class="responsive-table-line" style="margin:0px auto;max-width:700px;">
	    <table class="table table-bordered table-condensed table-body-center" >

	        <tbody>
	            <tr>
	                <td data-title="EpiID"><asp:label runat="server" ID="L_EpiID">/</asp:label></td>
	            </tr>
	            <tr>
	                <td data-title="Marque et modèle"><asp:label runat="server" ID="L_Marque">/</asp:label></td>
	            </tr>
	            <tr>
	                <td data-title="Numéro de série (S/N)"><asp:label runat="server" ID="L_NumSe">/</asp:label></td>
	            </tr>
	            <tr>
	                <td data-title="Statut"><asp:label runat="server" ID="L_Statut">/</asp:label></td>
	            </tr>
	            <tr>
	                <td data-title="Localisation actuelle"><asp:label runat="server" ID="L_Loc">/</asp:label></td>
	            </tr>
	            <tr>
	                <td data-title="Stocké par"><asp:label runat="server" ID="L_Agent">/</asp:label></td>
	            </tr>
	            <tr>
	                <td data-title="Stocké le"><asp:label runat="server" ID="L_Date">/</asp:label></td>
	            </tr>
	        </tbody>
	    </table>
	</div>
    <hr />
</asp:Content>
