﻿<%@ Page Title="EpiLabel" MasterPageFile="~/Site.Master" Language="C#" AutoEventWireup="true" CodeBehind="EpiLabel.aspx.cs" Inherits="AccessApp.EpiLabel" enableEventValidation="false" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="page-header">
        <h2>EpiLabel</h2>
    </div> 

    <p>
        <label for="DDL_Printer">Site</label>
        <asp:DropDownList runat="server" ID="DDL_Printer" />
    </p>

    <p>
        <label for="TB_code">Code QR</label>
        <asp:TextBox ID="TB_code" CssClass="form-control" runat="server" name="TB_code"></asp:TextBox>
    </p>
    
    <p>
        <label for="TB_info">Info</label>
        <asp:TextBox ID="TB_info" CssClass="form-control" runat="server" name="TB_info"></asp:TextBox>
    </p>

    <asp:Button ID="B_afficher" CssClass="btn btn-primary" runat="server" OnClick="B_afficher_Click" Text="Générer"/> 

    <hr />

    <div class="input-group">
        <span class="input-group-btn">
            <span class="btn btn-primary btn-file">
                Browse&hellip; <input type="file" single>
            </span>
        </span>
        <input type="text" class="form-control" readonly>
    </div>
    <br/>

    <asp:Button ID="B_generer_fichier" CssClass="btn btn-primary" runat="server" OnClick="B_generer_fichier_Click" Text="Générer"/> 


    <p>
        <asp:Literal ID="L_result" runat="server"></asp:Literal>
    </p>

</asp:Content>



