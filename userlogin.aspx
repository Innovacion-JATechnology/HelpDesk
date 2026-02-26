<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="userlogin.aspx.cs" Inherits="HelpDesk.userlogin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container">
        <div class="row">
            <div class="col-md-6 mx-auto">

                <div class="card">
                    <div class="card-body">

                        <div class="row">
                            <div class="col">
                                <center>
                                    <img width="150px" src="imgs/usuario.png" />
                                </center>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col">
                                <center>
                                    <h3>Ingreso de Usuarios</h3>
                                </center>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col">
                                <center>
                                    <hr>
                                </center>
                            </div>
                        </div>


                        <div class="row">
                            <div class="col">
                                <label>Usuario</label>
                                <div class="form-group">
                                    <asp:TextBox CssClass="form-control" ID="usuario"
                                        runat="server" placeholder="Usuario"></asp:TextBox>
                                </div>

                                <label>Contraseña ID</label>
                                <div class="form-group">
                                    <asp:TextBox CssClass="form-control" ID="contrasena"
                                        runat="server" placeholder="Contraseña"></asp:TextBox>
                                </div>

                                <div class="form-group">
                                    <asp:Button ID="continuarIngreso"
                                        runat="server"
                                        Text="Continuar"
                                        CssClass="btn btn-primary btn-block btn-lg btn-2b399b" OnClick="Continuar_LogIn_Click" />
                                </div>
                            </div>
                        </div>


                    </div>
                </div>

                <a href="homepage.aspx"><< Regresar al Inicio</a><br>
                <br />
            </div>
        </div>
    </div>
</asp:Content>
