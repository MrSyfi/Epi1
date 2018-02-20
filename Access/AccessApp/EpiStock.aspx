<%@ Page Title="EpiCMBD" MasterPageFile="~/Site.Master" Language="C#" AutoEventWireup="true" CodeBehind="EpiStock.aspx.cs" Inherits="AccessApp.EpiStock" enableEventValidation="false" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
   
    <div class="page-header" style="text-align:center;">
        <h2>EpiCMBD</h2>
    </div> 

    <label for="TB_id_resp">Id de l'opérateur</label>
    <asp:TextBox ID="TB_id_resp" CssClass="form-control input-lg" runat="server" name="TB_id_resp"></asp:TextBox>

    <label for="TB_id_materiel">Id du matériel</label>
    <asp:TextBox ID="TB_id_materiel" CssClass="form-control input-lg" runat="server" name="TB_id_materiel" OnTextChanged="TB_id_materiel_TextChanged"></asp:TextBox>

    <label for="DDL_status">Statut</label>
    <asp:DropDownList runat="server" CssClass="form-control" ID="DDL_status" Enabled="false"></asp:DropDownList>

    <label for="TB_id_local">Localisation</label>
    <asp:TextBox ID="TB_id_local" CssClass="form-control input-lg" runat="server" name="TB_id_local"></asp:TextBox>

    <label for="TB_id_local">Note (optionnel)</label>
    <asp:TextBox id="TextArea1" CssClass="form-control input-lg" TextMode="multiline" Columns="50" Rows="5" runat="server" />

    <asp:Button ID="B_apply" CssClass="btn btn-primary btn-lg" runat="server" OnClick="B_apply_Click" Text="Confirmer" Enabled="false"/>

</asp:Content>
