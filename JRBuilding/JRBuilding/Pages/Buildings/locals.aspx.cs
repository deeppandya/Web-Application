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
    public partial class locals : System.Web.UI.Page
    {
        string con = ConfigurationManager.ConnectionStrings["JRBConnectionString"].ToString();
        string buildingid;
        string localid;
        string action;
        string viewscreen;
        string roleid;
        protected void Page_Load(object sender, EventArgs e)
        {
            buildingid = Request.QueryString["bid"];
            localid = Request.QueryString["lid"];
            action = Request.QueryString["action"];
            roleid=Session["roleid"].ToString();
            viewscreen = Request.QueryString["viewscreen"];
            populate_dropdown();
            if (!IsPostBack)
            {
                if (action == "view" && roleid=="Admin")
                {
                    btnsubmit.Visible = false;
                    btnupdate.Visible = false;
                    btnedit.Visible = true;
                    btndelete.Visible = true;
                    txtaddress.ReadOnly = true;
                    drpissuits.Enabled = false;
                    headertxt.Text = "View Civic";
                    viewdata();
                }
                else if (action == "view" && roleid != "Admin")
                {
                    btnsubmit.Visible = false;
                    btnupdate.Visible = false;
                    btnedit.Visible = false;
                    btndelete.Visible = false;
                    txtaddress.ReadOnly = true;
                    drpissuits.Enabled = false;
                    headertxt.Text = "View Civic";
                    viewdata();
                }
                else if (action == "edit" && roleid == "Admin")
                {
                    btnsubmit.Visible = false;
                    btnupdate.Visible = true;
                    btnedit.Visible = false;
                    btndelete.Visible = false;
                    txtaddress.ReadOnly = false;
                    drpissuits.Enabled = false;
                    headertxt.Text = "Edit Civic";
                    viewdata();
                }
                else if (action == "add" && roleid=="Admin")
                {
                    btnsubmit.Visible = true;
                    btnupdate.Visible = false;
                    btnedit.Visible = false;
                    btndelete.Visible = false;
                    txtaddress.ReadOnly = false;
                    drpissuits.Enabled = true;
                    headertxt.Text = "Add new Civic";
                    
                }
            }
        }

        public void viewdata()
        {
            SqlConnection _con = new SqlConnection(con);
            string sql = "SELECT * FROM LOCALS WHERE LOCAL_ID='" + localid + "'";
            DataTable dt = new DataTable();
            try
            {
                _con.Open();
                SqlCommand com = new SqlCommand(sql, _con);
                dt.Load(com.ExecuteReader());
                foreach (DataRow rw in dt.Rows)
                {
                    txtaddress.Text = rw["LOCAL_ADDRESS"].ToString();
                    drpissuits.Items.FindByValue(rw["IS_SUITS"].ToString()).Selected = true;
                    if (action=="edit")
                    {
                        drpissuits.Enabled = true;
                    }
                    else
                    {
                        drpissuits.Enabled = false;
                    }
                }
                _con.Close();
            }
            catch (Exception exe)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(" + exe.Message + ")", true);
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

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection _con = new SqlConnection(con);
                string sql = "INSERT INTO LOCALS (LOCAL_ADDRESS,BUILDING_ID,IS_SUITS) VALUES ('" + txtaddress.Text + "','" + drpbuilding.SelectedValue + "','" + drpissuits.SelectedValue + "')";
                sql = sql + "; SELECT SCOPE_IDENTITY()";
                _con.Open();
                SqlCommand com = new SqlCommand(sql, _con);
                string id=com.ExecuteScalar().ToString();
                _con.Close();
                Response.Redirect("../Buildings/locals.aspx?lid=" + id + "&bid=" + drpbuilding.SelectedValue.ToString() + "&action=view");
            }
            catch (Exception exe)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(" + exe.Message + ")", true);
            }
        }

        //protected void btnaddsuits_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("Suites.aspx?lid=" + localid + "&bid=" + buildingid);
        //}

        //protected void btnassignsuits_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("AssignSuites.aspx?bid=" + buildingid + "&lid=" + localid + "&action=add");
        //}

        //protected void btnuploadlease_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("../Lease/UploadLease.aspx?bid=" + buildingid + "&lid=" + localid + "&action=add");
        //}

        protected void drpissuits_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(drpissuits.SelectedValue=="Yes")
            {
                
            }
            else if(drpissuits.SelectedValue=="No")
            {

            }
        }

        //protected void btnviewlease_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("../Lease/ViewLease.aspx?bid=" + buildingid + "&lid=" + localid + "&action=add");
        //}

        protected void btnback_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Buildings/Building.aspx?id=" + buildingid + "&action=view");
        }

        protected void txtedit_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Buildings/locals.aspx?lid=" + localid + "&bid=" + buildingid + "&action=edit");
        }

        protected void txtdelete_Click(object sender, EventArgs e)
        {
            SqlConnection _con = new SqlConnection(con);
            string sql1 = "DELETE FROM TENANTS WHERE BUILDING_ID='" + buildingid + "' AND LOCAL_ID='" + localid + "'";
            string sql2 = "DELETE FROM SUITS WHERE BUILDING_ID='" + buildingid + "' AND LOCAL_ID='" + localid + "'";
            string sql3 = "DELETE FROM LEASE WHERE BUILDING_ID='" + buildingid + "' AND LOCAL_ID='" + localid + "'";
            string sql4 = "DELETE FROM RENT WHERE BUILDING_ID='" + buildingid + "' AND LOCAL_ID='" + localid + "'";
            string sql5 = "DELETE FROM LOCALS WHERE BUILDING_ID='" + buildingid + "' AND LOCAL_ID='" + localid + "'";
            try
            {
                _con.Open();
                SqlCommand com1 = new SqlCommand(sql1, _con);
                com1.ExecuteNonQuery();
                SqlCommand com2 = new SqlCommand(sql2, _con);
                com2.ExecuteNonQuery();
                SqlCommand com3 = new SqlCommand(sql3, _con);
                com3.ExecuteNonQuery();
                SqlCommand com4 = new SqlCommand(sql4, _con);
                com4.ExecuteNonQuery();
                SqlCommand com5 = new SqlCommand(sql5, _con);
                com5.ExecuteNonQuery();
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
            string updatesql = "update locals set local_address='"+txtaddress.Text+"',is_suits='"+drpissuits.SelectedValue.ToString()+"' where local_id='"+localid+ "'and building_id='"+buildingid+"'";
            try
            {
            _con.Open();
            SqlCommand com2 = new SqlCommand(updatesql, _con);
            com2.ExecuteNonQuery();
            _con.Close();
            Response.Redirect("../Buildings/locals.aspx?lid=" + localid + "&bid=" + buildingid + "&action=view");
            }
            catch (Exception exe)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(" + exe.Message + ")", true);
            }
        }
    }
}