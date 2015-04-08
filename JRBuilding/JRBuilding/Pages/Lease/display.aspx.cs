using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JRBuilding.Pages.Lease
{
    public partial class display : System.Web.UI.Page
    {
        string con = ConfigurationManager.ConnectionStrings["JRBConnectionString"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        { 
            String action = Request.QueryString["action"];
            String id = Request.QueryString["id"];
            if (action.Equals("Lease"))
            {
                SqlConnection _con = new SqlConnection(con);
                DataTable dt = new DataTable();
                string sql = "SELECT * FROM LEASE WHERE TENANT_LEASE_ID='"+id+"'";
                try
                {
                    _con.Open();
                    SqlCommand com = new SqlCommand(sql, _con);
                    dt.Load(com.ExecuteReader());
                }
                catch (Exception exe)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(" + exe.Message + ")", true);
                    dt = null;
                }
                foreach (DataRow rw in dt.Rows)
                {
                    //String data = rw["Lease_Data"].ToString();
                    byte[] bytedata = (byte[])rw["Lease_Data"];
                    Response.Clear();
                    MemoryStream ms = new MemoryStream(bytedata);
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=" + rw["LEASE_NAME"]);
                    Response.Buffer = true;
                    ms.WriteTo(Response.OutputStream);
                    Response.End();
                    //System.IO.File.WriteAllBytes("hello.pdf", Encoding.UTF8.GetBytes(rw["Lease_Data"].ToString()));
                    Response.Redirect("ViewDocuments.aspx");
                }
            }
            else if (action.Equals("Rent"))
            {
                SqlConnection _con = new SqlConnection(con);
                DataTable dt = new DataTable();
                string sql = "SELECT * FROM RENT WHERE CHEQUE_ID='"+id+"'";
                try
                {
                    _con.Open();
                    SqlCommand com = new SqlCommand(sql, _con);
                    dt.Load(com.ExecuteReader());
                }
                catch (Exception exe)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(" + exe.Message + ")", true);
                    dt = null;
                }
                foreach(DataRow rw in dt.Rows)
                {
                    //String data=rw["Cheque_Data"].ToString();
                    byte[] bytedata = (byte[])rw["Cheque_Data"];
                    Response.Clear();
                    MemoryStream ms = new MemoryStream(bytedata);
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=" + rw["CHEQUE_NAME"]);
                    Response.Buffer = true;
                    ms.WriteTo(Response.OutputStream);
                    Response.End();
                    Response.Redirect("ViewDocuments.aspx");
                }
            }
        }

        protected void btnback_Click(object sender, EventArgs e)
        {
            Response.Redirect("ViewDocuments.aspx");
        }
    }
}