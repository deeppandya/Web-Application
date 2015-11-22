using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Transactions;
using System.Diagnostics;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Oracle;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

using System.Data.OracleClient;
using log4net;


namespace Radiqal.Common
{

  #region enum definitions
  public enum DatabaseType { ODBC = 0, MSSQL = 1, ORACLE = 2 }
  public enum DMLComparator
  {
    [StringValue("=")]
    Equal = 0,
    [StringValue("!=")]
    NotEqual = 1,
    [StringValue("like")]
    Like = 2,
    [StringValue(">")]
    Greater = 3,
    [StringValue("<")]
    Less = 4,
    [StringValue(">=")]
    GreaterEqual = 5,
    [StringValue("<=")]
    LessEqual = 6,
    [StringValue("in")]
    In = 7,
    [StringValue("not in")]
    NotIn = 8,
    [StringValue("is null")]      //Added later on
    IsNull = 9,
    [StringValue("is not null")]  //Added later on
    IsNotNull = 10
  }
  public enum DMLBooleanOperator
  {
    [StringValue("OR")]
    OR = 0,
    [StringValue("AND")]
    AND = 1
  }
  public enum DMLOrderOperator
  {
    [StringValue("DESC")]
    DESC = 0,
    [StringValue("ASC")]
    ASC = 1
  }



  /// <summary>
  /// Represents database boolean values.
  /// </summary>
  public struct DMLFlag
  {
    public const string TRUE = "Y";
    public const string FALSE = "N";
  }

  /// <summary>
  /// List of available Types.
  /// </summary>
  public struct DMLType
  {
    public static readonly Type INT = Type.GetType((typeof(int)).ToString());

    public static readonly Type INT16 = Type.GetType((typeof(Int16)).ToString());
    public static readonly Type INT32 = Type.GetType((typeof(Int32)).ToString());
    public static readonly Type INT64 = Type.GetType((typeof(Int64)).ToString());
    public static readonly Type DECIMAL = Type.GetType((typeof(Decimal)).ToString());

    public static readonly Type UINT16 = Type.GetType((typeof(UInt16)).ToString());
    public static readonly Type UINT32 = Type.GetType((typeof(UInt32)).ToString());
    public static readonly Type UINT64 = Type.GetType((typeof(UInt64)).ToString());
    public static readonly Type DOUBLE = Type.GetType((typeof(Double)).ToString());

    public static readonly Type DATE_TIME = Type.GetType((typeof(DateTime)).ToString());

    public static readonly Type CHAR = Type.GetType((typeof(char)).ToString());
    public static readonly Type STRING = Type.GetType((typeof(string)).ToString());
  }
  #endregion

  #region class RadDBImpl definition
  public class RadDBImpl
  {

    public const string endl = "\n";

		public static int Module_DBImpl = 100;
		
		private static void displayErrors(Exception ex){}
		private static void appAddlInfo(Dictionary<string, object> addlInfo){}

    public static readonly ICollection<string> CapsByPassColumns = new List<string>() { "MEMO", "COMMENTS", "EXT_DESCRIPTION", "COMMENT", "DISPLAY_NAME", "TEXT" };

		static RadDBImpl()	
		{
		}

    Database _db;
    string connectionString = string.Empty;
    public RadDBImpl()
    {
       connectionString = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;//ConfigurationSettings.AppSettings["constr"]
      string providerName = Convert.ToString(System.Configuration.ConfigurationSettings.AppSettings["ProviderName"]);
      //InitLogger();
      
      if (string.IsNullOrEmpty(connectionString) || string.IsNullOrEmpty(providerName))
      {

      }
      else
      {
        if (providerName == "System.Data.OracleClient")
        {
          OracleDatabase oraDb = new OracleDatabase(connectionString);
          _db = oraDb;
        }
        else if (providerName == "System.Data.SqlClient")
        {
          SqlDatabase sqlDb = new SqlDatabase(connectionString);
          _db = sqlDb;
        }
        else
        {
        }
      }
    }

    /// <summary>
    /// This method returns an instance of System.Data.Common.DbDataReader class.
    /// The DbDataReader methods indicate to the underlying ADO.NET call to automatically close the 
    /// connection when the DbDataReader is finished. In this situation, it is considered a best 
    /// practice for the application to ensure that the DbDataReader is closed in a timely fashion, 
    /// either by explicitly closing the reader with the DbDataReader.close method or by forcing 
    /// the disposal of the DbDataReader, which results in the Close method being called.
    /// 
    /// The using statement (Using in Visual Basic) ensures that the DbDataReader object is disposed, 
    /// which closes the DbDataReader object.
    /// </summary>
    /// <param name="sql"></param>
    /// <returns></returns>
    public IDataReader GetDataReader(string sql)
    {
			IDataReader dataReader = null;

			try
			{
				DbCommand dbCommand = _db.GetSqlStringCommand(sql);

				dataReader = _db.ExecuteReader(dbCommand);
			}
			catch (Exception e)
			{
				throw e;
			}

			return dataReader;
    }

    /// <summary>
		/// This method returns an instance of System.Data.DataSet class.
		/// The ExecuteDataSet method returns a DataSet object that contains all the data. 
		/// This gives you your own local copy. The call to ExecuteDataSet opens a connection, 
		/// populates a DataSet, and closes the connection before returning the result.
		/// </summary>
		/// <param name="sql"></param>
		/// <returns></returns>
		public DataSet GetDataSet(string sql)
		{
			DataSet ds = null;
			DbCommand dbCommand = null;
			try
			{
				dbCommand = _db.GetSqlStringCommand(sql);

				ds = _db.ExecuteDataSet(dbCommand);
			}
			catch (Exception e)
			{
 //       Logging.WriteError("DataSet Query Failed " + sql );
//				RadLogger.Write(Module_DBImpl, TraceEventType.Error, MaxPriority, sql + "->" + e.StackTrace, SystemAuditCategories);
				throw e;
			}

			return ds;
		}

    /// <summary>
    /// This method executes the passed in insert, update, delete and returns the rows affected
    /// </summary>
    /// <param name="sql"></param>
    /// <returns></returns>
    public int ExeDML(string sql)
    {

      string upperSQL = sql;

			int rows = 0;
			try
			{
        rows = _db.ExecuteNonQuery(CommandType.Text, upperSQL);


				if (rows <= 0)
				{
				}
			}
			catch(Exception e)
			{
				throw;
			}
			return rows;
    }

    /// <summary>
    /// This method executes the passed in insert, update, delete and returns the rows affected. 
    /// The bool ignoreNoRows controls if no rows are affected should throw exceptions
    /// </summary>
    /// <param name="sql"></param>
    /// <returns></returns>
    public int ExeDML(string sql, bool ignoreNoRows)
    {
      string upperSQL = sql;

      int rows = 0;
      try
      {
        rows = _db.ExecuteNonQuery(CommandType.Text, upperSQL);

        if (!ignoreNoRows && rows <= 0)
        {
        }
      }
      catch (Exception e)
      {
        throw;
      }
      return rows;
    }


    /// <summary>
    /// This method uses the TransactionScope class that creates a local, lightweight transaction. 
    /// It assumes that the queries use a single connection for all of the database calls that occur 
    /// within the transaction. This method does not span across database instances.
    /// Multiple threads sharing the same transaction in a transaction scope will cause the following 
    /// exception : "Transaction context in use by another session."
    /// Note:
    /// The maximum number of distributed transactions that an Oracle database can participate in at one 
    /// time is set to 10 by default. After the 10th transaction when connected to an Oracle database, 
    /// an exception is thrown. Oracle does not support DDL inside of a distributed transaction.
    /// 
    /// Although you can use the TransactionScope class with the Oracle client, transactions are always treated 
    /// as distributed transactions rather than lightweight transactions. Distributed transactions have a 
    /// higher performance overhead. The .NET Framework managed provider for Oracle requires a file named 
    /// oramts.dll in order to use the TransactionScope class. For more information, see on the 
    /// Microsoft Help and Support Web site. If you are using Oracle with the Microsoft Transaction Server, 
    /// see http://www.oracle.com/technology/software/tech/windows/ora_mts/htdocs/utilsoft.html on the Oracle Web 
    /// site for the appropriate downloads.
    /// </summary>
    /// <param name="sqls"></param>
    /// <returns></returns>
    public ICollection<int> ExeTransUsingScope(ICollection<string> sqls)
    {
      ICollection<int> rowsList = new List<int>();
      using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
      {
        foreach(string sql in sqls)
        {
          string upperSQL = sql;
          
          int rows = 0;
					try
					{
						rows = _db.ExecuteNonQuery(CommandType.Text, upperSQL);
					}
					catch (Exception e)
					{
//						RadLogger.Write(Module_DBImpl, TraceEventType.Error, MaxPriority, upperSQL + "->" + e.StackTrace, SystemAuditCategories);
						scope.Dispose();
						throw;
					}				
          if (rows <= 0)
          {
            scope.Dispose();
	//					RadLogger.Write(Module_DBImpl, TraceEventType.Error, MaxPriority, upperSQL + "->" + "Ret Rows = " + rows, SystemAuditCategories);
		//				throw new RadDBImplExceptions.TransNoRowsAffectedException(upperSQL);
          }
					rowsList.Add(rows);
					//string upperSQLEnc = RadUtilWrapper.GetEncryptedString(upperSQL);
					//RadLogger.Write(Module_DBImpl, TraceEventType.Information, MinPriority, upperSQLEnc, DbAuditCategories);
        }
        scope.Complete();
      }
      return rowsList;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sqls"></param>
    /// <returns></returns>
    public ICollection<int> ExeTrans(ICollection<string> sqls, bool allowNoRowUpdates)
    {
      ICollection<int> rowsList = new List<int>();
      ICollection<DbCommand> dbCommands = new List<DbCommand>();

      using (DbConnection dbConn = _db.CreateConnection())
      {
        dbConn.Open();
        DbTransaction dbTrans = dbConn.BeginTransaction();
        try
        {          
          foreach (string sql in sqls)
          {
            string upperSQL = sql;
						
            
            DbCommand dbCmd = _db.GetSqlStringCommand(upperSQL);
            int rows = 0;
						try
						{
                
//             Logging.WriteMessage("Query " + upperSQL );  
							rows = _db.ExecuteNonQuery(dbCmd, dbTrans);
						}
						catch (Exception e)
						{
							//RadLogger.Write(Module_DBImpl, TraceEventType.Error, , upperSQL + "->" + e.StackTrace, SystemAuditCategories);
//              Logging.WriteError ("Query Failed " + upperSQL );
							throw;
						}						

            if ((!allowNoRowUpdates) && (rows <= 0))
            {
//              Logging.WriteError("Query Failed " + upperSQL);
						//	RadLogger.Write(Module_DBImpl, TraceEventType.Error, , upperSQL + "->" + "Ret Rows = " + rows, SystemAuditCategories);
						//	throw new RadDBImplExceptions.TransNoRowsAffectedException(upperSQL);
            }
						rowsList.Add(rows);
						//string upperSQLEnc = RadUtilWrapper.GetEncryptedString(upperSQL);
						//RadLogger.Write(Module_DBImpl, TraceEventType.Information, MinPriority, upperSQLEnc, DbAuditCategories);
          }
          dbTrans.Commit();
        }
        catch
        {
          dbTrans.Rollback();
          throw;        
        }
        finally
        {
          dbConn.Close();
        }
      }
      return rowsList;
    }

    public static ICollection<int> ExeTrans(ICollection<string> sqls, ICollection<string> namedDBInstances)
    {
      if (sqls.Count != namedDBInstances.Count)
      {
  //      throw new RadDBImplExceptions.InArgumentsMismatchException("Each SQL query should have a corresponding Database Instance defined.");
      }
      ICollection<int> rowsList = new List<int>();
      Hashtable dbs = new Hashtable();


      string aNamedDBInstance = "";
      foreach (string namedDBInstance in namedDBInstances)
      {
        if (aNamedDBInstance.CompareTo(namedDBInstance) != 0)
        {
          dbs.Add(namedDBInstance, DatabaseFactory.CreateDatabase(namedDBInstance));
        }
        aNamedDBInstance = namedDBInstance;
      }

      using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
      {
        for (int i = 0; i < sqls.Count; i++)
        {
          Database db = (Database)dbs[namedDBInstances.ElementAt(i)];
					
          string upperSQL = sqls.ElementAt(i);
          
          int rows = 0;
					try
					{
						rows = db.ExecuteNonQuery(CommandType.Text, upperSQL);
					}
					catch (Exception e)
					{
					//	RadLogger.Write(Module_DBImpl, TraceEventType.Error, MaxPriority, upperSQL + "->" + e.StackTrace, SystemAuditCategories);
//            Logging.WriteError("Query Failed " + upperSQL);
						scope.Dispose();
						throw;
					}					
          if (rows <= 0)
          {
            scope.Dispose();
//            Logging.WriteError("Query Failed " + upperSQL);
						//RadLogger.Write(Module_DBImpl, TraceEventType.Error, MaxPriority, upperSQL + "->" + "Ret Rows = " + rows, SystemAuditCategories);
  //          throw new RadDBImplExceptions.TransNoRowsAffectedException(sqls.ElementAt(i));
          }
					//string upperSQLEnc = RadUtilWrapper.GetEncryptedString(upperSQL);
					//RadLogger.Write(Module_DBImpl, TraceEventType.Information, MinPriority, upperSQLEnc, DbAuditCategories);
        }
        scope.Complete();
      }
      return rowsList;
    }

    /// <summary>
    /// Replaces escape characters for sql qurey
    /// </summary>
    /// <param name="strValue"></param>
    /// <returns></returns>
    public static string ReplaceEscChars(string strValue)
    { 

      return (!string.IsNullOrEmpty(strValue)) ? strValue.Replace("'", "''"): strValue;
    }


    public string ExecuteBlog(string deviceId, string blob)
    {

      byte[] byteArray = System.Convert.FromBase64String(blob);

      string sql = "SELECT device_id,dt,image_name FROM image";
      DbCommand dbCommand = _db.GetSqlStringCommand(sql);
      using (OracleConnection dbConn = new OracleConnection(connectionString))
      {
        dbConn.Open();
        OracleDataAdapter da = new OracleDataAdapter ("SELECT device_id,dt,image_name FROM image", dbConn);
        DataTable dt = new DataTable();
        
        da.FillSchema(dt, SchemaType.Source);

        OracleCommandBuilder cb = new OracleCommandBuilder(da);


        //   create a row containing the data
        DataRow row = dt.NewRow();
        row["device_id"] = deviceId;
        row["dt"] = DateTime.Now.ToString();
        row["image_name"] = byteArray;


        dt.Rows.Add(row);

        //  update the table
        int i = 20;
        try
        {
          i = da.Update(dt);
        }
        catch (Exception e)
        {
          return e.Message.ToString();
        }
        finally
        {
          dbConn.Close();
        }
        return "true";
      }
    }
    //public static void InitLogger()
    //{
    //  try
    //  {
    //    string loggerConfigFile = ConfigurationSettings.AppSettings["AndroidSecurityLoggerConfig"];
    //    log4net.Config.XmlConfigurator.Configure(new System.IO.FileInfo(loggerConfigFile));
    //  }
    //  catch { }
    //}
  }
  #endregion
}
