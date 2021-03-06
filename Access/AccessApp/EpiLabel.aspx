﻿<%@ Page Title="EpiLabel" MasterPageFile="~/Site.Master" Language="C#" AutoEventWireup="true" CodeBehind="EpiLabel.aspx.cs" Inherits="AccessApp.EpiLabel" EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="page-header" style="text-align: center;">
        <h2>EpiLabel</h2>
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

    <div class="left">

        <h2>Manuellement</h2>

        <hr />
        <p>
            <label for="TB_code">Données du code QR</label>
            <asp:TextBox ID="TB_code" CssClass="form-control" runat="server" MaxLength="15" name="TB_code" placeholder="ex: 1-020-01"></asp:TextBox>
        </p>

        <p>
            <label for="TB_info">Titre de l'étiquette</label>
            <asp:TextBox ID="TB_info" CssClass="form-control" runat="server" name="TB_info" MaxLength="30" placeholder="ex: BAU Economat IN"></asp:TextBox><br />
        </p>

    </div>

    <div class="right">

        <h2>Depuis un fichier </h2>
        <hr />
        <p>
            <asp:FileUpload ID="FileUploader" runat="server" AllowMultiple="false" /><br />
        </p>

    </div>

    <table width="100%">
        <tr>
            <td width="45%" align='right'>
                <asp:Button ID="B_afficher" CssClass="btn btn-primary " runat="server" OnClick="B_afficher_Click" Text="Imprimer" />
            </td>
            <td width="50%" align='right'>
                <asp:Button ID="B_generer_fichier" CssClass="btn btn-primary " runat="server" OnClick="B_generer_fichier_Click" Text="Imprimer" />
            </td>
        </tr>
    </table>
</asp:Content>



