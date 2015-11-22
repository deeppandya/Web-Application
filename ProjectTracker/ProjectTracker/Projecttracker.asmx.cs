using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Web.Script.Services;
using Radiqal.Common;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using System.Text;

namespace ProjectTracker
{
  /// <summary>
  /// Summary description for Service1
  /// </summary>
  /// 
  

  [WebService(Namespace = "http://tempuri.org/")]
  [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
  [ToolboxItem(false)]
  // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
  // [System.Web.Script.Services.ScriptService]
  public class Projecttracker : System.Web.Services.WebService
  {
    
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public bool RegisterUser(string UserId, string password,string fname, string lname,string aabv)
    {
      RadDBImpl _dbImpl = new RadDBImpl();
      if (string.IsNullOrEmpty(UserId) || string.IsNullOrEmpty(password)
        || string.IsNullOrEmpty(fname) || string.IsNullOrEmpty(lname) || string.IsNullOrEmpty(aabv)
        )
      {
        return false;
      }
      else
      {
        //string sqlInsert = "insert into users (DEVICE_ID,REGKEY,USERID,PASSWORD,DEVICE_NAME,PHONE_NO,SERVICE_PROVIDER,QUESTION,ANSWER,EMAIL_ID,dt) " +
        //" VALUES ('" + deviceId + "', '" + regKey + "','" + UserId.ToUpper() + "','" + password.ToUpper() + "' , '" + deviceName  + "','" + phoneNo + "','" + service_provider + "' ,'" + question + "','" + answer + "','" + email_id + "', sysdate )";

        string insertUser = "INSERT INTO LKP_USERS (USER_ID,PASSWORD,FIRST_NAME,LAST_NAME,ABBREVIATION) " +
        " VALUES ('" + UserId.ToUpper() + "', '" + password.ToUpper() + "','" + fname.ToUpper() + "','" + lname.ToUpper() + "' ,'" + aabv + "')";
      //  return insertUser;


        IRadDbTransaction objTransaction = new RadDbTransaction();
        objTransaction.AddSql(insertUser);
        try
        {
          
          objTransaction.ExecuteTransaction();
          //Logging.WriteMessage("Register User Succeed");
          return true;
         
        }
        catch (Exception e)
        {
          //Logging.WriteError("Register User Error ");
          return false;
        
        }

      }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public bool AddProject(string company_id,string clientId, string Bugid, string Project_type_id, string bugdesc,string start_st,string end_dt,string user_id,string action_taken,string contact_person)
    {
      RadDBImpl _dbImpl = new RadDBImpl();

      if (string.IsNullOrEmpty(company_id) || string.IsNullOrEmpty(Bugid) || string.IsNullOrEmpty(clientId)
        || string.IsNullOrEmpty(Project_type_id) || string.IsNullOrEmpty(bugdesc)
        || string.IsNullOrEmpty(start_st) || string.IsNullOrEmpty(user_id)
        || string.IsNullOrEmpty(action_taken) || string.IsNullOrEmpty(contact_person)
        )
      {
        //return ProjectName+','+Bugid+','+Project_type_id+','+bugdesc+','+start_st+','+user_id+','+action_taken+','+contact_person;
        return false;
      }
      else
      {
        //string sqlInsert = "insert into users (DEVICE_ID,REGKEY,USERID,PASSWORD,DEVICE_NAME,PHONE_NO,SERVICE_PROVIDER,QUESTION,ANSWER,EMAIL_ID,dt) " +
        //" VALUES ('" + deviceId + "', '" + regKey + "','" + UserId.ToUpper() + "','" + password.ToUpper() + "' , '" + deviceName  + "','" + phoneNo + "','" + service_provider + "' ,'" + question + "','" + answer + "','" + email_id + "', sysdate )";
        string status = string.Empty;
        string session_id = GetNewID(Bugid);
        DateTime? end_date = null;

        if (string.IsNullOrEmpty(end_dt))
        {
          status = "N";
        }
        else
        {
          status = "Y";
          end_date = Convert.ToDateTime(end_dt);
        }
 
        string insertProject = "INSERT INTO PROJECT (COMPANY_ID,CLIENT_ID,BUG_ID,PROJECT_TYPE_ID,BUG_DESC,START_DT,END_DT,USER_ID,STATUS,ACTION_TAKEN,SESSION_ID,CONTACT_PERSON) " +
        " VALUES ('" + company_id.ToUpper() + "', '" + clientId + "','" + Bugid.ToUpper() + "','" + Project_type_id.ToUpper() + "','" + bugdesc + "', to_date('" + Convert.ToDateTime(start_st) + "','MM/dd/yyyy hh:mi:ss am'),to_date('" + end_date + "','MM/dd/yyyy hh:mi:ss am'),'" + user_id.ToUpper() + "','" + status.ToUpper() + "','" + action_taken + "','"+session_id+"','"+contact_person+"')";

        IRadDbTransaction objTransaction = new RadDbTransaction();
        objTransaction.AddSql(insertProject);
        try
        {
          objTransaction.ExecuteTransaction();
          //Logging.WriteMessage("Register User Succeed");
          return true;
          //return insertProject;
        }
        catch (Exception e)
        {
          //Logging.WriteError("Register User Error ");
          //return insertProject;
          return false;
        }

      }
    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public bool UpdateProject(string company_id, string clientID, string Bugid, string Project_type_id, string bugdesc, string start_st, string end_dt, string action_taken, string contact_person, string sessionId)
    {
      bool val = false;
      int rowsAffected = 0;
      RadDBImpl _dbImpl = new RadDBImpl();

      if ( string.IsNullOrEmpty(Bugid)
        || string.IsNullOrEmpty(Project_type_id) || string.IsNullOrEmpty(bugdesc)
        || string.IsNullOrEmpty(start_st) || string.IsNullOrEmpty(action_taken)
        || string.IsNullOrEmpty(contact_person)
        )
      {
        val = false;
      }
      else
      {
        string status = string.Empty;
        DateTime? end_date = null;

        if (string.IsNullOrEmpty(end_dt))
        {
          status = "N";
        }
        else
        {
          status = "Y";
          end_date = Convert.ToDateTime(end_dt);
        }

        string sql = "update PROJECT set Project_type_id = '" + Project_type_id.ToUpper() + "',bug_desc = '" + bugdesc + "',start_dt = to_date('" + Convert.ToDateTime(start_st) + "','MM/dd/yyyy hh:mi:ss am'),end_dt = to_date('" + end_date + "','MM/dd/yyyy hh:mi:ss am'),status='" + status.ToUpper() + "',ACTION_TAKEN='" + action_taken + "',CONTACT_PERSON='"+contact_person+"' where bug_id = '" + Bugid + "'";
          rowsAffected = _dbImpl.ExeDML(sql);
          if (rowsAffected > 0)
            val = true;
        }
        return val;
      }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string GetProjects(string user_id)
    {
      RadDBImpl _dbImpl = new RadDBImpl();
      string sql = "select A.* , C.NAME, D.CLIENT_NAME from project A , CLIENT_COMPANY B , LKP_COMPANY C , CLIENT D where A.CLIENT_ID = B.CLIENT_ID AND A.COMPANY_ID = B.COMPANY_ID AND B.COMPANY_ID= C.COMPANY_ID AND B.CLIENT_ID = D.CLIENT_ID AND A.CLIENT_ID= D.CLIENT_ID AND user_id='" + user_id.ToUpper() + "' order by start_dt desc";
      DataTable dt = _dbImpl.GetDataSet(sql).Tables[0];
      dt.Columns.Add("START", typeof(string));
      dt.Columns.Add("END", typeof(string));
      foreach (DataRow rw in dt.Rows)
      {
        string startdt = rw["START_DT"].ToString();
        rw["START"] = startdt;
        string enddt = rw["END_DT"].ToString();
        rw["END"] = enddt;
      }


      return GetJson(dt);
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string GetProjectDetail(string bug_id)
    {
      RadDBImpl _dbImpl = new RadDBImpl();
      string sql = "select A.* , C.NAME, D.CLIENT_NAME from project A , CLIENT_COMPANY B , LKP_COMPANY C , CLIENT D where A.CLIENT_ID = B.CLIENT_ID AND A.COMPANY_ID = B.COMPANY_ID AND B.COMPANY_ID= C.COMPANY_ID AND B.CLIENT_ID = D.CLIENT_ID AND A.CLIENT_ID= D.CLIENT_ID AND  bug_id='" + bug_id.ToUpper() + "'";
      DataTable dt = _dbImpl.GetDataSet(sql).Tables[0];
      dt.Columns.Add("START", typeof(string));
      dt.Columns.Add("END", typeof(string));
      foreach (DataRow rw in dt.Rows)
      {
        string startdt = rw["START_DT"].ToString();
        rw["START"] = startdt;
        string enddt = rw["END_DT"].ToString();
        rw["END"] = enddt;
      }
      return GetJson(dt);
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string GetSearchResult(string user_id,string fname,string lname,string bug_id)
    {
      RadDBImpl _dbImpl = new RadDBImpl();
      DataTable dt = new DataTable();
      string sql = "select LC.NAME,C.CLIENT_NAME, P.BUG_ID,P.PROJECT_TYPE_ID,P.BUG_DESC,P.START_DT,P.END_DT,P.USER_ID,P.STATUS,P.ACTION_TAKEN,P.SESSION_ID,P.CONTACT_PERSON "+
                   " FROM PROJECT P,LKP_COMPANY LC,CLIENT C , LKP_USERS U "+
                   " WHERE P.USER_ID=U.USER_ID AND P.STATUS='Y' AND P.CLIENT_ID = C.CLIENT_ID AND P.COMPANY_ID = LC.COMPANY_ID" ;
      if (string.IsNullOrEmpty(user_id) && string.IsNullOrEmpty(fname)
       && string.IsNullOrEmpty(lname) && string.IsNullOrEmpty(bug_id))
      {
        dt = _dbImpl.GetDataSet(sql).Tables[0];
      }
      else
      {
        if (!string.IsNullOrEmpty(user_id))
        {
          sql = sql + " AND P.USER_ID='"+user_id.ToUpper()+"'";
        }

        if (!string.IsNullOrEmpty(fname))
        {
          sql = sql + " AND U.FIRST_NAME='" + fname.ToUpper() + "'";
        }

        if (!string.IsNullOrEmpty(lname))
        {
          sql = sql + " AND U.LAST_NAME='" + lname.ToUpper() + "'";
        }

        if (!string.IsNullOrEmpty(bug_id))
        {
          sql = sql + " AND P.BUG_ID='" + bug_id.ToUpper() + "'";
        }

        sql = sql + " ORDER BY P.START_DT DESC";
        dt = _dbImpl.GetDataSet(sql).Tables[0];
        
      }
      dt.Columns.Add("START", typeof(string));
      dt.Columns.Add("END", typeof(string));
      foreach (DataRow rw in dt.Rows)
      {
        string startdt = rw["START_DT"].ToString();
        rw["START"] = startdt;
        string enddt = rw["END_DT"].ToString();
        rw["END"] = enddt;
      }
      return GetJson(dt);
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string GetProject_Type_Id()
    {
      RadDBImpl _dbImpl = new RadDBImpl();
      string sql = "select * from LKP_PROJECT_TYPE";
      DataTable dt = _dbImpl.GetDataSet(sql).Tables[0];

      return GetJson(dt);
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public bool CheckUser(string UserId, string password)
    {
      RadDBImpl _dbImpl = new RadDBImpl();
      DataTable dt = new DataTable();
      dt.Columns.Add("check", typeof(string));
      DataRow dr = dt.NewRow();
      bool val = false;
      string sql = "select USER_ID from LKP_USERS where USER_ID = '" + UserId.ToUpper() + "' and PASSWORD = '" + password.ToUpper() + "'";

      if (string.IsNullOrEmpty(UserId) || string.IsNullOrEmpty(password))
      {
        //dr["check"] = "false";
        //dt.Rows.Add(dr);
        return val;
      }
      using (IDataReader reader = _dbImpl.GetDataReader(sql))
      {
        try
        {
          if (reader.Read())
          {
            //dr["check"] = "true";
            //dt.Rows.Add(dr);
            val = true;
          }
          else
          {
            //dr["check"] = "false";
            //dt.Rows.Add(dr);
            val=false;
          }
        }
        catch
        { }
        finally
        {
          reader.Close();
        }
      }
      //Logging.WriteMessage("Check User" + UserId.ToUpper() + " - " + val);

      //return GetJson(dt);
      return val;
    }

    public string GetNewID(string prefix)
    {
      DateTime xDatetime = System.DateTime.Now;

      string id = String.Format("{0}{1}{2}{3}{4}{5}{6}",
        DateTime.Now.Year.ToString("0000"), DateTime.Now.Month.ToString("00"),
        DateTime.Now.Day.ToString("00"), DateTime.Now.Hour.ToString("00"),
        DateTime.Now.Minute.ToString("00"), DateTime.Now.Second.ToString("00"),
        DateTime.Now.Millisecond.ToString("00"));

      id = prefix + id;
      return id;
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

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public bool AddCompany(string CompanyId, string Desc,string CompanyNo)
    {
      RadDBImpl _dbImpl = new RadDBImpl();
      if (string.IsNullOrEmpty(CompanyId) || string.IsNullOrEmpty(Desc))
      {
        return false;
      }
      else
      {

        string insertComp = "INSERT INTO LKP_COMPANY (COMPANY_ID,NAME,COMPANY_NO) " +
        " VALUES ('" + CompanyId.ToUpper() + "', '" + Desc.ToUpper() + "','"+ CompanyNo + "')";


        IRadDbTransaction objTransaction = new RadDbTransaction();
        objTransaction.AddSql(insertComp);
        try
        {

          objTransaction.ExecuteTransaction();
          //Logging.WriteMessage("Register User Succeed");
          return true;

        }
        catch (Exception e)
        {
          //Logging.WriteError("Register User Error ");
          return false;

        }

      }
    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public bool EditCompany(string CompanyId, string Desc,string companyNo)
    {
      RadDBImpl _dbImpl = new RadDBImpl();
      if (string.IsNullOrEmpty(CompanyId) || string.IsNullOrEmpty(Desc))
      {
        return false;
      }
      else
      {

        string updateComp = "UPDATE  LKP_COMPANY SET NAME = '" + Desc + "',COMPANY_NO = '" + companyNo + "' WHERE COMPANY_ID = '" + CompanyId.ToUpper() + "'";

        IRadDbTransaction objTransaction = new RadDbTransaction();
        objTransaction.AddSql(updateComp);
        try
        {

          objTransaction.ExecuteTransaction();
          //Logging.WriteMessage("Register User Succeed");
          return true;

        }
        catch (Exception e)
        {
          //Logging.WriteError("Register User Error ");
          return false;

        }

      }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string GetCompany(string companyId)
    {
      string sql = string.Empty ;
      RadDBImpl _dbImpl = new RadDBImpl();

      if (string.IsNullOrEmpty(companyId))
        sql = "select company_id , name,company_no from lkp_company";
      else
        sql = "select company_id,name ,company_no from lkp_company where company_id = '" + companyId.ToUpper() + "'";

      DataTable dt = _dbImpl.GetDataSet(sql).Tables[0];

      foreach (DataRow rw in dt.Rows)
      {
        string company_id = rw["company_id"].ToString();
        rw["company_id"] = company_id;
        string name = rw["name"].ToString();
        rw["name"] = name;
        string company_no = rw["company_no"].ToString();
        rw["company_no"] = company_no;
      }

      return GetJson(dt);
    }



    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public bool AddClient(string ClientId, string ClientName,Double Charge,string billingId,string companyId)
    {
      IRadDbTransaction objTransaction = new RadDbTransaction();
      RadDBImpl _dbImpl = new RadDBImpl();
      if (string.IsNullOrEmpty(ClientId) || string.IsNullOrEmpty(ClientName) || string.IsNullOrEmpty(companyId))
      {
        return false;
      }
      else
      {

        string insertClient = "INSERT INTO CLIENT (CLIENT_ID,CLIENT_NAME,AMT_CHARGE,BILLING_ID) " +
        " VALUES ('" + ClientId.ToUpper() + "', '" + ClientName.ToUpper() + "','" + Charge + "','"+ billingId + "')";


        string insertClientComp = "INSERT INTO CLIENT_COMPANY (CLIENT_ID,COMPANY_ID) " +
        " VALUES ('" + ClientId.ToUpper() + "', '" + companyId .ToUpper() + "')";

        objTransaction.AddSql(insertClient);
        objTransaction.AddSql(insertClientComp);

        try
        {

          objTransaction.ExecuteTransaction();
          //Logging.WriteMessage("Register User Succeed");
          return true;

        }
        catch (Exception e)
        {
          //Logging.WriteError("Register User Error ");
          return false;

        }

      }
    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public bool EditClient(string ClientId, string ClientName, Double Charge, string billingId, string companyId)
    {
      RadDBImpl _dbImpl = new RadDBImpl();

      
      if (string.IsNullOrEmpty(ClientId) || string.IsNullOrEmpty(ClientName) || string.IsNullOrEmpty(companyId))
      {
        return false;
      }
      else
      {

        string updateClient = "UPDATE  CLIENT SET CLIENT_NAME = '" + ClientName.ToUpper()   + "' , AMT_CHARGE = '" + Charge + "' , BILLING_ID = '" + billingId + "' WHERE CLIENT_ID ='" + ClientId.ToUpper() + "'";
        
        IRadDbTransaction objTransaction = new RadDbTransaction();
        objTransaction.AddSql(updateClient);
        try
        {

          objTransaction.ExecuteTransaction();
          //Logging.WriteMessage("Register User Succeed");
          return true;

        }
        catch (Exception e)
        {
          //Logging.WriteError("Register User Error ");
          return false;

        }

      }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string GetClient(string clientId)
    {
      string sql = string.Empty;
      RadDBImpl _dbImpl = new RadDBImpl();

      if (string.IsNullOrEmpty(clientId))
        sql = "select a.client_id,client_name,amt_charge,a.billing_Id,name ,b.company_id, bill_to from client a, CLIENT_COMPANY b ,lkp_company c , lkp_billing d where a.client_id = b.client_id and b.company_id = c.company_id and a.billing_id = d.billing_id ";
      else
        sql = "select a.client_id,client_name,amt_charge,a.billing_Id ,name,b.company_id, bill_to  from client a, CLIENT_COMPANY b ,lkp_company c , lkp_billing d where a.client_id = b.client_id  and b.company_id = c.company_id and a.billing_id = d.billing_id and client_id = '" + clientId.ToUpper() + "'";

      DataTable dt = _dbImpl.GetDataSet(sql).Tables[0];
      //dt.Columns.Add("client_id", typeof(string));
      //dt.Columns.Add("client_name", typeof(string));
      //dt.Columns.Add("amt_charge", typeof(Double));
      //dt.Columns.Add("billing_id", typeof(string));
      
      foreach (DataRow rw in dt.Rows)
      {
        string client_id = rw["client_id"].ToString();
        rw["client_id"] = client_id;
        
        string client_name = rw["client_name"].ToString();
        rw["client_name"] = client_name;

        double  amt_charge = Convert.ToDouble( rw["amt_charge"]);
        rw["amt_charge"] = amt_charge;

        string billing_Id = rw["billing_Id"].ToString();
        rw["billing_Id"] = billing_Id;

        string bill_to = rw["bill_to"].ToString();
        rw["bill_to"] = bill_to;

        string name = rw["name"].ToString();
        rw["name"] = name;

        string company_id = rw["company_id"].ToString();
        rw["company_id"] = company_id;

      }

      return GetJson(dt);
    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public bool AddBillingDetails(string BillingId, string BillTo, string ContactPerson, string address, string city,string state,string country,string zip)
    {
      IRadDbTransaction objTransaction = new RadDbTransaction();
      RadDBImpl _dbImpl = new RadDBImpl();
      if (string.IsNullOrEmpty(BillingId) || string.IsNullOrEmpty(BillTo) || string.IsNullOrEmpty(ContactPerson))
      {
        return false;
      }
      else
      {

        string insertBill = "INSERT INTO LKP_BILLING (BILLING_ID,BILL_TO,CONTACT_PERSON,ADDRESS,CITY,STATE,COUNTRY,ZIP) " +
        " VALUES ('" + BillingId .ToUpper() + "', '" + BillTo.ToUpper() + "','" + ContactPerson.ToUpper()   + "','" +address.ToUpper()  + "','" + city.ToUpper()+ "','" + state.ToUpper() + "' , '" + country.ToUpper() + "', '" + zip + "')";



        objTransaction.AddSql(insertBill );
       

        try
        {

          objTransaction.ExecuteTransaction();
          //Logging.WriteMessage("Register User Succeed");
          return true;

        }
        catch (Exception e)
        {
          //Logging.WriteError("Register User Error ");
          return false;

        }

      }
    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public bool EditBillingDetails(string BillingId, string BillTo, string ContactPerson, string address, string city,string state,string country,string zip)
    {
      RadDBImpl _dbImpl = new RadDBImpl();

      
      if (string.IsNullOrEmpty(BillingId) || string.IsNullOrEmpty(BillTo) || string.IsNullOrEmpty(ContactPerson))
      {
        return false;
      }
      else
      {

        string updateBillilng = "UPDATE  LKP_BILLING SET BILL_TO = '" + BillTo.ToUpper()   + "' , CONTACT_PERSON = '" + ContactPerson.ToUpper() + "' , ADDRESS = '" + address.ToUpper()  + "', CITY = '" + city.ToString() + "', STATE = '" + state.ToUpper() + "', COUNTRY = '" + country.ToUpper() + "' , ZIP = '" + zip   +  "' WHERE BILLING_ID ='" + BillingId .ToUpper() + "'";
        
        IRadDbTransaction objTransaction = new RadDbTransaction();
        objTransaction.AddSql(updateBillilng);
        try
        {

          objTransaction.ExecuteTransaction();
          //Logging.WriteMessage("Register User Succeed");
          return true;

        }
        catch (Exception e)
        {
          //Logging.WriteError("Register User Error ");
          return false;

        }

      }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string GetBillingDetails(string billingId)
    {
      string sql = string.Empty;
      RadDBImpl _dbImpl = new RadDBImpl();

      if (string.IsNullOrEmpty(billingId))
        sql = "select billing_id,bill_to,contact_person,address,city,state,country,zip from lkp_billing";
      else
        sql = "select billing_id,bill_to,contact_person,address,city,state,country,zip from lkp_billing where billing_id = '" + billingId.ToUpper() + "'";

      DataTable dt = _dbImpl.GetDataSet(sql).Tables[0];
   

      foreach (DataRow rw in dt.Rows)
      {
        string billing_Id = rw["billing_id"].ToString();
        rw["billing_id"] = billing_Id;

        string bill_to = rw["bill_to"].ToString();
        rw["bill_to"] = bill_to;

        string contact_person = rw["contact_person"].ToString();
        rw["contact_person"] = contact_person;

        string BillAddress = rw["address"].ToString();
        rw["address"] = BillAddress;

        string Bill_city = rw["city"].ToString();
        rw["city"] = Bill_city;

        string Bill_state = rw["state"].ToString();
        rw["state"] = Bill_state;

        string Bill_country = rw["country"].ToString();
        rw["country"] = Bill_country;

        string bill_zip = rw["zip"].ToString();
        rw["zip"] = bill_zip;
      }

      return GetJson(dt);
    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string GetCompanyClients(string companyId)
    {
      string sql = string.Empty;
      RadDBImpl _dbImpl = new RadDBImpl();

      if (string.IsNullOrEmpty(companyId))
        sql = "select A.CLIENT_ID,CLIENT_NAME from CLIENT A,CLIENT_COMPANY B WHERE A.CLIENT_ID = B.CLIENT_ID";
      else
        sql = "select A.CLIENT_ID,CLIENT_NAME from CLIENT A,CLIENT_COMPANY B WHERE A.CLIENT_ID = B.CLIENT_ID AND  COMPANY_ID = '" + companyId.ToUpper() + "'";

      DataTable dt = _dbImpl.GetDataSet(sql).Tables[0];


      foreach (DataRow rw in dt.Rows)
      {
        string CLIENT_ID = rw["CLIENT_ID"].ToString();
        rw["CLIENT_ID"] = CLIENT_ID;

        string CLIENT_NAME = rw["CLIENT_NAME"].ToString();
        rw["CLIENT_NAME"] = CLIENT_NAME;


      }

      return GetJson(dt);
    }

    [WebMethod]
    public DataTable GetReportDS()
    {
      string sql = string.Empty;
      RadDBImpl _dbImpl = new RadDBImpl();

      sql = "SELECT BILL.BILLING_ID, BILL.BILL_TO, BILL.CONTACT_PERSON, BILL.ADDRESS, "
          + " BILL.CITY, BILL.STATE, BILL.COUNTRY, BILL.ZIP,CL.CLIENT_NAME, "
          + " CL.AMT_CHARGE,CL.CLIENT_ID,P.BUG_ID, P.BUG_DESC, P.START_DT, P.END_DT "
          + " FROM LKP_BILLING BILL, CLIENT CL, PROJECT P "
          + " WHERE  BILL.BILLING_ID = CL.BILLING_ID AND CL.CLIENT_ID = P.CLIENT_ID";

      DataTable dt = _dbImpl.GetDataSet(sql).Tables[0];


  

      return (dt);
    }


  }
}
