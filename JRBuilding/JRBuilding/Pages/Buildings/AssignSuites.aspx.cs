using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using System.Drawing;
using System.IO;
using iTextSharp.text.html.simpleparser;

namespace JRBuilding.Pages.Buildings
{
    public partial class AssignSuits : System.Web.UI.Page
    {
        string buildingid, localid,tenantid, action,roleid,suit_id;
        string con = ConfigurationManager.ConnectionStrings["JRBConnectionString"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            buildingid = Request.QueryString["bid"];
            localid = Request.QueryString["lid"];
            action = Request.QueryString["action"];
            tenantid = Request.QueryString["tid"];
            suit_id = Request.QueryString["sid"];
            roleid = Session["roleid"].ToString();
            populate_dropdown();
            populate_suit_dropdown();
            if(!IsPostBack)
            {
                if(action=="add" && roleid=="Admin")
                {
                    btnsubmit.Visible = true;
                    btnupdate.Visible = false;
                    btnedit.Visible = false;
                    btndelete.Visible = false;
                    btnupload.Visible = false;
                    ChangeData();
                }
                else if(action=="view" && roleid=="Admin")
                {
                    btnsubmit.Visible = false;
                    btnupdate.Visible = false;
                    btnedit.Visible = true;
                    btndelete.Visible = true;
                    btnupload.Visible = true;
                    ViewData();
                    getdata();
                }
                else if (action == "view" && roleid != "Admin")
                {
                    btnsubmit.Visible = false;
                    btnupdate.Visible = false;
                    btnedit.Visible = false;
                    btndelete.Visible = false;
                    btnupload.Visible = false;
                    ViewData();
                    getdata();
                }
                else if (action == "edit" && roleid == "Admin")
                {
                    btnsubmit.Visible = false;
                    btnupdate.Visible = true;
                    btnedit.Visible = false;
                    btndelete.Visible = false;
                    btnupload.Visible = false;
                    ChangeData();
                    getdata();
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

        public void populate_suit_dropdown()
        {
            if(action=="add")
            {
                SqlConnection _con = new SqlConnection(con);
                DataTable dt = new DataTable();
                string sql = "SELECT * FROM SUITS WHERE SUIT_ID NOT IN ( SELECT SUIT_ID FROM TENANTS WHERE BUILDING_ID='" + buildingid + "' AND LOCAL_ID='" + localid + "') AND  BUILDING_ID='" + buildingid + "' AND LOCAL_ID='" + localid + "'";
                try
                {
                    _con.Open();
                    SqlCommand com = new SqlCommand(sql, _con);
                    dt.Load(com.ExecuteReader());
                    drplocal.DataSource = dt;
                    drplocal.DataValueField = "Suit_id";
                    drplocal.DataTextField = "Suit_Name";
                    drplocal.DataBind();
                }
                catch (Exception exe)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(" + exe.Message + ")", true);
                }
            }
            else
            {
                SqlConnection _con = new SqlConnection(con);
                DataTable dt = new DataTable();
                string sql = "SELECT * FROM SUITS WHERE  BUILDING_ID='" + buildingid + "' AND LOCAL_ID='" + localid + "'";
                try
                {
                    _con.Open();
                    SqlCommand com = new SqlCommand(sql, _con);
                    dt.Load(com.ExecuteReader());
                    drplocal.DataSource = dt;
                    drplocal.DataValueField = "Suit_id";
                    drplocal.DataTextField = "Suit_Name";
                    drplocal.DataBind();
                }
                catch (Exception exe)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(" + exe.Message + ")", true);
                }
            }

            
        }

        public void AddData()
        {
            SqlConnection _con = new SqlConnection(con);
            string _into = "TENANT1_NAME,TENANT1_PHONE_NUMBER,TENANT1_CELL_NUMBER,TENANT1_EMAIL,TENANT1_ALT_EMAIL,TENANT1_SIN" +
                        ",TENANT1_HOME_ADDRESS,TENANT1_TIME_AT_ADDRESS,TENANT1_RENTAL_AMOUNT,TENANT1_DATE_OF_BIRTH,TENANT1_REASON" +
                        ",TENANT2_NAME,TENANT2_PHONE_NUMBER,TENANT2_CELL_NUMBER,TENANT2_EMAIL,TENANT2_ALT_EMAIL,TENANT2_SIN" +
                        ",TENANT2_HOME_ADDRESS,TENANT2_TIME_AT_ADDRESS,TENANT2_RENTAL_AMOUNT,TENANT2_DATE_OF_BIRTH,TENANT2_REASON" +
                        ",BANK_NAME,BANK_ADDRESS,BANK_CONTACT_NUMBER,BANK_ACCOUNT_NUMBER,EMP_COMPANY_NAME,EMP_ADDRESS" +
                        ",EMP_CONTACT_NAME,EMP_PHONE_NUMBER,COM_LANDLORD_NAME,COM_DATE_IN,COM_DATE_OUT,COM_ADDRESS" +
                        ",COM_PHONE_NUMBER,COM_REASON,SUIT_ID,LOCAL_START_DATE,LOCAL_DEPOSITE_LEFT,LOCAL_DURATION,LOCAL_RENTAL_AMOUNT,OPTIONS,ANNUAL_PER,IS_CURRENT,BUILDING_ID,LOCAL_ID,RENTAL_INCREASE";
                string _values = "'" + txtname1.Text + "' , '" + txtphnum1.Text +
                                       "' , '" + txtcellnum1.Text +
                                       "' , '" + txtemail1.Text +
                                       "' , '" + txtaltemail1.Text +
                                       "' , '" + txtsocial1.Text +
                                       "' , '" + txtaddress1.Text +
                                       "' , '" + txtduration1.Text +
                                       "' , '" + txtrental1.Text;
                                    if (txtdob1.Text == string.Empty)
                                    {
                                        _values = _values + "' , '" + string.Empty;
                                    }
                                    else
                                    {
                                        try
                                        {
                                            _values = _values + "' , '" + Convert.ToDateTime(txtdob1.Text).ToString();
                                        }
                                        catch (Exception ex)
                                        {
                                            _values = _values + "' , '" + string.Empty;
                                        }
                                    }
                                    _values = _values + "' , '" + txtpurpose1.Text +
                                   "' , '" + txtname2.Text +
                                   "' , '" + txtphnum2.Text +
                                   "' , '" + txtcellnum2.Text +
                                   "' , '" + txtemail2.Text +
                                   "' , '" + txtaltemail2.Text +
                                   "' , '" + txtsocial2.Text +
                                   "' , '" + txtaddress2.Text +
                                   "' , '" + txtduration2.Text +
                                   "' , '" + txtrental2.Text;
                                    if(txtdob2.Text==string.Empty)
                                    {
                                        _values = _values + "' , '" + string.Empty;
                                    }
                                    else
                                    {
                                        try
                                        {
                                            _values = _values + "' , '" + Convert.ToDateTime(txtdob2.Text).ToString();
                                        }
                                        catch(Exception ex)
                                        {
                                            _values = _values + "' , '" + string.Empty;
                                        }
                                    }
                                    _values = _values + "' , '" + txtpurpose2.Text +
                                   "' , '" + txtbankname.Text +
                                   "' , '" + txtbankaddress.Text +
                                   "' , '" + txtbankph.Text +
                                   "' , '" + txtaccountnumber.Text +
                                   "' , '" + txtcompanyname.Text +
                                   "' , '" + txtempaddress.Text +
                                   "' , '" + txtcontactname.Text +
                                   "' , '" + txtempphnumber.Text +
                                   "' , '" + txtlandlordname.Text;
                                    if (txtrentaldatein.Text == string.Empty)
                                    {
                                        _values = _values + "' , '" + string.Empty;
                                    }
                                    else
                                    {
                                        try
                                        {
                                            _values = _values + "' , '" + Convert.ToDateTime(txtrentaldatein.Text).ToString();
                                        }
                                        catch(Exception ex)
                                        {
                                            _values = _values + "' , '" + string.Empty;
                                        }
                                    }
                                    if (txtrentaldateout.Text == string.Empty)
                                    {
                                        _values = _values + "' , '" + string.Empty;
                                    }
                                    else
                                    {
                                        try
                                        {
                                            _values = _values + "' , '" + Convert.ToDateTime(txtrentaldateout.Text).ToString();
                                        }
                                        catch (Exception ex)
                                        {
                                            _values = _values + "' , '" + string.Empty;
                                        }
                                    }
                                    _values = _values + "' , '" + txtrentaladdress.Text +
                                   "' , '" + txtrentalphnumber.Text +
                                   "' , '" + txtrentalreason.Text +
                                   "' , '" + drplocal.SelectedValue +
                                   "' , '" + Convert.ToDateTime(txtlocalstartdate.Text).ToString() +
                                   "' , '" + txtlocaldeposite.Text +
                                   "' , '" + txtlocalduration.Text +
                                   "' , '" + txtlocalamount.Text +
                                   "' , '" + txtoptions.Text +
                                   "' , '" + txtannualper.Text +
                                   "' , '" + drpcurrent.SelectedValue.ToString() +
                                   "' , '" + buildingid +
                                   "' , '" + localid+
                                   "' , '" + txtrentalincrease.Text + "'";
            string sql = "INSERT INTO TENANTS ("+_into+") VALUES ("+_values+")";
            sql = sql + "; SELECT SCOPE_IDENTITY()";

            try
            {
                _con.Open();
                SqlCommand com = new SqlCommand(sql, _con);
                string id=com.ExecuteScalar().ToString();
                _con.Close();
                Response.Redirect("../Buildings/AssignSuites.aspx?bid=" + buildingid + "&lid=" + localid + "&sid=" + suit_id + "&tid=" + id + "&action=view");
            }
            catch (Exception exe)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(" + exe.Message + ")", true);
            }
        }

        public void EditData()
        {
            SqlConnection _con = new SqlConnection(con);
            string setstring = "TENANT1_NAME=" + txtname1.Text +
                               ",TENANT1_PHONE_NUMBER=" + txtphnum1.Text +
                               ",TENANT1_CELL_NUMBER=" + txtcellnum1.Text +
                               ",TENANT1_EMAIL=" + txtemail1.Text +
                               ",TENANT1_ALT_EMAIL=" + txtaltemail1.Text +
                               ",TENANT1_SIN=" + txtsocial1.Text +
                               ",TENANT1_HOME_ADDRESS=" + txtaddress1.Text +
                               ",TENANT1_TIME_AT_ADDRESS=" + txtduration1.Text +
                               ",TENANT1_RENTAL_AMOUNT=" + txtrental1.Text;
                                if (txtdob1.Text == string.Empty)
                                {
                                    setstring = setstring + ",TENANT1_DATE_OF_BIRTH=" + string.Empty;
                                }
                                else
                                {
                                    try
                                    {
                                        setstring = setstring + ",TENANT1_DATE_OF_BIRTH=" + Convert.ToDateTime(txtdob1.Text);
                                    }
                                    catch (Exception ex)
                                    {
                                        setstring = setstring + ",TENANT1_DATE_OF_BIRTH=" + string.Empty;
                                    }
                                }
                                setstring = setstring + ",TENANT1_REASON=" + txtpurpose1.Text +
                               ",TENANT2_NAME=" + txtname2.Text +
                               ",TENANT2_PHONE_NUMBER=" + txtphnum2.Text +
                               ",TENANT2_CELL_NUMBER=" + txtcellnum2.Text +
                               ",TENANT2_EMAIL=" + txtemail2.Text +
                               ",TENANT2_ALT_EMAIL=" + txtaltemail2.Text +
                               ",TENANT2_SIN=" + txtsocial2.Text +
                               ",TENANT2_HOME_ADDRESS=" + txtaddress2.Text +
                               ",TENANT2_TIME_AT_ADDRESS=" + txtduration2.Text +
                               ",TENANT2_RENTAL_AMOUNT=" + txtrental2.Text;
                                if (txtdob2.Text == string.Empty)
                                {
                                    setstring = setstring + ",TENANT2_DATE_OF_BIRTH=" + string.Empty;
                                }
                                else
                                {
                                    try
                                    {
                                        setstring = setstring + ",TENANT2_DATE_OF_BIRTH=" + Convert.ToDateTime(txtdob2.Text);
                                    }
                                    catch (Exception ex)
                                    {
                                        setstring = setstring + ",TENANT2_DATE_OF_BIRTH=" + string.Empty;
                                    }
                                }
                                setstring = setstring + ",TENANT2_REASON=" + txtpurpose2.Text +
                               ",BANK_NAME=" + txtbankname.Text +
                               ",BANK_ADDRESS=" + txtbankaddress.Text +
                               ",BANK_CONTACT_NUMBER=" + txtbankph.Text +
                               ",BANK_ACCOUNT_NUMBER=" + txtaccountnumber.Text +
                               ",EMP_COMPANY_NAME=" + txtcompanyname.Text +
                               ",EMP_ADDRESS=" + txtempaddress.Text +
                               ",EMP_CONTACT_NAME=" + txtcontactname.Text +
                               ",EMP_PHONE_NUMBER=" + txtempphnumber.Text +
                               ",COM_LANDLORD_NAME=" + txtlandlordname.Text;
                                if (txtrentaldatein.Text == string.Empty)
                                {
                                    setstring = setstring + ",COM_DATE_IN=" + string.Empty;
                                }
                                else
                                {
                                    try
                                    {
                                        setstring = setstring + ",COM_DATE_IN=" + Convert.ToDateTime(txtrentaldatein.Text);
                                    }
                                    catch (Exception ex)
                                    {
                                        setstring = setstring + ",COM_DATE_IN=" + string.Empty;
                                    }
                                }
                                if (txtrentaldateout.Text == string.Empty)
                                {
                                    setstring = setstring + ",COM_DATE_OUT=" + string.Empty;
                                }
                                else
                                {
                                    try
                                    {
                                        setstring = setstring + ",COM_DATE_OUT=" + Convert.ToDateTime(txtrentaldateout.Text);
                                    }
                                    catch (Exception ex)
                                    {
                                        setstring = setstring + ",COM_DATE_OUT=" + string.Empty;
                                    }
                                }
                                setstring=setstring+",COM_ADDRESS=" + txtrentaladdress.Text +
                               ",COM_PHONE_NUMBER=" + txtrentalphnumber.Text +
                               ",COM_REASON=" + txtrentalreason.Text +
                               ",SUIT_ID=" + drplocal.SelectedValue +
                               ",LOCAL_START_DATE=" + Convert.ToDateTime(txtlocalstartdate.Text) +
                               ",LOCAL_DEPOSITE_LEFT=" + txtlocaldeposite.Text +
                               ",LOCAL_DURATION=" + txtlocalduration.Text +
                               ",LOCAL_RENTAL_AMOUNT=" + txtlocalamount.Text+
                               ",OPTIONS=" + txtoptions.Text +
                               ",ANNUAL_PER=" + txtannualper.Text +
                               ",IS_CURRENT=" + drpcurrent.SelectedValue.ToString()+
                               ",RENTAL_INCREASE="+txtrentalincrease.Text;
            string sql = "UPDATE TENANTS SET " + setstring;
            try
            {
                _con.Open();
                SqlCommand com = new SqlCommand(sql, _con);
                com.ExecuteNonQuery();
                _con.Close();
                Response.Redirect("../Buildings/locals.aspx?lid=" + localid + "&bid=" + buildingid + "&action=view");
            }
            catch (Exception exe)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(" + exe.Message + ")", true);
            }
        }

        public void ViewData()
        {
            txtname1.ReadOnly = true;
            txtphnum1.ReadOnly = true;
            txtcellnum1.ReadOnly = true;
            txtemail1.ReadOnly = true;
            txtaltemail1.ReadOnly = true;
            txtsocial1.ReadOnly = true;
            txtaddress1.ReadOnly = true;
            txtduration1.ReadOnly = true;
            txtrental1.ReadOnly = true;
            txtdob1.ReadOnly = true;
            txtpurpose1.ReadOnly = true;
            txtname2.ReadOnly = true;
            txtphnum2.ReadOnly = true;
            txtcellnum2.ReadOnly = true;
            txtemail2.ReadOnly = true;
            txtaltemail2.ReadOnly = true;
            txtsocial2.ReadOnly = true;
            txtaddress2.ReadOnly = true;
            txtduration2.ReadOnly = true;
            txtrental2.ReadOnly = true;
            txtdob2.ReadOnly = true;
            txtpurpose2.ReadOnly = true;
            txtbankname.ReadOnly = true;
            txtbankaddress.ReadOnly = true;
            txtbankph.ReadOnly = true;
            txtaccountnumber.ReadOnly = true;
            txtcompanyname.ReadOnly = true;
            txtempaddress.ReadOnly = true;
            txtcontactname.ReadOnly = true;
            txtempphnumber.ReadOnly = true;
            txtlandlordname.ReadOnly = true;
            txtrentaldatein.ReadOnly = true;
            txtrentaldateout.ReadOnly = true;
            txtrentaladdress.ReadOnly = true;
            txtrentalphnumber.ReadOnly = true;
            txtrentalreason.ReadOnly = true;
            drplocal.Enabled = false;
            txtlocalstartdate.ReadOnly = true;
            txtlocaldeposite.ReadOnly = true;
            txtlocalduration.ReadOnly = true;
            txtlocalamount.ReadOnly = true;
            txtoptions.ReadOnly = true;
            txtannualper.ReadOnly = true;
            txtrentalincrease.ReadOnly = true;
            drpcurrent.Enabled = false;
        }

        public void getdata()
        {
            SqlConnection _con = new SqlConnection(con);
            DataTable dt = new DataTable();
            string sql = "SELECT * FROM TENANTS WHERE BUILDING_ID='" + drpbuilding.SelectedValue.ToString() + "' AND LOCAL_ID='" + localid + "' AND SUIT_ID='" + suit_id + "' AND IS_CURRENT='Yes'";
            try
            {
                _con.Open();
                SqlCommand com = new SqlCommand(sql, _con);
                dt.Load(com.ExecuteReader());
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        txtname1.Text = dr["Tenant1_name"].ToString();
                        txtphnum1.Text = dr["Tenant1_phone_number"].ToString();
                        txtcellnum1.Text = dr["Tenant1_cell_number"].ToString();
                        txtemail1.Text = dr["Tenant1_email"].ToString();
                        txtaltemail1.Text = dr["Tenant1_alt_email"].ToString();
                        txtsocial1.Text = dr["Tenant1_sin"].ToString();
                        txtaddress1.Text = dr["Tenant1_home_address"].ToString();
                        txtduration1.Text = dr["Tenant1_time_at_address"].ToString();
                        txtrental1.Text = dr["Tenant1_rental_amount"].ToString();
                        txtdob1.Text = dr["Tenant1_date_of_birth"].ToString();
                        txtpurpose1.Text = dr["Tenant1_reason"].ToString();
                        txtname2.Text = dr["Tenant2_name"].ToString();
                        txtphnum2.Text = dr["Tenant2_phone_number"].ToString();
                        txtcellnum2.Text = dr["Tenant2_cell_number"].ToString();
                        txtemail2.Text = dr["Tenant2_email"].ToString();
                        txtaltemail2.Text = dr["Tenant2_alt_email"].ToString();
                        txtsocial2.Text = dr["Tenant2_sin"].ToString();
                        txtaddress2.Text = dr["Tenant2_home_address"].ToString();
                        txtduration2.Text = dr["Tenant2_time_at_address"].ToString();
                        txtrental2.Text = dr["Tenant2_rental_amount"].ToString();
                        txtdob2.Text = dr["Tenant2_date_of_birth"].ToString();
                        txtpurpose2.Text = dr["Tenant2_reason"].ToString();
                        txtbankname.Text = dr["Bank_name"].ToString();
                        txtbankaddress.Text = dr["Bank_address"].ToString();
                        txtbankph.Text = dr["Bank_contact_number"].ToString();
                        txtaccountnumber.Text = dr["Bank_account_number"].ToString();
                        txtcompanyname.Text = dr["Emp_company_name"].ToString();
                        txtempaddress.Text = dr["Emp_address"].ToString();
                        txtcontactname.Text = dr["Emp_contact_name"].ToString();
                        txtempphnumber.Text = dr["Emp_phone_number"].ToString();
                        txtlandlordname.Text = dr["Com_landlord_name"].ToString();
                        txtrentaldatein.Text = dr["Com_date_in"].ToString();
                        txtrentaldateout.Text = dr["Com_date_out"].ToString();
                        txtrentaladdress.Text = dr["Com_address"].ToString();
                        txtrentalphnumber.Text = dr["Com_phone_number"].ToString();
                        txtrentalreason.Text = dr["Com_reason"].ToString();
                        drplocal.Enabled = false;
                        drplocal.SelectedValue = dr["SUIT_ID"].ToString(); ;
                        txtlocalstartdate.Text = dr["Local_start_date"].ToString();
                        txtlocaldeposite.Text = dr["Local_deposite_left"].ToString();
                        txtlocalduration.Text = dr["Local_duration"].ToString();
                        txtlocalamount.Text = dr["Local_rental_amount"].ToString();
                        txtoptions.Text = dr["Options"].ToString();
                        txtannualper.Text = dr["Annual_per"].ToString();
                        txtrentalincrease.Text = dr["RENTAL_INCREASE"].ToString();
                        drpcurrent.Enabled = false;
                        drpcurrent.SelectedValue = dr["IS_CURRENT"].ToString();
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

        public void ChangeData()
        {
            txtname1.ReadOnly = false;
            txtphnum1.ReadOnly = false;
            txtcellnum1.ReadOnly = false;
            txtemail1.ReadOnly = false;
            txtaltemail1.ReadOnly = false;
            txtsocial1.ReadOnly = false;
            txtaddress1.ReadOnly = false;
            txtduration1.ReadOnly = false;
            txtrental1.ReadOnly = false;
            txtdob1.ReadOnly = false;
            txtpurpose1.ReadOnly = false;
            txtname2.ReadOnly = false;
            txtphnum2.ReadOnly = false;
            txtcellnum2.ReadOnly = false;
            txtemail2.ReadOnly = false;
            txtaltemail2.ReadOnly = false;
            txtsocial2.ReadOnly = false;
            txtaddress2.ReadOnly = false;
            txtduration2.ReadOnly = false;
            txtrental2.ReadOnly = false;
            txtdob2.ReadOnly = false;
            txtpurpose2.ReadOnly = false;
            txtbankname.ReadOnly = false;
            txtbankaddress.ReadOnly = false;
            txtbankph.ReadOnly = false;
            txtaccountnumber.ReadOnly = false;
            txtcompanyname.ReadOnly = false;
            txtempaddress.ReadOnly = false;
            txtcontactname.ReadOnly = false;
            txtempphnumber.ReadOnly = false;
            txtlandlordname.ReadOnly = false;
            txtrentaldatein.ReadOnly = false;
            txtrentaldateout.ReadOnly = false;
            txtrentaladdress.ReadOnly = false;
            txtrentalphnumber.ReadOnly = false;
            txtrentalreason.ReadOnly = false;
            drplocal.Enabled = true;
            txtlocalstartdate.ReadOnly = false;
            txtlocaldeposite.ReadOnly = false;
            txtlocalduration.ReadOnly = false;
            txtlocalamount.ReadOnly = false;
            txtoptions.ReadOnly = false;
            txtannualper.ReadOnly = false;
            txtrentalincrease.ReadOnly = false;
            drpcurrent.Enabled = true;
        }

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            AddData();
            //Response.Redirect("GeneratePDF.aspx?bid="+buildingid+"&lid="+localid+"&sid="+drplocal.SelectedValue);
            //Response.Redirect("GeneratePDF.aspx?bid=" + buildingid + "&lid=" + localid + "&sid=4");
        }

        protected void btnback_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Buildings/locals.aspx?lid=" + localid + "&bid=" + buildingid + "&action=view");
        }

        protected void btnupdate_Click(object sender, EventArgs e)
        {
            EditData();
        }

        //protected void btnedittax_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("AssignSuites.aspx?bid=" + buildingid + "&lid=" + localid + "&action=edit");
        //}

        protected void btnedit_Click(object sender, EventArgs e)
        {

        }

        protected void btndelete_Click(object sender, EventArgs e)
        {
            SqlConnection _con = new SqlConnection(con);
            string update = "UPDATE TENANTS SET IS_CURRENT='No' WHERE TENANT_ID='"+tenantid+"'";
            try
            {
                _con.Open();
                SqlCommand com = new SqlCommand(update, _con);
                com.ExecuteNonQuery();
                _con.Close();
                Response.Redirect("../Buildings/Building.aspx?id=" + buildingid + "&action=view");
            }
            catch (Exception exe)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(" + exe.Message + ")", true);
            }
        }

        protected void btnupload_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Lease/UploadLease.aspx?bid=" + buildingid + "&lid=" + localid + "&sid="+suit_id+"&tid="+tenantid+"&action=add");
        }

        protected void btnlaese_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Buildings/GeneratePDF.aspx?bid=" + buildingid + "&lid=" + localid + "&sid=" + suit_id);
        }
    }
}