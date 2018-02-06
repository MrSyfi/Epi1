<%@ Page Title="Gestion" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Gestion.aspx.cs" Inherits="AccessApp.Gestion" EnableEventValidation="false" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <label for="Recherche">Recherche</label>
    <asp:TextBox ID="TB_recherche" CssClass="form-control input-lg" runat="server" OnTextChanged="TB_recherche_TextChanged" placeholder="Nom, prénom, id..." name="Recherche" AutoPostBack="true"></asp:TextBox>
 

    <hr />
    <asp:Label runat="server" ID="L_result">Pas de résultat</asp:Label>
    <asp:GridView ID="GridView1" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" OnPageIndexChanging="OnPaging" runat="server" AllowPaging="true" PageSize="10" OnRowCreated="GridView1_RowCreated" OnRowCommand="GridView1_RowCommand">
        <Columns>
             <asp:BoundField DataField="ID" HeaderText="ID" />
             <asp:BoundField DataField="LAST_NAME" HeaderText="Nom" />
             <asp:BoundField DataField="FIRST_NAME" HeaderText="Prénom" />
             <asp:BoundField DataField="USERNAME" HeaderText="Username" />
             <asp:BoundField DataField="PHONE_NBR" HeaderText="Téléphone" />
            <asp:BoundField DataField="SERVICE" HeaderText="Service" />
         </Columns>
    </asp:GridView>
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
            <td> 
                <label for="TB_username">Username</label>
                <asp:TextBox ID="TB_username" CssClass="form-control input-lg" runat="server" Enabled="false" name="TB_username"></asp:TextBox>
            </td>
            <td> 
                <label for="TB_service">Service</label>
                <asp:TextBox ID="TB_service" CssClass="form-control input-lg" runat="server" Enabled="false" name="TB_service"></asp:TextBox>
            </td>
        </tr>
    </table>

    <asp:DropDownList runat="server" ID="DDL_status">
        <asp:ListItem>test 1</asp:ListItem>
        
        <asp:ListItem>test 2</asp:ListItem>
    </asp:DropDownList>

    
    <asp:TextBox ID="TextBox3" Cssclass="form-control input-lg" runat="server" ></asp:TextBox>
    <asp:Button ID="Btn" runat="server" OnClick="Btn_Click"/>

        


</asp:Content>

