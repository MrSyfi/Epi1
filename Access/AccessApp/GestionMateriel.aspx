<%@ Page Title="EpiCMDB" MasterPageFile="~/Site.Master" Language="C#" AutoEventWireup="true" CodeBehind="GestionMateriel.aspx.cs" Inherits="AccessApp.GestionMateriel" EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">



    <div class="page-header" style="text-align: center;">
        <h2>EpiCMBD</h2>
    </div>

    <table style="margin:auto;">
         <tr>
             <td><asp:TextBox ID="TB_recherche" CssClass="form-control input-lg" runat="server" name="TB_recherche" placeholder="EpiID"></asp:TextBox></td>
             <td><asp:Button runat="server" ID="B_afficher" CssClass="btn btn-primary btn-lg" Text="Afficher" OnClick="B_afficher_Click"/></td>
         </tr>
    </table>

    <hr />

    <asp:Literal runat="server" ID="L_Body"></asp:Literal>
    <asp:Literal runat="server" ID="L_Histo"></asp:Literal>


</asp:Content>
