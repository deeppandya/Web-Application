using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JRBuilding.Pages.Lease
{
    public partial class ChequeReport : System.Web.UI.Page
    {

        string con = ConfigurationManager.ConnectionStrings["JRBConnectionString"].ToString();
        string buildingid;
        protected void Page_Load(object sender, EventArgs e)
        {
            buildingid = Session["buildingid"].ToString();
        }

        protected void drptype_SelectedIndexChanged(object sender, EventArgs e)
        {
            grid.Visible = false;
            btnexport.Visible = false;
            if(drptype.SelectedValue.ToString().Equals("1"))
            {
                lblselected.Text = "Duration";
                SqlConnection _con = new SqlConnection(con);
                DataTable dt = new DataTable();
                string sql = "SELECT DISTINCT R.CHEQUE_DATE FROM BUILDINGS B,LOCALS L,SUITS S,TENANTS T,RENT R WHERE B.BUILDING_ID=L.BUILDING_ID AND L.LOCAL_ID=S.LOCAL_ID AND S.SUIT_ID=T.SUIT_ID AND T.TENANT_ID=R.TENANT_ID AND B.BUILDING_ID='"+buildingid+"'";
                try
                {
                    _con.Open();
                    SqlCommand com = new SqlCommand(sql, _con);
                    dt.Load(com.ExecuteReader());
                    drpselected.Items.Clear();
                    drpselected.DataSource = dt;
                    drpselected.Items.Insert(0, new ListItem("-- choose one--", "0"));
                    int i = 1;
                    foreach (DataRow dr in dt.Rows)
                    {
                        drpselected.Items.Insert(i, new ListItem(dr["CHEQUE_DATE"].ToString(), i.ToString()));
                        i++;
                    }
                }
                catch (Exception exe)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(" + exe.Message + ")", true);
                }
            }
            else if (drptype.SelectedValue.ToString().Equals("2"))
            {
                lblselected.Text = "Tenants";
                SqlConnection _con = new SqlConnection(con);
                DataTable dt = new DataTable();
                string sql = "SELECT DISTINCT T.TENANT1_NAME,T.TENANT_ID FROM BUILDINGS B,LOCALS L,SUITS S,TENANTS T,RENT R WHERE B.BUILDING_ID=L.BUILDING_ID AND L.LOCAL_ID=S.LOCAL_ID AND S.SUIT_ID=T.SUIT_ID AND T.TENANT_ID=R.TENANT_ID AND B.BUILDING_ID='" + buildingid + "'";
                try
                {
                    _con.Open();
                    SqlCommand com = new SqlCommand(sql, _con);
                    dt.Load(com.ExecuteReader());
                    drpselected.Items.Clear();
                    drpselected.DataSource = dt;
                    drpselected.Items.Insert(0, new ListItem("-- choose one--", "0"));
                    int i = 1;
                    foreach (DataRow dr in dt.Rows)
                    {
                        drpselected.Items.Insert(i, new ListItem(dr["TENANT1_NAME"].ToString(), dr["TENANT_ID"].ToString()));
                        i++;
                    }
                }
                catch (Exception exe)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(" + exe.Message + ")", true);
                }
            }
        }

        protected void drpselected_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            Show_Data();
        }

        public void Show_Data()
        {
            grid.Visible = false;
            btnexport.Visible = false;
            if (drptype.SelectedValue.ToString().Equals("1"))
            {
                SqlConnection _con = new SqlConnection(con);
                DataTable dt = new DataTable();
                string sql = "SELECT B.BUILDING_NAME,L.LOCAL_ADDRESS,S.SUIT_NAME,T.TENANT1_NAME,R.CHEQUE_NUMBER,R.CHEQUE_DATE,R.AMOUNT FROM BUILDINGS B,LOCALS L,SUITS S,TENANTS T,RENT R WHERE B.BUILDING_ID=L.BUILDING_ID AND L.LOCAL_ID=S.LOCAL_ID AND S.SUIT_ID=T.SUIT_ID AND T.TENANT_ID=R.TENANT_ID AND R.CHEQUE_DATE='" + drpselected.SelectedItem.ToString() + "' AND B.BUILDING_ID='"+buildingid+"'";
                try
                {
                    _con.Open();
                    SqlCommand com = new SqlCommand(sql, _con);
                    dt.Load(com.ExecuteReader());
                    if (dt.Rows.Count > 0)
                    {
                        btnexport.Visible = true;
                    }
                    grid.Visible = true;
                    grid.DataSource = dt;
                    grid.DataBind();
                }
                catch (Exception exe)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(" + exe.Message + ")", true);
                }
            }
            else if (drptype.SelectedValue.ToString().Equals("2"))
            {
                lblselected.Text = "Tenants";
                SqlConnection _con = new SqlConnection(con);
                DataTable dt = new DataTable();
                string sql = "SELECT B.BUILDING_NAME,L.LOCAL_ADDRESS,S.SUIT_NAME,T.TENANT1_NAME,R.CHEQUE_NUMBER,R.CHEQUE_DATE,R.AMOUNT FROM BUILDINGS B,LOCALS L,SUITS S,TENANTS T,RENT R WHERE B.BUILDING_ID=L.BUILDING_ID AND L.LOCAL_ID=S.LOCAL_ID AND S.SUIT_ID=T.SUIT_ID AND T.TENANT_ID=R.TENANT_ID AND R.TENANT_ID='" + drpselected.SelectedValue.ToString() + "' AND B.BUILDING_ID='"+buildingid+"'";
                try
                {
                    _con.Open();
                    SqlCommand com = new SqlCommand(sql, _con);
                    dt.Load(com.ExecuteReader());
                    if (dt.Rows.Count > 0)
                    {
                        btnexport.Visible = true;
                    }
                    grid.Visible = true;
                    grid.DataSource = dt;
                    grid.DataBind();
                }
                catch (Exception exe)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(" + exe.Message + ")", true);
                }
            }
        }

        protected void btnexport_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=Post Dated Cheques Analysis .xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                grid.AllowPaging = false;
                this.Show_Data();

                grid.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in grid.HeaderRow.Cells)
                {
                    cell.BackColor = grid.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in grid.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = grid.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = grid.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                grid.RenderControl(hw);

                //style to format numbers to string
                string style = @"<style> .textmode { } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }

    }
}