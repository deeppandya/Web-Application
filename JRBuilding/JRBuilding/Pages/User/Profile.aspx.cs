using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JRBuilding.Pages.User
{
    public partial class Profile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Session["Building_id"] = Request.QueryString["id"];
            SetRole();
        }
        protected void SignOutOnclick(object sender, EventArgs e)
        {
            Response.Write("Hello");
        }
        public void SetRole()
        {
            if(Session["roleid"].ToString()=="Admin")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "SetRole(hidden)", true);
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "SetRole()", true);
            }
        }
    }
}