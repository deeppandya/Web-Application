using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JRBuilding.Pages.Lease
{
    public partial class ViewLease : System.Web.UI.Page
    {string localid;
        string buildingid;
        string con = ConfigurationManager.ConnectionStrings["JRBConnectionString"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            localid = Request.QueryString["lid"];
            buildingid = Request.QueryString["bid"];
            if (!IsPostBack)
            {
                populate_dropdown();
                populate_local_dropdown();
                //populate_Tenant_dropdown();
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
            string sql = "SELECT * FROM LOCALS WHERE Is_Suits='"+_yes+"'";
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

        protected void drpdocumenttype_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpdocumenttype.SelectedValue.ToString() == "2")
            {
                lblsuites.Visible = true;
                drpsuites.Visible = true;
                drpsuites.Items.Clear();
                drptenant.Items.Clear();
                drpdate.Items.Clear();
                drptenant.Visible = false;
                lbltenant.Visible = false;
                lbldate.Visible = false;
                drpdate.Visible = false;
                populate_suites_dropdown("Rent");
            }
            else if (drpdocumenttype.SelectedValue.ToString() == "1")
            {
                lblsuites.Visible = true;
                drpsuites.Visible = true;
                drpsuites.Items.Clear();
                drptenant.Items.Clear();
                drpdate.Items.Clear();
                drptenant.Visible = false;
                lbltenant.Visible = false;
                lbldate.Visible = false;
                drpdate.Visible = false;
                populate_suites_dropdown("Lease");
            }
            else if (drpdocumenttype.SelectedValue.ToString() == "0")
            {
                lblsuites.Visible = false;
                drpsuites.Visible = false;
                drpsuites.Items.Clear();
                drptenant.Items.Clear();
                drpdate.Items.Clear();
                drptenant.Visible = false;
                lbltenant.Visible = false;
                lbldate.Visible = false;
                drpdate.Visible = false;
            }
        }

        public void populate_suites_dropdown(string type)
        {
            if(type=="Rent")
            {
                SqlConnection _con = new SqlConnection(con);
                DataTable dt = new DataTable();
                string sql = "SELECT * FROM RENTS R,SUITS S WHERE R.BUILDING_ID='" + drpbuilding.SelectedValue.ToString() + "' AND R.LOCAL_ID='" + drplocal.SelectedValue.ToString() + "' AND R.SUIT_ID=S.SUIT_ID";
                try
                {
                    _con.Open();
                    SqlCommand com = new SqlCommand(sql, _con);
                    dt.Load(com.ExecuteReader());
                    drpsuites.DataSource = dt;
                    drpsuites.Items.Insert(0, new ListItem("-- choose one--", "0"));
                    int i = 1;
                    foreach (DataRow dr in dt.Rows)
                    {
                        drpsuites.Items.Insert(i, new ListItem(dr["SUIT_Name"].ToString(), dr["SUIT_Id"].ToString()));
                        i++;
                    }
                }
                catch (Exception exe)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(" + exe.Message + ")", true);
                }
            }
            else if(type=="Lease")
            {
                SqlConnection _con = new SqlConnection(con);
                DataTable dt = new DataTable();
                string sql = "SELECT * FROM UPLOAD_LEASE U,SUITS S WHERE U.BUILDING_ID='"+drpbuilding.SelectedValue.ToString()+"' AND U.LOCAL_ID='"+drplocal.SelectedValue.ToString()+"' AND U.SUIT_ID=S.SUIT_ID";
                try
                {
                    _con.Open();
                    SqlCommand com = new SqlCommand(sql, _con);
                    dt.Load(com.ExecuteReader());
                    drpsuites.DataSource = dt;
                    drpsuites.Items.Insert(0, new ListItem("-- choose one--", "0"));
                    int i = 1;
                    foreach (DataRow dr in dt.Rows)
                    {
                        drpsuites.Items.Insert(i, new ListItem(dr["SUIT_Name"].ToString(), dr["SUIT_Id"].ToString()));
                        i++;
                    }
                }
                catch (Exception exe)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(" + exe.Message + ")", true);
                }
            }
        }

        //public void populate_Tenant_dropdown(string type)
        //{
        //    SqlConnection _con = new SqlConnection(con);
        //    DataTable dt = new DataTable();
           
        //    string sql = "SELECT * FROM TENANTS";
        //    try
        //    {
        //        _con.Open();
        //        SqlCommand com = new SqlCommand(sql, _con);
        //        dt.Load(com.ExecuteReader());
        //        drptenant.DataSource = dt; // <-- Get your data from somewhere.
        //        //drptenant.DataValueField = "Tenant_id";
        //        //drptenant.DataTextField = "Tenant1_Name";
        //        //drptenant.DataBind();
        //        drptenant.Items.Insert(0, new ListItem("-- choose one--", "0"));
        //        int i = 1;
        //        foreach (DataRow dr in dt.Rows)
        //        {
        //            drptenant.Items.Insert(i, new ListItem(dr["Tenant1_Name"].ToString(), dr["Tenant_Id"].ToString()));
        //            i++;
        //        }
        //        //drptenant.Items.FindByValue(localid).Selected = true;
        //        //drptenant.Enabled = false;
        //        //drpbuilding.SelectedValue = myValue.ToString();
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}

        //protected void drptenant_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    lblsuits.Visible = true;
        //    drpsuits.Visible = true;
        //    SqlConnection _con = new SqlConnection(con);
        //    DataTable dt = new DataTable();

        //    string sql = "SELECT S.Suit_id,S.SUIT_NAME FROM TENANTS T,SUITS S WHERE T.BUILDING_ID='"+buildingid+"' AND T.LOCAL_ID='"+localid+"' AND T.TENANT_ID='"+drptenant.SelectedValue+"' AND T.SUIT_ID=S.SUIT_ID";
        //    try
        //    {
        //        _con.Open();
        //        SqlCommand com = new SqlCommand(sql, _con);
        //        dt.Load(com.ExecuteReader());
        //        drpsuits.DataSource = dt; // <-- Get your data from somewhere.
        //        drpsuits.DataValueField = "Suit_id";
        //        drpsuits.DataTextField = "Suit_Name";
        //        drpsuits.DataBind();
                
        //        drpsuits.Enabled = false;
        //        //drpbuilding.SelectedValue = myValue.ToString();
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}

        protected void btnback_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Buildings/locals.aspx?lid=" + localid + "&bid=" + buildingid + "&action=view");
        }

       

        protected void drpsuites_SelectedIndexChanged(object sender, EventArgs e)
        {
            drptenant.Visible = true;
            lbltenant.Visible = true;
            SqlConnection _con = new SqlConnection(con);
            DataTable dt = new DataTable();
            string sql = "SELECT * FROM TENANTS WHERE BUILDING_ID='" + drpbuilding.SelectedValue.ToString() + "' AND LOCAL_ID='" + drplocal.SelectedValue.ToString() + "' AND SUIT_ID='"+drpsuites.SelectedValue.ToString()+"'";
            try
            {
                _con.Open();
                SqlCommand com = new SqlCommand(sql, _con);
                dt.Load(com.ExecuteReader());
                drptenant.DataSource = dt;
                drptenant.DataValueField = "TENANT_id";
                drptenant.DataTextField = "TENANT1_Name";
                drptenant.DataBind();
                drptenant.Enabled = false;
                if(drpdocumenttype.SelectedValue.ToString()=="1")
                {
                    lbldate.Visible = false;
                    drpdate.Visible = false;
                }
                else if (drpdocumenttype.SelectedValue.ToString() == "2")
                {
                    lbldate.Visible = true;
                    drpdate.Visible = true;
                }
                populate_date();
            }
            catch (Exception exe)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(" + exe.Message + ")", true);
            }
        }

        public void populate_date()
        {
            SqlConnection _con = new SqlConnection(con);
            DataTable dt = new DataTable();
            string sql = "SELECT * FROM RENTS R WHERE R.BUILDING_ID='" + drpbuilding.SelectedValue.ToString() + "' AND R.LOCAL_ID='" + drplocal.SelectedValue.ToString() + "' AND R.SUIT_ID='"+drpsuites.SelectedValue.ToString()+"'";
            try
            {
                _con.Open();
                SqlCommand com = new SqlCommand(sql, _con);
                dt.Load(com.ExecuteReader());
                drpdate.DataSource = dt;
                drpdate.Items.Insert(0, new ListItem("-- choose one--", "0"));
                int i = 1;
                foreach (DataRow dr in dt.Rows)
                {
                    drpdate.Items.Insert(i, new ListItem(dr["Date"].ToString(), dr["cheque_Id"].ToString()));
                    i++;
                }
            }
            catch (Exception exe)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(" + exe.Message + ")", true);
            }
        }

        protected void drpdate_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            if(drpdocumenttype.SelectedValue.ToString()=="1")
            {
                SqlConnection _con = new SqlConnection(con);
                DataTable dt = new DataTable();
                string sql = "SELECT LEASE_NAME FROM UPLOAD_LEASE WHERE BUILDING_ID='" + buildingid + "' AND LOCAL_ID='" + localid + "' AND TENANT_ID='" + drptenant.SelectedValue + "' AND SUIT_ID='" + drpsuites.SelectedValue + "'";
                try
                {
                    _con.Open();
                    SqlCommand com = new SqlCommand(sql, _con);
                    dt.Load(com.ExecuteReader());
                    string name = string.Empty;
                    foreach (DataRow rw in dt.Rows)
                    {
                        name = rw["LEASE_NAME"].ToString();
                    }
                    iframepdf.Visible = true;
                    iframepdf.Src = "../Lease/Documents/" + name;
                }
                catch (Exception exe)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(" + exe.Message + ")", true);
                }
            }
            else if (drpdocumenttype.SelectedValue.ToString() == "2")
            {
                SqlConnection _con = new SqlConnection(con);
                DataTable dt = new DataTable();
                string sql = "SELECT CHEQUE_NAME FROM RENTS WHERE BUILDING_ID='" + buildingid + "' AND LOCAL_ID='" + localid + "' AND TENANT_ID='" + drptenant.SelectedValue + "' AND SUIT_ID='" + drpsuites.SelectedValue + "' AND CHEQUE_ID='"+drpdate.SelectedValue.ToString()+"'";
                try
                {
                    _con.Open();
                    SqlCommand com = new SqlCommand(sql, _con);
                    dt.Load(com.ExecuteReader());
                    string name = string.Empty;
                    foreach (DataRow rw in dt.Rows)
                    {
                        name = rw["CHEQUE_NAME"].ToString();
                    }
                    iframepdf.Visible = true;
                    iframepdf.Src = "../Lease/Rent_Cheques/" + name;
                }
                catch (Exception exe)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(" + exe.Message + ")", true);
                }
            }
        }

        protected void drptenant_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
   }
}