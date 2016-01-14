using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cleaver.Core
{
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
