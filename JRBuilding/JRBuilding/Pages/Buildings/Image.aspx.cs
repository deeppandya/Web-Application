using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JRBuilding.Pages.Buildings
{
    public partial class Image : System.Web.UI.Page
    {
        String strConnString = System.Configuration.ConfigurationManager.ConnectionStrings["JRBConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["Building_Id"] != null)
            {
                string strQuery = "select * from BUildings where Building_Id=@id";
                SqlCommand cmd = new SqlCommand(strQuery);
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = Convert.ToInt32(Request.QueryString["Building_Id"]);
                DataTable dt = GetData(cmd);
                if (dt != null)
                {
                    Byte[] bytes = (Byte[])dt.Rows[0]["Image_Data"];
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    //Response.ContentType = dt.Rows[0]["ContentType"].ToString();
                    Response.AddHeader("content-disposition", "attachment;filename=" + dt.Rows[0]["Building_Name"].ToString());
                    Response.BinaryWrite(bytes);
                    Response.Flush();
                    Response.End();
                }
            }
        }
        private DataTable GetData(SqlCommand cmd)
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(strConnString);
            SqlDataAdapter sda = new SqlDataAdapter();
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            try
            {
                con.Open();
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(" + ex.Message + ")", true);
                return null;
            }
            finally
            {
                con.Close();
                sda.Dispose();
                con.Dispose();
            }
        }
    }
}