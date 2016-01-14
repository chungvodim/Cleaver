using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
//using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cleaver.Utils
{
    public class CleaverDebugger
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        //private static TextWriterTraceListener myListener = new TextWriterTraceListener("Info.svclog");
        public CleaverDebugger()
        {
            //System.Diagnostics.Debug.Listeners.Add(myListener);         
            //System.Diagnostics.Trace.Listeners.Add(myListener);
            //System.Diagnostics.Debug.WriteLine("Error");
            //System.Diagnostics.Debug.Flush();
            //t.WriteLine(DateTime.Now.ToString() + " > " + "Error");
            //t.Flush();
        }
        public static void Info(object obj)
        {
            _logger.Info(obj);
        }
        public static void Info(string msg)
        {
            //String path = Directory.GetCurrentDirectory();
            DateTime datetime = DateTime.Now;
            String fileName = "Cleaver_" + datetime.ToString("dd_MM_yyyy") + ".Log";
            File.AppendAllText(fileName, datetime.ToString("MM-dd hh:mm") + " > " + msg);
            File.AppendAllText(fileName, "\n");
            //myListener.WriteLine("Sending trace information");
            //Trace.Flush();
            //myListener.Flush();
        }

        public static void Error(string msg)
        {
            _logger.Error(msg);
        }
        public static void Error(Exception ex)
        {
            _logger.Error(ex);
            //if (ex.GetType() == typeof(DbEntityValidationException))
            //{
            //    foreach (var eve in (ex as DbEntityValidationException).EntityValidationErrors)
            //    {
            //        _logger.Error("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:", eve.Entry.Entity.GetType().Name, eve.Entry.State);
            //        foreach (var ve in eve.ValidationErrors)
            //        {
            //            _logger.Error("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage);
            //        }
            //    }
            //}
            //else
            //{
            //    _logger.Error(ex);
            //}
        }
        //public static void Log(String message)
        //{
        //    DateTime datetime = DateTime.Now;
        //    String fileName = "Cleaver_" + datetime.ToString("dd_MM_yyyy") + ".Log";
        //    if (!File.Exists(fileName))
        //    {
        //        System.IO.FileStream f = File.Create(fileName);
        //        f.Close();
        //    }

        //    try
        //    {
        //        System.IO.StreamWriter writter = File.AppendText(fileName);
        //        writter.WriteLine(datetime.ToString("MM-dd hh:mm") + " > " + message);
        //        writter.Flush();
        //        writter.Close();
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.Message.ToString());
        //    }
        //}
    }
}
