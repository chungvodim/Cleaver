using Cleaver.Utils;
using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace Cleaver.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "BankService" in both code and config file together.
    public class BankService : IBankService
    {
        public string Transfer(string id, string accountNumber, string accountName, decimal amount, string w88TransId, string toBankId)
        {
            try
            {
                var x = 0;
                var y = 0;
                var z = x / y;
                return string.Join("-", id, accountNumber, accountName, amount, w88TransId, toBankId);
            }
            //catch (Exception ex)
            //{
            //    //throw new FaultException<ProductFault>(new ProductFault(ex.Message), "unknown reason");
            //    throw new FaultException(ex.Message, new FaultCode("internal error"));// Use it for SOAP
            //}
            catch
            {
                
                throw new WebFaultException(HttpStatusCode.BadRequest);// Use it for REST
            }
        }

        public string GetBalance(string id, string accountNumber, string accountName, decimal amount, string w88TransId, string lastUpdateTime)
        {
            try
            {
                return string.Join("-",id, accountNumber, accountName, amount, w88TransId, lastUpdateTime);
            }
            //catch (Exception ex)
            //{
            //    throw new FaultException(ex.Message, new FaultCode("internal error"));// Use it for SOAP
            //}
            catch
            {
                throw new WebFaultException(HttpStatusCode.BadRequest);// Use it for REST
            }
        }
    }
}
