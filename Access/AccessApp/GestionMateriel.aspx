<%@ Page Title="EpiCMBD" MasterPageFile="~/Site.Master" Language="C#" AutoEventWireup="true" CodeBehind="GestionMateriel.aspx.cs" Inherits="AccessApp.GestionMateriel" enableEventValidation="false" MaintainScrollPositionOnPostback="true" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
   
    <div class="page-header" style="text-align:center;">
        <h2>EpiCMBD</h2>
    </div> 
   
    <asp:TextBox ID="TB_recherche" CssClass="form-control" runat="server" OnTextChanged="TB_recherche_TextChanged" placeholder="EPIiD..." name="Recherche" AutoPostBack="true" TextMode="Search" style="margin: auto"></asp:TextBox>

    <hr />
    
    <asp:Literal runat="server" ID="L_Body" ></asp:Literal>
    

    <section id="cd-timeline" class="cd-container">
		<div class="cd-timeline-block">
			<div class="cd-timeline-img cd-picture">
				<img src="img/cd-icon-picture.svg" alt="Picture">
			</div> <!-- cd-timeline-img -->

			<div class="cd-timeline-content">
				<h2>Title of section 1</h2>
				<p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Iusto, optio, dolorum provident rerum aut hic quasi placeat iure tempora laudantium ipsa ad debitis unde? Iste voluptatibus minus veritatis qui ut.</p>
				<a href="#0" class="cd-read-more">Read more</a>
				<span class="cd-date">Jan 14</span>
			</div> <!-- cd-timeline-content -->
		</div> <!-- cd-timeline-block -->

		
	</section> <!-- cd-timeline -->

</asp:Content>
