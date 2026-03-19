using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using HelpDesk.Security; // Igual que en userlogin: PasswordCrypto

namespace HelpDesk
{
    public partial class adminlogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void IngresoAdm_Click(object sender, EventArgs e)
        {
            string strcon = ConfigurationManager.ConnectionStrings["ServerCon"].ConnectionString;

            try
            {
                var userEmail = administrador?.Text == null
                    ? ""
                    : administrador.Text.Trim().ToLowerInvariant();

                var pwd = contrasena?.Text ?? "";

                if (string.IsNullOrWhiteSpace(userEmail) || string.IsNullOrWhiteSpace(pwd))
                {
                    Alert("Por favor ingresa correo y contraseña.");
                    return;
                }

                using (var con = new SqlConnection(strcon))
                using (var cmd = new SqlCommand(@"
                    SELECT TOP 1
                        agenteId,
                        email,
                        nombre,
                        passwordHash,
                        passwordSalt,
                        nivel
                    FROM hd.Agente
                    WHERE email = @Email;
                ", con))
                {
                    cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 320).Value = userEmail;

                    con.Open();
                    using (var rdr = cmd.ExecuteReader())
                    {
                        if (!rdr.HasRows)
                        {
                            Alert("Credenciales inválidas.");
                            return;
                        }

                        if (rdr.Read())
                        {
                            // Obtener salt y hash almacenados
                            var dbSalt = rdr["passwordSalt"] as byte[];
                            var dbHash = rdr["passwordHash"] as byte[];

                            // Validar tamaños esperados (mismo patrón que usas en userlogin)
                            if (dbSalt == null || dbHash == null ||
                                dbSalt.Length != PasswordCrypto.SaltSize ||
                                dbHash.Length != PasswordCrypto.HashSize)
                            {
                                Alert("Error de credenciales (formato inválido).");
                                return;
                            }

                            // Verificar contraseña
                            var ok = PasswordCrypto.VerifyPassword(pwd, dbSalt, dbHash);
                            if (!ok)
                            {
                                Alert("Credenciales inválidas.");
                                return;
                            }

                            // Autenticación OK → setear sesión
                            Session["username"] = rdr["email"].ToString();   // correo del agente
                            Session["fullname"] = rdr["nombre"].ToString();  // nombre del agente
                            Session["role"] = "agente";                  // ← rol de agente
                            Session["agentid"] = rdr["agenteId"].ToString();
                            Session["nivel"] = rdr["nivel"].ToString();

                            // (Compatibilidad con código previo que esperaba "ID")
                            Session["ID"] = rdr["agenteId"].ToString();

                            Response.Redirect("InicioAgente.aspx", false);
                        }
                    }
                }
            }
            catch (SqlException)
            {
                Alert("Ocurrió un error al iniciar sesión (SQL).");
            }
            catch (Exception)
            {
                Alert("Ocurrió un error al iniciar sesión.");
            }
        }

        private void Alert(string message)
        {
            var safe = HttpUtility.JavaScriptStringEncode(message ?? string.Empty);
            ClientScript.RegisterStartupScript(
                this.GetType(),
                "alert_" + Guid.NewGuid().ToString("N"),
                $"alert('{safe}');",
                true
            );
        }
    }
}