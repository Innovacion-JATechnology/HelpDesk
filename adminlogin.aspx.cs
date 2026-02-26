using System;
using System.Configuration;
using System.Data.SqlClient;

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
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == System.Data.ConnectionState.Closed)
                    con.Open();

                SqlCommand cmd = new SqlCommand("SELECT * from hd.usuario WHERE Correo='" +
                    administrador.Text.Trim() + "' AND contraseña='" + contrasena.Text.Trim() + "'", con);

                SqlDataReader dr = cmd.ExecuteReader();


                if (dr.HasRows)
                { 
                    while (dr.Read())
                    { 
                        Session["username"] = dr["Correo"].ToString();
                        Session["fullname"] = dr["Nombre"].ToString();
                        Session["role"] = "admin";
                        Session["status"] = dr["Estatus"].ToString();
                        Response.Redirect("homepage.aspx", false);
                    }
                }
                else
                    Response.Write("<script>alert('" + "Credenciales invalidas" + "');</script>");
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }

        }
    }
}