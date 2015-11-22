using System;
using System.Collections.Generic;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using System.IO;
using System.Web.Services;
using System.Data.OracleClient;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Oracle;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Radiqal.Common;  

namespace AndroidWS
{
  public class DBConnection
  {
    public string connectionStr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;//ConfigurationSettings.AppSettings["constr"];
    public OracleConnection conn;
    public OracleCommand cmd;
    public OracleDataReader dr;


    public DBConnection()
    {
      conn = new OracleConnection(connectionStr);
      if (CheckConnectionState() == false) OpenConnection();
    }
    public Boolean CheckConnectionState()
    {
      try
      {
        if (conn.State == ConnectionState.Open)
        {
          return true;
        }
        else
        {
          return false;
        }
      }
      catch (Exception ex)
      {
        return false;
      }
    }

    public void OpenConnection()
    {
      try
      {
        conn.Open();
      }
      catch (Exception ex)
      {
      }
    }

    public Boolean CloseConnection()
    {
      try
      {
        conn.Close();
        return true;
      }
      catch (Exception ex)
      {
        return false;
      }
    }

    //public bool ExecuteNonQuery(string sql)
    //{ 
    //  try{
    //     cmd = new OracleCommand(sql, conn);
    //     int rowsUpdated = cmd.ExecuteNonQuery();
    //     if (rowsUpdated == 0)
    //       return false;
    //     else
    //       return true;
    //  }
    //  catch (Exception e)
    //  {
    //    return false;
    //  }
    //}

    //public OracleDataReader  ExcuteReader(string sql)
    //{
    //  cmd = new OracleCommand(sql, conn);
    //  OracleDataReader rd = cmd.ExecuteReader();
    //  return rd;
      
    //}

    //public string ExcuteScalar(string sql)
    //{
    //  cmd = new OracleCommand(sql, conn);
    //  return Convert .ToString (cmd.ExecuteScalar()); 
    //}

   

    //public byte[] imageToByteArray(System.Drawing.Image imageIn)
    //{
    //  MemoryStream ms = new MemoryStream();
    //  imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
    //  return ms.ToArray();
    //}

    //public DataTable  getDataTable(string sql)
    //{
    //  DataTable myTable = new DataTable();
    //  try
    //  {

    //    cmd = new OracleCommand(sql, conn);
    //    dr = cmd.ExecuteReader();
    //    myTable.Rows.Clear();
    //    myTable.TableName = "mydt";
    //    myTable.Load(dr);
    //    dr.Close();
    //    return myTable;
    //  }
    //  catch (Exception e)
    //  {
    //    string s = e.Message.ToString();
    //    return myTable;
    //  }
    //}

    //    public string getImage(string regKey)
//{
//            string sql = "select dt,image_c from image where REGKEY = '" + regKey + "' order by dt desc";
//            byte[] byteArray = null;
//            string imageString = string.Empty;
//            using (OracleCommand cmd = new OracleCommand(sql, conn))
//            {
//              try{
//                using (IDataReader dataReader = cmd.ExecuteReader())
//                {
//                  while (dataReader.Read())
//                  {
//                    //byteArray = (Byte[])dataReader["IMAGE_NAME"];
//                    imageString = dataReader["image_c"].ToString();

//                  }
//                }
//              }
//                catch (Exception e){return e.ToString();}
//            }
//           // return System.Convert.ToBase64String(byteArray) ;
//            return imageString;

//}

    //public byte[] getImageByteArray(string deviceId)
    //{
    //  string sql = string.Empty;
    //  sql = "select dt,IMAGE_NAME from image where device_id = '" + deviceId + "' order by dt desc";
    //  byte[] byteArray = null;
    //  string imageString = string.Empty;
    //  using (OracleCommand cmd = new OracleCommand(sql, conn))
    //  {
    //   // try
    //   // {
    //      using (IDataReader dataReader = cmd.ExecuteReader())
    //      {
    //        try
    //        {
    //          if (dataReader.Read())
    //          {
    //            byteArray = (Byte[])dataReader["IMAGE_NAME"];
    //          }
    //        }
    //        catch { }
    //        finally { dataReader.Close(); }
    //      }
    //    //}
    //    //catch (Exception e) { return byteArray; }
    //  }
    //  // return System.Convert.ToBase64String(byteArray) ;
    //  return byteArray ;

    //}

    //public string InsertImageByteArray(string regKey, byte [] bytearray)
    //{

    //  //string imagename = "C:\\Users\\vijal\\Desktop\\CA-wp1.jpg";




    //  //FileStream fs = new FileStream(@imagename, FileMode.Open, FileAccess.Read);
    //  //byte[] blob = new byte[fs.Length];
    //  //fs.Read(blob, 0, blob.Length);
    //  //fs.Close();

    //  OracleDataAdapter da = new OracleDataAdapter("SELECT regkey,dt,image_name,image_c FROM image", conn);
    //  DataTable dt = new DataTable();
    //  // get the schema
    //  da.FillSchema(dt, SchemaType.Source);

    //  OracleCommandBuilder cb = new OracleCommandBuilder(da);

    //  // create a row containing the data
    //  DataRow row = dt.NewRow();
    //  row["regkey"] = regKey;
    //  row["dt"] = DateTime.Now.ToString();
    //  row["image_name"] = bytearray;
    //  row["image_c"] = System.Convert.ToBase64String(bytearray);

    //  dt.Rows.Add(row);

    //  // update the table
    //  int i = 20;
    //  try
    //  {
    //    i = da.Update(dt);
    //  }
    //  catch (Exception e)
    //  {
    //    return e.Message.ToString();
    //  }
    //  return "true";
    //}

    //public IDataReader GetDataReader(string sql)
    //{
    //  IDataReader dataReader = null;

    //  try
    //  {
    //    cmd = new OracleCommand(sql, conn);

    //    dataReader = cmd.ExecuteReader();
    //  }
    //  catch (Exception e)
    //  {
    //  }

    //  return dataReader;
    //}

    //public ICollection<int> ExeTrans(ICollection<string> sqls)
    //{
    //  ICollection<int> rowsList = new List<int>();
    //  ICollection<DbCommand> dbCommands = new List<DbCommand>();
    //  Database _db = DatabaseFactory.CreateDatabase(); ;
    //  using (conn)
    //  {
        
    //    DbTransaction dbTrans = conn.BeginTransaction();
    //    try
    //    {
    //      foreach (string sql in sqls)
    //      {
    //        string upperSQL = sql;


    //        DbCommand dbCmd = _db.GetSqlStringCommand(upperSQL);
    //        int rows = 0;
    //        try
    //        {
    //          rows = _db.ExecuteNonQuery(dbCmd, dbTrans);
    //        }
    //        catch (Exception e)
    //        {
    //          throw;
    //        }

    //        //if ((!allowNoRowUpdates) && (rows <= 0))
    //        //{

    //        //}
    //        rowsList.Add(rows);
    //      }
    //      dbTrans.Commit();
    //    }
    //    catch
    //    {
    //      dbTrans.Rollback();
    //      throw;
    //    }
    //    finally
    //    {
    //      conn.Close();
    //    }
    //  }
    //  return rowsList;
    //}

  }
}