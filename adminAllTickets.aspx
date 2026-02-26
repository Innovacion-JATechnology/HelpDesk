<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="adminAllTickets.aspx.cs" Inherits="HelpDesk.adminAllTickets" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
     $(document).ready(function () { 
        $(".table").prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable();
    });
    </script>
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div class="container">
        <div class="row">
            <div class="col-md-6">

                <div class="card">
                    <div class="card-body">



                        <div class="row">
                            <div class="col">
                                <center>
                                    <h3>Usuario</h3>
                                </center>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col">
                                <center>
                                    <img width="150px" src="imgs/allUsers.png" />
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
                                <label>ID</label>
                                <div class="form-group">
                                    <div class="input-group">
                                        <asp:TextBox CssClass="form-control" ID="id"
                                            runat="server" placeholder="ID"></asp:TextBox>
                                        <asp:Button
                                            CssClass="btn btn-primary btn-2b399b"
                                            ID="Button1" runat="server" Text="Go" />
                                    </div>
                                </div>
                            </div>


                            <div class="col-md-8">
                                <label>Nombre</label>
                                <div class="form-group">
                                    <asp:TextBox CssClass="form-control" ID="nombre"
                                        runat="server" placeholder="Nombre"></asp:TextBox>
                                </div>
                            </div>

                        </div>


                        <div class="row">
                            <div class="col-4">
                                <asp:Button ID="bttnAgregar" class="btn btn-lg btn-block btn-success"
                                    runat="server" Text="Agregar" OnClick="bttnAgregar_Click" />

                            </div>
                            <div class="col-4">
                                <asp:Button ID="bttnActualizar" class="btn btn-lg btn-block btn-warning"
                                    runat="server" Text="Actualizar" OnClick="bttnActualizar_Click" />

                            </div>

                            <div class="col-4">
                                <asp:Button ID="bttnBorrar" class="btn btn-lg btn-block btn-danger"
                                    runat="server" Text="Borrar" OnClick="bttnBorrar_Click" />

                            </div>
                        </div>


                    </div>

                </div>

                <a href="homepage.aspx"><< Regresar al Inicio</a><br />
                <br />
            </div>

            <div class="col-md-6">
                <div class="card">
                    <div class="card-body">



                        <div class="row">
                            <div class="col">
                                <center>
                                    <h3>Historial de  Tickets</h3>

                                </center>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col">
                                <center>
                                    <img width="150px" src="imgs/allTickets.png" />
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
                                <hr />
                            </div>
                        </div>

                        <div class="row">
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ServerCon %>" SelectCommand="SELECT * FROM [hd].[Ticket]"></asp:SqlDataSource>
                            <div class="col">
                                <asp:GridView class="table table-striped table-bordered"
                                    ID="GridView1" runat="server" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="TicketId" DataSourceID="SqlDataSource1" ForeColor="#333333" GridLines="None" Width="222px">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:BoundField DataField="TicketId" HeaderText="TicketId" InsertVisible="False" ReadOnly="True" SortExpression="TicketId" />
                                      
                                        <asp:TemplateField>
                                            <ItemTemplate>

                                                <div class="container-fluid">
                                                    <div class="row">

                                                        <div class="col-lg-11">
                                                            <div class="row">
                                                                <div class="col-12">
                                                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("Asunto") %>' Font-Size="Large"></asp:Label>
                                                                </div>
                                                            </div>

                                                            <div class="row">

                                                                <div class="col-12">
                                                                    Estatus:
                                                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("Estatus") %>'></asp:Label>
                                                                    &nbsp;|&nbsp;&nbsp; AgenteID:
                                                                    <asp:Label ID="Label4" runat="server" Text='<%# Eval("AgenteId") %>'></asp:Label>
                                                                    &nbsp;Creado:&nbsp;
                                                                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("CreadoUtc", "{0:g}") %>'></asp:Label>
                                                                </div>
                                                            </div>

                                                            <div class="row">
                                                                <div class="col-12">
                                                                    AgenteId:
                                                                    <asp:Label ID="Label5" runat="server" Text='<%# Eval("AgenteId") %>'></asp:Label>
                                                                </div>
                                                            </div>

                                                            <div class="row">
                                                                <div class="col-12">

                                                                </div>
                                                            </div>

                                                            <div class="row">
                                                                <div class="col-12">

                                                                </div>
                                                            </div>

                                                        </div>

                                                        <div class="col-lg-1">
                                                            <asp:Image
                                                                ID="Image1"
                                                                runat="server"
                                                                class="img-fluid"
                                                                ImageUrl='<%# Eval("Adjuntos") %>' />
                                                        </div>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                    <EditRowStyle BackColor="#2461BF" />
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#EFF3FB" />
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                </asp:GridView>
                            </div>
                        </div>


                    </div>

                </div>
            </div>

        </div>
    </div>



</asp:Content>
