using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JRBuilding.Pages.User
{
    public partial class Profile2 : System.Web.UI.Page
    {
        string buildingid;
        protected void Page_Load(object sender, EventArgs e)
        {
            buildingid = Request.QueryString["id"];
            Session["buildingid"] = buildingid;
            iframecontent.Src = "../Buildings/Building.aspx?id=" + buildingid + "&action=view";
            SetRole();
        }
        protected void SignOutOnclick(object sender, EventArgs e)
        {
            Response.Write("Hello");
        }
        public void SetRole()
        {
            if (Session["roleid"].ToString() == "Admin")
            {
                addoptions.Visible = true;
                anotification.Visible = true;
            }
            else if (Session["roleid"].ToString() == "Finance")
            {
                addoptions.Visible = true;
                idcivic.Visible = false;
                idsuits.Visible = false;
                idtenant.Visible = false;
                idtax.Visible = true;
                anotification.Visible = false;
            }
            else
            {
                addoptions.Visible = false;
                anotification.Visible = false;
            }
        }
    }
}