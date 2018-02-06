<%@ Page Title="Gestion" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Gestion.aspx.cs" Inherits="AccessApp.Gestion" EnableEventValidation="false" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
  
    <asp:TextBox ID="TB_recherche" class="form-control" runat="server" OnTextChanged="TB_recherche_TextChanged" AutoPostBack="true"></asp:TextBox>

    
   
    <asp:GridView ID="GridView1" CssClass="table table-striped table-bordered table-hover" runat="server" AllowPaging="false" OnRowCreated="GridView1_RowCreated" OnRowCommand="GridView1_RowCommand"></asp:GridView>


    <asp:TextBox ID="TextBox1" class="form-control" runat="server" OnTextChanged="TB_recherche_TextChanged" Enabled="false"></asp:TextBox>
     <asp:TextBox ID="TextBox2" class="form-control" runat="server" OnTextChanged="TB_recherche_TextChanged" Enabled="false"></asp:TextBox>

       
     <asp:TextBox ID="TextBox3" class="form-control" runat="server" ></asp:TextBox>
     <asp:Button ID="Btn" runat="server" OnClick="Btn_Click"/>

</asp:Content>