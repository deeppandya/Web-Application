using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JRBuilding.Pages.User
{
    public partial class Login : System.Web.UI.Page
    {
        string con = ConfigurationManager.ConnectionStrings["JRBConnectionString"].ToString();
        string alertmsg;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                Session["username"] = "";
                Session["fname"] = "";
                Session["lname"] = "";
                Session["roleid"] = "";
                Session["buildingid"] = "";
            }
        }

        protected void btnlogin_Click(object sender, EventArgs e)
        {

            SqlConnection _con = new SqlConnection(con);
            DataTable dt = new DataTable();
            string sql = "SELECT * FROM USERS WHERE USERNAME='"+txtusername.Text+"' AND PASSWORD='"+txtpassword.Text+"'";
            try
            {
                _con.Open();
                SqlCommand com = new SqlCommand(sql, _con);
                dt.Load(com.ExecuteReader());
                if (dt.Rows.Count <= 0)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('User Login Failed ')", true);
                }
                else
                {
                    foreach(DataRow rw in dt.Rows)
                    {
                        Session["username"] = rw["Username"].ToString();
                        Session["fname"] = rw["FName"].ToString();
                        Session["lname"] = rw["LName"].ToString();
                        Session["roleid"] = rw["RoleID"].ToString();
                    }
                    Response.Redirect("../Buildings/ViewBuildings.aspx");
                }
            }
            catch (Exception exe)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(" + exe.Message + ")", true);
            }
        }
    }
}