<%@ Page Title="EpiLabel" MasterPageFile="~/Site.Master" Language="C#" AutoEventWireup="true" CodeBehind="EpiLabel.aspx.cs" Inherits="AccessApp.EpiLabel" enableEventValidation="false" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="page-header">
        <h2>EpiLabel</h2>
    </div> 

    <p>
        <label for="TB_code">Code QR</label>
        <asp:TextBox ID="TB_code" CssClass="form-control input-lg" runat="server" name="TB_code"></asp:TextBox>
    </p>
    
    <p>
        <label for="TB_info">Info</label>
        <asp:TextBox ID="TB_info" CssClass="form-control input-lg" runat="server" name="TB_info"></asp:TextBox>
    </p>

    <asp:Button ID="B_afficher" CssClass="btn btn-danger btn-lg" runat="server" OnClick="B_afficher_Click" Text="Afficher"/> 
</asp:Content>



