using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JRBuilding.Pages.Lease
{
    public partial class Viewpdf : System.Web.UI.Page
    {
        string action, name;
        protected void Page_Load(object sender, EventArgs e)
        {
            action = Request.QueryString["action"];
            name = Request.QueryString["name"];
            if(action=="rent")
            {
                iframepdf.Src = "../Lease/Rent_Cheques/" + name;
            }
            else if(action=="lease")
            {
                iframepdf.Src = "../Lease/Documents/" + name;
            }
        }
    }
}