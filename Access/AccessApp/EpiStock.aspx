<%@ Page Title="EpiCMDB" MasterPageFile="~/Site.Master" Language="C#" AutoEventWireup="true" CodeBehind="EpiStock.aspx.cs" Inherits="AccessApp.EpiStock" EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="page-header">
        <h2>EpiCMDB</h2>
    </div>

     <table>
         <tr>
             <td><asp:TextBox ID="TB_id_materiel" CssClass="form-control input-lg" runat="server" name="TB_id_materiel" placeholder="EpiID"></asp:TextBox></td>
             <td><asp:Button runat="server" ID="B_afficher" OnClick="B_afficher_Click" CssClass="btn btn-primary btn-lg" Text="Afficher"/></td>
         </tr>
    </table>
    <hr />
    <p>
        <b><asp:Label ID="IdOperateur" runat="server" Visible="false">Id de l'opérateur *</asp:Label></b>
        <asp:TextBox ID="TB_id_resp" CssClass="form-control input-lg" runat="server" name="TB_id_resp" OnTextChanged="TB_id_resp_TextChanged" AutoPostBack="true" Visible="false"></asp:TextBox>
    </p>

    <p>
        <b><asp:Label ID="statut" runat="server" Visible="false">Statut *</asp:Label></b>
        <asp:DropDownList runat="server" CssClass="form-control" ID="DDL_status" Enabled="false" OnSelectedIndexChanged="DDL_status_SelectedIndexChanged" AutoPostBack="true" Visible="false"></asp:DropDownList>
    </p>

    <p>
        <b><asp:Label ID="localisation" runat="server" Visible="false">Localisation *</asp:Label></b>
        <asp:TextBox ID="TB_id_local" CssClass="form-control input-lg" runat="server" name="TB_id_local" Visible="false"></asp:TextBox>
    </p>



    <p>
       <b><asp:Label id="note" runat="server" Visible="false">Note</asp:Label></b>
        <asp:TextBox ID="TB_note" CssClass="form-control input-lg" TextMode="multiline" Columns="50" Rows="5" runat="server" Visible="false"/>
    </p>

    <p>
        <asp:Button ID="B_apply" CssClass="btn btn-primary btn-lg" runat="server" OnClick="B_apply_Click" Text="Confirmer" Enabled="true" Style="float: right;" Visible="false"/>
    </p>

    <p><i><asp:Label ID="info" runat="server" Visible="false">* Veuillez remplir ces champs.</asp:Label></i></p>


    <p style="text-align: center;">
        <asp:Literal ID="L_obsolete" runat="server" Visible="false"></asp:Literal>
    </p>

    <asp:Button ID="B_obsolete" CssClass="btn btn-danger btn-lg" runat="server" OnClick="B_obsolete_Click" Text="Rendre obsolète" Visible="false" Style="float: right;" />
</asp:Content>
