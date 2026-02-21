using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HelpDesk
{
    public partial class usersignup : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        //Login boton evento
        protected void Button1_Click(object sender, EventArgs e)
        {
            if(checkMemberExists())
                Response.Write("<script>alert('email already in our database');</script>");
            else
                signUpNewUser();
        }

        bool checkMemberExists() {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == System.Data.ConnectionState.Closed)
                    con.Open();

                SqlCommand cmd = new SqlCommand( "SELECT * from hd.usuario WHERE Correo='"+ 
                    correo.Text.Trim()+"';", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();

                if (dt.Rows.Count >= 1)
                    return true;
                else 
                    return false;


            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
                return false;
            }
        }

        void signUpNewUser()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if(con.State == System.Data.ConnectionState.Closed)
                    con.Open();

                SqlCommand cmd = new SqlCommand(
                    "INSERT INTO hd.usuario " +
                    "(Nombre, ApPaterno, ApMaterno, Correo, Contacto,  Empresa, Puesto, SLA, Activo," +
                    " CreadoEn, CreadoPor, ActualizadoEn, ActualizadoPor, Contraseña) VALUES " +
                    "(@Nombre, @ApPaterno, @ApMaterno, @Correo, @Contacto, @Empresa, @Puesto, @SLA, " +
                    "@Activo, @CreadoEn, @CreadoPor, @ActualizadoEn, @ActualizadoPor, @Contraseña)", con
                );

                cmd.Parameters.AddWithValue("@Nombre", nombre.Text.Trim());
                cmd.Parameters.AddWithValue("@ApPaterno", paterno.Text.Trim());
                cmd.Parameters.AddWithValue("@ApMaterno", materno.Text.Trim());
                cmd.Parameters.AddWithValue("@Correo", correo.Text.Trim());
                cmd.Parameters.AddWithValue("@Contacto", contacto.Text.Trim());
                /*
                                cmd.Parameters.AddWithValue("@Empresa", listaempresa.SelectedValue);
                                cmd.Parameters.AddWithValue("@Puesto", puesto.Text.Trim());
                                cmd.Parameters.AddWithValue("@SLA", listaSla.SelectedValue); */

                cmd.Parameters.AddWithValue("@Empresa",1);
                cmd.Parameters.AddWithValue("@Puesto", 1);
                cmd.Parameters.AddWithValue("@SLA", 1);

                cmd.Parameters.AddWithValue("@Activo", true);   // o 1

                cmd.Parameters.AddWithValue("@CreadoEn", DateTime.Now);
                cmd.Parameters.AddWithValue("@CreadoPor", 1);     // si no tienes usuario aún
                cmd.Parameters.AddWithValue("@ActualizadoEn", DBNull.Value); // null
                cmd.Parameters.AddWithValue("@ActualizadoPor", DBNull.Value);

                cmd.Parameters.AddWithValue("@Contraseña", contrasena.Text.Trim());

                cmd.ExecuteNonQuery();
                con.Close();

                Response.Write("<script>alert('Sign Up Successful. Go to User Login to Login');</script>");

            }
            catch (Exception ex)
            {
                               Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }




        protected void Paterno_TextChanged(object sender, EventArgs e)
        {

        }
    }
}