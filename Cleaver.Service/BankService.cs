using Cleaver.Utils;
using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Cleaver.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "BankService" in both code and config file together.
    public class BankService : IBankService
    {
        public string Transfer(int id)
        {
            try
            {
                Task.Run(() => {
                    CleaverDebugger.Info("hehehee");
                    } 
                );
                return "";
            }
            catch (Exception ex)
            {
                throw new FaultException<ProductFault>(new ProductFault(ex.Message), "unknown reason");
            }
        }

        public void GetBalance(int id)
        {
            try
            {
                Cleaver.Utils.CleaverDebugger.Info(id);
            }
            catch (Exception ex)
            {
                throw new FaultException<ProductFault>(new ProductFault(ex.Message), "unknown reason");
            }
        }
    }
}
