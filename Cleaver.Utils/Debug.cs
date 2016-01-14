using NLog;
using System;
using System.Collections.Generic;
//using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cleaver.Utils
{
    public class Debug
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        public static void Info(object obj)
        {
            _logger.Info(obj);
        }
        public static void Info(string msg)
        {
            _logger.Info(msg);
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
    }
}
