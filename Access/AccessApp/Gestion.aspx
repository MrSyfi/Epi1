<%@ Page Title="Gestion" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Gestion.aspx.cs" Inherits="AccessApp.Gestion" enableEventValidation="false"%>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <table>
        <tr>
            <td>
                <label for="Recherche">Recherche</label>
                <asp:TextBox ID="TB_recherche" CssClass="form-control" runat="server" OnTextChanged="TB_recherche_TextChanged" placeholder="Nom, prénom..." name="Recherche" AutoPostBack="true"></asp:TextBox>
            </td>
            <td>
                <label for="Nb_element">Nombre d'élément par page</label>
                <asp:DropDownList runat="server" CssClass="form-control" ID="DDL_nb_page" OnSelectedIndexChanged="DDL_nb_page_SelectedIndexChanged" AutoPostBack="true" name="Nb_element">
                    <asp:ListItem>10</asp:ListItem>
                    <asp:ListItem>25</asp:ListItem>
                    <asp:ListItem>50</asp:ListItem>
                    <asp:ListItem>100</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    
    <hr />
    <h3><asp:Label runat="server" ID="L_result">Pas de résultat</asp:Label></h3>
    <asp:GridView ID="GridView1" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" OnPageIndexChanging="OnPaging" runat="server" AllowPaging="true" PageSize="10" OnRowCreated="GridView1_RowCreated" OnRowCommand="GridView1_RowCommand">
        <Columns>
            <asp:BoundField DataField="ID" HeaderText="ID" />
            <asp:BoundField DataField="LAST_NAME" HeaderText="Nom" />
            <asp:BoundField DataField="FIRST_NAME" HeaderText="Prénom" />
            <asp:BoundField DataField="USERNAME" HeaderText="Username" />
            <asp:BoundField DataField="PHONE_NBR" HeaderText="Téléphone" />
            <asp:BoundField DataField="SERVICE" HeaderText="Service" />
            <asp:BoundField DataField="AR_STATUS" HeaderText="Statut" />
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
                <label for="TB_last_name">Nom</label>
                <asp:TextBox ID="TB_last_name" CssClass="form-control input-lg" runat="server" Enabled="false" name="TB_last_name"></asp:TextBox>
            </td>
            <td> 
                <label for="TB_first_name">Prénom</label>
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
        <tr>
            <td>
                <label for="TB_status">Status</label>
                <asp:DropDownList runat="server" CssClass="form-control" ID="DDL_status" Enabled="false"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="Btn" CssClass="btn btn-primary btn-lg" runat="server" OnClick="Btn_Click" Text="Confirmer" Enabled="false"/>
            </td>
        </tr>
    </table>
</asp:Content>

