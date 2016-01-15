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
    //[ServiceContract(Namespace = "http://adventure-works.com/2010/07/28", Name = "BankService")]
    [ServiceContract]
    public interface IBankService
    {
        //[OperationContract(IsOneWay = true)]
        [OperationContract]
        [FaultContract(typeof(ProductFault))]
        //[WebGet(UriTemplate = "/Transfer?id={id}&accountname={accountname}")]
        [WebInvoke(UriTemplate = "/Transfer?id={id}&ca={accountNumber}&n={accountName}&a={amount}&c={w88TransId}&tb={toBankId}", Method = "GET", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        string Transfer(string id, string accountNumber, string accountName, decimal amount, string w88TransId, string toBankId);

        [OperationContract]
        [FaultContract(typeof(ProductFault))]
        [WebInvoke(UriTemplate = "/GetBalance?id={id}&ca={accountNumber}&n={accountName}&a={amount}&c={w88TransId}&t={lastUpdateTime}", Method = "GET", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        string GetBalance(string id, string accountNumber, string accountName, decimal amount, string w88TransId, string lastUpdateTime);

        //[OperationContract(AsyncPattern = true)]
        //IAsyncResult BeginCalculateTotalValueOfStock(string id, AsyncCallback cb,
        //object s);
        //int EndCalculateTotalValueOfStock(IAsyncResult r);
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
        public string AccountNumber { get; set; }
        public decimal Amount { get; set; }
        public string W88TransId { get; set; }
        /// <summary>
        /// UTC +0
        /// </summary>
        public DateTime UpdateTime { get; set; }
        public string ToBankID { get; set; }
    }
}
