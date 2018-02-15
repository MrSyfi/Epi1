<%@ Page Title="EpiCMBD" MasterPageFile="~/Site.Master" Language="C#" AutoEventWireup="true" CodeBehind="GestionMateriel.aspx.cs" Inherits="AccessApp.GestionMateriel" enableEventValidation="false" MaintainScrollPositionOnPostback="true" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
   
    <div class="page-header" style="text-align:center;">
        <h2>EpiCMBD</h2>
    </div> 
   
    <asp:TextBox ID="TB_recherche" CssClass="form-control" runat="server" OnTextChanged="TB_recherche_TextChanged" placeholder="EpiID..." name="Recherche" AutoPostBack="true" TextMode="Search" style="margin: auto"></asp:TextBox>

    <hr />
    
    <asp:Literal runat="server" ID="L_Body" ></asp:Literal>
    <asp:Literal runat="server" ID="L_Histo"></asp:Literal>

</asp:Content>
