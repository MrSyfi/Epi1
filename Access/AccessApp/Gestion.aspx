<%@ Page Title="Gestion" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Gestion.aspx.cs" Inherits="AccessApp.Gestion" EnableEventValidation="false" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <asp:TextBox ID="TB_recherche" class="form-control" runat="server" OnTextChanged="TB_recherche_TextChanged" Width="100%"></asp:TextBox>
    <asp:Label runat="server" ID="test"></asp:Label>
       
    <asp:GridView ID="GridView1" CssClass="table table-striped table-bordered table-hover" runat="server" AllowPaging="false" OnRowCreated="GridView1_RowCreated" OnRowCommand="GridView1_RowCommand"></asp:GridView>

</asp:Content>