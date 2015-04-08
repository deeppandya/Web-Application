using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JRBuilding.Pages.Lease
{
    public partial class UploadLease : System.Web.UI.Page
    {
        string localid;
        string buildingid,tenantid,suitid;
        string con = ConfigurationManager.ConnectionStrings["JRBConnectionString"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            localid = Request.QueryString["lid"];
            buildingid = Request.QueryString["bid"];
            suitid = Request.QueryString["sid"];
            tenantid = Request.QueryString["tid"];

            if (!IsPostBack)
            {
                populate_dropdown();
                populate_local_dropdown();
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
            string sql = "SELECT * FROM LOCALS WHERE LOCAL_ID='"+localid+"'";
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

        //public void populate_Type_dropdown()
        //{
            
        //        drptenant.Items.Insert(0, new ListItem("-- choose one--", "0"));
        //        drptenant.Items.Insert(1, new ListItem("Lease", "1"));
        //        drptenant.Items.Insert(2, new ListItem("Rental Cheques", "2"));
        //}

        protected void drpdocumenttype_SelectedIndexChanged(object sender, EventArgs e)
        {
            drptenant.Visible = false;
            if(drpdocumenttype.SelectedValue.ToString()=="2")
            {
                //lbldate.Visible = true;
                //txtdate.Visible = true;
                lblrentcomment.Visible = true;
                txtrentcomment.Visible = true;
                lblchequenum.Visible = true;
                txtchequenum.Visible = true;
                lblamount.Visible = true;
                txtamount.Visible = true;
                lblchktime.Visible = true;
                drpchktime.Visible = true;
                //drpsuites.Items.Clear();
                //drpsuites.Visible = true;
                //populate_Tenant_dropdown("Rental Cheques");
                lblpdf.Visible = true;
                pdfupload.Visible = true;
                //lblsuites.Visible = true;
                populate_chk_time();
            }
            else if (drpdocumenttype.SelectedValue.ToString()=="1")
            {
                //lbldate.Visible = false;
                //txtdate.Visible = false;
                lblrentcomment.Visible = false;
                txtrentcomment.Visible = false;
                lblchequenum.Visible = false;
                txtchequenum.Visible = false;
                lblamount.Visible = false;
                txtamount.Visible = false;
                lblchktime.Visible = false;
                drpchktime.Visible = false;
                //drpsuites.Items.Clear();
                //drpsuites.Visible = true;
                //populate_Tenant_dropdown("Lease");
                lblpdf.Visible = true;
                pdfupload.Visible = true;
                //lblsuites.Visible = true;
            }
            else if (drpdocumenttype.SelectedValue.ToString() == "0")
            {
                //lbldate.Visible = false;
                //txtdate.Visible = false;
                lblrentcomment.Visible = false;
                txtrentcomment.Visible = false;
                lblchequenum.Visible = false;
                txtchequenum.Visible = false;
                //drpsuites.Items.Clear();
                //drpsuites.Visible = false;
                lblpdf.Visible = false;
                pdfupload.Visible = false;
                //lblsuites.Visible = false;
                //lbltenant.Visible = false;
            }
        }
        public void populate_Tenant_dropdown(string type)
        {
            if (type == "Lease")
            {
                SqlConnection _con = new SqlConnection(con);
                DataTable dt = new DataTable();
                string sql = "SELECT * FROM SUITS WHERE SUIT_ID NOT IN ( SELECT SUIT_ID FROM UPLOAD_LEASE WHERE BUILDING_ID='" + buildingid + "' AND LOCAL_ID='" + localid + "') AND SUIT_ID IN (SELECT SUIT_ID FROM TENANTS WHERE BUILDING_ID='" + buildingid + "' AND LOCAL_ID='" + localid + "')";
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
                        drpsuites.Items.Insert(i, new ListItem(dr["Suit_Name"].ToString(), dr["Suit_Id"].ToString()));
                        i++;
                    }
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
                string sql = "SELECT * FROM SUITS WHERE BUILDING_ID='" + buildingid + "' AND LOCAL_ID='" + localid + "' AND SUIT_ID IN (SELECT SUIT_ID FROM TENANTS WHERE BUILDING_ID='" + buildingid + "' AND LOCAL_ID='" + localid + "')";
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
                        drpsuites.Items.Insert(i, new ListItem(dr["Suit_Name"].ToString(), dr["Suit_Id"].ToString()));
                        i++;
                    }
                }
                catch (Exception exe)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(" + exe.Message + ")", true);
                }
            }
        }

        protected void drpsuites_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbltenant.Visible = true;
            drptenant.Visible = true;
            SqlConnection _con = new SqlConnection(con);
            DataTable dt = new DataTable();

            string sql = "SELECT T.Tenant_id,T.Tenant1_name FROM TENANTS2 T,SUITS S WHERE T.BUILDING_ID='" + buildingid + "' AND T.LOCAL_ID='" + localid + "' AND S.SUIT_ID='" + drpsuites.SelectedValue + "' AND T.SUIT_ID=S.SUIT_ID";
            try
            {
                _con.Open();
                SqlCommand com = new SqlCommand(sql, _con);
                dt.Load(com.ExecuteReader());
                drptenant.DataSource = dt; // <-- Get your data from somewhere.
                drptenant.DataValueField = "Tenant_id";
                drptenant.DataTextField = "Tenant1_name";
                drptenant.DataBind();
                drptenant.Enabled = false;
            }
            catch (Exception exe)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(" + exe.Message + ")", true);
            }
        }

        public void populate_chk_time()
        {
            try
            {
                DateTime now = DateTime.Now;
                drpchktime.Items.Insert(0, new ListItem("-- choose one--", "0"));
                for (int i = 1; i < 13; i++)
                {
                    //Response.Write(now.ToString("MMM")+"-"+now.ToString("yy"));
                    drpchktime.Items.Insert(i, new ListItem(now.ToString("MMM") + "-" + now.ToString("yy"),i.ToString()));
                    now = now.AddMonths(1);
                }
            }
            catch (Exception exe)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(" + exe.Message + ")", true);
            }
        }

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            string fileExt = System.IO.Path.GetExtension(pdfupload.FileName).ToString().ToLower();
            string fileName = suitid + "_" + tenantid + "_" + DateTime.Now.ToString("yyyymmddMMss") + "_" + fileExt;
            if (pdfupload.HasFile)
            {
                if (fileExt == ".pdf")
                {

                    Stream fs = pdfupload.PostedFile.InputStream;
                    BinaryReader br = new BinaryReader(fs);
                    Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                    if(drpdocumenttype.SelectedValue.ToString()=="1")
                    {
                        try
                        {
                            SqlConnection _con = new SqlConnection(con);
                            string sql = "INSERT INTO LEASE VALUES ('" + drpbuilding.SelectedValue + "','" + drplocal.SelectedValue + "','" + tenantid + "',@Data,'" + suitid + "','"+tenantid+"_Lease.pdf')";
                            _con.Open();
                            SqlCommand com = new SqlCommand(sql, _con);
                            com.Parameters.Add("@Data", SqlDbType.Binary).Value = bytes;
                            com.ExecuteNonQuery();
                            _con.Close();
                            //pdfupload.SaveAs(Server.MapPath(@"~\Pages\Lease\Documents\" + fileName));
                            Response.Redirect("../Buildings/AssignSuites.aspx?bid=" + drpbuilding.SelectedValue.ToString() + "&lid=" + drplocal.SelectedValue.ToString() + "&sid=" + suitid + "&tid=" + tenantid + "&action=view");

                        }
                        catch (Exception exe)
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(" + exe.Message + ")", true);
                        }
                    }
                    else if (drpdocumenttype.SelectedValue.ToString() == "2")
                    {
                        try
                        {
                            SqlConnection _con = new SqlConnection(con);
                            string sql = "INSERT INTO RENT VALUES ('" + drpbuilding.SelectedValue + "','" + drplocal.SelectedValue + "','" + tenantid + "','" + suitid + "','" + txtchequenum.Text + "','" + drpchktime.SelectedItem.ToString() + "','" + txtrentcomment.Text + "',@Data,'" + txtamount.Text + "','" + drpchktime.SelectedItem.ToString() + "_rent.pdf')";
                            _con.Open();
                            SqlCommand com = new SqlCommand(sql, _con);
                            com.Parameters.Add("@Data", SqlDbType.Binary).Value = bytes;
                            com.ExecuteNonQuery();
                            _con.Close();
                            //pdfupload.SaveAs(Server.MapPath(@"~\Pages\Lease\Rent_Cheques\" + fileName));
                            Response.Redirect("../Buildings/AssignSuites.aspx?bid=" + drpbuilding.SelectedValue.ToString() + "&lid=" + drplocal.SelectedValue.ToString() + "&sid=" + suitid + "&tid=" + tenantid + "&action=view");

                        }
                        catch (Exception exe)
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(" + exe.Message + ")", true);
                        }
                    }
                    
                    
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('File cannot be uploaded plaese try againg with .pdf file')", true);
                }
            }
        }

        protected void btnback_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Buildings/AssignSuites.aspx?bid=" + drpbuilding.SelectedValue.ToString() + "&lid=" + drplocal.SelectedValue.ToString() + "&sid=" + suitid + "&tid=" + tenantid + "&action=view");
        }
    }
}