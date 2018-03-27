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
    <hr />
</asp:Content>



