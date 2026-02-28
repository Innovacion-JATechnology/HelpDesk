<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="misTickets.aspx.cs" Inherits="HelpDesk.misTickets" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 
    <style>
        /* Prevent images from widening columns */
        .table img {
            max-width: 100%;
            height: auto;
            display: block;
        }
        /* Avoid long words/URLs breaking layout */
        .table { word-break: break-word; }

        /* Optional: make the card content breathe on small screens */
        .card-body { padding: 1.25rem; }
    </style>

  <script>
      $(function () {
          $(".table").each(function () {
              var $t = $(this);

              // Ensure THEAD exists (leave this if you need it)
              if ($t.find("thead").length === 0) {
                  var $firstRow = $t.find("tr:first");
                  if ($firstRow.length) {
                      $t.prepend($("<thead/>").append($firstRow));
                  }
              }

              if ($.fn.DataTable && !$t.hasClass("dataTable")) {
                  $t.DataTable({
                      responsive: true,
                      autoWidth: false,

                      // ✅ Default page size
                      pageLength: 5,

                      // ✅ Page-length dropdown options
                      lengthMenu: [[5, 10, 20, 50, -1], [5, 10, 20, 50, "Todos"]],

                      // ✅ All UI strings customized (Spanish example)
                      language: {
                          lengthMenu: "_MENU_  . Registros por página",     // replaces "entries per page"
                          search: "Buscar:",
                          info: "Mostrando _START_ a _END_ de _TOTAL_ registros",
                          infoEmpty: "Mostrando 0 a 0 de 0 registros",
                          infoFiltered: "(filtrado de _MAX_ registros totales)",
                          loadingRecords: "Cargando...",
                          processing: "Procesando...",
                          zeroRecords: "No se encontraron resultados",
                          emptyTable: "No hay datos disponibles en la tabla",
                          paginate: {
                              first: "Primero",
                              previous: "Anterior",
                              next: "Siguiente",
                              last: "Último"
                          },
                          aria: {
                              sortAscending: ": activar para ordenar la columna ascendente",
                              sortDescending: ": activar para ordenar la columna descendente"
                          }
                      },

                      // (Optional) Re-arrange controls; Bootstrap 4 compatible
                      // dom: "<'row'<'col-sm-12 col-md-6'l><'col-sm-12 col-md-6'f>>" +
                      //      "<'row'<'col-sm-12'tr>>" +
                      //      "<'row'<'col-sm-12 col-md-5'i><'col-sm-12 col-md-7'p>>"
                  });
              }
          });
      });
  </script>
     
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container">
        <div class="row">

            <div class="col-md-8 mx-auto">
                <div class="card">
                    <div class="card-body">

                        <div class="row">
                            <div class="col text-center">
                                <h3>Historial de Tickets</h3>
                            </div>
                        </div>

                        <div class="row">
                            <!-- Filtro: ID -->
                            <div class="col-md-4">
                                <label for="id">ID</label>
                                <div class="form-group">
                                    <div class="input-group">
                                        <asp:TextBox CssClass="form-control" ID="id" runat="server" placeholder="ID"></asp:TextBox>
                                        <div class="input-group-append">
                                            <asp:Button CssClass="btn btn-primary btn-2b399b" ID="Button1" runat="server" Text="Go" />
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <!-- Filtro: Nombre -->
                            <div class="col-md-8">
                                <label for="nombre">Nombre</label>
                                <div class="form-group">
                                    <asp:TextBox CssClass="form-control" ID="nombre" runat="server" placeholder="Nombre"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server"
                                ConnectionString="<%$ ConnectionStrings:ServerCon %>"
                                SelectCommand="SELECT * FROM [hd].[Ticket]">
                            </asp:SqlDataSource>

                            <div class="col">
                                <!-- Responsive wrapper prevents overflow -->
                                <div class="table-responsive">
                                    <asp:GridView
                                        ID="GridView1"
                                        runat="server"
                                        CssClass="table table-striped table-bordered"
                                        AutoGenerateColumns="False"
                                        DataKeyNames="TicketId"
                                        DataSourceID="SqlDataSource1"
                                        ForeColor="#333333"
                                        GridLines="None"
                                        OnSelectedIndexChanged="GridView1_SelectedIndexChanged">

                                        <AlternatingRowStyle BackColor="White" />
                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <RowStyle BackColor="#EFF3FB" />
                                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />

                                        <Columns> 
                                            <asp:BoundField DataField="TicketId" HeaderText="ID" InsertVisible="False" ReadOnly="True" SortExpression="TicketId">
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:BoundField>

                                             <asp:TemplateField HeaderText="Detalle">
                                                <ItemTemplate>
                                                    <div class="row no-gutters align-items-start">
                                                        <div class="col-lg-11 pr-lg-2">
                                                            <div class="mb-1">
                                                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("Asunto") %>' Font-Size="Large"></asp:Label>
                                                            </div>

                                                            <div class="text-muted small">
                                                                Estatus:
                                                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("Estatus") %>'></asp:Label>
                                                                &nbsp;|&nbsp; AgenteID:
                                                                <asp:Label ID="Label4" runat="server" Text='<%# Eval("AgenteId") %>'></asp:Label>
                                                                &nbsp;Creado:&nbsp;
                                                                <asp:Label ID="Label3" runat="server" Text='<%# Eval("CreadoUtc", "{0:g}") %>'></asp:Label>
                                                            </div>

                                                            <div class="text-muted small">
                                                                AgenteId:
                                                                <asp:Label ID="Label5" runat="server" Text='<%# Eval("AgenteId") %>'></asp:Label>
                                                            </div>
                                                        </div>

                                                        <div class="col-lg-1 text-lg-right text-left pl-lg-2">
                                                            <asp:Image
                                                                ID="Image1"
                                                                runat="server"
                                                                CssClass="img-fluid"
                                                                ImageUrl='<%# Eval("Adjuntos") %>' />
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                             <asp:BoundField DataField="Asunto" HeaderText="Asunto" ReadOnly="True" SortExpression="Asunto" />


                                            <asp:TemplateField HeaderText="Descripción" SortExpression="Descripcion">
                                                <ItemTemplate>
                                                    <span
                                                        title='<%# Eval("Descripcion") %>'
                                                        class="d-inline-block text-truncate"
                                                        style="max-width: 220px;">
                                                        <%# (Eval("Descripcion") == null) 
                                                        ? "" : (Eval("Descripcion").ToString().Length <= 44 
                                                        ? Eval("Descripcion").ToString() 
                                                        : Eval("Descripcion").ToString().Substring(0, 44) + "…")  %>
                                                    </span>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>

                <a href="<%= ResolveUrl("~/homepage.aspx") %>">&laquo; Regresar al Inicio</a><br /><br />
            </div>

        </div>
    </div>
</asp:Content>