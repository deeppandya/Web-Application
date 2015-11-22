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
    public partial class TaxandExpense : System.Web.UI.Page
    {
        string con = ConfigurationManager.ConnectionStrings["JRBConnectionString"].ToString();
        string buildingid;
        string action,roleid;
        string viewscreen;
        protected void Page_Load(object sender, EventArgs e)
        {
            buildingid = Request.QueryString["id"];
            action = Request.QueryString["action"];
            viewscreen = Request.QueryString["viewscreen"];
            roleid = Session["roleid"].ToString();
            populate_dropdown();
            
            if(!IsPostBack)
            {
                if (action == "add" && roleid == "Admin")
                {
                    btnsubmit.Visible = true;
                    btnupdate.Visible = false;
                    btnedittax.Visible = false;
                    btndelete.Visible = false;
                    DataTable dt = CheckData();
                    if(dt.Rows.Count>0)
                    {
                        lblerror.Text = "Tax and expanses for this building are already set.";
                        sectiondata.Visible = false;
                    }
                    
                }
                else if (action == "view" && roleid == "Admin")
                {
                    btnsubmit.Visible = false;
                    btnupdate.Visible = false;
                    btnedittax.Visible = true;
                    btndelete.Visible = true;
                    ViewData(CheckData());
                }
                else if (action == "view" && roleid == "Finance")
                {
                    btnsubmit.Visible = false;
                    btnupdate.Visible = false;
                    btnedittax.Visible = true;
                    btndelete.Visible = true;
                    ViewData(CheckData());
                }
                else if (action == "view" && roleid != "Admin")
                {
                    btnsubmit.Visible = false;
                    btnupdate.Visible = false;
                    btnedittax.Visible = false;
                    btndelete.Visible = false;
                    ViewData(CheckData());
                }
                else if (action == "edit" && roleid == "Admin")
                {
                    btnsubmit.Visible = false;
                    btnupdate.Visible = true;
                    btnedittax.Visible = false;
                    btndelete.Visible = false;
                    ViewData(CheckData());
                    EditData();
                }
            }
        }

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            AddData();
        }
        public void ViewData(DataTable _table)
        {
            txtfromdate.ReadOnly = true;
            txttodate.ReadOnly = true;
            txtgstnum.ReadOnly = true;
            txtqstnum.ReadOnly = true;
            txttax1name.ReadOnly = true;
            txttax2name.ReadOnly = true;
            txttax3name.ReadOnly = true;
            txttax4name.ReadOnly = true;
            txttax5name.ReadOnly = true;
            txttax6name.ReadOnly = true;
            txttax7name.ReadOnly = true;
            txttax8name.ReadOnly = true;
            txttax9name.ReadOnly = true;
            txttax10name.ReadOnly = true;
            txttax11name.ReadOnly = true;
            txttax12name.ReadOnly = true;
            txttax13name.ReadOnly = true;
            txttax14name.ReadOnly = true;
            txttax15name.ReadOnly = true;
            txttax1value.ReadOnly = true;
            txttax2value.ReadOnly = true;
            txttax3value.ReadOnly = true;
            txttax4value.ReadOnly = true;
            txttax5value.ReadOnly = true;
            txttax6value.ReadOnly = true;
            txttax7value.ReadOnly = true;
            txttax8value.ReadOnly = true;
            txttax9value.ReadOnly = true;
            txttax10value.ReadOnly = true;
            txttax11value.ReadOnly = true;
            txttax12value.ReadOnly = true;
            txttax13value.ReadOnly = true;
            txttax14value.ReadOnly = true;
            txttax15value.ReadOnly = true;
            //txtadminfees.ReadOnly = true;
            //txtrepairs.ReadOnly = true;
            txtgstper.ReadOnly = true;
            txtqstper.ReadOnly = true;
            btnsubmit.Enabled = false;
            
            DataTable dt = _table;
            try
            {
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow rw in dt.Rows)
                    {
                        txtfromdate.Text = rw["FROM_DATE"].ToString();
                        txttodate.Text = rw["TO_DATE"].ToString();
                        txtgstnum.Text = rw["GST_NUM"].ToString();
                        txtqstnum.Text = rw["QST_NUM"].ToString();
                        txttax1name.Text = rw["TAX_1_TEXT"].ToString();
                        txttax1value.Text = rw["TAX_1_VALUE"].ToString();
                        txttax2name.Text = rw["TAX_2_TEXT"].ToString();
                        txttax2value.Text = rw["TAX_2_VALUE"].ToString();
                        txttax3name.Text = rw["TAX_3_TEXT"].ToString();
                        txttax3value.Text = rw["TAX_3_VALUE"].ToString();
                        txttax4name.Text = rw["TAX_4_TEXT"].ToString();
                        txttax4value.Text = rw["TAX_4_VALUE"].ToString();
                        txttax5name.Text = rw["TAX_5_TEXT"].ToString();
                        txttax5value.Text = rw["TAX_5_VALUE"].ToString();
                        txttax6name.Text = rw["TAX_6_TEXT"].ToString();
                        txttax6value.Text = rw["TAX_6_VALUE"].ToString();
                        txttax7name.Text = rw["TAX_7_TEXT"].ToString();
                        txttax7value.Text = rw["TAX_7_VALUE"].ToString();
                        txttax8name.Text = rw["TAX_8_TEXT"].ToString();
                        txttax8value.Text = rw["TAX_8_VALUE"].ToString();
                        txttax9name.Text = rw["TAX_9_TEXT"].ToString();
                        txttax9value.Text = rw["TAX_9_VALUE"].ToString();
                        txttax10name.Text = rw["TAX_10_TEXT"].ToString();
                        txttax10value.Text = rw["TAX_10_VALUE"].ToString();
                        txttax11name.Text = rw["TAX_11_TEXT"].ToString();
                        txttax11value.Text = rw["TAX_11_VALUE"].ToString();
                        txttax12name.Text = rw["TAX_12_TEXT"].ToString();
                        txttax12value.Text = rw["TAX_12_VALUE"].ToString();
                        txttax13name.Text = rw["TAX_13_TEXT"].ToString();
                        txttax13value.Text = rw["TAX_13_VALUE"].ToString();
                        txttax14name.Text = rw["TAX_14_TEXT"].ToString();
                        txttax14value.Text = rw["TAX_14_VALUE"].ToString();
                        txttax15name.Text = rw["TAX_15_TEXT"].ToString();
                        txttax15value.Text = rw["TAX_15_VALUE"].ToString();

                        //txtadminfees.Text = rw["ADMIN_FEES"].ToString();
                        //txtrepairs.Text = rw["REPAIRS_AND_MAINTENANCE"].ToString();
                        txtgstper.Text = rw["GST_PER"].ToString();
                        txtqstper.Text = rw["QST_PER"].ToString();
                        headertxt.Text = "View Tax and Expenses";
                    }
                }
                else
                {
                    lblerror.Text = "No Data found";
                    sectiondata.Visible = false;
                }
                
            }
            catch (Exception exe)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(" + exe.Message + ")", true);
            }

        }
        public void AddData()
        {
            SqlConnection _con = new SqlConnection(con);
            

            try
            {
                string sql = "INSERT INTO TAXES (FROM_DATE,TO_DATE,GST_NUM,QST_NUM,TAX_1_TEXT,TAX_1_VALUE,TAX_2_TEXT,TAX_2_VALUE,TAX_3_TEXT,TAX_3_VALUE,TAX_4_TEXT,TAX_4_VALUE,TAX_5_TEXT,TAX_5_VALUE,TAX_6_TEXT,TAX_6_VALUE,TAX_7_TEXT,TAX_7_VALUE,TAX_8_TEXT,TAX_8_VALUE,TAX_9_TEXT,TAX_9_VALUE,TAX_10_TEXT,TAX_10_VALUE,TAX_11_TEXT,TAX_11_VALUE,TAX_12_TEXT,TAX_12_VALUE,TAX_13_TEXT,TAX_13_VALUE,TAX_14_TEXT,TAX_14_VALUE,TAX_15_TEXT,TAX_15_VALUE,GST_PER,QST_PER,BUILDING_ID) VALUES ('" + Convert.ToDateTime(txtfromdate.Text) + "','" + Convert.ToDateTime(txttodate.Text) + "','" + txtgstnum.Text + "','" + txtqstnum.Text + "','" + txttax1name.Text + "','" + txttax1value.Text + "','" + txttax2name.Text + "','" + txttax2value.Text + "','" + txttax3name.Text + "','" + txttax3value.Text + "','" + txttax4name.Text + "','" + txttax4value.Text + "','" + txttax5name.Text + "','" + txttax5value.Text + "','" + txttax6name.Text + "','" + txttax6value.Text + "','" + txttax7name.Text + "','" + txttax7value.Text + "','" + txttax8name.Text + "','" + txttax8value.Text + "','" + txttax9name.Text + "','" + txttax9value.Text + "','" + txttax10name.Text + "','" + txttax10value.Text + "','" + txttax11name.Text + "','" + txttax11value.Text + "','" + txttax12name.Text + "','" + txttax12value.Text + "','" + txttax13name.Text + "','" + txttax13value.Text + "','" + txttax14name.Text + "','" + txttax14value.Text + "','" + txttax15name.Text + "','" + txttax15value.Text + "','" + txtgstper.Text + "','" + txtqstper.Text + "','" + buildingid + "')";
                _con.Open();
                SqlCommand com = new SqlCommand(sql, _con);
                com.ExecuteNonQuery();
                _con.Close();
                Response.Redirect("../Buildings/TaxandExpense.aspx?id=" + buildingid + "&action=view");

            }
            catch (Exception exe)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(" + exe.Message + ")", true);
            }
        }
        public void EditData()
        {
            txtfromdate.ReadOnly = false;
            txttodate.ReadOnly = false;
            txtgstnum.ReadOnly = false;
            txtqstnum.ReadOnly = false;
            txttax1name.ReadOnly = false;
            txttax2name.ReadOnly = false;
            txttax3name.ReadOnly = false;
            txttax4name.ReadOnly = false;
            txttax5name.ReadOnly = false;
            txttax6name.ReadOnly = false;
            txttax7name.ReadOnly = false;
            txttax8name.ReadOnly = false;
            txttax9name.ReadOnly = false;
            txttax10name.ReadOnly = false;
            txttax11name.ReadOnly = false;
            txttax12name.ReadOnly = false;
            txttax13name.ReadOnly = false;
            txttax14name.ReadOnly = false;
            txttax15name.ReadOnly = false;
            txttax1value.ReadOnly = false;
            txttax2value.ReadOnly = false;
            txttax3value.ReadOnly = false;
            txttax4value.ReadOnly = false;
            txttax5value.ReadOnly = false;
            txttax6value.ReadOnly = false;
            txttax7value.ReadOnly = false;
            txttax8value.ReadOnly = false;
            txttax9value.ReadOnly = false;
            txttax10value.ReadOnly = false;
            txttax11value.ReadOnly = false;
            txttax12value.ReadOnly = false;
            txttax13value.ReadOnly = false;
            txttax14value.ReadOnly = false;
            txttax15value.ReadOnly = false;
            //txtadminfees.ReadOnly = false;
            //txtrepairs.ReadOnly = false;
            txtgstper.ReadOnly = false;
            txtqstper.ReadOnly = false;
            headertxt.Text = "Update Tax and Expense";
            btnsubmit.Text = "Update Tax and Expense";
            btnsubmit.Enabled = true;
            
        }
        public void populate_dropdown()
        {
            SqlConnection _con = new SqlConnection(con);
            DataTable dt = new DataTable();
            string sql = "SELECT * FROM BUILDINGS";
            //if (string.IsNullOrEmpty(UserId) || string.IsNullOrEmpty(password))
            //{
            //    dt = null;
            //}
            try
            {
                _con.Open();
                SqlCommand com = new SqlCommand(sql, _con);
                dt.Load(com.ExecuteReader());
                drpbuilding.DataSource = dt; // <-- Get your data from somewhere.
                drpbuilding.DataValueField = "Building_id";
                drpbuilding.DataTextField = "Building_Name";
                drpbuilding.DataBind();
                drpbuilding.Items.FindByValue(buildingid).Selected = true;
                drpbuilding.Enabled = false;
                //drpbuilding.SelectedValue = myValue.ToString();
            }
            catch (Exception exe)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(" + exe.Message + ")", true);
            }
        }
        public DataTable CheckData()
        {
            SqlConnection _con = new SqlConnection(con);
            string sql = "SELECT * FROM TAXES WHERE BUILDING_ID='" + buildingid + "'";
            DataTable dt = new DataTable();
            try
            {
                _con.Open();
                SqlCommand com = new SqlCommand(sql, _con);
                dt.Load(com.ExecuteReader());
                _con.Close();
            }
            catch (Exception exe)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(" + exe.Message + ")", true);
                dt = null;
            }
            return dt;
        }

        protected void btnedittax_Click(object sender, EventArgs e)
        {
            Response.Redirect("TaxandExpense.aspx?id="+buildingid+"&action=edit");
        }

        protected void btnback_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Buildings/Building.aspx?id=" + buildingid + "&action=view");
        }

        protected void btnupdate_Click(object sender, EventArgs e)
        {
            SqlConnection _con = new SqlConnection(con);
            string sql = "UPDATE TAXES SET FROM_DATE='" + Convert.ToDateTime(txtfromdate.Text) + "',TO_DATE='" + Convert.ToDateTime(txttodate.Text) + "',GST_NUM='" + txtgstnum.Text + "',QST_NUM='" + txtqstnum.Text + "',TAX_1_TEXT='" + txttax1name.Text + "',TAX_1_VALUE='" + txttax1value.Text + "',TAX_2_TEXT='" + txttax2name.Text + "',TAX_2_VALUE='" + txttax2value.Text + "',TAX_3_TEXT='" + txttax3name.Text + "',TAX_3_VALUE='" + txttax3value.Text + "',TAX_4_TEXT='" + txttax4name.Text + "',TAX_4_VALUE='" + txttax4value.Text + "',TAX_5_TEXT='" + txttax5name.Text + "',TAX_5_VALUE='" + txttax5value.Text + "',TAX_6_TEXT='" + txttax6name.Text + "',TAX_6_VALUE='" + txttax6value.Text + "',TAX_7_TEXT='" + txttax7name.Text + "',TAX_7_VALUE='" + txttax7value.Text + "',TAX_8_TEXT='" + txttax8name.Text + "',TAX_8_VALUE='" + txttax8value.Text + "',TAX_9_TEXT='" + txttax9name.Text + "',TAX_9_VALUE='" + txttax9value.Text + "',TAX_10_TEXT='" + txttax10name.Text + "',TAX_10_VALUE='" + txttax10value.Text + "',TAX_11_TEXT='" + txttax11name.Text + "',TAX_11_VALUE='" + txttax11value.Text + "',TAX_12_TEXT='" + txttax12name.Text + "',TAX_12_VALUE='" + txttax12value.Text + "',TAX_13_TEXT='" + txttax13name.Text + "',TAX_13_VALUE='" + txttax13value.Text + "',TAX_14_TEXT='" + txttax14name.Text + "',TAX_14_VALUE='" + txttax14value.Text + "',TAX_15_TEXT='" + txttax15name.Text + "',TAX_15_VALUE='" + txttax15value.Text + "',GST_PER='" + txtgstper.Text + "',QST_PER='" + txtqstper.Text + "',BUILDING_ID='" + buildingid + "'";
            try
            {
                _con.Open();
                SqlCommand com = new SqlCommand(sql, _con);
                com.ExecuteNonQuery();
                _con.Close();
                Response.Redirect("../Buildings/TaxandExpense.aspx?id=" + buildingid + "&action=view");
            }
            catch (Exception exe)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(" + exe.Message + ")", true);
            }
        }

        protected void btndelete_Click(object sender, EventArgs e)
        {
            SqlConnection _con = new SqlConnection(con);
            string sql = "DELETE FROM TAXES WHERE BUILDING_ID='" + buildingid + "'";
            try
            {
                _con.Open();
                SqlCommand com = new SqlCommand(sql, _con);
                com.ExecuteNonQuery();
                _con.Close();
                Response.Redirect("../User/Profile2.aspx?id=" + buildingid + "&action=view");
            }
            catch (Exception exe)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(" + exe.Message + ")", true);
            }
        }
    }
}