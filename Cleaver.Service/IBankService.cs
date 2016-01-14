using Cleaver.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Cleaver.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IBankService" in both code and config file together.
    [ServiceContract]
    public interface IBankService
    {
        [OperationContract]
        [FaultContract(typeof(ProductFault))]
        [WebInvoke(Method = "GET", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        void Transfer(TransferTask task);
        [OperationContract]
        [FaultContract(typeof(ProductFault))]
        [WebInvoke(Method = "GET", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        void GetBalance(TransferTask task);
    }

    [DataContract]
    public class ProductFault
    {
        public ProductFault(string msg)
        {
            FaultMessage = msg;
        }

        [DataMember]
        public string FaultMessage;
    }
}
