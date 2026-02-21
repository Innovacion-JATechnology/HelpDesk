using System;

namespace HelpDesk
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

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
    }
}