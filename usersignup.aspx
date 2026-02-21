<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="usersignup.aspx.cs" Inherits="HelpDesk.usersignup" %>

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
                                    <h3>Registro</h3>
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
                            <div class="col-md-4">
                                <label>Nombre(s)</label>
                                <div class="form-group">
                                    <asp:TextBox CssClass="form-control" ID="TextBox3"
                                        runat="server" placeholder="Nombre(s)"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-4">
                                <label>Apellido Paterno</label>
                                <div class="form-group">
                                    <asp:TextBox CssClass="form-control" ID="TextBox4"
                                        runat="server" placeholder="Apellido Paterno"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-4">
                                <label>Apellido Materno</label>
                                <div class="form-group">
                                    <asp:TextBox CssClass="form-control" ID="TextBox9"
                                        runat="server" placeholder="Apellido Materno"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">

                            <div class="col-md-6">
                                <label>Correo-e</label>
                                <div class="form-group">
                                    <asp:TextBox CssClass="form-control" ID="TextBox5"
                                        runat="server" placeholder="Correo-e" TextMode="Email"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-6">
                                <label>Numero de Contacto</label>
                                <div class="form-group">
                                    <asp:TextBox CssClass="form-control" ID="TextBox6"
                                        runat="server" placeholder="Número de Contacto" TextMode="Phone"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-4">
                                <label>Empresa</label>
                                <div class="form-group">
                                    <asp:DropDownList class="form-control" ID="DropDownList1" runat="server">
                                        <asp:ListItem Text="Select" Value="Select" />
                                        <asp:ListItem Text="Cepesmar" Value="Cepesmar" />
                                        <asp:ListItem Text="JA Technology" Value="JA Technology" />
                                        <asp:ListItem Text="JA Carrier" Value="JA Carrier" />
                                        <asp:ListItem Text="JA Forwarding" Value="JA Forwarding" />
                                        <asp:ListItem Text="Otra" Value="Otra" />
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="col-md-6">
                                <label>Puesto</label>
                                <div class="form-group">
                                    <asp:TextBox CssClass="form-control" ID="TextBox8"
                                        runat="server" placeholder="Puesto"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-2">
                                <label>SLA</label>
                                <div class="form-group">
                                    <asp:DropDownList class="form-control" ID="DropDownList2" runat="server">

                                        <asp:ListItem Text="1" Value="1" />
                                        <asp:ListItem Text="2" Value="2" />
                                        <asp:ListItem Text="3" Value="3" />
                                        <asp:ListItem Text="4" Value="4" />
                                        <asp:ListItem Text="5" Value="5" />
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col text-center">
                                <span class="badge badge-pill badge-primary align-middle">Credenciales</span>
                                <br/> <br/>
                            </div>
                        </div>


                        <div class="row">
                            <div class="col-md-6">
                                <label>ID</label>
                                <div class="form-group">
                                    <asp:TextBox CssClass="form-control" ID="TextBox1"
                                        runat="server" placeholder="ID"></asp:TextBox>
                                </div>
                                </div>
                            <div class="col-md-6">
                                <label>Contraseña</label>
                                <div class="form-group">
                                    <asp:TextBox CssClass="form-control" ID="TextBox2"
                                        runat="server" placeholder="Contraseña" TextMode="Password"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col">
                                <div class="form-group">
                                    <asp:Button ID="Button1"
                                        runat="server"
                                        Text="Continuar"
                                        CssClass="btn btn-primary btn-block btn-lg btn-2b399b" OnClick="Button1_Click" />
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
