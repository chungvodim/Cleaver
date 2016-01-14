using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Cleaver.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "BankService" in both code and config file together.
    public class BankService : IBankService
    {
        public void Transfer(Core.TransferTask task)
        {
            try
            {

            }
            catch (Exception ex)
            {
                throw new FaultException<ProductFault>(new ProductFault(ex.Message), "unknown reason");
            }
        }

        public void GetBalance(Core.TransferTask task)
        {
            try
            {

            }
            catch (Exception ex)
            {
                throw new FaultException<ProductFault>(new ProductFault(ex.Message), "unknown reason");
            }
        }
    }
}
