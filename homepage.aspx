<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="homepage.aspx.cs" Inherits="HelpDesk.homepage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section> 
        <img src="imgs/barra.png" class="img-fluid w-100 d-block" alt="barra">
    </section>
    <section>
        <div class="container">
            <div class="row">
                <div class="col-12">
                    <center>
                    <h2> Soporte</h2>
                    <p><b> </b></p>                        
                    </center>
                </div>
            </div>

            <div class="row">
                <div class="col-md-4">
                    <center>
                        <img width="200px" src="imgs/levanta.png" />
                            <h4> Crear Ticket</h4>
                        <p class="text-center">
                           Click para crear tickets para el area de soporte.
                        </p>
                    </center>
                </div>
             
                <div class="col-md-4">
                    <center>
                        <img width="222px" src="imgs/ticket.png" />
                        <h4>Mis Tickets</h4>
                        <p class="text-center">
                            Click para ver historial de tickets.
                        </p>
                    </center>
                </div> 

                <div class="col-md-4">
                    <center>
                        <img width="200px" src="imgs/preferencias.png" />
                        <h4>Preferencias</h4>
                        <p class="text-center">
                            Preferencias de la aplicación
                        </p>
                    </center>
                </div>
            
            </div>

        </div>
    </section>
</asp:Content>
