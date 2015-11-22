using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Radiqal.Common
{
  /// <summary>
  /// This class is used to execute multiple sql statements as one single transaction.
  /// i.e. it maintains atomicity for all sql statements.
  /// Either all statements will execute or none.
  /// </summary>
  public class RadDbTransaction : IRadDbTransaction
  {
    #region Variables
    /// <summary>
    /// Collection of sql statements.
    /// </summary>
    protected ICollection<string> _sqls;
		protected ICollection<string> _dbNames;
    protected RadDBImpl _dbImpl;
    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new ExecuteMultipleQuery object.
    /// </summary>
    public RadDbTransaction()
    {
      _sqls = new List<string>();
			_dbNames = new List<string>();
      _dbImpl = new RadDBImpl();
    }
    #endregion

    #region Exposed Functionality
    /// <summary>
    /// Add a new sql statement.
    /// </summary>
    /// <param name="sql">Sql Statement to be added</param>
    public void AddSql(string sql)
    {
      _sqls.Add(sql);
    }

		/// <summary>
		/// Add a new DB Name.
		/// </summary>
		/// <param name="dbName">DB Name to be added</param>
		public void AddDBName(string dbName)
		{
			_dbNames.Add(dbName);
		}

    /// <summary>
    /// Clear all sql statements.
    /// </summary>
    public void Clear()
    {
      _sqls.Clear();
			_dbNames.Clear();
    }

    /// <summary>
    /// Execute all sql statements.
    /// </summary>
    /// <returns>Execution success status.</returns>
    public ICollection<int> ExecuteTransaction()
    {
      ICollection<int> rowsAffected;
      try
      {
        rowsAffected = _dbImpl.ExeTrans(_sqls, false);
      }
      catch (Exception ex)
      {
        throw ex;
      }
			//Call Clear from the caller code since Add was called from there
			//finally
			//{
			//  Clear();
			//}
      return rowsAffected;      
    }

    public ICollection<int> ExecuteTransaction(bool allowNoRowUpdate)
    {
      ICollection<int> rowsAffected;
      try
      {
        rowsAffected = _dbImpl.ExeTrans(_sqls, true);
      }
      catch (Exception ex)
      {
        throw ex;
      }
      //Call Clear from the caller code since Add was called from there
      //finally
      //{
      //  Clear();
      //}
      return rowsAffected;
    }

		/// <summary>
		/// Execute all sql statements.
		/// </summary>
		/// <returns>Execution success status.</returns>
		public ICollection<int> Execute2PTransaction()
		{
			ICollection<int> rowsAffected;
			try
			{
				rowsAffected = RadDBImpl.ExeTrans(_sqls, _dbNames);
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return rowsAffected;
		}


		public ICollection<string> GetSql()
		{
			return _sqls;
		}



    #endregion

    #region Internal Functionality
    #endregion
  }
}
