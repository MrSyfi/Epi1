<%@ Page Title="CopyLabel" MasterPageFile="~/Site.Master" Language="C#" AutoEventWireup="true" CodeBehind="CopyLabel.aspx.cs" Inherits="AccessApp.CopyLabel" EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="page-header" style="text-align: center;">
        <h2>Copie d'étiquette</h2>
    </div>

    <p>
        <label for="DDL_Printer">Où imprimer ?</label>
        <asp:DropDownList runat="server" CssClass="form-control" ID="DDL_Printer">
            <asp:ListItem Text="" value=""></asp:ListItem>
            <asp:ListItem Text="Hornu" value="HU"></asp:ListItem>
            <asp:ListItem Text="Ath" value="AH"></asp:ListItem>
            <asp:ListItem Text="Baudour" value="BR"></asp:ListItem>
        </asp:DropDownList>
    </p>
    <br />
    <p>
        <b><asp:Label ID="L_id_op" runat="server">Id de l'opérateur *</asp:Label></b>
        <asp:TextBox ID="TB_id_op" CssClass="form-control input-lg" runat="server" name="TB_id_op"></asp:TextBox>
    </p>
    <p>
        <b><asp:Label ID="L_EpiID" runat="server">EpiID</asp:Label></b>
        <asp:TextBox ID="TB_EpiID" CssClass="form-control input-lg" runat="server" name="TB_EpiID"></asp:TextBox>
    </p>
    <p>
        <asp:Button ID="B_apply" CssClass="btn btn-primary btn-lg" runat="server" OnClick="B_apply_Click" Text="Confirmer" Enabled="true"/>
    </p>
</asp:Content>



