<%@ Page Title="Gestion" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Gestion.aspx.cs" Inherits="AccessApp.Gestion" EnableEventValidation="false" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <label for="Recherche">Recherche</label>
    <asp:TextBox ID="TB_recherche" CssClass="form-control input-lg" runat="server" OnTextChanged="TB_recherche_TextChanged" placeholder="Nom, prénom, id..." name="Recherche" AutoPostBack="true"></asp:TextBox>
 

    <hr />
    <asp:Label runat="server" ID="L_result">Pas de résultat</asp:Label>
    <asp:GridView ID="GridView1" CssClass="table table-striped table-bordered table-hover" runat="server" AllowPaging="false" OnRowCreated="GridView1_RowCreated" OnRowCommand="GridView1_RowCommand"></asp:GridView>
    <hr />
    <table>
        <tr>
            <td> 
                <label for="TB_id">ID</label>
                <asp:TextBox ID="TB_id" CssClass="form-control input-lg" runat="server"  Enabled="false" name="TB_id"></asp:TextBox>
            </td>
            <td> 
                <label for="TB_last_name">Last Name</label>
                <asp:TextBox ID="TB_last_name" CssClass="form-control input-lg" runat="server" Enabled="false" name="TB_last_name"></asp:TextBox>
            </td>
            <td> 
                <label for="TB_first_name">First Name</label>
                <asp:TextBox ID="TB_first_name" CssClass="form-control input-lg" runat="server" Enabled="false" name="TB_first_name"></asp:TextBox>
            </td>
        </tr>
    </table>

    <asp:DropDownList runat="server" ID="DDL_status">
        <asp:ListItem>test 1</asp:ListItem>
        
        <asp:ListItem>test 2</asp:ListItem>
    </asp:DropDownList>

    <asp:Button ID="Btn" runat="server" OnClick="Btn_Click"/>

        


</asp:Content>

