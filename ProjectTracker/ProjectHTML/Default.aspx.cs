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
using System.Collections.Generic;

namespace ProjectHTML
{
  public partial class _Default : System.Web.UI.Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      
    }

    [System.Web.Services.WebMethod]
    public static string RegisterUser(string UserId, string password, string fname, string lname,string abb)
    {
      ProjectService.Projecttracker _service = new ProjectService.Projecttracker();
      bool _signup = _service.RegisterUser(UserId, password,fname,lname,abb);
      DataTable dt = new DataTable();
      dt.Columns.Add("SIGNUP", typeof(string));
      DataRow dr = dt.NewRow();
      dr["SIGNUP"] = _signup.ToString();
      dt.Rows.Add(dr);
      return GetJson(dt);
    }

    [System.Web.Services.WebMethod]
    public static string checkUser(string UserId, string password)
    {
      ProjectService.Projecttracker _service = new ProjectService.Projecttracker();
      bool _check= _service.CheckUser(UserId, password);
      DataTable dt = new DataTable();
      dt.Columns.Add("CHECK", typeof(string));
      DataRow dr = dt.NewRow();
      dr["CHECK"] = _check.ToString();
      dt.Rows.Add(dr);
      return GetJson(dt);
    }

    [System.Web.Services.WebMethod]
    public static string GetProjects(string user_id)
    {
      ProjectService.Projecttracker _service = new ProjectService.Projecttracker();
      string jsonprojects = _service.GetProjects(user_id);

      return jsonprojects;
    }

    [System.Web.Services.WebMethod]
    public static string AddProject(string client_id,string company_id,string Bugid, string Project_type_id, string bugdesc, string start_st, string end_dt, string user_id, string action_taken, string contact_person)
    {
      ProjectService.Projecttracker _service = new ProjectService.Projecttracker();
      bool _add = _service.AddProject(company_id,client_id, Bugid, Project_type_id, bugdesc, start_st, end_dt, user_id, action_taken, contact_person);
      DataTable dt = new DataTable();
      dt.Columns.Add("ADD", typeof(string));
      DataRow dr = dt.NewRow();
      dr["ADD"] = _add.ToString();
      dt.Rows.Add(dr);
      return GetJson(dt);
    }

    [System.Web.Services.WebMethod]
    public static string AddCompany(string CompanyId, string Desc,string companyno)
    {
      ProjectService.Projecttracker _service = new ProjectService.Projecttracker();
      bool _add = _service.AddCompany(CompanyId,Desc,companyno);
      DataTable dt = new DataTable();
      dt.Columns.Add("ADD", typeof(string));
      DataRow dr = dt.NewRow();
      dr["ADD"] = _add.ToString();
      dt.Rows.Add(dr);
      return GetJson(dt);
    }

    [System.Web.Services.WebMethod]
    public static string AddClient(string ClientId, string ClientName,string Charge,string billingId,string companyId)
    {
      ProjectService.Projecttracker _service = new ProjectService.Projecttracker();
      bool _add = _service.AddClient(ClientId,ClientName,Convert.ToDouble(Charge),billingId,companyId);
      DataTable dt = new DataTable();
      dt.Columns.Add("ADD", typeof(string));
      DataRow dr = dt.NewRow();
      dr["ADD"] = _add.ToString();
      dt.Rows.Add(dr);
      return GetJson(dt);
    }

    [System.Web.Services.WebMethod]
    public static string GetClient()
    {
      ProjectService.Projecttracker _service = new ProjectService.Projecttracker();
      string jsonprojects = _service.GetClient(string.Empty );

      return jsonprojects;
    }

    [System.Web.Services.WebMethod]
    public static string EditClient(string ClientId,string ClientName,string Charge,string billingId,string companyId)
    {
      ProjectService.Projecttracker _service = new ProjectService.Projecttracker();
      bool _check = _service.EditClient(ClientId, ClientName, Convert.ToDouble(Charge), billingId, companyId);
      DataTable dt = new DataTable();
      dt.Columns.Add("UPDATE", typeof(string));
      DataRow dr = dt.NewRow();
      dr["UPDATE"] = _check.ToString();
      dt.Rows.Add(dr);
      return GetJson(dt);
    }

    [System.Web.Services.WebMethod]
    public static string GetCompany()
    {
      ProjectService.Projecttracker _service = new ProjectService.Projecttracker();
      string jsonprojects = _service.GetCompany(string.Empty);

      return jsonprojects;
    }


    [System.Web.Services.WebMethod]
    public static string AddBillingDetails(string BillingId,string BillTo,string ContactPerson,string Address,string City,string State,string Country,string Zip)
    {
      ProjectService.Projecttracker _service = new ProjectService.Projecttracker();
      bool _add = _service.AddBillingDetails(BillingId, BillTo, ContactPerson, Address, City, State, Country, Zip);
      DataTable dt = new DataTable();
      dt.Columns.Add("ADD", typeof(string));
      DataRow dr = dt.NewRow();
      dr["ADD"] = _add.ToString();
      dt.Rows.Add(dr);
      return GetJson(dt);
    }

    [System.Web.Services.WebMethod]
    public static string GetBillingDetails()
    {
      ProjectService.Projecttracker _service = new ProjectService.Projecttracker();
      string jsonprojects = _service.GetBillingDetails(string.Empty);

      return jsonprojects;
    }

    [System.Web.Services.WebMethod]
    public static string EditBillingDetails(string BillingId,string BillTo,string ContactPerson,string Address,string City,string State,string Country,string Zip)
    {
      ProjectService.Projecttracker _service = new ProjectService.Projecttracker();
      bool _check = _service.EditBillingDetails(BillingId,BillTo,ContactPerson,Address,City,State,Country,Zip);
      DataTable dt = new DataTable();
      dt.Columns.Add("UPDATE", typeof(string));
      DataRow dr = dt.NewRow();
      dr["UPDATE"] = _check.ToString();
      dt.Rows.Add(dr);
      return GetJson(dt);
    }

    [System.Web.Services.WebMethod]
    public static string EditCompany(string CompanyId, string Desc,string companyno)
    {
      ProjectService.Projecttracker _service = new ProjectService.Projecttracker();
      bool _check = _service.EditCompany(CompanyId,Desc,companyno);
      DataTable dt = new DataTable();
      dt.Columns.Add("UPDATE", typeof(string));
      DataRow dr = dt.NewRow();
      dr["UPDATE"] = _check.ToString();
      dt.Rows.Add(dr);
      return GetJson(dt);
    }

    [System.Web.Services.WebMethod]
    public static string GetProjectDetail(string Bugid)
    {
      ProjectService.Projecttracker _service = new ProjectService.Projecttracker();
      string jsonprojects = _service.GetProjectDetail(Bugid);

      return jsonprojects;
    }

    [System.Web.Services.WebMethod]
    public static string UpdateProject(string client_id,string company_id, string Bugid, string Project_type_id, string bugdesc, string start_st, string end_dt,string action_taken, string contact_person)
    {
      //ProjectService.Projecttracker _service = new ProjectService.Projecttracker();
      //bool _check = _service.UpdateProject(client_id,company_id,Bugid,Project_type_id,bugdesc,start_st,end_dt,action_taken,contact_person);
      DataTable dt = new DataTable();
      //dt.Columns.Add("UPDATE", typeof(string));
      //DataRow dr = dt.NewRow();
      //dr["UPDATE"] = _check.ToString();
      //dt.Rows.Add(dr);
      return GetJson(dt);
    }

    [System.Web.Services.WebMethod]
    public static string GetSearchResult(string user_id, string fname, string lname, string bug_id)
    {
      ProjectService.Projecttracker _service = new ProjectService.Projecttracker();
      string jsonprojects = _service.GetSearchResult(user_id, fname, lname, bug_id);

      return jsonprojects;
    }
    

    [System.Web.Services.WebMethod]
    public static string GetProject_Type_Id()
    {
      ProjectService.Projecttracker _service = new ProjectService.Projecttracker();
      string jsonprojects = _service.GetProject_Type_Id();

      return jsonprojects;
    }
    [System.Web.Services.WebMethod]
    public static string getCompanyClients(string companyId)
    {
      ProjectService.Projecttracker _service = new ProjectService.Projecttracker();
      string jsonprojects = _service.GetCompanyClients(companyId ) ;

      return jsonprojects;

    }

    public static string GetJson(DataTable dt)
    {
      System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
      List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
      Dictionary<string, object> row = null;

      foreach (DataRow dr in dt.Rows)
      {
        row = new Dictionary<string, object>();
        foreach (DataColumn col in dt.Columns)
        {
          string s = dr[col].ToString();
          row.Add(col.ColumnName, dr[col]);
        }
        rows.Add(row);
      }
      return serializer.Serialize(rows);
    }

  }
}
