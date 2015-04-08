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

namespace JRBuilding.Pages.Buildings
{
    public partial class Building : System.Web.UI.Page
    {
        string con = ConfigurationManager.ConnectionStrings["JRBConnectionString"].ToString();
        string buildingid ;
        string action;
        string viewscreen;
        string roleid;
        protected void Page_Load(object sender, EventArgs e)
        {
            buildingid = Request.QueryString["id"];
            action = Request.QueryString["action"];
            roleid = Session["roleid"].ToString();
            viewscreen = Request.QueryString["viewscreen"];
            if(!IsPostBack)
            {
                if (action == "view" && roleid=="Admin")
                {
                    //BindData();
                    btnsubmit.Visible = false;
                    btnupdate.Visible = false;
                    btnedit.Visible = true;
                    btndelete.Visible = true;
                    txtname.ReadOnly = true;
                    txtaddress.ReadOnly = true;
                    imgupload.Visible = false;
                    headertxt.Text = "View Building";
                    lblpicture.Visible = false;
                    //GridView1.Visible = true;
                    ShowData();

                }
                else if (action == "view" && roleid != "Admin")
                {
                    //BindData();
                    btnsubmit.Visible = false;
                    btnupdate.Visible = false;
                    btnedit.Visible = false;
                    btndelete.Visible = false;
                    txtname.ReadOnly = true;
                    txtaddress.ReadOnly = true;
                    imgupload.Visible = false;
                    headertxt.Text = "View Building";
                    lblpicture.Visible = false;
                    //GridView1.Visible = true;
                    ShowData();

                }
                else if (action == "add" && roleid =="Admin")
                {
                    btnsubmit.Visible = true;
                    btnupdate.Visible = false;
                    btnaddlocals.Visible = false;
                    btnedit.Visible = false;
                    btndelete.Visible = false;
                    btntax.Visible = false;
                    txtname.ReadOnly = false;
                    txtaddress.ReadOnly = false;
                    imgupload.Visible = true;
                    headertxt.Text = "Add Building";
                    lblpicture.Visible = true;
                    GridView1.Visible = false;

                }
                else if (action == "edit" && roleid == "Admin")
                {
                    btnsubmit.Visible = false;
                    btnupdate.Visible = true;
                    btnaddlocals.Visible = false;
                    btnedit.Visible = false;
                    btndelete.Visible = false;
                    btntax.Visible = false;
                    txtname.ReadOnly = false;
                    txtaddress.ReadOnly = false;
                    imgupload.Visible = false;
                    headertxt.Text = "Update Building";
                    lblpicture.Visible = false;
                    GridView1.Visible = false;
                    ShowData();
                }
               // SetRole();
            }
        }

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            string filePath = imgupload.PostedFile.FileName;
            string filename = Path.GetFileName(filePath);
            string ext = Path.GetExtension(filename);
            string contenttype = String.Empty;

            //Set the contenttype based on File Extension
            switch (ext)
            {
                case ".jpg":
                    contenttype = "image/jpg";
                    break;
                case ".png":
                    contenttype = "image/png";
                    break;
            }
            if (contenttype != String.Empty)
            {

                Stream fs = imgupload.PostedFile.InputStream;
                BinaryReader br = new BinaryReader(fs);
                Byte[] bytes = br.ReadBytes((Int32)fs.Length);

                //insert the file into database
                string strQuery = "INSERT INTO BUILDINGS VALUES ('" + txtname.Text + "','" + txtaddress.Text + "',@Data)";
                SqlCommand cmd = new SqlCommand(strQuery);
                cmd.Parameters.Add("@Data", SqlDbType.Binary).Value = bytes;
                InsertData(cmd);
                Response.Redirect("ViewBuildings.aspx");
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Please upload building Image in .png or .jpg')", true);
            }



            //string fileExt = System.IO.Path.GetExtension(imgupload.FileName).ToString().ToLower();
            //string fileName = txtname.Text+fileExt;
            //if (buildingid == null)
            //{
            //    if (imgupload.HasFile)
            //    {
            //        if (fileExt == ".png" || fileExt == ".jpg")
            //        {
            //            try
            //            {
            //                SqlConnection _con = new SqlConnection(con);
            //                string sql = "INSERT INTO BUILDINGS VALUES ('" + txtname.Text + "','" + txtaddress.Text + "','" + fileName + "')";

            //                try
            //                {
            //                    _con.Open();
            //                    SqlCommand com = new SqlCommand(sql, _con);
            //                    com.ExecuteNonQuery();
            //                    _con.Close();

            //                    imgupload.SaveAs(Server.MapPath("~/Pages/Buildings/Buildingimg/" + fileName));
            //                    Response.Redirect("ViewBuildings.aspx");
                                
            //                }
            //                catch (Exception exe)
            //                {
            //                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(" + exe.Message + ")", true);
            //                }
            //            }
            //            catch (Exception exe)
            //            {
            //                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(" + exe.Message + ")", true);
            //            }
            //        }
            //        else
            //        {
            //            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Please upload file in .png or .jpg format')", true);
            //        }
            //    }
            //    else
            //    {
            //        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Please upload building Image in .png or .jpg')", true);
            //    }
            //}
        }

        private Boolean InsertData(SqlCommand cmd)
        {
            SqlConnection connection = new SqlConnection(con);
            cmd.CommandType = CommandType.Text;
            cmd.Connection = connection;
            try
            {
                connection.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
                return false;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
        }

        protected void btnupdate_Click(object sender, EventArgs e)
        {
            string fileExt = System.IO.Path.GetExtension(imgupload.FileName).ToString().ToLower();
            string fileName = txtname.Text + fileExt;
            if (buildingid != null)
            {
                SqlConnection _con = new SqlConnection(con);
                string sql = "UPDATE BUILDINGS SET ";
                if (txtname.Text != "")
                {
                    sql = sql + "BUILDING_NAME='" + txtname.Text + "'";
                }
                if (txtaddress.Text != "")
                {
                    sql = sql + ",BUILDING_ADDRESS='" + txtaddress.Text + "'";
                }
                if (imgupload.HasFile)
                {
                    if (fileExt == ".png" || fileExt == ".jpg")
                    {
                        sql = sql + ",IMAGE_NAME='" + fileName + "'";
                    }
                }


                sql = sql + " WHERE BUILDING_ID='" + buildingid + "'";

                try
                {
                    _con.Open();
                    SqlCommand com = new SqlCommand(sql, _con);
                    com.ExecuteNonQuery();
                    _con.Close();
                    imgupload.SaveAs(Server.MapPath(@"~\Pages\Buildings\Buildingimg\" + fileName));
                    Response.Redirect("../Buildings/Building.aspx?id=" + buildingid + "&action=view");
                }
                catch (Exception exe)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(" + exe.Message + ")", true);
                }
            }
        }

        public void ShowData()
        {
            SqlConnection _con = new SqlConnection(con);
            string sql = "SELECT * FROM BUILDINGS WHERE BUILDING_ID='" + buildingid + "'";
            DataTable dt = new DataTable();
            try
            {
                _con.Open();
                SqlCommand com = new SqlCommand(sql, _con);
                dt.Load(com.ExecuteReader());
                foreach (DataRow rw in dt.Rows)
                {
                    txtname.Text = rw["Building_Name"].ToString();
                    txtaddress.Text = rw["Building_Address"].ToString();
                }
                _con.Close();
            }
            catch (Exception exe)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(" + exe.Message + ")", true);
            }
        }

        protected void btnedit_Click(object sender, EventArgs e)
        {
            Response.Redirect("Building.aspx?action=edit&id="+buildingid);
        }

        protected void btndelete_Click(object sender, EventArgs e)
        {
            SqlConnection _con = new SqlConnection(con);
            
            string sql1 = "DELETE FROM TAXES WHERE BUILDING_ID='" + buildingid + "'";
            string sql2 = "DELETE FROM TENANTS WHERE BUILDING_ID='" + buildingid + "'";
            string sql3 = "DELETE FROM SUITS WHERE BUILDING_ID='" + buildingid + "'";
            string sql4 = "DELETE FROM LOCALS WHERE BUILDING_ID='" + buildingid + "'";
            string sql5 = "DELETE FROM BUILDINGS WHERE BUILDING_ID='" + buildingid + "'";
            
            
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
                Response.Redirect("../User/Login.aspx");
            }
            catch (Exception exe)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(" + exe.Message + ")", true);
            }
        }
        private void BindData()
        {
            string strQuery = "select * from LOCALS WHERE BUILDING_ID='"+buildingid+"'";

            SqlCommand cmd = new SqlCommand(strQuery);
            GridView1.DataSource = GetData(cmd);
            GridView1.DataBind();
        }
        protected void EditCustomer(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            BindData();
        }
        protected void CancelEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            BindData();
        }

        protected void RowDataBound(object sender, GridViewRowEventArgs e)
        {
           string strQuery = "select * from LOCALS WHERE BUILDING_ID='" + buildingid + "'";
           SqlCommand cmd = new SqlCommand(strQuery);
           DataRowView drv = e.Row.DataItem as DataRowView;
           if (e.Row.RowType == DataControlRowType.DataRow)
           {
               if ((e.Row.RowState & DataControlRowState.Edit) > 0)
               {
                   DropDownList dp = (DropDownList)e.Row.FindControl("drpissuits");
                   DataTable dt = GetData(cmd);
                   //for (int i = 0; i < dt.Rows.Count; i++)
                   //{
                   //    ListItem lt = new ListItem();
                   //    lt.Text = dt.Rows[i][0].ToString();
                   //    dp.Items.Add(lt);
                   //}
                   dp.SelectedValue = drv[3].ToString();
               }
           }
        }

        protected void UpdateCustomer(object sender, GridViewUpdateEventArgs e)
        {
            string local_id = ((Label)GridView1.Rows[e.RowIndex]
                                .FindControl("lbllocalid")).Text;
            //string Name = ((TextBox)GridView1.Rows[e.RowIndex]
            //                    .FindControl("txtlocalname")).Text;
            string address = ((TextBox)GridView1.Rows[e.RowIndex]
                                .FindControl("txtaddress")).Text;
            string building = ((TextBox)GridView1.Rows[e.RowIndex]
                                .FindControl("txtbuilding")).Text;
            
            DropDownList ddl = (DropDownList)GridView1.Rows[e.RowIndex].FindControl("drpissuits");
            string issuits = ddl.SelectedValue.ToString();
            SqlConnection conn = new SqlConnection(con);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "update locals set " +
             "local_address=@local_address,is_suits=@issuits where local_id=@local_id;" +
             "select * from LOCALS WHERE BUILDING_ID='" + buildingid + "'";
            cmd.Parameters.Add("@local_id", SqlDbType.Int).Value = local_id;
            //cmd.Parameters.Add("@local_name", SqlDbType.VarChar).Value = Name;
            cmd.Parameters.Add("@local_address", SqlDbType.VarChar).Value = address;
            cmd.Parameters.Add("@issuits", SqlDbType.VarChar).Value = issuits;
            //cmd.Parameters.Add("@building_id", SqlDbType.Int).Value = building;
            GridView1.EditIndex = -1;
            GridView1.DataSource = GetData(cmd);
            GridView1.DataBind();
        }

        protected void DeleteCustomer(object sender, EventArgs e)
        {
            LinkButton lnkRemove = (LinkButton)sender;
            SqlConnection conn = new SqlConnection(con);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "delete from  locals where " +
            "local_id=@local_id;" +
             "select * from LOCALS WHERE BUILDING_ID='" + buildingid + "'";
            cmd.Parameters.Add("@local_id", SqlDbType.Int).Value
                = lnkRemove.CommandArgument;
            GridView1.DataSource = GetData(cmd);
            GridView1.DataBind();
        }

        protected void OnPaging(object sender, GridViewPageEventArgs e)
        {
            BindData();
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataBind();
        }
        private DataTable GetData(SqlCommand cmd)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(con);
            SqlDataAdapter sda = new SqlDataAdapter();
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;
            conn.Open();
            sda.SelectCommand = cmd;
            sda.Fill(dt);
            return dt;
        }

        protected void btnaddlocals_Click(object sender, EventArgs e)
        {
            Response.Redirect("locals.aspx?bid="+buildingid+"&action=add");
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string local = (GridView1.SelectedRow.FindControl("lbllocalid") as Label).Text;
            //string building = (GridView1.SelectedRow.FindControl("lblbuilding") as Label).Text;
            Response.Redirect("locals.aspx?lid="+local+"&bid="+buildingid+"&action=view");
        }

        protected void btntax_Click(object sender, EventArgs e)
        {
            Response.Redirect("TaxandExpense.aspx?id="+buildingid+"&action=view");
        }

        protected void btnback_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Buildings/ViewBuildings.aspx");
        }

        public void SetRole()
        {
            if (Session["roleid"].ToString() == "Admin")
            {
                btnaddlocals.Visible = true;
                btndelete.Visible = true;
                btnedit.Visible = true;
                btntax.Visible = true;
                GridView1.Visible = true;
            }
            else
            {
                btnaddlocals.Visible = false;
                btndelete.Visible = false;
                btnedit.Visible = false;
                btntax.Visible = true;
                GridView1.Visible = false;
            }
            if(viewscreen=="yes")
            {
                btnaddlocals.Visible = false;
                btndelete.Visible = false;
                btnedit.Visible = false;
                btntax.Visible = true;
                GridView1.Visible = false;
                btnback.Visible = false;
                btntax.Visible = false;
            }
        }
    }
}