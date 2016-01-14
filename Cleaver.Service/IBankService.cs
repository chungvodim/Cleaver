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
        string Transfer(int id);

        [OperationContract]
        [FaultContract(typeof(ProductFault))]
        [WebInvoke(Method = "GET", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        void GetBalance(int id);
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
    public class TransferTask
    {
        public string ID { get; set; }
        public string AccountName { get; set; }
        public string CreditAccount { get; set; }
        public decimal Amount { get; set; }
        public string Content { get; set; }
        /// <summary>
        /// UTC +0
        /// </summary>
        public DateTime UpdateTime { get; set; }
        public string ToBankID { get; set; }
    }
}
