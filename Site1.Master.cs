using System;
using System.Web.UI.WebControls;

namespace HelpDesk
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                var role = Session["role"] as string;
                if (string.IsNullOrEmpty(role))
                {
                    linkIngreso.Visible = true;     // user login link button
                    linkSalir.Visible = true;      // logout link button
                    linkHola.Visible = false;       // hello user link button

                    linkAdministrador.Visible = true; // admin login link button
                    linkMantenimiento.Visible = false;     
                    linkCatalogo.Visible = false; // member management link button
                    linkHistorial.Visible = false; // historial link button
                    linkRegistro.Visible = false; // regsitro link button
                }
                else if (Session["role"].Equals("usuario"))
                {
                    linkIngreso.Visible = false;     // user login link button
                    linkSalir.Visible = true;      // logout link button
                    linkHola.Visible = true;       // hello user link button
                    linkHola.Text = "Usuario: " + Session["fullname"];

                    linkAdministrador.Visible = true; // admin login link button
                    linkMantenimiento.Visible = false;
                    linkCatalogo.Visible = false; // member management link button
                    linkHistorial.Visible = false; // hirtorial link button
                    linkRegistro.Visible = false; // reistro link button

                }
                else if (Session["role"].Equals("admin"))
                {
                    linkIngreso.Visible = false;     // user login link button
                    linkSalir.Visible = true;      // logout link button
                    linkHola.Visible = true;       // hello user link button
                    linkHola.Text = "Admin: " + Session["fullname"];

                    linkAdministrador.Visible = false; // admin login link button
                    linkMantenimiento.Visible = true;
                    linkCatalogo.Visible = true; // member management link button
                    linkHistorial.Visible = true; // ticket management link button
                    linkRegistro.Visible = true; // registro link button
                }
            }
            catch (Exception ex)
            {
            }

        }

        protected void LinkButton6_Click(object sender, EventArgs e)
        {
            Response.Redirect("adminlogin.aspx");
        }

        protected void LinkButton8_Click(object sender, EventArgs e)
        {

            Response.Redirect("enConstruccion.aspx");
        }

        protected void LinkButton11_Click(object sender, EventArgs e)
        {

           
        }

        protected void LinkButton12_Click(object sender, EventArgs e)
        {

            Response.Redirect("adminAllTickets.aspx");
        }

        protected void LinkButton10_Click(object sender, EventArgs e)
        {

            Response.Redirect("usersignup.aspx");
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Response.Redirect("userlogin.aspx");
        }

        protected void linkSalir_Click(object sender, EventArgs e)
        {
            Session["username"] = "";
            Session["fullname"] = "";
            Session["role"] = "";
            Session["status"] = "";

            linkIngreso.Visible = true;     // user login link button
            linkSalir.Visible = true;      // logout link button
            linkHola.Visible = false;       // hello user link button

            linkAdministrador.Visible = true; // admin login link button
            linkMantenimiento.Visible = false;
            linkCatalogo.Visible = false; // member management link button
            linkHistorial.Visible = false; // historial link button
            linkRegistro.Visible = false; // regsitro link button

            Response.Redirect("homepage.aspx", false);
        }
    }
}