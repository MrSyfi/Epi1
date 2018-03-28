<%@ Page Title="LinkObjet" MasterPageFile="~/Site.Master" Language="C#" AutoEventWireup="true" CodeBehind="LinkObject.aspx.cs" Inherits="AccessApp.LinkObject" EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="page-header" style="text-align: center;">
        <h2>Lier un matériel</h2>
    </div>
    <table>
        <tr>
            <td>
                <p>
                    <b><asp:Label ID="L_id_ticket" runat="server">Id du ticket</asp:Label></b>
                    <asp:TextBox ID="TB_id_ticket" CssClass="form-control input-lg" runat="server" name="TB_id_ticket" AutoPostBack="true" OnTextChanged="TB_id_ticket_TextChanged"></asp:TextBox>
                </p>
                <p>
                    <b><asp:Label ID="L_id_op" runat="server">Id de l'opérateur</asp:Label></b>
                    <asp:TextBox ID="TB_id_op" CssClass="form-control input-lg" runat="server" name="TB_id_op" AutoPostBack="true" OnTextChanged="TB_id_op_TextChanged"></asp:TextBox>
                </p>
                <p>
                    <b><asp:Label ID="L_EpiID" runat="server">EpiID</asp:Label></b>
                    <asp:TextBox ID="TB_EpiID" CssClass="form-control input-lg" runat="server" name="TB_EpiID" AutoPostBack="true" OnTextChanged="TB_EpiID_TextChanged"></asp:TextBox>
                </p>
            </td>
        </tr>
        <tr>
            <td align='right'>
                <p>
                    <asp:Button ID="B_apply" CssClass="btn btn-primary btn-lg" runat="server" OnClick="B_apply_Click" Text="Confirmer" Enabled="true"/>
                </p>
            </td>
        </tr>
    </table>

</asp:Content>
