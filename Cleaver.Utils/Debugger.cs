using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cleaver.Utils
{
    public class Debugger
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public static void Info(object obj)
        {
            _logger.Info(obj);
        }
        public static void Info(string msg)
        {
            //String path = Directory.GetCurrentDirectory();
            Log(msg, "[INFO]");
        }
        public static void Error(string msg)
        {
            Log(msg,"[ERROR]");
        }
        public static void Error(Exception ex)
        {
            if (ex.GetType() == typeof(DbEntityValidationException))
            {
                foreach (var eve in (ex as DbEntityValidationException).EntityValidationErrors)
                {
                    Error(string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:", eve.Entry.Entity.GetType().Name, eve.Entry.State));
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Error(string.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage));
                    }
                }
            }
            else
            {
                Error(ex.Message);
            }
        }
        private static void Log(string msg, string prefix)
        {
            DateTime datetime = DateTime.Now;
            String fileName = "Cleaver_" + datetime.ToString("dd_MM_yyyy") + ".Log";
            File.AppendAllText(fileName, prefix + datetime.ToString("MM-dd hh:mm") + " > " + msg);
            File.AppendAllText(fileName, "\n");
        }
    }
}
