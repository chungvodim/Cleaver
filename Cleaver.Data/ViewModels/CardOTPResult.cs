using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Cleaver.Data
{
    [DataContract]
    public class CardOTPResult
    {

        private static readonly DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(CardOTPResult));

        [DataMember(Name = "status")]
        public int Status { get; set; }

        [DataMember(Name = "card1")]
        public string Card1 { get; set; }

        [DataMember(Name = "card2")]
        public string Card2 { get; set; }

        public bool Error
        {
            get { return Status < 1; }
        }

        public static CardOTPResult From(string json)
        {
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {
                return serializer.ReadObject(stream) as CardOTPResult;
            }
        }
    }
}
