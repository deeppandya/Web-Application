using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JRBuilding.Pages.Buildings
{
    public partial class GeneratePDF : System.Web.UI.Page
    {
        string con = ConfigurationManager.ConnectionStrings["JRBConnectionString"].ToString();
        string buildingid, localid, suitid;
        public static Boolean isenglish = false;
        static string _leasedata=string.Empty;
        static string _scheduleb = string.Empty;
        static string _schedulec = string.Empty;
        static string owner = string.Empty;
        static string owneraddress = string.Empty;
        static string tenant1_name = string.Empty;
        static string tenant_address = string.Empty;
        static string bname = string.Empty;
        static string suitname = string.Empty;
        static string buildingname = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            buildingid = Request.QueryString["bid"];
            localid = Request.QueryString["lid"];
            suitid = Request.QueryString["sid"];
        }
        void DisplayData(string _version)
        {
            SqlConnection _con = new SqlConnection(con);
            string sql;
            
            if(_version=="French")
            {
                sql = "SELECT Lease_Data_French,Schedule_B_French,Schedule_C_French FROM LEASE_DATA";
                DataTable dt = new DataTable();
                
                try
                {
                    _con.Open();
                    SqlCommand com = new SqlCommand(sql, _con);
                    dt.Load(com.ExecuteReader());
                    foreach (DataRow rw in dt.Rows)
                    {
                        _leasedata = rw["LEASE_DATA_FRENCH"].ToString();
                        _scheduleb = rw["SCHEDULE_B_FRENCH"].ToString();
                        _schedulec = rw["SCHEDULE_C_FRENCH"].ToString();
                    }
                    _con.Close();
                }
                catch (Exception exe)
                {

                }
            }
            else if(_version=="English")
            {
                sql = "SELECT Lease_Data_English,Schedule_B_English,Schedule_C_English FROM LEASE_DATA";
                DataTable dt = new DataTable();
                try
                {
                    _con.Open();
                    SqlCommand com = new SqlCommand(sql, _con);
                    dt.Load(com.ExecuteReader());
                    foreach (DataRow rw in dt.Rows)
                    {
                        _leasedata = rw["LEASE_DATA_ENGLISH"].ToString();
                        _scheduleb = rw["SCHEDULE_B_ENGLISH"].ToString();
                        _schedulec = rw["SCHEDULE_C_ENGLISH"].ToString();
                    }
                    _con.Close();
                }
                catch (Exception exe)
                {

                }
            }
            GetTenantData();
        }
        public void GetTenantData()
        {
            SqlConnection _con = new SqlConnection(con);
            string sql;
            sql = "SELECT T.COM_LANDLORD_NAME,T.COM_ADDRESS,T.TENANT1_NAME,T.TENANT1_HOME_ADDRESS,T.TENANT1_REASON,S.SUIT_NAME,B.BUILDING_NAME,T.LOCAL_DURATION,T.LOCAL_START_DATE,T.LOCAL_RENTAL_AMOUNT FROM TENANTS T,BUILDINGS B,SUITS S WHERE T.BUILDING_ID="+buildingid+" AND T.LOCAL_ID="+localid+" AND T.SUIT_ID="+suitid+" AND T.SUIT_ID=S.SUIT_ID AND T.BUILDING_ID=B.BUILDING_ID";
            DataTable dt = new DataTable();
            try
            {
                _con.Open();
                SqlCommand com = new SqlCommand(sql, _con);
                dt.Load(com.ExecuteReader());
                foreach (DataRow rw in dt.Rows)
                {
                    _leasedata = _leasedata.Replace("#Owner#", rw["COM_LANDLORD_NAME"].ToString());
                    owner=rw["COM_LANDLORD_NAME"].ToString();
                    _leasedata = _leasedata.Replace("#AddressOwner#", rw["COM_ADDRESS"].ToString());
                    owneraddress=rw["COM_ADDRESS"].ToString();
                    _leasedata = _leasedata.Replace("#Tenant1#", rw["TENANT1_NAME"].ToString());
                    tenant1_name=rw["TENANT1_NAME"].ToString();
                    _leasedata = _leasedata.Replace("#Address1#", rw["TENANT1_HOME_ADDRESS"].ToString());
                    tenant_address=rw["TENANT1_HOME_ADDRESS"].ToString();
                    _leasedata = _leasedata.Replace("#BName#", rw["TENANT1_REASON"].ToString());
                    bname=rw["TENANT1_REASON"].ToString();
                    _leasedata = _leasedata.Replace("#SuitNum#", rw["SUIT_NAME"].ToString());
                    suitname=rw["SUIT_NAME"].ToString();
                    _leasedata = _leasedata.Replace("#Building#", rw["BUILDING_NAME"].ToString());
                    buildingname=rw["BUILDING_NAME"].ToString();
                    _leasedata = _leasedata.Replace("#BuildingAddress#", rw["COM_ADDRESS"].ToString());
                    _leasedata = _leasedata.Replace("#LeasePeriod#", rw["LOCAL_DURATION"].ToString());
                    _leasedata = _leasedata.Replace("#StartDate#", rw["LOCAL_START_DATE"].ToString());
                    _leasedata = _leasedata.Replace("#Rental#", rw["LOCAL_RENTAL_AMOUNT"].ToString());    
                }
                txtleasedata.Text = _leasedata;
                txtscheduleb.Text = _scheduleb;
                txtschedulec.Text = _schedulec;

                _con.Close();
            }
            catch (Exception exe)
            {

            }
        }

        protected void btnenglish_Click(object sender, EventArgs e)
        {
            DisplayData("English");
            pnldata.Visible = true;
            isenglish = true;
        }

        protected void btnfrench_Click(object sender, EventArgs e)
        {
            DisplayData("French");
            pnldata.Visible = true;
            isenglish = false;
        }

        public void Generate_PDF_English()
        {
            Document document = new Document(PageSize.LEGAL, 20f, 20f, 10f, 10f);
            using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
            {
                PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
                Phrase phrase = null;
                PdfPCell cell = null;
                PdfPTable table = null;
                Color color = Color.Empty;

                document.Open();

                //Header Table
                table = new PdfPTable(1);
                table.TotalWidth = 800f;
                table.LockedWidth = true;
                
                //Company Name and Address
                phrase = new Phrase();
                phrase.Add(new Chunk("Commercial Lease\n\n\n", FontFactory.GetFont("Calibri", 36, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER);
                cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                table.AddCell(cell);

                //Separater Line
                //color = new Color(System.Drawing.ColorTranslator.FromHtml("#A9A9A9"));
                //DrawLine(writer, 25f, document.Top - 79f, document.PageSize.Width - 25f, document.Top - 79f, color);
                //DrawLine(writer, 25f, document.Top - 80f, document.PageSize.Width - 25f, document.Top - 80f, color);
                document.Add(table);

                table = new PdfPTable(2);
                //table.HorizontalAlignment = Element.ALIGN_LEFT;
                //table.SetWidths(new float[] { 0.3f, 1f });
                table.SpacingBefore = 40f;

                
                phrase = new Phrase();
                cell = PhraseCell(phrase, PdfPCell.ALIGN_JUSTIFIED);
                table.AddCell(cell);

                //Name
                phrase = new Phrase();
                phrase.Add(new Chunk("BETWEEN : \n", FontFactory.GetFont("Calibri", 12, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                phrase.Add(new Chunk("Jordan Rosenzweig, having a place of business at 720 Décarie, suite 235, Boulevard, City of Saint-Laurent, Quebec H4L 3L5.\n\n", FontFactory.GetFont("Calibri", 12, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                //phrase.Add(new Chunk(owner+", having a place of \n", FontFactory.GetFont("Calibri", 12, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                //phrase.Add(new Chunk("business at "+buildingname+", suite "+suitname+", \n", FontFactory.GetFont("Calibri", 12, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                //phrase.Add(new Chunk(owneraddress+"  \n", FontFactory.GetFont("Calibri", 12, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                //phrase.Add(new Chunk("H4L 3L5.  \n", FontFactory.GetFont("Calibri", 12, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                phrase.Add(new Chunk("(Hereinafter called the “Landlord”)  \n", FontFactory.GetFont("Calibri", 12, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                cell = PhraseCell(phrase, PdfPCell.ALIGN_JUSTIFIED);
                //cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
                table.AddCell(cell);



                document.Add(table);

                table = new PdfPTable(2);
                //table.HorizontalAlignment = Element.ALIGN_LEFT;
                //table.SetWidths(new float[] { 0.3f, 1f });
                table.SpacingBefore = 50f;


                phrase = new Phrase();
                phrase.Add(new Chunk("AND : \n", FontFactory.GetFont("Calibri", 12, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                cell = PhraseCell(phrase, PdfPCell.ALIGN_JUSTIFIED);
                cell.VerticalAlignment = PdfPCell.ALIGN_LEFT;
                table.AddCell(cell);

                //Name
                phrase = new Phrase();
                phrase.Add(new Chunk(tenant1_name+"\n\n"+tenant_address+"\n", FontFactory.GetFont("Calibri", 12, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                phrase.Add(new Chunk("carrying on business under the trade name "+bname+"\n\n(Hereinafter called the “Tenant”)", FontFactory.GetFont("Calibri", 12, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                cell = PhraseCell(phrase, PdfPCell.ALIGN_JUSTIFIED);
                //cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
                cell.VerticalAlignment = PdfPCell.ALIGN_RIGHT;
                table.AddCell(cell);
                document.Add(table);

                table = new PdfPTable(2);
                //table.HorizontalAlignment = Element.ALIGN_LEFT;
                //table.SetWidths(new float[] { 0.3f, 1f });
                table.SpacingBefore = 50f;


                phrase = new Phrase();
                phrase.Add(new Chunk("AND : \n", FontFactory.GetFont("Calibri", 12, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                cell = PhraseCell(phrase, PdfPCell.ALIGN_JUSTIFIED);
                cell.VerticalAlignment = PdfPCell.ALIGN_LEFT;
                table.AddCell(cell);

                //Name
                phrase = new Phrase();
                phrase.Add(new Chunk("Guarantor:\n\n________________________________\n\nAddress:\n\n________________________________\n\n________________________________\n\n________________________________\n\n", FontFactory.GetFont("Calibri", 12, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                phrase.Add(new Chunk("(Hereinafter selectively and solidarity called the “Guarantor”)", FontFactory.GetFont("Calibri", 12, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                cell = PhraseCell(phrase, PdfPCell.ALIGN_JUSTIFIED);
                cell.VerticalAlignment = PdfPCell.ALIGN_RIGHT;
                //cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
                table.AddCell(cell);
                document.Add(table);

                document.NewPage();

                

                table = new PdfPTable(1);
                //table.HorizontalAlignment = Element.ALIGN_LEFT;
                //table.SetWidths(new float[] { 0.3f, 1f });
                table.SpacingBefore = 50f;


                phrase = new Phrase();
                phrase.Add(new Chunk("LEASE dated the____ day of _________, ______\n\n", FontFactory.GetFont("Calibri", 12, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                cell = PhraseCell(phrase, PdfPCell.ALIGN_JUSTIFIED);
                table.AddCell(cell);


                document.Add(table);

                //table = new PdfPTable(1);
                //table.SpacingBefore = 50f;


                //phrase = new Phrase();
                //phrase.Add(new Chunk("(1)  NET LEASE\n\n", FontFactory.GetFont("Calibri", 12, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                //phrase.Add(new Chunk("This Lease is intended by the parties to be an absolutely net lease to Landlord, except as otherwise expressly provided herein.  Any amount and any obligation, which is not expressly declared herein to be that of Landlord, shall be deemed to be an obligation of Tenant to be performed and paid for by Tenant.", FontFactory.GetFont("Calibri", 12, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                //cell = PhraseCell(phrase, PdfPCell.ALIGN_JUSTIFIED);
                //table.AddCell(cell);

                //document.Add(table);

                //table = new PdfPTable(1);
                //table.SpacingBefore = 30f;


                //phrase = new Phrase();
                //phrase.Add(new Chunk("(2)  LEASED PREMISES\n\n", FontFactory.GetFont("Calibri", 12, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                //phrase.Add(new Chunk("The Landlord hereby leases to the Tenant those certain premises bearing number 207, (the “Leased Premises”) in the building located at 997 Décarie Boulevard in the City of Saint Laurent, province of Québec, Canada (the “Building”). The Tenant acknowledges having seen and examined the said Leased Premises and declares being satisfied therewith.", FontFactory.GetFont("Calibri", 12, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                //cell = PhraseCell(phrase, PdfPCell.ALIGN_JUSTIFIED);
                //table.AddCell(cell);

                //document.Add(table);

                //table = new PdfPTable(1);
                //table.SpacingBefore = 30f;


                //phrase = new Phrase();
                //phrase.Add(new Chunk("(3)  TERM\n\n", FontFactory.GetFont("Calibri", 12, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                //phrase.Add(new Chunk("The present Lease is made for a term of three (3) years, which term shall commence on July 1, 2014  (the “Lease Commencement Date”) and expire on June 30, 2017  (the “Term”).", FontFactory.GetFont("Calibri", 12, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                //cell = PhraseCell(phrase, PdfPCell.ALIGN_JUSTIFIED);
                //table.AddCell(cell);

                //document.Add(table);

                table = new PdfPTable(1);
                table.SpacingBefore = 30f;

                phrase = new Phrase();
                phrase.Add(new Chunk(txtleasedata.Text, FontFactory.GetFont("Calibri", 12, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                cell = PhraseCell(phrase, PdfPCell.ALIGN_JUSTIFIED);
                table.AddCell(cell);

                document.Add(table);             

                document.NewPage();

                table = new PdfPTable(1);
                table.SpacingBefore = 30f;
                phrase = new Phrase();
                phrase.Add(new Chunk("IN WITNESS WHEREOF, THE PARTIES HAVE EXECUTED THIS AGREEMENT, AT THE PLACE AND ON THE DATE FIRST ABOVE WRITTEN.\n\n", FontFactory.GetFont("Calibri", 12, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                cell = PhraseCell(phrase, PdfPCell.ALIGN_JUSTIFIED);
                table.AddCell(cell);

                document.Add(table);


                table = new PdfPTable(1);
                table.SpacingBefore = 30f;
                phrase = new Phrase();
                phrase.Add(new Chunk("SIGNED by Tenant as of the     _______    day of    ________ 2014\n\n", FontFactory.GetFont("Calibri", 12, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                cell = PhraseCell(phrase, PdfPCell.ALIGN_JUSTIFIED);
                cell.VerticalAlignment = PdfPCell.ALIGN_LEFT;
                table.AddCell(cell);

                document.Add(table);

                table = new PdfPTable(2);
                table.SpacingBefore = 30f;

                phrase = new Phrase();
                cell = PhraseCell(phrase, PdfPCell.ALIGN_JUSTIFIED);
                cell.VerticalAlignment = PdfPCell.ALIGN_LEFT;
                table.AddCell(cell);

                phrase = new Phrase();
                phrase.Add(new Chunk("Tenant\n\n"+"per: \n _______________________________\n\n"+"Name: Romcan Electronics\n\n"+"Title:\n\n"+"I have the authority to bind the corporation.", FontFactory.GetFont("Calibri", 12, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfPCell.ALIGN_RIGHT;
                table.AddCell(cell);

                document.Add(table);

                table = new PdfPTable(1);
                table.SpacingBefore = 30f;
                phrase = new Phrase();
                phrase.Add(new Chunk("SIGNED by Landlord as of the     _______    day of    ________ 2014\n\n", FontFactory.GetFont("Calibri", 12, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                cell = PhraseCell(phrase, PdfPCell.ALIGN_JUSTIFIED);
                cell.VerticalAlignment = PdfPCell.ALIGN_LEFT;
                table.AddCell(cell);

                document.Add(table);

                table = new PdfPTable(2);
                table.SpacingBefore = 30f;

                phrase = new Phrase();
                cell = PhraseCell(phrase, PdfPCell.ALIGN_JUSTIFIED);
                cell.VerticalAlignment = PdfPCell.ALIGN_LEFT;
                table.AddCell(cell);

                phrase = new Phrase();
                phrase.Add(new Chunk("Landlord\n\n" + "per: \n _______________________________\n\n" + "Name: Romcan Electronics\n\n" + "Title:\n\n" + "I have the authority to bind the corporation.", FontFactory.GetFont("Calibri", 12, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfPCell.ALIGN_RIGHT;
                table.AddCell(cell);

                document.Add(table);

                table = new PdfPTable(1);
                table.SpacingBefore = 30f;
                phrase = new Phrase();
                phrase.Add(new Chunk("SIGNED by Guarantor as of the     _______    day of    ________ 2014\n\n", FontFactory.GetFont("Calibri", 12, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                cell = PhraseCell(phrase, PdfPCell.ALIGN_JUSTIFIED);
                cell.VerticalAlignment = PdfPCell.ALIGN_LEFT;
                table.AddCell(cell);

                document.Add(table);

                table = new PdfPTable(2);
                table.SpacingBefore = 30f;

                phrase = new Phrase();
                cell = PhraseCell(phrase, PdfPCell.ALIGN_JUSTIFIED);
                cell.VerticalAlignment = PdfPCell.ALIGN_LEFT;
                table.AddCell(cell);

                phrase = new Phrase();
                phrase.Add(new Chunk("Guarantor\n\n" + "per: \n _______________________________\n\n" + "Name: Romcan Electronics\n\n" + "Title:\n\n" + "I have the authority to bind the corporation.\n\n\n", FontFactory.GetFont("Calibri", 12, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                phrase.Add(new Chunk("per: \n _______________________________\n\n" + "Name: Romcan Electronics\n\n" + "Title:\n\n" + "I have the authority to bind the corporation.", FontFactory.GetFont("Calibri", 12, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfPCell.ALIGN_RIGHT;
                table.AddCell(cell);

                document.Add(table);

                document.NewPage();

                table = new PdfPTable(1);
                table.SpacingBefore = 30f;
                phrase = new Phrase();
                phrase.Add(new Chunk("SCHEDULE \"A\"\n\n", FontFactory.GetFont("Calibri", 14, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                phrase.Add(new Chunk("Operations expenditures and property taxes as additional rental in addition to the base rental\nPeriod from JULY 1 2014 TO JUNE 30 2015\n\n\n\n\n     	GST #	123754269RT\n			QST #	1012427740-TQ0001\n\n\nTenant:		Romcan Electronics	\n\nPremises:	997 Decarie #207, St.-Laurent, Quebec H4L 3M7\n\n\n\n\n\n\n", FontFactory.GetFont("Calibri", 14, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                string startdate, enddate, gst_num, qst_num,gst_per,qst_per;
                double total = 0;
                SqlConnection _con2 = new SqlConnection(con);
                string sql;
                sql = "SELECT * FROM TAXES WHERE BUILDING_ID=" + buildingid;
                DataTable dt = new DataTable();
                try
                {
                    _con2.Open();
                    SqlCommand com = new SqlCommand(sql, _con2);
                    dt.Load(com.ExecuteReader());
                    foreach (DataRow rw in dt.Rows)
                    {
                        startdate = rw["FROM_DATE"].ToString();
                        enddate = rw["TO_DATE"].ToString();
                        gst_num = rw["GST_NUM"].ToString();
                        qst_num = rw["QST_NUM"].ToString();
                        gst_per = rw["GST_PER"].ToString();
                        qst_per = rw["QST_PER"].ToString();
                        for(int i=1;i<16;i++)
                        {
                            if (rw["TAX_" + i + "_VALUE"].ToString()!=string.Empty)
                            {
                                phrase.Add(new Chunk(rw["TAX_" + i + "_TEXT"].ToString() + ":			$" + rw["TAX_" + i + "_VALUE"].ToString() + "\n\n", FontFactory.GetFont("Calibri", 14, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                                total = total + Convert.ToDouble(rw["TAX_" + i + "_VALUE"].ToString());
                            }
                            
                        }
                    }
                    _con2.Close();
                }
                catch (Exception exe)
                {

                }
                phrase.Add(new Chunk(" \n\n\n			Total:	$"+total+" \n\n\n\nProportionate share:	incl.		\n\nAnnual quota:0% of	$"+total+" 		$0.00 \n\n\n\n\n\n\nBase rent as per lease:		$463.50 \n\n\nAdditional monthly rental 	$0.00 	 	$0.00 \n\n			Sub-total:	$463.50 \n\n			GST 5%	$23.18 \n\n			QST 9.975%	$46.23 \n\n			Total:	$532.91  \n\n", FontFactory.GetFont("Calibri", 14, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER);
                table.AddCell(cell);
                document.Add(table);

                document.NewPage();

                table = new PdfPTable(1);
                table.SpacingBefore = 30f;
                phrase = new Phrase();
                phrase.Add(new Chunk("SCHEDULE \"B\"\n\n", FontFactory.GetFont("Calibri", 14, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                phrase.Add(new Chunk("DEFINITIONS \n\n", FontFactory.GetFont("Calibri", 14, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER);
                table.AddCell(cell);
                document.Add(table);

                table = new PdfPTable(1);
                table.SpacingBefore = 30f;
                phrase = new Phrase();
                phrase.Add(new Chunk(txtscheduleb.Text+"\n\n", FontFactory.GetFont("Calibri", 12, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                cell = PhraseCell(phrase, PdfPCell.ALIGN_JUSTIFIED);
                table.AddCell(cell);
                document.Add(table);

                document.NewPage();

                table = new PdfPTable(1);
                table.SpacingBefore = 30f;
                phrase = new Phrase();
                phrase.Add(new Chunk("SCHEDULE \"C\"\n\n", FontFactory.GetFont("Calibri", 14, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                phrase.Add(new Chunk("GUARANTEE \n\n", FontFactory.GetFont("Calibri", 14, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER);
                table.AddCell(cell);
                document.Add(table);

                table = new PdfPTable(1);
                table.SpacingBefore = 30f;
                phrase = new Phrase();
                phrase.Add(new Chunk("LANDLORD    :    _____________________\n\nTENANT      :    _____________________\n\n GUARANTOR   :    _____________________ \n\n\n", FontFactory.GetFont("Calibri", 12, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                phrase.Add(new Chunk(txtschedulec.Text+"\n\n", FontFactory.GetFont("Calibri", 12, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                cell = PhraseCell(phrase, PdfPCell.ALIGN_JUSTIFIED);
                table.AddCell(cell);
                document.Add(table);

                table = new PdfPTable(1);
                table.SpacingBefore = 30f;
                phrase = new Phrase();
                phrase.Add(new Chunk("SIGNED BY THE GUARANTOR AT ____________________, THIS______ DAY OF ______________, 2014.\n\n", FontFactory.GetFont("Calibri", 12, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                cell = PhraseCell(phrase, PdfPCell.ALIGN_JUSTIFIED);
                cell.VerticalAlignment = PdfPCell.ALIGN_LEFT;
                table.AddCell(cell);

                document.Add(table);

                table = new PdfPTable(2);
                table.SpacingBefore = 30f;

                phrase = new Phrase();
                phrase.Add(new Chunk("________________________________\n\nGuarantor’s Name\n\n________________________________\n\nDate of Birth		\n\n________________________________\n\nSocial Insurance Number\n\n________________________________\n\nWitness		\n\n", FontFactory.GetFont("Calibri", 12, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                cell = PhraseCell(phrase, PdfPCell.ALIGN_JUSTIFIED);
                cell.VerticalAlignment = PdfPCell.ALIGN_LEFT;
                table.AddCell(cell);

                phrase = new Phrase();
                cell = PhraseCell(phrase, PdfPCell.ALIGN_JUSTIFIED);
                cell.VerticalAlignment = PdfPCell.ALIGN_RIGHT;
                table.AddCell(cell);

                document.Add(table);

                document.NewPage();

                table = new PdfPTable(2);
                table.SpacingBefore = 30f;

                phrase = new Phrase();
                phrase.Add(new Chunk("\n\n\n________________________________\n\nGuarantor’s Name\n\n________________________________\n\nDate of Birth		\n\n________________________________\n\nSocial Insurance Number\n\n________________________________\n\nWitness\n\n", FontFactory.GetFont("Calibri", 12, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                cell = PhraseCell(phrase, PdfPCell.ALIGN_JUSTIFIED);
                cell.VerticalAlignment = PdfPCell.ALIGN_LEFT;
                table.AddCell(cell);

                phrase = new Phrase();
                cell = PhraseCell(phrase, PdfPCell.ALIGN_JUSTIFIED);
                cell.VerticalAlignment = PdfPCell.ALIGN_RIGHT;
                table.AddCell(cell);

                document.Add(table);

                document.Close();
                byte[] bytes = memoryStream.ToArray();
                memoryStream.Close();
                Response.Clear();
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition", "attachment; filename=Lease(English).pdf");
                Response.ContentType = "application/pdf";
                Response.Buffer = true;
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.BinaryWrite(bytes);
                Response.End();
                Response.Close();
            }
        }

        public void Generate_PDF_French()
        {
            Document document = new Document(PageSize.LEGAL, 20f, 20f, 10f, 10f);
            using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
            {
                PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
                Phrase phrase = null;
                PdfPCell cell = null;
                PdfPTable table = null;
                Color color = Color.Empty;

                document.Open();

                //Header Table
                table = new PdfPTable(1);
                table.TotalWidth = 800f;
                table.LockedWidth = true;

                //Company Name and Address
                phrase = new Phrase();
                phrase.Add(new Chunk("Bail Commercial\n\n\n", FontFactory.GetFont("Calibri", 36, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER);
                cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                table.AddCell(cell);

                //Separater Line
                //color = new Color(System.Drawing.ColorTranslator.FromHtml("#A9A9A9"));
                //DrawLine(writer, 25f, document.Top - 79f, document.PageSize.Width - 25f, document.Top - 79f, color);
                //DrawLine(writer, 25f, document.Top - 80f, document.PageSize.Width - 25f, document.Top - 80f, color);
                document.Add(table);

                table = new PdfPTable(2);
                //table.HorizontalAlignment = Element.ALIGN_LEFT;
                //table.SetWidths(new float[] { 0.3f, 1f });
                table.SpacingBefore = 40f;


                phrase = new Phrase();
                cell = PhraseCell(phrase, PdfPCell.ALIGN_JUSTIFIED);
                table.AddCell(cell);

                //Name
                phrase = new Phrase();
                phrase.Add(new Chunk("ENTRE : \n", FontFactory.GetFont("Calibri", 12, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                phrase.Add(new Chunk("Jordan Rosenzweig, ayant une place d'affaires au 720 Boulevard Décarie, suite 235, Ville Saint-Laurent, Québec H4L 3L5.\n\n", FontFactory.GetFont("Calibri", 12, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                //phrase.Add(new Chunk(owner + ", having a place of \n", FontFactory.GetFont("Calibri", 12, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                //phrase.Add(new Chunk("business at " + buildingname + ", suite " + suitname + ", \n", FontFactory.GetFont("Calibri", 12, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                //phrase.Add(new Chunk(owneraddress + "  \n", FontFactory.GetFont("Calibri", 12, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                //phrase.Add(new Chunk("H4L 3L5.  \n", FontFactory.GetFont("Calibri", 12, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                phrase.Add(new Chunk("(Ci-après nommé le «Propriétaire »)  \n", FontFactory.GetFont("Calibri", 12, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                cell = PhraseCell(phrase, PdfPCell.ALIGN_JUSTIFIED);
                //cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
                table.AddCell(cell);



                document.Add(table);

                table = new PdfPTable(2);
                //table.HorizontalAlignment = Element.ALIGN_LEFT;
                //table.SetWidths(new float[] { 0.3f, 1f });
                table.SpacingBefore = 50f;


                phrase = new Phrase();
                phrase.Add(new Chunk("ET : \n", FontFactory.GetFont("Calibri", 12, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                cell = PhraseCell(phrase, PdfPCell.ALIGN_JUSTIFIED);
                cell.VerticalAlignment = PdfPCell.ALIGN_LEFT;
                table.AddCell(cell);

                //Name
                phrase = new Phrase();
                phrase.Add(new Chunk(tenant1_name + "\n" + tenant_address + "\n\n", FontFactory.GetFont("Calibri", 12, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                phrase.Add(new Chunk("(Ci-après nommé(s) le “Locataire”)", FontFactory.GetFont("Calibri", 12, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                cell = PhraseCell(phrase, PdfPCell.ALIGN_JUSTIFIED);
                //cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
                cell.VerticalAlignment = PdfPCell.ALIGN_RIGHT;
                table.AddCell(cell);
                document.Add(table);

                table = new PdfPTable(2);
                //table.HorizontalAlignment = Element.ALIGN_LEFT;
                //table.SetWidths(new float[] { 0.3f, 1f });
                table.SpacingBefore = 50f;


                phrase = new Phrase();
                phrase.Add(new Chunk("ET : \n", FontFactory.GetFont("Calibri", 12, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                cell = PhraseCell(phrase, PdfPCell.ALIGN_JUSTIFIED);
                cell.VerticalAlignment = PdfPCell.ALIGN_LEFT;
                table.AddCell(cell);

                //Name
                phrase = new Phrase();
                phrase.Add(new Chunk("Caution:\n\n________________________________\n\nAdresse:\n\n________________________________\n\n________________________________\n\n________________________________\n\n", FontFactory.GetFont("Calibri", 12, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                phrase.Add(new Chunk("(Ci-après nommé collectivement et solidairement la “Caution”)", FontFactory.GetFont("Calibri", 12, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                cell = PhraseCell(phrase, PdfPCell.ALIGN_JUSTIFIED);
                cell.VerticalAlignment = PdfPCell.ALIGN_RIGHT;
                //cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
                table.AddCell(cell);
                document.Add(table);

                document.NewPage();



                table = new PdfPTable(1);
                //table.HorizontalAlignment = Element.ALIGN_LEFT;
                //table.SetWidths(new float[] { 0.3f, 1f });
                table.SpacingBefore = 50f;


                phrase = new Phrase();
                phrase.Add(new Chunk("BAIL daté ce____ jour de _________, ______\n\n", FontFactory.GetFont("Calibri", 12, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                cell = PhraseCell(phrase, PdfPCell.ALIGN_JUSTIFIED);
                table.AddCell(cell);


                document.Add(table);

                //table = new PdfPTable(1);
                //table.SpacingBefore = 50f;


                //phrase = new Phrase();
                //phrase.Add(new Chunk("(1)  NET LEASE\n\n", FontFactory.GetFont("Calibri", 12, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                //phrase.Add(new Chunk("This Lease is intended by the parties to be an absolutely net lease to Landlord, except as otherwise expressly provided herein.  Any amount and any obligation, which is not expressly declared herein to be that of Landlord, shall be deemed to be an obligation of Tenant to be performed and paid for by Tenant.", FontFactory.GetFont("Calibri", 12, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                //cell = PhraseCell(phrase, PdfPCell.ALIGN_JUSTIFIED);
                //table.AddCell(cell);

                //document.Add(table);

                //table = new PdfPTable(1);
                //table.SpacingBefore = 30f;


                //phrase = new Phrase();
                //phrase.Add(new Chunk("(2)  LEASED PREMISES\n\n", FontFactory.GetFont("Calibri", 12, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                //phrase.Add(new Chunk("The Landlord hereby leases to the Tenant those certain premises bearing number 207, (the “Leased Premises”) in the building located at 997 Décarie Boulevard in the City of Saint Laurent, province of Québec, Canada (the “Building”). The Tenant acknowledges having seen and examined the said Leased Premises and declares being satisfied therewith.", FontFactory.GetFont("Calibri", 12, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                //cell = PhraseCell(phrase, PdfPCell.ALIGN_JUSTIFIED);
                //table.AddCell(cell);

                //document.Add(table);

                //table = new PdfPTable(1);
                //table.SpacingBefore = 30f;


                //phrase = new Phrase();
                //phrase.Add(new Chunk("(3)  TERM\n\n", FontFactory.GetFont("Calibri", 12, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                //phrase.Add(new Chunk("The present Lease is made for a term of three (3) years, which term shall commence on July 1, 2014  (the “Lease Commencement Date”) and expire on June 30, 2017  (the “Term”).", FontFactory.GetFont("Calibri", 12, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                //cell = PhraseCell(phrase, PdfPCell.ALIGN_JUSTIFIED);
                //table.AddCell(cell);

                //document.Add(table);

                table = new PdfPTable(1);
                table.SpacingBefore = 30f;

                phrase = new Phrase();
                phrase.Add(new Chunk(txtleasedata.Text, FontFactory.GetFont("Calibri", 12, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                cell = PhraseCell(phrase, PdfPCell.ALIGN_JUSTIFIED);
                table.AddCell(cell);

                document.Add(table);



                document.NewPage();

                table = new PdfPTable(1);
                table.SpacingBefore = 30f;
                phrase = new Phrase();
                phrase.Add(new Chunk("EN FOI DE QUOI, LES PARTIES ONT SIGNÉ LE PRÉSENT ACCORD À L'ENDROIT ET À LA DATE SUSMENTIONNÉE,\n\n", FontFactory.GetFont("Calibri", 12, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                cell = PhraseCell(phrase, PdfPCell.ALIGN_JUSTIFIED);
                table.AddCell(cell);

                document.Add(table);


                table = new PdfPTable(1);
                table.SpacingBefore = 30f;
                phrase = new Phrase();
                phrase.Add(new Chunk("SIGNÉ par le Locataire ce     _______     jour du mois de    ________ 2014\n\n", FontFactory.GetFont("Calibri", 12, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                cell = PhraseCell(phrase, PdfPCell.ALIGN_JUSTIFIED);
                cell.VerticalAlignment = PdfPCell.ALIGN_LEFT;
                table.AddCell(cell);

                document.Add(table);

                table = new PdfPTable(2);
                table.SpacingBefore = 30f;

                phrase = new Phrase();
                cell = PhraseCell(phrase, PdfPCell.ALIGN_JUSTIFIED);
                cell.VerticalAlignment = PdfPCell.ALIGN_LEFT;
                table.AddCell(cell);

                phrase = new Phrase();
                phrase.Add(new Chunk("Locataire\n\n" + "per: \n _______________________________\n\n" + "Name: Romcan Electronics\n\n" + "Titre:\n\n" + "Nous avons l'autorité nécessaire pour lier la société ou la compagnie. ", FontFactory.GetFont("Calibri", 12, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfPCell.ALIGN_RIGHT;
                table.AddCell(cell);

                document.Add(table);

                table = new PdfPTable(1);
                table.SpacingBefore = 30f;
                phrase = new Phrase();
                phrase.Add(new Chunk("SIGNÉ par le Propriétaire  ce     _______     jour du mois de    ________ 2014\n\n", FontFactory.GetFont("Calibri", 12, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                cell = PhraseCell(phrase, PdfPCell.ALIGN_JUSTIFIED);
                cell.VerticalAlignment = PdfPCell.ALIGN_LEFT;
                table.AddCell(cell);

                document.Add(table);

                table = new PdfPTable(2);
                table.SpacingBefore = 30f;

                phrase = new Phrase();
                cell = PhraseCell(phrase, PdfPCell.ALIGN_JUSTIFIED);
                cell.VerticalAlignment = PdfPCell.ALIGN_LEFT;
                table.AddCell(cell);

                phrase = new Phrase();
                phrase.Add(new Chunk("Propriétaire\n\n" + "per: \n _______________________________\n\n" + "Name: Romcan Electronics\n\n" + "Titre:\n\n" + "Nous avons l'autorité nécessaire pour lier la société ou la compagnie. ", FontFactory.GetFont("Calibri", 12, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfPCell.ALIGN_RIGHT;
                table.AddCell(cell);

                document.Add(table);

                table = new PdfPTable(1);
                table.SpacingBefore = 30f;
                phrase = new Phrase();
                phrase.Add(new Chunk("SIGNÉ par la Caution ce     _______    jour du mois de    ________ 2014\n\n", FontFactory.GetFont("Calibri", 12, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                cell = PhraseCell(phrase, PdfPCell.ALIGN_JUSTIFIED);
                cell.VerticalAlignment = PdfPCell.ALIGN_LEFT;
                table.AddCell(cell);

                document.Add(table);

                table = new PdfPTable(2);
                table.SpacingBefore = 30f;

                phrase = new Phrase();
                cell = PhraseCell(phrase, PdfPCell.ALIGN_JUSTIFIED);
                cell.VerticalAlignment = PdfPCell.ALIGN_LEFT;
                table.AddCell(cell);

                phrase = new Phrase();
                phrase.Add(new Chunk("Caution\n\n" + "per: \n _______________________________\n\n" + "Name: Romcan Electronics\n\n" + "Titre:\n\n", FontFactory.GetFont("Calibri", 12, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                phrase.Add(new Chunk("per: \n _______________________________\n\n" + "Name: Romcan Electronics\n\n" + "Titre:\n\n", FontFactory.GetFont("Calibri", 12, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfPCell.ALIGN_RIGHT;
                table.AddCell(cell);

                document.Add(table);

                document.NewPage();

                table = new PdfPTable(1);
                table.SpacingBefore = 30f;
                phrase = new Phrase();
                phrase.Add(new Chunk("ANNEXE \"A\"\n\n", FontFactory.GetFont("Calibri", 14, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                phrase.Add(new Chunk("Operations expenditures and property taxes as additional rental in addition to the base rental\nPeriod from JULY 1 2014 TO JUNE 30 2015\n\n\n\n\n     	GST #	123754269RT\n			QST #	1012427740-TQ0001\n\n\nTenant:		Romcan Electronics	\n\nPremises:	997 Decarie #207, St.-Laurent, Quebec H4L 3M7\n\n\n\n\n\n\n", FontFactory.GetFont("Calibri", 14, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                string startdate, enddate, gst_num, qst_num, gst_per, qst_per;
                double total = 0;
                SqlConnection _con2 = new SqlConnection(con);
                string sql;
                sql = "SELECT * FROM TAXES WHERE BUILDING_ID=" + buildingid;
                DataTable dt = new DataTable();
                try
                {
                    _con2.Open();
                    SqlCommand com = new SqlCommand(sql, _con2);
                    dt.Load(com.ExecuteReader());
                    foreach (DataRow rw in dt.Rows)
                    {
                        startdate = rw["FROM_DATE"].ToString();
                        enddate = rw["TO_DATE"].ToString();
                        gst_num = rw["GST_NUM"].ToString();
                        qst_num = rw["QST_NUM"].ToString();
                        gst_per = rw["GST_PER"].ToString();
                        qst_per = rw["QST_PER"].ToString();
                        for (int i = 1; i < 16; i++)
                        {
                            if (rw["TAX_" + i + "_VALUE"].ToString() != string.Empty)
                            {
                                phrase.Add(new Chunk(rw["TAX_" + i + "_TEXT"].ToString() + ":			$" + rw["TAX_" + i + "_VALUE"].ToString() + "\n\n", FontFactory.GetFont("Calibri", 14, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                                total = total + Convert.ToDouble(rw["TAX_" + i + "_VALUE"].ToString());
                            }

                        }
                    }
                    _con2.Close();
                }
                catch (Exception exe)
                {

                }
                phrase.Add(new Chunk(" \n\n\n			Total:	$" + total + " \n\n\n\nProportionate share:	incl.		\n\nAnnual quota:0% of	$" + total + " 		$0.00 \n\n\n\n\n\n\nBase rent as per lease:		$463.50 \n\n\nAdditional monthly rental 	$0.00 	 	$0.00 \n\n			Sub-total:	$463.50 \n\n			GST 5%	$23.18 \n\n			QST 9.975%	$46.23 \n\n			Total:	$532.91  \n\n", FontFactory.GetFont("Calibri", 14, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER);
                table.AddCell(cell);
                document.Add(table);

                document.NewPage();

                table = new PdfPTable(1);
                table.SpacingBefore = 30f;
                phrase = new Phrase();
                phrase.Add(new Chunk("ANNEXE \"B\"\n\n", FontFactory.GetFont("Calibri", 14, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                phrase.Add(new Chunk("DEFINITIONS \n\n", FontFactory.GetFont("Calibri", 14, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER);
                table.AddCell(cell);
                document.Add(table);

                table = new PdfPTable(1);
                table.SpacingBefore = 30f;
                phrase = new Phrase();
                phrase.Add(new Chunk(txtscheduleb.Text + "\n\n", FontFactory.GetFont("Calibri", 12, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                cell = PhraseCell(phrase, PdfPCell.ALIGN_JUSTIFIED);
                table.AddCell(cell);
                document.Add(table);

                document.NewPage();

                table = new PdfPTable(1);
                table.SpacingBefore = 30f;
                phrase = new Phrase();
                phrase.Add(new Chunk("ANNEXE  \"C\"\n\n", FontFactory.GetFont("Calibri", 14, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                phrase.Add(new Chunk("GUARANTEE \n\n", FontFactory.GetFont("Calibri", 14, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER);
                table.AddCell(cell);
                document.Add(table);

                table = new PdfPTable(1);
                table.SpacingBefore = 30f;
                phrase = new Phrase();
                phrase.Add(new Chunk("PROPRIÉTAIRE    :    _____________________\n\nLOCATAIRE      :    _____________________\n\n CAUTION   :    _____________________ \n\n\n", FontFactory.GetFont("Calibri", 12, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                phrase.Add(new Chunk(txtschedulec.Text + "\n\n", FontFactory.GetFont("Calibri", 12, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                cell = PhraseCell(phrase, PdfPCell.ALIGN_JUSTIFIED);
                table.AddCell(cell);
                document.Add(table);

                table = new PdfPTable(1);
                table.SpacingBefore = 30f;
                phrase = new Phrase();
                phrase.Add(new Chunk("SIGNÉ PAR LA CAUTION À ____________________, CE______ JOUR DU MOIS DE  ______________, 20___.\n\n", FontFactory.GetFont("Calibri", 12, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                cell = PhraseCell(phrase, PdfPCell.ALIGN_JUSTIFIED);
                cell.VerticalAlignment = PdfPCell.ALIGN_LEFT;
                table.AddCell(cell);

                document.Add(table);

                table = new PdfPTable(2);
                table.SpacingBefore = 30f;

                phrase = new Phrase();
                phrase.Add(new Chunk("________________________________\n\nNom de Caution\n\n________________________________\n\nDate de naissance 		\n\n________________________________\n\nNuméro d’assurance sociale\n\n________________________________\n\nTémoin		\n\n", FontFactory.GetFont("Calibri", 12, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                cell = PhraseCell(phrase, PdfPCell.ALIGN_JUSTIFIED);
                cell.VerticalAlignment = PdfPCell.ALIGN_LEFT;
                table.AddCell(cell);

                phrase = new Phrase();
                cell = PhraseCell(phrase, PdfPCell.ALIGN_JUSTIFIED);
                cell.VerticalAlignment = PdfPCell.ALIGN_RIGHT;
                table.AddCell(cell);

                document.Add(table);

                document.NewPage();

                table = new PdfPTable(2);
                table.SpacingBefore = 30f;

                phrase = new Phrase();
                phrase.Add(new Chunk("\n\n\n________________________________\n\nNom de Caution\n\n________________________________\n\nDate de naissance 		\n\n________________________________\n\nNuméro d’assurance sociale\n\n________________________________\n\nTémoin		\n\n", FontFactory.GetFont("Calibri", 12, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                cell = PhraseCell(phrase, PdfPCell.ALIGN_JUSTIFIED);
                cell.VerticalAlignment = PdfPCell.ALIGN_LEFT;
                table.AddCell(cell);

                phrase = new Phrase();
                cell = PhraseCell(phrase, PdfPCell.ALIGN_JUSTIFIED);
                cell.VerticalAlignment = PdfPCell.ALIGN_RIGHT;
                table.AddCell(cell);

                document.Add(table);

                document.Close();
                byte[] bytes = memoryStream.ToArray();
                memoryStream.Close();
                Response.Clear();
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition", "attachment; filename=Lease(French).pdf");
                Response.ContentType = "application/pdf";
                Response.Buffer = true;
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.BinaryWrite(bytes);
                Response.End();
                Response.Close();
            }
        }

            private static PdfPCell PhraseCell(Phrase phrase, int align)
        {
            PdfPCell cell = new PdfPCell(phrase);
            cell.BorderColor = iTextSharp.text.BaseColor.WHITE;
            cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
            cell.HorizontalAlignment = align;
            cell.PaddingBottom = 2f;
            cell.PaddingTop = 0f;
            return cell;
        }

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            if(isenglish)
            {
                Generate_PDF_English();
            }
            else
            {
                Generate_PDF_French();
            }
            Response.Redirect("../Buildings/locals.aspx?lid=" + localid + "&bid=" + buildingid + "&action=view");
        }

        protected void btnback_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Buildings/AssignSuites.aspx");
        }

     }
}