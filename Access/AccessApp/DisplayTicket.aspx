﻿<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DisplayTicket.aspx.cs" Inherits="AccessApp.DisplayTicket" enableEventValidation="false" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
   
    <div class="page-header" style="text-align:center;">
        <h2>Informations du ticket</h2>
    </div> 
    <asp:Literal runat="server" ID="L_Body" ></asp:Literal>
    <asp:TextBox runat="server" ID="Param"></asp:TextBox>
    
</asp:Content>