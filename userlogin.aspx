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
                                <asp:TextBox CssClass="form-control" ID="TextBox1"
                                    runat="server" placeholder="Usuario"></asp:TextBox>
                            </div>

                            <label>Contraseña ID</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="TextBox2"
                                    runat="server" placeholder="Contraseña"></asp:TextBox>
                            </div> 
                            
                            <div class="form-group">
                                <asp:Button ID="Button1"
                                    runat="server"
                                    Text="Continuar"
                                    CssClass="btn btn-primary btn-block btn-lg btn-2b399b" />
                            </div>


                            <div class="form-group">
                                <a href="usersignup.aspx">    <input class="id=btn btn-secondary btn-block btn-lg" ="Button2"  type="button" value="Registro" />
                            </div>
                        </div>
                    </div>


                </div>
            </div>

            <a href="homepage.aspx"><< Regresar al Inicio</a><br><br />
        </div>
    </div>
    </div>
</asp:Content>
