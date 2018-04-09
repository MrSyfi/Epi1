<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LogOn.aspx.cs" Inherits="AccessApp.LogOn"  EnableEventValidation="false" MaintainScrollPositionOnPostback="true"%>

<form runat="server" >
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap.min.css" rel="stylesheet" crossorigin="anonymous">
    <div class="page-header" style="text-align: center;">
        <h2>Authentification EpiTOOLS</h2>
    </div>
    <div style="text-align: center;">
        <table  style="margin:auto;">
            <tr>
                <td>
                    <p>
                        <label for="txtUserName">Nom d'utilisateur</label>
                        <asp:TextBox ID="txtUserName" type="text" CssClass="form-control" runat="server" name="txtUserName"></asp:TextBox>
                        <asp:RequiredFieldValidator ControlToValidate="txtUserName" Display="Static" ErrorMessage="*" runat="server" ID="vUserName" />
                    </p>
                </td>
            </tr>
            <tr>
                <td>
                    <p>
                        <label for="txtUserPass">Mot de passe</label>
                        <asp:TextBox ID="txtUserPass" type="password" CssClass="form-control" runat="server" name="txtUserPass"></asp:TextBox>
                        <ASP:RequiredFieldValidator ControlToValidate="txtUserPass" Display="Static" ErrorMessage="*" runat="server" ID="vUserPass" />
                    </p>
                </td>
            </tr>
            <tr>
                <td>
                    <p><input type="submit" Value="Se connecter" runat="server" ID="cmdLogin" Class="btn btn-primary"></p>
                    <p><asp:Label id="lblMsg" ForeColor="red" Font-Name="Verdana" Font-Size="10" runat="server" /></p>
                </td>
            </tr>
        </table>
    </div>
</form>

