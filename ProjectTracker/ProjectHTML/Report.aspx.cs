using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Web;
using CrystalDecisions.Shared;
using System.Collections.Generic;
using System.IO;
using System.Globalization;

namespace ProjectHTML
{
  public partial class Report : System.Web.UI.Page
  {
    ReportDocument _reportDocument;
    protected void Page_Load(object sender, EventArgs e)
    {
      ShowReport();
    }

    
    protected void ShowReport()
    {
      string billto=string.Empty;
      TextInfo myTI = new CultureInfo("en-US", false).TextInfo;
      ProjectService.Projecttracker _service = new ProjectService.Projecttracker();
      DataTable dtReport = _service.GetReportDS();
      dtReport.Columns.Add("HOURS",typeof(Int32));
      dtReport.Columns.Add("IMAGE", System.Type.GetType("System.Byte[]"));
      dtReport.Columns.Add("IMAGEURL", typeof(String));
      double mins = Convert.ToDouble(ConfigurationManager.AppSettings["Minutes"]);
      double weekendHours = Convert.ToDouble(ConfigurationManager.AppSettings["WeekEndHours"]);
      foreach (DataRow dr in dtReport.Rows)
      {
        double hours = 0;
        TimeSpan ts=Convert.ToDateTime(dr["END_DT"])- Convert.ToDateTime(dr["START_DT"]);
        if (ts.Days > 0)
        {
          hours = ts.Days * 24;
        }
        if (ts.Hours > 0)
        {
          hours =hours +ts.Hours;
        }
        if (ts.Minutes > 0)
        {
          if (ts.Minutes > mins)
          {
            hours = hours + 1;
          }
        }
        if (Convert.ToDateTime(dr["START_DT"]).DayOfWeek == DayOfWeek.Saturday || Convert.ToDateTime(dr["START_DT"]).DayOfWeek == DayOfWeek.Sunday)
        {
          hours = hours * weekendHours;
          dr["BUG_DESC"] = dr["BUG_DESC"].ToString() + " (WORK IN WEEKENDS)";
        }
        dr["HOURS"] = hours;
        billto = myTI.ToTitleCase(dr["BILL_TO"].ToString().ToLower()) + "\n" + myTI.ToTitleCase(dr["CONTACT_PERSON"].ToString().ToLower()) + "\n" + myTI.ToTitleCase(dr["ADDRESS"].ToString().ToLower())+"\n"+myTI.ToTitleCase(dr["CITY"].ToString().ToLower())+","+dr["STATE"].ToString()+","+dr["COUNTRY"].ToString()+" - "+myTI.ToTitleCase(dr["ZIP"].ToString().ToLower());
      }
      CrystalReportViewer1.ToolPanelView = CrystalDecisions.Web.ToolPanelViewType.None;
      _reportDocument = new ReportDocument();
      _reportDocument.Load(Server.MapPath("CrystalReport1.rpt"));
      _reportDocument.SetDataSource(dtReport);
      TextObject txtcdate = _reportDocument.ReportDefinition.ReportObjects["txtcurrentdate"] as TextObject;
      txtcdate.Text = DateTime.Today.ToString("MM/dd/yyyy");
      TextObject txtinvoice = _reportDocument.ReportDefinition.ReportObjects["txtinvoice"] as TextObject;
      txtinvoice.Text = "7201329";
      TextObject txtbillto = _reportDocument.ReportDefinition.ReportObjects["txtbillto"] as TextObject;
      txtbillto.Text = billto;
      TextObject txtduedate = _reportDocument.ReportDefinition.ReportObjects["txtduedate"] as TextObject;
      txtduedate.Text = DateTime.Today.AddDays(20).Date.ToString("MM/dd/yyyy");
      TextObject txtponumber = _reportDocument.ReportDefinition.ReportObjects["txtponumber"] as TextObject;
      txtponumber.Text = "390022";
      CrystalReportViewer1.ReportSource = _reportDocument;
    }
  }
}
