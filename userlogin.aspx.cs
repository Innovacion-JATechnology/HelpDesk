using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HelpDesk
{
    public partial class userlogin : System.Web.UI.Page
    {

        string strcon = ConfigurationManager.ConnectionStrings["ServerCon"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Continuar_LogIn_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == System.Data.ConnectionState.Closed)
                    con.Open();

                SqlCommand cmd = new SqlCommand("SELECT * from hd.usuario WHERE Correo='" +
                    usuario.Text.Trim() + "' AND contraseña='" + contrasena.Text.Trim() + "'", con);

                SqlDataReader dr =  cmd.ExecuteReader();                 

                if (dr.HasRows)
                { 
                    while (dr.Read())
                    { 
                        Session["username"] = dr["Correo"].ToString();
                        Session["fullname"] = dr["Nombre"].ToString();
                        Session["role"] = "usuario";
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