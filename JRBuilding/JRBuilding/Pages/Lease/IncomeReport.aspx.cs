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
    public partial class IncomeReport : System.Web.UI.Page
    {
        string con = ConfigurationManager.ConnectionStrings["JRBConnectionString"].ToString();
        string buildingid;
        protected void Page_Load(object sender, EventArgs e)
        {
            buildingid = Session["buildingid"].ToString();
            if(!IsPostBack)
            {
                Show_Data();
            }
        }
        public void Show_Data()
        {
            SqlConnection _con = new SqlConnection(con);
            DataTable dt = new DataTable();
            string sql = "SELECT B.BUILDING_NAME as BuildingName,L.LOCAL_ADDRESS as LocalAddress,S.SUIT_NAME as SuitNumber,T.TENANT1_NAME as TenantName,CONVERT(VARCHAR(10),T.LOCAL_START_DATE,111) as StartDate,CONVERT(VARCHAR(10),CONVERT(DATETIME,DATEADD(YY,CONVERT(FLOAT,T.LOCAL_DURATION),T.LOCAL_START_DATE),111),111) as EndDate,T.OPTIONS as Options,T.RENTAL_INCREASE as RentalIncrease,CONVERT(VARCHAR(10),DATEADD(month, -3, DATEADD(YY,2,T.LOCAL_START_DATE)),111) as NOTICE,T.TENANT1_RENTAL_AMOUNT as Rental,((CONVERT(FLOAT,T.ANNUAL_PER)/100)*T.TENANT1_RENTAL_AMOUNT) AS AdditionalRent FROM TENANTS T,BUILDINGS B,LOCALS L,SUITS S WHERE T.BUILDING_ID=B.BUILDING_ID AND T.LOCAL_ID=L.LOCAL_ID AND T.SUIT_ID=S.SUIT_ID AND B.BUILDING_ID='" + buildingid + "'";
            try
            {
                _con.Open();
                SqlCommand com = new SqlCommand(sql, _con);
                dt.Load(com.ExecuteReader());
                if(dt.Rows.Count>0)
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

        protected void btnexport_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=Rental Income Analysis .xls");
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