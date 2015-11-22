using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Principal;
using System.Diagnostics;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.EnterpriseLibrary.Logging.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Logging.ExtraInformation;
using Microsoft.Practices.EnterpriseLibrary.Logging.Filters;
using Microsoft.Practices.EnterpriseLibrary.Logging.Formatters;

using log4net;


namespace Radiqal.Common
{
	#region namespace RadLoggerDelegates
	namespace RadLoggerDelegates
	{
		//datatypes that can be used externally
		public delegate void ConveyErrorDelegate(Exception ex);
		public delegate void ApplAddlInfoDelegate(Dictionary<string, object> addlInfo);
	}
	#endregion

	public class SystemAuditLogger
	{
		// Create a logger for use in this class
		public static readonly object ClassLogger = (object)RadLogger.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		// NOTE that using System.Reflection.MethodBase.GetCurrentMethod().DeclaringType
		// is equivalent to typeof(Program) but is more portable
		// i.e. you can copy the code directly into another class without
		// needing to edit the code.	
	}

	public class DBAuditLogger
	{
		// Create a logger for use in this class
		public static readonly object ClassLogger = (object)RadLogger.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		// NOTE that using System.Reflection.MethodBase.GetCurrentMethod().DeclaringType
		// is equivalent to typeof(Program) but is more portable
		// i.e. you can copy the code directly into another class without
		// needing to edit the code.	
	}

	public class TM1AuditLogger
	{
		// Create a logger for use in this class
		public static readonly object ClassLogger = (object)RadLogger.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		// NOTE that using System.Reflection.MethodBase.GetCurrentMethod().DeclaringType
		// is equivalent to typeof(Program) but is more portable
		// i.e. you can copy the code directly into another class without
		// needing to edit the code.	
	}

	public class SMPMGuestInterfaceLogger
	{
		// Create a logger for use in this class
		public static readonly object ClassLogger = (object)RadLogger.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		// NOTE that using System.Reflection.MethodBase.GetCurrentMethod().DeclaringType
		// is equivalent to typeof(Program) but is more portable
		// i.e. you can copy the code directly into another class without
		// needing to edit the code.	
	}

  public class CMASDataAdaptorLogger
  {
    // Create a logger for use in this class
    public static readonly object ClassLogger = (object)RadLogger.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    // NOTE that using System.Reflection.MethodBase.GetCurrentMethod().DeclaringType
    // is equivalent to typeof(Program) but is more portable
    // i.e. you can copy the code directly into another class without
    // needing to edit the code.	
  }

	#region class RadLogger definition
	public class RadLogger
	{
    // Create a logger for use in this class
    private static readonly object classLogger = (object)RadLogger.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    // NOTE that using System.Reflection.MethodBase.GetCurrentMethod().DeclaringType
    // is equivalent to typeof(Program) but is more portable
    // i.e. you can copy the code directly into another class without
    // needing to edit the code.
		
		
		//Defined priorites
		//You can set minimum priority levels in App.config
		public enum Priorities
		{
			Min = 0,
			Max = 10
		}

    public static int MaxPriority
    {
      get { return Int32.Parse(Enum.Format(typeof(RadLogger.Priorities), RadLogger.Priorities.Max, "d")); }
    }

    public static int MinPriotiry
    {
      get { return Int32.Parse(Enum.Format(typeof(RadLogger.Priorities), RadLogger.Priorities.Min, "d")); }
    }

		//variable that can be set externally
		public static RadLoggerDelegates.ConveyErrorDelegate conveyError;

		//the default category to log the event
		private static ICollection<string> defCategories = new List<string>() { "General" };
		
		public static string SystemAuditCategory = "SystemAudit";
		public static string DBAuditCategory = "DBAudit";
		public static string TM1AuditCategory = "TM1Audit";
		public static string SMPMGuestInterfaceAuditCategory = "SMPMGuestInterface";    

		/// <summary>
		/// Displays current logger configuration
		/// </summary>
		public static String DispConfig()
		{
			//try
			//{
			//  // Get configuration settings for Logging and Instrmentation Application Block. 
			//  // This assumes the configuration source is the SystemConfigurationSource, which
			//  // is the default setting when the QuickStart ships.
			//  LoggingSettings settings = LoggingSettings.GetLoggingSettings(new SystemConfigurationSource());

			//  string defaultCategory = settings.DefaultCategory;

			//  StringBuilder results = new StringBuilder();

			//  results.Append("Current Configuration");
			//  results.Append(Environment.NewLine);
			//  results.Append("---------------------------------");
			//  results.Append(Environment.NewLine);
			//  results.Append(Environment.NewLine);
			//  results.Append("Default Category: " + settings.DefaultCategory + Environment.NewLine + Environment.NewLine);
			//  results.Append("Categories and category listeners");
			//  results.Append(Environment.NewLine);
			//  results.Append(Environment.NewLine);

			//  // Grab the list of categories and loop through for display.
			//  NamedElementCollection<TraceSourceData> sources = settings.TraceSources;

			//  foreach (TraceSourceData source in sources)
			//  {
			//    results.Append("   " + source.Name);

			//    // Flag any of the categories that would be denied based upon
			//    // the current category filter configuration.
			//    if (!Logger.GetFilter<CategoryFilter>().ShouldLog(source.Name))
			//    {
			//      results.Append("*");
			//    }

			//    // Loop through the list of trace listeners for the category.
			//    NamedElementCollection<TraceListenerReferenceData> TraceListeners = source.TraceListeners;

			//    StringBuilder listener = new StringBuilder();
			//    listener.Append("  -  ");
			//    foreach (TraceListenerReferenceData listenerData in TraceListeners)
			//    {
			//      listener.Append(listenerData.Name + ", ");
			//    }
			//    // Remove trailing comma and space
			//    listener.Remove(listener.Length - 2, 2);
			//    results.Append(listener.ToString());
			//    results.Append(Environment.NewLine);
			//  }
			//  results.Append(Environment.NewLine);
			//  results.Append("   * Events in category will not be logged");

			//  return results.ToString();
			//}
			//catch (Exception ex)
			//{
			//  if (conveyError != null)
			//  {
			//    conveyError(ex);
			//  }
			//}

			return null;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="appAddlInfo"></param>
		/// <returns></returns>
		private static Dictionary<string, object> ProcessAddlInfo(RadLoggerDelegates.ApplAddlInfoDelegate appAddlInfo)
		{
			////try getting standard additional info
			//AppDomain.CurrentDomain.SetPrincipalPolicy(PrincipalPolicy.WindowsPrincipal);
			//Dictionary<string, object> dictionary = new Dictionary<string, object>();
			//ManagedSecurityContextInformationProvider informationHelper = new ManagedSecurityContextInformationProvider();
			//informationHelper.PopulateDictionary(dictionary);
			//if (appAddlInfo != null)
			//{
			//  appAddlInfo(dictionary);
			//}
			//return dictionary;

			return null;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="evID"></param>
		/// <param name="evSeverity"></param>
		/// <param name="evPriority"></param>
		/// <param name="message"></param>
		/// <returns></returns>
		private static LogEntry CreateLogEntry(int evID, TraceEventType evSeverity, int evPriority, string message)
		{
			LogEntry logEntry = new LogEntry();
			logEntry.EventId = evID;
			logEntry.Priority = evPriority;
			logEntry.Severity = evSeverity;
			logEntry.Message = message;
			logEntry.Categories = defCategories;
			return logEntry;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="evID"></param>
		/// <param name="evSeverity"></param>
		/// <param name="evPriority"></param>
		/// <param name="message"></param>
		/// <param name="categories"></param>
		/// <returns></returns>
		private static LogEntry CreateLogEntry(int evID, TraceEventType evSeverity, int evPriority, string message, ICollection<string> categories)
		{
			string msg = string.Format("{0} {1}", DateTime.Now.ToString(), message);
			Console.WriteLine(msg);

			LogEntry logEntry = new LogEntry();
			logEntry.EventId = evID;
			logEntry.Priority = evPriority;
			logEntry.Severity = evSeverity;
			logEntry.Message = message;
			logEntry.Categories = categories;
			return logEntry;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="evID"></param>
		/// <param name="evSeverity"></param>
		/// <param name="evPriority"></param>
		/// <param name="message"></param>
		//public static void Write(int evID, TraceEventType evSeverity, int evPriority, string message)
		//{
		//  LogEntry logEntry = CreateLogEntry(evID, evSeverity, evPriority, message);
		//  Logger.Write(logEntry);
		//}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="evID"></param>
		/// <param name="evSeverity"></param>
		/// <param name="evPriority"></param>
		/// <param name="message"></param>
		/// <param name="categories"></param>

		public static void Write(int evID, TraceEventType evSeverity, int evPriority, string message, ICollection<string> categories)
		{ 
			Write(evID, evSeverity, evPriority, message, categories, null);
		}

		public static void Write(int evID, TraceEventType evSeverity, int evPriority, string message, ICollection<string> categories, Exception e)
		{
			//LogEntry logEntry = CreateLogEntry(evID, evSeverity, evPriority, message, categories);
			//Logger.Write(logEntry);

      if (categories != null && categories.Count > 0)
      {
        object log = null;

        if (categories.Contains(SystemAuditCategory))
        {
          log = SystemAuditLogger.ClassLogger;
        }
        else if (categories.Contains(DBAuditCategory))
        {
          log = DBAuditLogger.ClassLogger;
        }
        else if (categories.Contains(TM1AuditCategory))
        {
          log = TM1AuditLogger.ClassLogger;
        }
        else if (categories.Contains(SMPMGuestInterfaceAuditCategory))
        {
          log = SMPMGuestInterfaceLogger.ClassLogger;
        }
       
        if (log != null)
        {
          if (e == null)
            Write(log, evSeverity, message);
          else
            Write(log, evSeverity, message, e);
        }
      }
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="evID"></param>
		/// <param name="evSeverity"></param>
		/// <param name="evPriority"></param>
		/// <param name="message"></param>
		/// <param name="appAddlInfo"></param>
		//public static void Write(int evID, TraceEventType evSeverity, int evPriority, string message, RadLoggerDelegates.ApplAddlInfoDelegate appAddlInfo)
		//{
		//  LogEntry logEntry = CreateLogEntry(evID, evSeverity, evPriority, message);
		//  Logger.Write(logEntry, ProcessAddlInfo(appAddlInfo));
		//}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="evID"></param>
		/// <param name="evSeverity"></param>
		/// <param name="evPriority"></param>
		/// <param name="message"></param>
		/// <param name="categories"></param>
		/// <param name="appAddlInfo"></param>
		//public static void Write(int evID, TraceEventType evSeverity, int evPriority, string message, ICollection<string> categories, RadLoggerDelegates.ApplAddlInfoDelegate appAddlInfo)
		//{
		//  LogEntry logEntry = CreateLogEntry(evID, evSeverity, evPriority, message, categories);
		//  Logger.Write(logEntry, ProcessAddlInfo(appAddlInfo));
		//}

		/*
		public static void Write(int evID, TraceEventType evSeverity, Priorities priority, string message)
		{
			LogEntry logEntry = CreateLogEntry(evID, (int)priority, message);
			Logger.Write(logEntry);
		}

		public static void Write(int evID, TraceEventType evSeverity, Priorities priority, string message, ICollection<string> categories)
		{
			LogEntry logEntry = CreateLogEntry(evID, (int)priority, message, categories);
			Logger.Write(logEntry);
		}

		public static void Write(int evID, TraceEventType evSeverity, Priorities priority, string message, RadLoggerDelegates.ApplAddlInfoDelegate appAddlInfo)
		{
			LogEntry logEntry = CreateLogEntry(evID, (int)priority, message);
			Logger.Write(logEntry, ProcessAddlInfo(appAddlInfo));
		}

		public static void Write(int evID, TraceEventType evSeverity, Priorities priority, string message, ICollection<string> categories, RadLoggerDelegates.ApplAddlInfoDelegate appAddlInfo)
		{
			LogEntry logEntry = CreateLogEntry(evID, (int)priority, message, categories);
			Logger.Write(logEntry, ProcessAddlInfo(appAddlInfo));
		}

    
		public static void Debug(int evID, string message)
		{
			Write(evID, Priorities.Debug, message);
		}
    
		public static void Debug(int evID, string message, ICollection<string> categories)
		{
			Write(evID, Priorities.Debug, message, categories);
		}

		public static void Debug(int evID, string message, RadLoggerDelegates.ApplAddlInfoDelegate appAddlInfo)
		{
			Write(evID, Priorities.Debug, message, appAddlInfo);
		}

		public static void Debug(int evID, string message, ICollection<string> categories, RadLoggerDelegates.ApplAddlInfoDelegate appAddlInfo)
		{
			Write(evID, Priorities.Debug, message, categories, appAddlInfo);
		}

		public static void Info(int evID, string message)
		{
			Write(evID, Priorities.Info, message);
		}

		public static void Info(int evID, string message, ICollection<string> categories)
		{
			Write(evID, Priorities.Info, message, categories);
		}

		public static void Info(int evID, string message, RadLoggerDelegates.ApplAddlInfoDelegate appAddlInfo)
		{
			Write(evID, Priorities.Info, message, appAddlInfo);
		}

		public static void Info(int evID, string message, ICollection<string> categories, RadLoggerDelegates.ApplAddlInfoDelegate appAddlInfo)
		{
			Write(evID, Priorities.Info, message, categories, appAddlInfo);
		}

		public static void Trace(int evID, string message)
		{
			Write(evID, Priorities.Trace, message);
		}

		public static void Trace(int evID, string message, ICollection<string> categories)
		{
			Write(evID, Priorities.Trace, message, categories);
		}

		public static void Trace(int evID, string message, RadLoggerDelegates.ApplAddlInfoDelegate appAddlInfo)
		{
			Write(evID, Priorities.Trace, message, appAddlInfo);
		}

		public static void Trace(int evID, string message, ICollection<string> categories, RadLoggerDelegates.ApplAddlInfoDelegate appAddlInfo)
		{
			Write(evID, Priorities.Trace, message, categories, appAddlInfo);
		}

		public static void Warn(int evID, string message)
		{
			Write(evID, Priorities.Warn, message);
		}

		public static void Warn(int evID, string message, ICollection<string> categories)
		{
			Write(evID, Priorities.Warn, message, categories);
		}

		public static void Warn(int evID, string message, RadLoggerDelegates.ApplAddlInfoDelegate appAddlInfo)
		{
			Write(evID, Priorities.Warn, message, appAddlInfo);
		}

		public static void Warn(int evID, string message, ICollection<string> categories, RadLoggerDelegates.ApplAddlInfoDelegate appAddlInfo)
		{
			Write(evID, Priorities.Warn, message, categories, appAddlInfo);
		}

		public static void Fatal(int evID, string message)
		{
			Write(evID, Priorities.Fatal, message);
		}

		public static void Fatal(int evID, string message, ICollection<string> categories)
		{
			Write(evID, Priorities.Fatal, message, categories);
		}

		public static void Fatal(int evID, string message, RadLoggerDelegates.ApplAddlInfoDelegate appAddlInfo)
		{
			Write(evID, Priorities.Fatal, message, appAddlInfo);
		}

		public static void Fatal(int evID, string message, ICollection<string> categories, RadLoggerDelegates.ApplAddlInfoDelegate appAddlInfo)
		{
			Write(evID, Priorities.Fatal, message, categories, appAddlInfo);
		}

		public static void Error(int evID, string message)
		{
			Write(evID, Priorities.Error, message);
		}

		public static void Error(int evID, string message, ICollection<string> categories)
		{
			Write(evID, Priorities.Error, message, categories);
		}

		public static void Error(int evID, string message, RadLoggerDelegates.ApplAddlInfoDelegate appAddlInfo)
		{
			Write(evID, Priorities.Error, message, appAddlInfo);
		}

		public static void Error(int evID, string message, ICollection<string> categories, RadLoggerDelegates.ApplAddlInfoDelegate appAddlInfo)
		{
			Write(evID, Priorities.Error, message, categories, appAddlInfo);
		}
		 * */



		public static ILog GetLogger(Type loggerType)
		{
			return LogManager.GetLogger(loggerType);
		}

		public static void Write(object log, TraceEventType evSeverity, string message, Exception exception)
		{
			if (evSeverity == TraceEventType.Verbose)
				((log4net.ILog)log).Debug(message, exception);
			else if (evSeverity == TraceEventType.Information)
				((log4net.ILog)log).Info(message, exception);
			else if (evSeverity == TraceEventType.Warning)
				((log4net.ILog)log).Warn(message, exception);
			else if (evSeverity == TraceEventType.Error)
				((log4net.ILog)log).Error(message, exception);
			else if (evSeverity == TraceEventType.Critical)
				((log4net.ILog)log).Fatal(message, exception);
			else
				((log4net.ILog)log).Info(message, exception);
		}

		public static void Write(object log, TraceEventType evSeverity, string message)
		{
			if (evSeverity == TraceEventType.Verbose)
				((log4net.ILog)log).Debug(message);
			else if (evSeverity == TraceEventType.Information)
				((log4net.ILog)log).Info(message);
			else if (evSeverity == TraceEventType.Warning)
				((log4net.ILog)log).Warn(message);
			else if (evSeverity == TraceEventType.Error)
				((log4net.ILog)log).Error(message);
			else if (evSeverity == TraceEventType.Critical)
				((log4net.ILog)log).Fatal(message);
			else
				((log4net.ILog)log).Info(message);
		}

		public static void Write(object log, int evID, TraceEventType evSeverity, string message, Exception exception)
		{
			Write(log, evSeverity, string.Format("{0}\t{1}", evID, message), exception);
		}

		public static void Write(object log, int evID, TraceEventType evSeverity, string message)
		{
			Write(log, evSeverity, string.Format("{0}\t{1}", evID, message));
		}

		public static void Write(object log, int evID, TraceEventType evSeverity, int evPriority, string message, Exception exception)
		{
			Write(log, evSeverity, string.Format("{0}\t{1}\t{2}", evID, evPriority, message), exception);
		}

		public static void Write(object log, int evID, TraceEventType evSeverity, int evPriority, string message)
		{
			Write(log, evSeverity, string.Format("{0}\t{1}\t{2}", evID, evPriority, message));
		}
	
	}
	#endregion
}
