using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace JRBuilding.Pages.Buildings
{
    public partial class ViewBuildings : System.Web.UI.Page
    {
        string con = ConfigurationManager.ConnectionStrings["JRBConnectionString"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            SetRole();
                SqlConnection _con = new SqlConnection(con);
                DataTable dt = new DataTable();
                string sql = "SELECT * FROM BUILDINGS";
                try
                {
                    _con.Open();
                    SqlCommand com = new SqlCommand(sql, _con);
                    dt.Load(com.ExecuteReader());
                    int i = 1;
                    if (dt.Rows.Count > 0)
                    {
                        string _div = "<script type='text/javascript'>" +
                                "var _div=document.getElementById('divcontent');" +
                                "_div.innerHTML=\"";
                        foreach (DataRow rw in dt.Rows)
                        {

                            _div = _div + "<div class='col-md-3'>" +
                                "<div class='box box-info'>" +
                                    "<div class='box-header'>" +
                            "<h3 class='box-title'>" + rw["Building_Name"] + "</h3>" +
                                    "</div>" +
                                    "<div class='box-body'>" +
                                        "<a href='../User/Profile2.aspx?id=" + rw["Building_Id"] + "'><img src='Image.aspx?Building_Id=" + rw["Building_Id"] + "' /></a>" +
                                        //"<br />" +
                                        //"<asp:Button class='btn btn-success btn-sm' style='float:right' runat=\"server\" Text=\"See Details...\" OnClientClick=\"alert(\'hello\')\"></asp:Button>" +
                                    //"<button class='btn btn-success btn-sm' style='float:right'>See Details...</button>" +
                                    "</div>" +
                                "</div>" +
                            "</div>";
                            i++;
                        }
                        _div = _div + "\";</script>";
                        Page page = HttpContext.Current.CurrentHandler as Page;
                        page.ClientScript.RegisterStartupScript(typeof(Page), "Test", _div);
                    }
                    else
                    {
                        Response.Write("true");
                    }
                }
                catch (Exception ex)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert("+ex.Message+")", true);
                }
            //}
            
        }

        protected void btnaddbuilding_Click(object sender, EventArgs e)
        {
            Response.Redirect("Building.aspx?action=add");
        }
        
        public void SetRole()
        {
            if(Session["roleid"].ToString()=="Admin")
            {
                btnaddbuilding.Visible = true;
            }
            else
            {
                btnaddbuilding.Visible = false;
            }
        }
    }
}