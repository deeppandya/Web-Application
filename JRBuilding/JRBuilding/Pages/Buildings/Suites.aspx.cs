using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JRBuilding.Pages.Buildings
{
    public partial class Suits : System.Web.UI.Page
    {
        string localid;
        string buildingid;
        string con = ConfigurationManager.ConnectionStrings["JRBConnectionString"].ToString();
        string action,roleid,suitid;
        protected void Page_Load(object sender, EventArgs e)
        {
            localid = Request.QueryString["lid"];
            buildingid = Request.QueryString["bid"];
            action = Request.QueryString["action"];
            roleid = Session["roleid"].ToString();
            suitid = Request.QueryString["sid"];
            populate_dropdown();
            populate_local_dropdown();
            Bindgridview();
            if (!IsPostBack)
            {
                if (action == "view" && roleid == "Admin")
                {
                    btnsubmit.Visible = false;
                    btnupdate.Visible = false;
                    btnedit.Visible = true;
                    btndelete.Visible = true;
                    txtname.ReadOnly = true;
                    headertxt.Text = "View Suite";
                    Viewdata();
                }
                else if (action == "view" && roleid != "Admin")
                {
                    btnsubmit.Visible = false;
                    btnupdate.Visible = false;
                    btnedit.Visible = false;
                    btndelete.Visible = false;
                    txtname.ReadOnly = true;
                    headertxt.Text = "View Suite";
                    Viewdata();
                }
                else if (action == "edit" && roleid == "Admin")
                {
                    btnsubmit.Visible = false;
                    btnupdate.Visible = true;
                    btnedit.Visible = false;
                    btndelete.Visible = false;
                    txtname.ReadOnly = false;
                    headertxt.Text = "Edit Suite";
                    Viewdata();
                }
                else if (action == "add" && roleid == "Admin")
                {
                    btnsubmit.Visible = true;
                    btnupdate.Visible = false;
                    btnedit.Visible = false;
                    btndelete.Visible = false;
                    headertxt.Text = "New Suite";
                    txtname.ReadOnly = false;
                }
            }
        }

        public void populate_dropdown()
        {
            SqlConnection _con = new SqlConnection(con);
            DataTable dt = new DataTable();
            string sql = "SELECT * FROM BUILDINGS";
            try
            {
                _con.Open();
                SqlCommand com = new SqlCommand(sql, _con);
                dt.Load(com.ExecuteReader());
                drpbuilding.DataSource = dt;
                drpbuilding.DataValueField = "Building_id";
                drpbuilding.DataTextField = "Building_Name";
                drpbuilding.DataBind();
                drpbuilding.Items.FindByValue(buildingid).Selected = true;
                drpbuilding.Enabled = false;
            }
            catch (Exception exe)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(" + exe.Message + ")", true);
            }
        }

        public void populate_local_dropdown()
        {
            SqlConnection _con = new SqlConnection(con);
            DataTable dt = new DataTable();
            string _yes = "Yes";
            string sql = "SELECT * FROM LOCALS WHERE BUILDING_ID='"+buildingid+"'";
            try
            {
                _con.Open();
                SqlCommand com = new SqlCommand(sql, _con);
                dt.Load(com.ExecuteReader());
                drplocal.DataSource = dt;
                drplocal.DataValueField = "local_id";
                drplocal.DataTextField = "local_address";
                drplocal.DataBind();
                drplocal.Items.FindByValue(localid).Selected = true;
                drplocal.Enabled = false;
            }
            catch (Exception exe)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(" + exe.Message + ")", true);
            }
        }

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            SqlConnection _con = new SqlConnection(con);
            string sql = "INSERT INTO SUITS (SUIT_NAME,LOCAL_ID,BUILDING_ID) VALUES ('" + txtname.Text + "','" + drplocal.SelectedValue + "','" + drpbuilding.SelectedValue + "')";
            sql = sql + "; SELECT SCOPE_IDENTITY()";
            try
            {
                _con.Open();
                SqlCommand com = new SqlCommand(sql, _con);
                string id=com.ExecuteScalar().ToString();
                _con.Close();

                Response.Redirect("../Buildings/Suites.aspx?bid=" + buildingid + "&lid=" + localid + "&sid=" + id + "&action=view");
            }
            catch (Exception exe)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(" + exe.Message + ")", true);
            }
        }

        //protected void btnback_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("../Buildings/locals.aspx?lid="+localid+"&bid="+buildingid+"&action=view");
        //}

        public void Viewdata()
        {
            SqlConnection _con = new SqlConnection(con);
            DataTable dt = new DataTable();
            string sql = "SELECT * FROM SUITS  WHERE BUILDING_ID='" + buildingid + "' AND LOCAL_ID='" + localid + "' AND SUIT_ID='"+suitid+"'";
            try
            {
                _con.Open();
                SqlCommand com = new SqlCommand(sql, _con);
                dt.Load(com.ExecuteReader());
                foreach (DataRow rw in dt.Rows)
                {
                    txtname.Text = rw["SUIT_NAME"].ToString();
                }
            }
            catch (Exception exe)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(" + exe.Message + ")", true);
            }
        }

        public void Bindgridview()
        {
            SqlConnection _con = new SqlConnection(con);
            DataTable dt = new DataTable();
            string sql = "SELECT S.SUIT_NAME,B.BUILDING_NAME,L.LOCAL_ADDRESS FROM SUITS S,BUILDINGS B,LOCALS L WHERE S.BUILDING_ID='"+buildingid+"' AND L.LOCAL_ID='"+localid+"' AND S.BUILDING_ID=B.BUILDING_ID AND S.LOCAL_ID=L.LOCAL_ID";
            try
            {
                _con.Open();
                SqlCommand com = new SqlCommand(sql, _con);
                dt.Load(com.ExecuteReader());
                gridsuites.DataSource = dt;
                gridsuites.DataBind();
            }
            catch (Exception exe)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(" + exe.Message + ")", true);
            }
        }

        protected void btnedit_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Buildings/Suites.aspx?bid=" + buildingid + "&lid=" + localid + "&sid=" + suitid + "&action=edit");
        }

        protected void btndelete_Click(object sender, EventArgs e)
        {
            SqlConnection _con = new SqlConnection(con);
            DataTable dt = new DataTable();
            string checksql = "SELECT * FROM TENANTS WHERE BUILDING_ID='" + buildingid + "' AND LOCAL_ID='" + localid + "' AND SUIT_ID='" + suitid + "'";
            string sql = "DELETE FROM SUITS WHERE BUILDING_ID='" + buildingid + "' AND LOCAL_ID='" + localid + "' AND SUIT_ID='" + suitid + "'";
            string sql2 = "DELETE FROM TENANTS WHERE BUILDING_ID='" + buildingid + "' AND LOCAL_ID='" + localid + "' AND SUIT_ID='"+suitid+"'";
            try
            {
                _con.Open();
                SqlCommand com = new SqlCommand(checksql, _con);
                dt.Load(com.ExecuteReader());
                if(dt.Rows.Count>0)
                {
                 SqlCommand com3 = new SqlCommand(sql2, _con);
                 com3.ExecuteNonQuery();
                }
                SqlCommand com2 = new SqlCommand(sql, _con);
                com2.ExecuteNonQuery();
                 
                _con.Close();
                Response.Redirect("../Buildings/Building.aspx?id=" + buildingid + "&action=view");
            }
            catch (Exception exe)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(" + exe.Message + ")", true);
            }
        }

        protected void btnupdate_Click(object sender, EventArgs e)
        {
            SqlConnection _con = new SqlConnection(con);
            string updatesql = "update suits set suit_name='" + txtname.Text + "' where local_id='" + localid + "'and building_id='" + buildingid + "' and suit_id='"+suitid+"'";
            try
            {
            _con.Open();
            SqlCommand com2 = new SqlCommand(updatesql, _con);
            com2.ExecuteNonQuery();
            _con.Close();
            Response.Redirect("../Buildings/Suites.aspx?bid=" + buildingid + "&lid=" + localid + "&sid=" + suitid + "&action=view");
            }
            catch (Exception exe)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(" + exe.Message + ")", true);
            }
        }
    }
}