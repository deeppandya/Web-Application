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
    public partial class viewscreen : System.Web.UI.Page
    {
        string con = ConfigurationManager.ConnectionStrings["JRBConnectionString"].ToString();
        string action,buildingid;
        protected void Page_Load(object sender, EventArgs e)
        {
            action = Request.QueryString["action"];
            buildingid = Session["buildingid"].ToString();
            if(!IsPostBack)
            { 
            populate_buildings();
            if (action == "viewbuildings")
            {
                lblcivic.Visible = false;
                drpcivic.Visible = false;
                lblsuites.Visible = false;
                drpsuites.Visible = false;
                lbltenant.Visible = false;
                drptenant.Visible = false;
            }
            else if (action == "viewcivic")
            {
                lblsuites.Visible = false;
                drpsuites.Visible = false;
                lbltenant.Visible = false;
                drptenant.Visible = false;
            }
            else if (action == "viewsuites")
            {
                lblsuites.Visible = true;
                drpsuites.Visible = true;
                lbltenant.Visible = false;
                drptenant.Visible = false;
                lblerror.Visible = false;
                iframecontent.Visible = true;
                //iframecontent.Src = "../Buildings/Building.aspx?id=" + drpbuilding.SelectedValue.ToString() + "&action=view";
            }
            else if (action == "viewtenant")
            {
                lbltenant.Visible = true;
                drptenant.Visible = true;
            }
            else if (action == "viewtax")
            {
                lblcivic.Visible = false;
                drpcivic.Visible = false;
                lblsuites.Visible = false;
                drpsuites.Visible = false;
                lbltenant.Visible = false;
                drptenant.Visible = false;
            }
            }
        }

        public void populate_buildings()
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
                drpbuilding.Items.Clear();
                drpbuilding.Items.Insert(0, new ListItem("-- choose one--", "0"));
                int i = 1;
                foreach (DataRow dr in dt.Rows)
                {
                    drpbuilding.Items.Insert(i, new ListItem(dr["Building_Name"].ToString(), dr["Building_id"].ToString()));
                    i++;
                }
                drpbuilding.Items.FindByValue(buildingid).Selected = true;
                drpbuilding.Enabled = false;
                populate_civic();
            }
            catch (Exception exe)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(" + exe.Message + ")", true);
            }
        }

        public void populate_civic()
        {
            SqlConnection _con = new SqlConnection(con);
            DataTable dt = new DataTable();
            string sql = "SELECT * FROM LOCALS WHERE BUILDING_ID='" + drpbuilding.SelectedValue.ToString() + "'";
            try
            {
                _con.Open();
                SqlCommand com = new SqlCommand(sql, _con);
                dt.Load(com.ExecuteReader());
                drpcivic.DataSource = dt;
                drpcivic.Items.Clear();
                drpcivic.Items.Insert(0, new ListItem("-- choose one--", "0"));
                int i = 1;
                foreach (DataRow dr in dt.Rows)
                {
                    drpcivic.Items.Insert(i, new ListItem(dr["local_address"].ToString(), dr["local_id"].ToString()));
                    i++;
                }
            }
            catch (Exception exe)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(" + exe.Message + ")", true);
            }
        }

        protected void drpbuilding_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        protected void drpcivic_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblerror.Visible = false;
            iframecontent.Visible = false;
            SqlConnection _con = new SqlConnection(con);
            DataTable dt = new DataTable();
            string sql = "SELECT * FROM SUITS WHERE BUILDING_ID='" + drpbuilding.SelectedValue.ToString() + "' AND LOCAL_ID='"+drpcivic.SelectedValue.ToString()+"'";
            try
            {
                _con.Open();
                SqlCommand com = new SqlCommand(sql, _con);
                dt.Load(com.ExecuteReader());
                drpsuites.DataSource = dt;
                drpsuites.Items.Clear();
                drpsuites.Items.Insert(0, new ListItem("-- choose one--", "0"));
                    int i = 1;
                foreach (DataRow dr in dt.Rows)
                {
                    drpsuites.Items.Insert(i, new ListItem(dr["suit_name"].ToString(), dr["suit_id"].ToString()));
                    i++;
                }
            }
            catch (Exception exe)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(" + exe.Message + ")", true);
            }
        }

        protected void drpsuites_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblerror.Visible = false;
            iframecontent.Visible = false;
            SqlConnection _con = new SqlConnection(con);
            DataTable dt = new DataTable();
            string sql = "SELECT * FROM TENANTS WHERE BUILDING_ID='" + drpbuilding.SelectedValue.ToString() + "' AND LOCAL_ID='" + drpcivic.SelectedValue.ToString() + "' AND SUIT_ID='"+drpsuites.SelectedValue.ToString()+"' AND IS_CURRENT='Yes'";
            try
            {
                _con.Open();
                SqlCommand com = new SqlCommand(sql, _con);
                dt.Load(com.ExecuteReader());
                if(dt.Rows.Count>0)
                {
                    drptenant.DataSource = dt; 
                    drptenant.Items.Clear();
                    drptenant.Items.Insert(0, new ListItem("-- choose one--", "0"));
                    int i = 1;
                    foreach (DataRow dr in dt.Rows)
                    {
                        drptenant.Items.Insert(i, new ListItem(dr["tenant1_name"].ToString(), dr["tenant_id"].ToString()));
                        i++;
                    }
                }
                else
                {
                    drptenant.Visible = false;
                    lblerror.Text = "No tenant information found";
                }
            }
            catch (Exception exe)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(" + exe.Message + ")", true);
            }
        }

        protected void drptenant_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public void Bindgridview()
        {
            SqlConnection _con = new SqlConnection(con);
            DataTable dt = new DataTable();
            string sql = "SELECT S.SUIT_NAME,B.BUILDING_NAME,L.LOCAL_ADDRESS FROM SUITS S,BUILDINGS B,LOCALS L WHERE S.BUILDING_ID='" + drpbuilding.SelectedValue.ToString() + "' AND L.LOCAL_ID='" + drpcivic.SelectedValue.ToString() + "' AND S.BUILDING_ID=B.BUILDING_ID AND S.LOCAL_ID=L.LOCAL_ID";
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

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            if(action=="viewbuildings")
            {
                if(!drpbuilding.SelectedValue.ToString().Equals("0"))
                {
                    lblerror.Visible = false;
                    iframecontent.Visible = true;
                    iframecontent.Src = "../Buildings/Building.aspx?id=" + drpbuilding.SelectedValue.ToString() + "&action=view";
                }
                else
                {
                    lblerror.Visible = true;
                    iframecontent.Visible = false;
                    lblerror.Text = "Please select correct option.";
                }
                
            }
            else if(action=="viewcivic")
            {
                if (!drpbuilding.SelectedValue.ToString().Equals("0") && !drpcivic.SelectedValue.ToString().Equals("0"))
                {
                    lblerror.Visible = false;
                    iframecontent.Visible = true;
                    iframecontent.Src = "../Buildings/locals.aspx?lid=" + drpcivic.SelectedValue.ToString() + "&bid=" + drpbuilding.SelectedValue.ToString() + "&action=view";
                }
                else
                {
                    lblerror.Visible = true;
                    iframecontent.Visible = false;
                    lblerror.Text = "Please select correct option.";
                }
            }
            else if (action == "viewsuites")
            {
                if (!drpbuilding.SelectedValue.ToString().Equals("0") && !drpcivic.SelectedValue.ToString().Equals("0") && !drpsuites.SelectedValue.ToString().Equals("0"))
                {
                    lblerror.Visible = false;
                    iframecontent.Visible = true;
                    iframecontent.Src = "../Buildings/Suites.aspx?bid=" + drpbuilding.SelectedValue.ToString() + "&lid=" + drpcivic.SelectedValue.ToString() + "&sid=" + drpsuites.SelectedValue.ToString() + "&action=view";
                }
                else
                {
                    lblerror.Visible = true;
                    iframecontent.Visible = false;
                    lblerror.Text = "Please select correct option.";
                }
            }
            else if (action == "viewtenant")
            {
                if (!drpbuilding.SelectedValue.ToString().Equals("0") && !drpcivic.SelectedValue.ToString().Equals("0") && !drpsuites.SelectedValue.ToString().Equals("0") && !drptenant.SelectedValue.ToString().Equals("0"))
                {
                    lblerror.Visible = false;
                    iframecontent.Visible = true;
                    iframecontent.Src = "../Buildings/AssignSuites.aspx?bid=" + drpbuilding.SelectedValue.ToString() + "&lid=" + drpcivic.SelectedValue.ToString() + "&sid=" + drpsuites.SelectedValue.ToString() + "&tid=" + drptenant.SelectedValue.ToString() + "&action=view";
                }
                else
                {
                    lblerror.Visible = true;
                    iframecontent.Visible = false;
                    lblerror.Text = "Please select correct option.";
                }                
            }
            else if (action == "viewtax")
            {
                if (!drpbuilding.SelectedValue.ToString().Equals("0"))
                {
                    lblerror.Visible = false;
                    iframecontent.Visible = true;
                    iframecontent.Src = "../Buildings/TaxandExpense.aspx?id=" + drpbuilding.SelectedValue.ToString() + "&action=view";
                }
                else
                {
                    lblerror.Visible = true;
                    iframecontent.Visible = false;
                    lblerror.Text = "Please select correct option.";
                }
            }
        }

        protected void btnback_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Buildings/ViewBuildings.aspx");
        }
    }
}