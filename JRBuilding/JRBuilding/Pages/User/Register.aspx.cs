using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JRBuilding.Pages.User
{
    public partial class Register : System.Web.UI.Page
    {
        string con = ConfigurationManager.ConnectionStrings["JRBConnectionString"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btn_submit_click(object sender, EventArgs e)
        {
            SqlConnection _con = new SqlConnection(con);
            string sql = "INSERT INTO USERS VALUES ('" + txtusername.Text + "','" + txtpassword.Text + "','" + txtfname.Text + "','" + txtlname.Text + "','" + drprole.SelectedValue + "');";

            try
            {
                _con.Open();
                SqlCommand com = new SqlCommand(sql, _con);
                com.ExecuteNonQuery();
                _con.Close();
                Response.Redirect("Login.aspx");
            }
            catch (Exception exe)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(Registration Failed \n Please try again...)", true);
            }
        }
    }
}