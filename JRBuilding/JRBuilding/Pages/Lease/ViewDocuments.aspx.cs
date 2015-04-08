using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml;

namespace JRBuilding.Pages.Lease
{
    public partial class ViewDocuments : System.Web.UI.Page
    {
        string con = ConfigurationManager.ConnectionStrings["JRBConnectionString"].ToString();
        DataTable rents, lease;
        string data;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                /*data = "<asp:TreeView ID='TreeView1' runat='server' ShowLines='True'>" +
                                "<Nodes>";
                DataTable bdt = GetBuildingData();
                if (bdt.Rows.Count > 0)
                {
                    foreach (DataRow brw in bdt.Rows)
                    {
                        data = data + "<asp:TreeNode Text='" + brw["BUILDING_NAME"].ToString() + "'>";
                        DataTable ldt = GetLocalData(brw["BUILDING_ID"].ToString());
                        if (ldt.Rows.Count > 0)
                        {
                            foreach (DataRow lrw in ldt.Rows)
                            {
                                data = data + "<asp:TreeNode Text='" + lrw["LOCAL_ADDRESS"].ToString() + "'>";
                                DataTable sdt = GetSuiteData(brw["BUILDING_ID"].ToString(), lrw["LOCAL_ID"].ToString());
                                if (sdt.Rows.Count > 0)
                                {
                                    foreach (DataRow srw in sdt.Rows)
                                    {
                                        data = data + "<asp:TreeNode Text='" + srw["SUIT_NAME"].ToString() + "'>";
                                        DataTable leasedt = GetLeaseData(brw["BUILDING_ID"].ToString(), lrw["LOCAL_ID"].ToString(), srw["SUIT_ID"].ToString());
                                        if (leasedt.Rows.Count > 0)
                                        {
                                            foreach (DataRow leaserw in leasedt.Rows)
                                            {
                                                data = data + "<asp:TreeNode Text='" + leaserw["LEASE_NAME"].ToString() + "'>";
                                                data = data + " </asp:TreeNode>";
                                            }
                                        }
                                        DataTable rentdt = GetRentData(brw["BUILDING_ID"].ToString(), lrw["LOCAL_ID"].ToString(), srw["SUIT_ID"].ToString());
                                        if (rentdt.Rows.Count > 0)
                                        {
                                            foreach (DataRow rentrw in rentdt.Rows)
                                            {
                                                data = data + "<asp:TreeNode Text='" + rentrw["CHEQUE_NAME"].ToString() + "'>";
                                                data = data + " </asp:TreeNode>";
                                            }
                                        }
                                        data = data + " </asp:TreeNode>";
                                    }
                                }
                                data = data + " </asp:TreeNode>";
                            }
                        }
                        data = data + " </asp:TreeNode>";
                    }
                }
                data = data + "</Nodes>" +
                          "</asp:TreeView>";*/
                //divcontent.InnerHtml=data;
                //GenerateXml();
                InitilizeTree();
            }            
        }

        public void InitilizeTree()
        {
            TreeView1.Nodes.Clear();
            DataTable bdt = GetBuildingData();
            if (bdt.Rows.Count > 0)
            {
                foreach (DataRow brw in bdt.Rows)
                {
                    TreeNode building = new TreeNode(brw["BUILDING_NAME"].ToString());
                    DataTable ldt = GetLocalData(brw["BUILDING_ID"].ToString());
                    if (ldt.Rows.Count > 0)
                    {
                        foreach (DataRow lrw in ldt.Rows)
                        {
                            //data = data + "<asp:TreeNode Text='" + lrw["LOCAL_ADDRESS"].ToString() + "'>";
                            TreeNode localnode = new TreeNode(lrw["LOCAL_ADDRESS"].ToString());
                            DataTable sdt = GetSuiteData(brw["BUILDING_ID"].ToString(), lrw["LOCAL_ID"].ToString());
                            if (sdt.Rows.Count > 0)
                            {
                                foreach (DataRow srw in sdt.Rows)
                                {
                                    //data = data + "<asp:TreeNode Text='" + srw["SUIT_NAME"].ToString() + "'>";
                                    TreeNode suitnode = new TreeNode(srw["SUIT_NAME"].ToString());
                                    DataTable leasedt = GetLeaseData(brw["BUILDING_ID"].ToString(), lrw["LOCAL_ID"].ToString(), srw["SUIT_ID"].ToString());
                                    if (leasedt.Rows.Count > 0)
                                    {
                                        TreeNode lnode = new TreeNode("Lease");
                                        foreach (DataRow leaserw in leasedt.Rows)
                                        {
                                            //data = data + "<asp:TreeNode Text='" + leaserw["LEASE_NAME"].ToString() + "'>";
                                            //data = data + " </asp:TreeNode>";
                                            TreeNode leasenode = new TreeNode(leaserw["LEASE_NAME"].ToString());
                                            leasenode.Value = "Lease#"+leaserw["TENANT_LEASE_ID"].ToString();
                                            lnode.ChildNodes.Add(leasenode);
                                        }
                                        suitnode.ChildNodes.Add(lnode);
                                    }
                                    DataTable rentdt = GetRentData(brw["BUILDING_ID"].ToString(), lrw["LOCAL_ID"].ToString(), srw["SUIT_ID"].ToString());
                                    if (rentdt.Rows.Count > 0)
                                    {
                                        TreeNode rnode = new TreeNode("Rental Cheques");
                                        foreach (DataRow rentrw in rentdt.Rows)
                                        {
                                            //data = data + "<asp:TreeNode Text='" + rentrw["CHEQUE_NAME"].ToString() + "'>";
                                            //data = data + " </asp:TreeNode>";
                                            TreeNode rentnode = new TreeNode(rentrw["CHEQUE_NAME"].ToString());
                                            rentnode.Value = "Rent#"+rentrw["CHEQUE_ID"].ToString();
                                            rnode.ChildNodes.Add(rentnode);
                                        }
                                        suitnode.ChildNodes.Add(rnode);
                                    }
                                    localnode.ChildNodes.Add(suitnode);
                                }
                            }
                            //data = data + " </asp:TreeNode>";
                            building.ChildNodes.Add(localnode);
                        }
                    }
                    //data = data + " </asp:TreeNode>";
                    TreeView1.Nodes.Add(building);
                }
            }
        }

        public void GenerateXml()
        {
            DataTable bdt = GetBuildingData();
            int i=1;
            int j=1;
            int k = 1;
            int l = 1;
            int m = 1;
            using (XmlWriter writer = XmlWriter.Create("C:\\Users\\Deep\\Desktop\\data.xml"))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("Data");
                if (bdt.Rows.Count > 0)
                {
                    foreach (DataRow brw in bdt.Rows)
                    {
                        writer.WriteStartElement("Building");
                        writer.WriteElementString("BUILDINGID", brw["BUILDING_NAME"].ToString());
                        DataTable ldt = GetLocalData(brw["BUILDING_ID"].ToString());
                        if (ldt.Rows.Count > 0)
                        {
                            foreach (DataRow lrw in ldt.Rows)
                            {
                                writer.WriteStartElement("Civic");
                                writer.WriteElementString("LOCALID", lrw["LOCAL_ADDRESS"].ToString());
                                DataTable sdt = GetSuiteData(brw["BUILDING_ID"].ToString(), lrw["LOCAL_ID"].ToString());
                                if (sdt.Rows.Count > 0)
                                {
                                    foreach (DataRow srw in sdt.Rows)
                                    {
                                        writer.WriteStartElement("Suit");
                                        writer.WriteAttributeString("SUITNAME", srw["SUIT_NAME"].ToString());
                                        //writer.WriteElementString("SUITNAME", srw["SUIT_NAME"].ToString());
                                        DataTable leasedt = GetLeaseData(brw["BUILDING_ID"].ToString(), lrw["LOCAL_ID"].ToString(), srw["SUIT_ID"].ToString());
                                        if (leasedt.Rows.Count > 0)
                                        {
                                            foreach (DataRow leaserw in leasedt.Rows)
                                            {
                                                writer.WriteStartElement("Lease");
                                                writer.WriteElementString("LeaseId", leaserw["LEASE_NAME"].ToString());
                                                writer.WriteEndElement();
                                                
                                            }
                                        }
                                        DataTable rentdt = GetRentData(brw["BUILDING_ID"].ToString(), lrw["LOCAL_ID"].ToString(), srw["SUIT_ID"].ToString());
                                        if (rentdt.Rows.Count > 0)
                                        {
                                            foreach (DataRow rentrw in rentdt.Rows)
                                            {
                                                writer.WriteStartElement("Rent");
                                                writer.WriteElementString("RentId", rentrw["CHEQUE_NAME"].ToString());
                                                writer.WriteEndElement();
                                                
                                            }
                                        }
                                        writer.WriteEndElement();
                                        
                                    }
                                }
                                writer.WriteEndElement();
                                
                            }
                        }
                        writer.WriteEndElement();
                        
                    }
                }
                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }

        public DataTable GetBuildingData()
        {
            SqlConnection _con = new SqlConnection(con);
            DataTable dt = new DataTable();
            string sql = "SELECT BUILDING_NAME,BUILDING_ID FROM BUILDINGS ORDER BY BUILDING_ID";
            try
            {
                _con.Open();
                SqlCommand com = new SqlCommand(sql, _con);
                dt.Load(com.ExecuteReader());
            }
            catch (Exception exe)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(" + exe.Message + ")", true);
                dt = null;
            }

            return dt;
        }

        public DataTable GetLocalData(string building_id)
        {
            SqlConnection _con = new SqlConnection(con);
            DataTable dt = new DataTable();
            string sql = "SELECT LOCAL_ADDRESS,LOCAL_ID FROM LOCALS WHERE BUILDING_ID='" + building_id + "'";
            try
            {
                _con.Open();
                SqlCommand com = new SqlCommand(sql, _con);
                dt.Load(com.ExecuteReader());
            }
            catch (Exception exe)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(" + exe.Message + ")", true);
                dt = null;
            }

            return dt;
        }

        public DataTable GetSuiteData(string building_id,string local_id)
        {
            SqlConnection _con = new SqlConnection(con);
            DataTable dt = new DataTable();
            string sql = "SELECT SUIT_NAME,SUIT_ID FROM SUITS WHERE BUILDING_ID='" + building_id + "' AND LOCAL_ID='"+local_id+"'";
            try
            {
                _con.Open();
                SqlCommand com = new SqlCommand(sql, _con);
                dt.Load(com.ExecuteReader());
            }
            catch (Exception exe)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(" + exe.Message + ")", true);
                dt = null;
            }

            return dt;
        }

        public DataTable GetLeaseData(string building_id,string local_id,string suit_id)
        {
            SqlConnection _con = new SqlConnection(con);
            DataTable dt = new DataTable();
            string sql = "SELECT LEASE_NAME,TENANT_LEASE_ID FROM LEASE WHERE BUILDING_ID='"+building_id+"' AND LOCAL_ID='"+local_id+"' AND SUIT_ID='"+suit_id+"'";
            try
            {
                _con.Open();
                SqlCommand com = new SqlCommand(sql, _con);
                dt.Load(com.ExecuteReader());
            }
            catch (Exception exe)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(" + exe.Message + ")", true);
                dt = null;
            }

            return dt;
        }

        public DataTable GetRentData(string building_id,string local_id,string suit_id)
        {
            SqlConnection _con = new SqlConnection(con);
            DataTable dt = new DataTable();
            string sql = "SELECT CHEQUE_NAME,CHEQUE_ID FROM RENT WHERE BUILDING_ID='"+building_id+"' AND LOCAL_ID='"+local_id+"' AND SUIT_ID='"+suit_id+"'";
            try
            {
                _con.Open();
                SqlCommand com = new SqlCommand(sql, _con);
                dt.Load(com.ExecuteReader());
            }
            catch (Exception exe)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(" + exe.Message + ")", true);
                dt = null;
            }

            return dt;
        }

        protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
        {
            String str = TreeView1.SelectedNode.Value;
            if(str.Contains("#"))
            {
                String[] words = str.Split('#');
                Response.Redirect("display.aspx?action=" + words[0] + "&id=" + words[1]);
            }
        }

    }
}