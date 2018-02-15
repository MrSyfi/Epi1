<%@ Page Title="EpiCMBD" MasterPageFile="~/Site.Master" Language="C#" AutoEventWireup="true" CodeBehind="GestionMateriel.aspx.cs" Inherits="AccessApp.GestionMateriel" enableEventValidation="false" MaintainScrollPositionOnPostback="true" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
   
    <div class="page-header" style="text-align:center;">
        <h2>EpiCMBD</h2>
    </div> 


    <table style="margin: auto;">
        <tr>
            <td><label>EPIiD</label></td>
        </tr>
        <tr>
            <td><asp:TextBox ID="TB_recherche" CssClass="form-control" runat="server" OnTextChanged="TB_recherche_TextChanged" placeholder="EPIiD..." name="Recherche" AutoPostBack="true" TextMode="Search"></asp:TextBox></td>
        </tr>
    </table>
    <hr />

    <div class="responsive-table-line" style="margin:0px auto;max-width:700px;">
	    <table class="table table-bordered table-condensed table-body-center" >
	        <tbody>
	            <tr>
	                <td data-title="Droit">EpiID</td>
	                <td data-title="Valeur alphanumérique">---</td>
	            </tr>
	            <tr>
	                <td data-title="Droit">Marque et modèle</td>
	                <td data-title="Valeur alphanumérique">---</td>
	            </tr>
	            <tr>
	                <td data-title="Droit">Numéro de série (S/N)</td>
	                <td data-title="Valeur alphanumérique">---</td>
	            </tr>
	            <tr>
	                <td data-title="Droit">Statut</td>
	                <td data-title="Valeur alphanumérique">---</td>
	            </tr>
	            <tr>
	                <td data-title="Droit">Localisation actuelle</td>
	                <td data-title="Valeur alphanumérique">---</td>
	            </tr>
	            <tr>
	                <td data-title="Droit">Stocké par</td>
	                <td data-title="Valeur alphanumérique">---</td>
	            </tr>
	            <tr>
	                <td data-title="Droit">Stocké le</td>
	                <td data-title="Valeur alphanumérique">---</td>
	            </tr>
	        </tbody>
	    </table>
	</div>
</asp:Content>
