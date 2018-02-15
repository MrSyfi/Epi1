﻿<%@ Page Title="EpiCMBD" MasterPageFile="~/Site.Master" Language="C#" AutoEventWireup="true" CodeBehind="GestionMateriel.aspx.cs" Inherits="AccessApp.GestionMateriel" enableEventValidation="false" MaintainScrollPositionOnPostback="true" %>


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
            <thead>
                <tr>
                    <th>EpiID</th>
                    <th>Marque et modèle</th>
                    <th>Numéro de série (S/N)</th>
                    <th>Statut</th>
                    <th>Localisation actuelle</th>
                    <th>Stocké par</th>
                    <th>Stocké le</th>
                </tr>
            </thead>
	        <tbody>
	            <tr>
	                <td data-title="EpiID">---</td>
	            </tr>
	            <tr>
	                <td data-title="Marque et modèleoit">---</td>
	            </tr>
	            <tr>
	                <td data-title="Numéro de série (S/N)">---</td>
	            </tr>
	            <tr>
	                <td data-title="Statut">---</td>
	            </tr>
	            <tr>
	                <td data-title="Localisation actuelle">---</td>
	            </tr>
	            <tr>
	                <td data-title="Stocké par">---</td>
	            </tr>
	            <tr>
	                <td data-title="Stocké le">---</td>
	            </tr>
	        </tbody>
	    </table>
	</div>
</asp:Content>
