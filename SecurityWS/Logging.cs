using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Radiqal.Common;

namespace Radiqal.Common
{
  public class Logging
  {
    public static void WriteMessage(string msg)
    {
      RadLogger.Write(1, System.Diagnostics.TraceEventType.Information, RadLogger.MaxPriority, msg, new List<string>() { RadLogger.SystemAuditCategory });
    }

    public static void WriteError(string msg)
    {
      RadLogger.Write(2, System.Diagnostics.TraceEventType.Error, RadLogger.MaxPriority, msg, new List<string>() { RadLogger.SystemAuditCategory });
    }
  }
}
