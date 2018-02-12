<%@ Page Title="EpiACCESS Close" MasterPageFile="~/Site.Master" Language="C#" AutoEventWireup="true" CodeBehind="EpiACCESS_Close.aspx.cs" Inherits="AccessApp.EpiACCESS_Close" enableEventValidation="false" MaintainScrollPositionOnPostback="true" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-header" style="text-align:center;">
        <h2>Fermeture EpiACCESS</h2>
    </div>
        
    <br />
    <table>
        <tr>
            <th>Recherche</th>
            <th>Nombre d'élément par page</th>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="TB_recherche" CssClass="form-control" runat="server" OnTextChanged="TB_recherche_TextChanged" placeholder="Nom, prénom, service,..." name="Recherche" AutoPostBack="true" TextMode="Search"></asp:TextBox>
            </td>
            <td>
                <asp:DropDownList runat="server" CssClass="form-control" ID="DDL_nb_page" OnSelectedIndexChanged="DDL_nb_page_SelectedIndexChanged" AutoPostBack="true" name="Nb_element">
                    <asp:ListItem>5</asp:ListItem>
                    <asp:ListItem Selected="True">10</asp:ListItem>
                    <asp:ListItem>25</asp:ListItem>
                    <asp:ListItem>50</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
    </table>

     <hr />
    <h4><i><asp:Label runat="server" ID="L_result">Pas de résultat...</asp:Label></i></h4>
    <asp:GridView ID="GridView1" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" OnPageIndexChanging="OnPaging" runat="server" AllowPaging="true" PageSize="10" OnRowCreated="GridView1_RowCreated" OnRowCommand="GridView1_RowCommand" AutoPostBack="false">
        <Columns>
            <asp:BoundField DataField="ID" HeaderText="ID" />
            <asp:BoundField DataField="LAST_NAME" HeaderText="Nom" />
            <asp:BoundField DataField="FIRST_NAME" HeaderText="Prénom" />
            <asp:BoundField DataField="USERNAME" HeaderText="Nom d'utilisateur" />
            <asp:BoundField DataField="PHONE_NBR" HeaderText="Téléphone" />
            <asp:BoundField DataField="SERVICE" HeaderText="Service" />
            <asp:BoundField DataField="AR_STATUS" HeaderText="Statut" />
            <asp:BoundField DataField="RESP_EMAIL" HeaderText="Email Resp." />
            <asp:BoundField DataField="TICKET_ID" HeaderText="ID Ticket" />
         </Columns>

    </asp:GridView>

    <hr />
    <table id="update">
        <tr>
            <th>ID</th>
            <th>Nom</th>
            <th>Prénom</th>
            <th>Nom d'utilisateur</th>
            <th>Service</th>
        </tr>
        <tr>
            <td> 
                <asp:TextBox ID="TB_id" CssClass="form-control input-lg" runat="server"  Enabled="false" name="TB_id"></asp:TextBox>
            </td>
            <td> 
                <asp:TextBox ID="TB_last_name" CssClass="form-control input-lg" runat="server" Enabled="false" name="TB_last_name"></asp:TextBox>
            </td>
            <td> 
                <asp:TextBox ID="TB_first_name" CssClass="form-control input-lg" runat="server" Enabled="false" name="TB_first_name"></asp:TextBox>
            </td>
            <td> 
                <asp:TextBox ID="TB_username" CssClass="form-control input-lg" runat="server" Enabled="false" name="TB_username"></asp:TextBox>
            </td>
            <td> 
                <asp:TextBox ID="TB_service" CssClass="form-control input-lg" runat="server" Enabled="false" name="TB_service"></asp:TextBox>
            </td>
        </tr>
        <tr>    
            <td> 
                <label for="TB_ticket">ID Ticket</label>
                <asp:TextBox ID="TB_ticket" CssClass="form-control input-lg" runat="server" Enabled="false" name="TB_ticket"></asp:TextBox>
            </td>
             <td colspan="2"> 
                <label for="TB_resp_mail">Mail du responsable</label>
                <asp:TextBox ID="TB_resp_mail" CssClass="form-control input-lg" runat="server" Enabled="false" name="TB_resp_mail" TextMode="Email"></asp:TextBox>
            </td>
        </tr>
    </table>
    <hr />
    <h4><asp:Label runat="server" Text="Preview de l'email généré : " ID="L_preview" Visible="false"></asp:Label></h4>
    <br />
    <asp:Literal runat="server" ID="L_mail" Mode="PassThrough"></asp:Literal>

    <asp:Button ID="B_apply" CssClass="btn btn-primary btn-lg" runat="server" OnClick="B_apply_Click" Text="Fermer la demande d'accès"  Visible="false"/>


</asp:Content>
