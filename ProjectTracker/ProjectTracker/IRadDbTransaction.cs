using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Radiqal.Common
{
  public interface IRadDbTransaction
  {
    /// <summary>
    /// Adds transcation to current Trasaction object
    /// </summary>
    /// <param name="sql"></param>
    void AddSql(string sql);

    /// <summary>
    /// Executes set of transactions
    /// </summary>
    ICollection<int> ExecuteTransaction();

    ICollection<int> ExecuteTransaction(bool allowNoRowUpdate);

		ICollection<string> GetSql();

    /// <summary>
    /// Clears current transaction
    /// </summary>
    void Clear();
  }
}
