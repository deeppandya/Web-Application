using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Data.OracleClient;
using System.Web.Script.Services;
using Radiqal.Common;

namespace Radiqal
{
  /// <summary>
  /// Summary description for RegisterUser
  /// </summary>
  [WebService(Namespace = "http://tempuri.org/")]
  [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
  [System.ComponentModel.ToolboxItem(false)]
  // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
  // [System.Web.Script.Services.ScriptService]
  public class AndroidSecurityWS : System.Web.Services.WebService
  {
    
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public bool registerUser(string deviceId,string regKey,string UserId,string password,string deviceName,string phoneNo,string service_provider,string question,string answer)
    {
      
      if (string.IsNullOrEmpty(regKey) || string.IsNullOrEmpty(UserId) || string.IsNullOrEmpty(password)
        || string.IsNullOrEmpty(deviceName) || string.IsNullOrEmpty(deviceId) 
        || string.IsNullOrEmpty(question) || string.IsNullOrEmpty(answer)
        )
      {
        return false;
      }
      else
      {
        //string sqlInsert = "insert into users (DEVICE_ID,REGKEY,USERID,PASSWORD,DEVICE_NAME,PHONE_NO,SERVICE_PROVIDER,QUESTION,ANSWER,EMAIL_ID,dt) " +
        //" VALUES ('" + deviceId + "', '" + regKey + "','" + UserId.ToUpper() + "','" + password.ToUpper() + "' , '" + deviceName  + "','" + phoneNo + "','" + service_provider + "' ,'" + question + "','" + answer + "','" + email_id + "', sysdate )";

        string insertDevice = "INSERT INTO DEVICE (DEVICE_ID,REGKEY,DEVICE_NAME,PHONE_NO,SERVICE_PROVIDER,DT) " +
        " VALUES ('" + deviceId + "', '" + regKey + "', '" + deviceName  + "','" + phoneNo + "','" + service_provider + "' , sysdate )";

        string insertUser = "INSERT INTO USERS (USERID,PASSWORD,QUESTION,ANSWER,ACTIVE,DT) " +
        " VALUES ('" + UserId  + "', '" + password  + "','" + question  + "','" + answer  + "' ,'Y', sysdate )";

        string insertDeviceUser = "INSERT INTO DEVICE_USERS (DEVICE_ID,USERID,ACTIVE,DT) " +
        " VALUES ('" + deviceId  + "', '" + UserId + "','Y', sysdate )";
        IRadDbTransaction objTransaction = new RadDbTransaction();
        objTransaction.AddSql(insertDevice);
        objTransaction.AddSql(insertUser);

        objTransaction.AddSql(insertDeviceUser);
        try
        {
          objTransaction.ExecuteTransaction();
          Logging.WriteMessage("Register User Succeed");
          return true;
        }
        catch(Exception e)
        {
          Logging.WriteError("Register User Error ");  
          return false; }
         
      }
    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public bool updateUserDevice(string deviceId, string regKey, string UserId,string oldUserId)
    {
      RadDBImpl _dbImpl = new RadDBImpl(); 
      if (string.IsNullOrEmpty(regKey) || string.IsNullOrEmpty(UserId) || string.IsNullOrEmpty(deviceId)
        )
      {
        return false;
      }
      else
      {
        string password = string.Empty;
        string ques = string.Empty;
        string ans = string.Empty;
        string active = string.Empty;
        IRadDbTransaction objTransaction = new RadDbTransaction();

        bool isActive = isUserActive(oldUserId);
        string updSql = "update users set active = 'N' where userId = '" + oldUserId + "'";

        string updateSql = "update device_users set active = 'N' where device_id ='" + deviceId + "' and userId = '" + oldUserId + "'";
        bool isUserExists = checkUserExists(UserId);
        if (isUserExists)
         {
           string sql = "update users set active = 'Y' where userId = '" + UserId  + "'";
           string sql1 = "update device_users set active = 'Y' where device_id ='" + deviceId + "' and userId = '" + UserId  + "'";
           objTransaction.AddSql(sql);
           objTransaction.AddSql(sql1);
         }
        else
         {
          string sql = "select password,question,answer,active from users where  userId = '" + oldUserId.ToUpper()  + "'";

          DataTable dt = _dbImpl.GetDataSet(sql).Tables[0];
          if (dt != null && dt.Rows.Count > 0)
          {
            password = dt.Rows[0]["password"].ToString();
            ques  = dt.Rows[0]["question"].ToString();
            ans = dt.Rows[0]["answer"].ToString();
            active  = dt.Rows[0]["active"].ToString();
          
          }

          if (!isUserExists)
          {

            string updateDevice = "update device set REGKEY = '" + regKey + "' where device_id = '" + deviceId + "'";

            string insertUser = "INSERT INTO USERS (USERID,PASSWORD,QUESTION,ANSWER,ACTIVE,DT) " +
            " VALUES ('" + UserId + "', '" + password + "','" + ques + "','" + ans + "' ,'Y', sysdate )";

            string insertDeviceUser = "INSERT INTO DEVICE_USERS (DEVICE_ID,USERID,ACTIVE,DT) " +
            " VALUES ('" + deviceId + "', '" + UserId + "','Y', sysdate )";

            objTransaction.AddSql(updateDevice);
            objTransaction.AddSql(insertUser);
            objTransaction.AddSql(insertDeviceUser);
          }
        }
         objTransaction.AddSql(updSql);
         objTransaction.AddSql(updateSql);


        try
        {
          objTransaction.ExecuteTransaction();
          Logging.WriteMessage("Update User succeedd"); 
          return true;
        }
        catch {
          Logging.WriteError ("Update User Failed");
          return false;
        
        }
      }
      
    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public bool checkUser(string UserId, string password)
    {
      RadDBImpl _dbImpl = new RadDBImpl();
      bool val = false;
      string sql = "select USERID from users where USERID = '" + UserId.ToUpper() + "' and PASSWORD = '" + password.ToUpper() + "' AND ACTIVE = 'Y' ";

       if (string.IsNullOrEmpty(UserId) || string.IsNullOrEmpty(password))
        {
          return false;
        }
       using (IDataReader reader = _dbImpl.GetDataReader(sql))
       {
         try
         {
           if (reader.Read())
             val= true;
           else
             val =false;
         }
         catch
         { }
         finally
         {
           reader.Close();
         }
       }
       Logging.WriteMessage("Check User" + UserId.ToUpper() + " - "  + val);

       return val;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string GetDeviceId(string userId)
    {
      RadDBImpl _dbImpl = new RadDBImpl();
      string returnVal = string.Empty;
      string sql = "select device_id from DEVICE_USERS where userId = '" + userId + "' AND ACTIVE = 'Y' ";
        if (string.IsNullOrEmpty(userId))
        {
          return string.Empty;
        }

        using (IDataReader reader = _dbImpl.GetDataReader(sql))
        {
          try
          {
            while (reader.Read())
            {
              returnVal = reader["device_id"].ToString();
            }
          }
          catch
          { }
          finally
          {
            reader.Close();
          }
        }
        return returnVal;
    }



    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string GetRegistrationKey(string deviceId)
    {
      string returnVal = string.Empty;
      RadDBImpl _dbImpl = new RadDBImpl();
      string sql = "select REGKEY from DEVICE where device_id = '" + deviceId + "'";
      if (string.IsNullOrEmpty(deviceId))
      {
          return string.Empty;
      }
      using (IDataReader reader = _dbImpl.GetDataReader(sql))
      {
        try
        {
          while (reader.Read())
          {
            returnVal = reader["REGKEY"].ToString();
          }
        }
        catch
        { }
        finally
        {
          reader.Close();
        }
      }
      
      return returnVal;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string GetUserId(string deviceId)
    {
      string returnVal = string.Empty;
      RadDBImpl _dbImpl = new RadDBImpl();
      string sql = "select userId from device_users where device_id = '" + deviceId + "' and active = 'Y'";
      if (string.IsNullOrEmpty(deviceId))
        {
          return string.Empty;
        }
      using (IDataReader reader = _dbImpl.GetDataReader(sql))
      {
        try
        {
          while (reader.Read())
          {
            returnVal = reader["userId"].ToString();
          }
        }
        catch
        { }
        finally
        {
          reader.Close();
        }
      }
      
    
      return returnVal;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string checkDeviceExists(string deviceId)
    {
      RadDBImpl _dbImpl = new RadDBImpl();

      string val = "false";
      
        if (string.IsNullOrEmpty(deviceId))
        {
          return "false";
        }
        else
        {
          string sql = "select device_id from device where device_id = '" + deviceId + "'";
          IDataReader  rd = _dbImpl.GetDataReader(sql); 
          try
          {
            if (rd.Read())
                val = "true";
              else
                val = "false";
          }
          catch { }
          finally { rd.Close(); }
        }
      
        return val;
        
    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public bool updateRegKey(string deviceId,string regKey)
    {
      RadDBImpl _dbImpl = new RadDBImpl();
      bool val = false ;
      int rowsAffected = 0;
      if (string.IsNullOrEmpty(deviceId))
      {
        return false;
      }
      else
      {
        string sql = "update device set REGKEY = '" + regKey + "' where device_id = '" + deviceId + "'";
        rowsAffected  = _dbImpl.ExeDML(sql);
        if (rowsAffected > 0)
          val=  true;
      }
      return val;
    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public bool InsertLocation(string deviceId, string  latitude , string longitude)
    {
      IRadDbTransaction objTransaction = new RadDbTransaction();
      bool val = false;
      
      
        if (string.IsNullOrEmpty(deviceId) || string.IsNullOrEmpty(latitude.ToString()) || string.IsNullOrEmpty(longitude.ToString()))
        {
          return false;
        }
        else
        {

          string sqlInsert = "insert into GeoLocation (DEVICE_ID,DT,LATITUDE,LONGITUDE) VALUES ('" + deviceId + "', sysdate ," + Convert.ToDouble(latitude) + "," + Convert.ToDouble(longitude) + ")";
          try{
            objTransaction.AddSql(sqlInsert);
            objTransaction.ExecuteTransaction(); 
             val= true;         
          }
          catch{ }
        }
        return val;
    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public DataTable  GetLocation(string deviceId)
    {
      DataTable dt = new DataTable();
      RadDBImpl _dbImpl = new RadDBImpl();
      try
      {
        if (string.IsNullOrEmpty(deviceId))
        {
          return null;
        }
        else
        {
          //    string sql = "select DT,LATITUDE,LONGITUDE from GeoLocation where REGKEY = '" + regkey + "' and rownum <= 5 order by dt desc";
          string sql = "select DT,LATITUDE,LONGITUDE from GeoLocation where device_id = '" + deviceId + "'  order by dt desc";

          dt = _dbImpl.GetDataSet(sql).Tables[0] ; 
          
        }
      }
      catch {}
     
      return dt;
    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string InsertImage(string deviceId, string byteStream,string toAddress)
    {

      string inserted = string.Empty;
      byte[] byteArray = System.Convert.FromBase64String(byteStream);

      RadDBImpl _dbImpl = new RadDBImpl();

      //string insertImage = "INSERT INTO DEVICE (device_id,dt,image_name) " +
      //                      " VALUES ('" + deviceId + "', sysdate , '" + byteArray + "')";

      //objTransaction.AddSql(insertImage);
      //objTransaction.ExecuteTransaction(); 

      if (string.IsNullOrEmpty(deviceId) || string.IsNullOrEmpty(byteStream.ToString()))
      {
        return "error";
      }
      else
      {
       inserted = _dbImpl.ExecuteBlog(deviceId, byteStream);
       string subject = Convert.ToString(System.Configuration.ConfigurationSettings.AppSettings["ImageEmailSubject"]);
       string EmailText = Convert.ToString(System.Configuration.ConfigurationSettings.AppSettings["ImageEmailText"]);

       sendMail(deviceId, byteStream, toAddress, subject, EmailText, true);
       return inserted;
      }
      //conn.CloseConnection();
    
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]

    public   bool sendMail(string deviceId,string byteArray,string ToUserId,string subject,string mailText,bool isAttachment)
    {
      return  new RadMail().sendMail(deviceId, byteArray,ToUserId ,subject ,mailText , isAttachment);
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public byte[] getImageByteArray(string deviceId)
    {
     string sql = "select dt,IMAGE_NAME from image where device_id = '" + deviceId + "' order by dt desc";

       byte[] byteArray = null;
      RadDBImpl _dbImpl = new RadDBImpl();

      using (IDataReader reader = _dbImpl.GetDataReader(sql))
      {
        try
        {
          while (reader.Read())
          {
            byteArray = (Byte[])reader["IMAGE_NAME"];
          }
        }
        catch
        { }
        finally
        {
          reader.Close();
        }
      }

      return byteArray;
    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public   void deleteImage(string deviceId)
    {
      int rowsAffected = 0;
      RadDBImpl _dbImpl = new RadDBImpl();
      string sql = "delete from image where device_id = '" + deviceId + "'";
      try
      {
        rowsAffected = _dbImpl.ExeDML(sql);
      }
      catch { }
      
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public DataTable getUserInformation(string deviceId)
    {
      DataTable dt = new DataTable();
      RadDBImpl _dbImpl = new RadDBImpl();
      try
      {
        string sql = "select device_name,phone_no,service_provider, userid,regkey from device a,device_users b where a.device_id = '" + deviceId + "' and a.device_id = b.device_id and active ='Y'";
        dt = _dbImpl.GetDataSet(sql).Tables[0] ; 
      }
      catch{}
      
      return dt;
    }

    //[WebMethod]
    //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    //public  bool checkUserOnRegKey(string regkey)
    //{
    //  DBConnection conn = new DBConnection();
    //  bool val = false;
    //  if (string.IsNullOrEmpty(regkey) )
    //  {
    //    return false;
    //  }
    //  else
    //  {
    //    string sql = "select regkey from user where regKey = '" + regkey + "'";
    //    OracleDataReader rd = conn.ExcuteReader(sql);

    //    while (rd.Read())
    //    {
    //      if (rd.HasRows)
    //        val = true;
    //      else
    //        val = false;
    //    }
    //    return val;
    //  }
    //}

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public bool checkUserExists(string userId)
    {

      bool val = false;
      RadDBImpl _dbImpl = new RadDBImpl();
      string sql = "select userId from users where userid = '" + userId.ToUpper() + "'";

      using (IDataReader reader = _dbImpl.GetDataReader(sql))
      {
        try
        {
          if (reader.Read())
            val= true;
          else
            val= false;
        }
        catch
        { }
        finally
        {
          reader.Close();
        }
      }
      return val;
    }
      

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public bool isUserActive(string userId)
    {
      RadDBImpl _dbImpl = new RadDBImpl();
      bool val = false;
      string sql = "select userId from users where userid = '" + userId.ToUpper() + "' and ACTIVE = 'Y'";

      using (IDataReader reader = _dbImpl.GetDataReader(sql))
      {
        try
        {
          if (reader.Read())
            val= true;
          else
            val =false;
          
        }
        catch
        { }
        finally
        {
          reader.Close();
        }
      }
      return val;
    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public DataTable getUserLoginInfo(string userId)
    {

      DataTable dt = new DataTable();
      RadDBImpl _dbImpl = new RadDBImpl();
      try
      {
        string sql = "select question,answer,password, userid from users where userid = '" + userId.ToUpper() + "'";
        dt = _dbImpl.GetDataSet(sql).Tables[0];
      }
      catch (Exception e)
      {
      }
      
      return dt;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public bool checkRegKey(string regKey,string deviceId)
    {
      string sql = "select regkey from device where regkey= '" + regKey + "' and device_id = '" + deviceId + "'";
      RadDBImpl _dbImpl = new RadDBImpl();
      bool val = false;
      using (IDataReader reader = _dbImpl.GetDataReader(sql))
      {
        try
        {
          if (reader.Read())
            val = true;
          else
            val= false;
          
        }
        catch
        { }
        finally
        {
          reader.Close();
        }
      }
      return val;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public bool sendLocationMail(string deviceId, string byteArray, string NetLat, string NetLong, string toAddress)
    {
      string subject = Convert.ToString(System.Configuration.ConfigurationSettings.AppSettings["ImageEmailSubject"]);
      string EmailText = Convert.ToString(System.Configuration.ConfigurationSettings.AppSettings["ImageEmailText"]);
      
      string locationURL = "http://maps.google.com/?q=Device+Current+Location@"+NetLat+","+NetLong;
      EmailText = EmailText + "<br/>"+ locationURL;
      return new RadMail().sendMail(string.Empty, byteArray, toAddress, subject, EmailText, true);
    }


  }
}
