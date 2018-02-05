<%@ Page Title="Gestion" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Gestion.aspx.cs" Inherits="AccessApp.Gestion" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <asp:TextBox ID="TB_recherhce" class="form-control" runat="server" OnTextChanged="TB_recherhce_TextChanged"></asp:TextBox>
    <asp:Label ID="Lbl_resultat" runat="server"></asp:Label>


</asp:Content>