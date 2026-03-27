using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HelpDesk.Utilities;

namespace HelpDesk
{
    public partial class adminAllTickets : Page
    {
        private readonly string strcon =
            ConfigurationManager.ConnectionStrings["ServerCon"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtSearch.Text = string.Empty;
                LoadTickets(null);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadTickets(txtSearch.Text);
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtSearch.Text = string.Empty;
            LoadTickets(null);
        }

        private void LoadTickets(string q)
        {
            string baseSql = @"
SELECT t.TicketId,
       u.nombre + ' ' + ISNULL(u.ApPaterno,'') AS UsuarioNombre,
       t.CreadoUtc,
       t.AgenteId,
       a.nombre AS AgenteNombre,
       ISNULL(a.estatus, 1) AS AgenteEstatus,
       t.Estatus,
       t.Asunto,
       t.Descripcion,
       t.Prioridad
FROM [hd].[Ticket] t
LEFT JOIN [hd].[Usuario] u ON t.UsuarioId = u.UsuarioId
LEFT JOIN [hd].[Agente] a ON t.AgenteId = a.agenteId
";

            if (string.IsNullOrWhiteSpace(q))
            {
                SqlDataSource1.SelectCommand = baseSql + " ORDER BY t.CreadoUtc ASC";
                SqlDataSource1.SelectParameters.Clear();
            }
            else
            {
                SqlDataSource1.SelectCommand = baseSql + @"
WHERE t.Asunto LIKE '%' + @q + '%'
   OR t.Descripcion LIKE '%' + @q + '%'
ORDER BY t.CreadoUtc DESC";

                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("q", q);
            }

            GridView1.PageIndex = 0;
            GridView1.DataBind();
        }

       protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow) return;

            var drv = (System.Data.DataRowView)e.Row.DataItem;

            e.Row.Attributes["data-ticketid"] = drv["TicketId"].ToString();
            e.Row.Attributes["data-asunto"] = drv["Asunto"].ToString();
            e.Row.Attributes["data-descripcion"] = drv["Descripcion"].ToString();
            e.Row.Attributes["data-prioridad"] = drv["Prioridad"].ToString();
            e.Row.Attributes["data-agenteid"] = drv["AgenteId"]?.ToString() ?? "";
            e.Row.Attributes["data-agentestatus"] = drv["AgenteEstatus"]?.ToString() ?? "1";

            e.Row.Attributes["style"] = "cursor:pointer;";
            e.Row.Attributes["onclick"] = "rowClick(this)";

            // Format AgenteEstatus column to show "Activo" or "Inactivo"
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                if (GridView1.HeaderRow.Cells[i].Text == "Estatus Agente")
                {
                    string statusValue = drv["AgenteEstatus"]?.ToString() ?? "1";
                    e.Row.Cells[i].Text = statusValue == "1" ? "Activo" : "Inactivo";
                    break;
                }
            }
        }
       

        protected void modalBtnAssign_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(modalHiddenTicketId.Value, out int ticketId)) return;
            if (!int.TryParse(modalDdlAgents.SelectedValue, out int agenteId)) return;

            try
            {
                // ✅ Assign ticket and capture previous agent
                int? previousAgent = AssignTicketToAgent(ticketId, agenteId);
                bool reassigned = previousAgent.HasValue && previousAgent.Value != agenteId;

                Logger.RegistrarInfo($"Ticket {ticketId} asignado a Agente {agenteId}. Reasignado: {reassigned}");

                // ✅ Email data
                string agentEmail = GetAgentEmail(agenteId);
                var info = GetTicketInfo(ticketId);
                string ticketUrl = GetTicketUrl(ticketId);

                // ✅ Queue email (SQL Server Mail)
                try
                {
                    var emailSvc = new EmailService();
                    emailSvc.QueueAssignmentEmail(
                        agentEmail,
                        ticketId,
                        info.Subject,
                        info.ShortDescription,
                        info.Priority,
                        ticketUrl,
                        reassigned
                    );
                    Logger.RegistrarInfo($"Email enlistado exitosamente ticket {ticketId} para {agentEmail}");
                }
                catch (Exception emailEx)
                {
                    // Log email error but don't fail the assignment
                    Logger.RegistrarError($"Error enviando email del ticket {ticketId} a {agentEmail}", emailEx);
                    ScriptManager.RegisterStartupScript(this, GetType(), "emailError",
                        $"alert('Ticket asignado pero error al enviar correo: {emailEx.Message}');", true);
                }

                // ✅ Redirect to refresh page cleanly without postback
                Response.Redirect(Request.Url.ToString());
            }
            catch (Exception ex)
            {
                // Handle assignment errors
                Logger.RegistrarError($"Error asignando ticket {modalHiddenTicketId.Value} a agente {modalDdlAgents.SelectedValue}", ex);
                ScriptManager.RegisterStartupScript(this, GetType(), "assignError",
                    $"alert('Error al asignar ticket: {ex.Message}');", true);
            }
        }

        protected void modalBtnUpdateStatus_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(modalDdlAgents.SelectedValue, out int agenteId)) return;
            if (!int.TryParse(modalDdlAgenteStatus.SelectedValue, out int estatus)) return;

            try
            {
                UpdateAgentStatus(agenteId, estatus);
                Logger.RegistrarInfo($"Agent {agenteId} estatus actualizado a {(estatus == 1 ? "Activo" : "Inactivo")}");

                // ✅ Redirect to refresh page cleanly without postback
                Response.Redirect(Request.Url.ToString());
            }
            catch (Exception ex)
            {
                // Handle status update errors
                Logger.RegistrarError($"Error updating status for agent {agenteId} to {estatus}", ex);
                ScriptManager.RegisterStartupScript(this, GetType(), "statusError",
                    $"alert('Error al actualizar estatus: {ex.Message}');", true);
            }
        }

        /* =========================
           DATA HELPERS
           ========================= */

        private string GetAgentEmail(int agenteId)
        {
            using (var cn = new SqlConnection(strcon))
            using (var cmd = cn.CreateCommand())
            {
                cmd.CommandText = "SELECT email FROM [hd].[Agente] WHERE agenteId = @id";
                cmd.Parameters.AddWithValue("@id", agenteId);
                cn.Open();
                return cmd.ExecuteScalar()?.ToString();
            }
        }

        private (string Subject, string ShortDescription, string Priority)
            GetTicketInfo(int ticketId)
        {
            using (var cn = new SqlConnection(strcon))
            using (var cmd = cn.CreateCommand())
            {
                cmd.CommandText = @"
SELECT Asunto, Descripcion, Prioridad
FROM [hd].[Ticket]
WHERE TicketId = @id";
                cmd.Parameters.AddWithValue("@id", ticketId);

                cn.Open();
                using (var r = cmd.ExecuteReader())
                {
                    if (r.Read())
                    {
                        string subject = r["Asunto"]?.ToString() ?? "";
                        string desc = r["Descripcion"]?.ToString() ?? "";
                        string priority = r["Prioridad"]?.ToString() ?? "Normal";
                        return (subject, FirstWords(desc, 40), priority);
                    }
                }
            }
            return ("", "", "Normal");
        }

        private string FirstWords(string text, int maxWords)
        {
            if (string.IsNullOrWhiteSpace(text)) return "";

            var words = text.Split(
                new[] { ' ', '\r', '\n', '\t' },
                StringSplitOptions.RemoveEmptyEntries);

            if (words.Length <= maxWords)
                return string.Join(" ", words);

            return string.Join(" ", words.Take(maxWords)) + "…";
        }

        private string GetTicketUrl(int ticketId)
        {
            var baseUrl = Request.Url.GetLeftPart(UriPartial.Authority);
            return $"{baseUrl}{ResolveUrl($"~/AdminAllTickets.aspx?ticketId={ticketId}")}";
        }

        private void UpdateAgentStatus(int agenteId, int estatus)
        {
            using (var cn = new SqlConnection(strcon))
            using (var cmd = cn.CreateCommand())
            {
                cmd.CommandText = @"
UPDATE [hd].[Agente]
SET estatus = @estatus
WHERE agenteId = @id";
                cmd.Parameters.AddWithValue("@estatus", estatus);
                cmd.Parameters.AddWithValue("@id", agenteId);
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        /* =========================
           ASSIGNMENT (returns previous agent)
           ========================= */

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Intentionally empty
            // Selection handled via RowDataBound + JS modal
        }


        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            // Not used (assignment via modal)
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataBind();
        }



        private int? AssignTicketToAgent(int ticketId, int agenteId)
        {
            int? previousAgent = null;

            using (var cn = new SqlConnection(strcon))
            {
                cn.Open();
                using (var tx = cn.BeginTransaction())
                using (var cmd = cn.CreateCommand())
                {
                    cmd.Transaction = tx;

                    // Read current agent
                    cmd.CommandText =
                        "SELECT AgenteId FROM [hd].[Ticket] WHERE TicketId = @id";
                    cmd.Parameters.AddWithValue("@id", ticketId);

                    var obj = cmd.ExecuteScalar();
                    if (obj != null && obj != DBNull.Value)
                        previousAgent = Convert.ToInt32(obj);

                    // Decrement old agent counter
                    if (previousAgent.HasValue && previousAgent.Value != agenteId)
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandText = @"
UPDATE [hd].[Agente]
SET tAbiertos = CASE WHEN tAbiertos > 0 THEN tAbiertos - 1 ELSE 0 END
WHERE agenteId = @id";
                        cmd.Parameters.AddWithValue("@id", previousAgent.Value);
                        cmd.ExecuteNonQuery();
                    }

                    // Assign new agent
                    cmd.Parameters.Clear();
                    cmd.CommandText = @"
UPDATE [hd].[Ticket]
SET AgenteId = @agenteId,
    ActualizadoUtc = SYSUTCDATETIME()
WHERE TicketId = @ticketId";
                    cmd.Parameters.AddWithValue("@agenteId", agenteId);
                    cmd.Parameters.AddWithValue("@ticketId", ticketId);
                    cmd.ExecuteNonQuery();

                    // Increment new agent counter
                    cmd.Parameters.Clear();
                    cmd.CommandText = @"
UPDATE [hd].[Agente]
SET tAbiertos = ISNULL(tAbiertos,0) + 1
WHERE agenteId = @id";
                    cmd.Parameters.AddWithValue("@id", agenteId);
                    cmd.ExecuteNonQuery();

                    tx.Commit();
                }
            }

            return previousAgent;
        }
    }
}