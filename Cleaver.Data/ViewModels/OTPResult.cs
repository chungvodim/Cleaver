using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Cleaver.Data {
    [DataContract]
    public class OTPResult {

        private static readonly DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(OTPResult));

        [DataMember(Name = "status")]
        public int Status { get; set; }

        [DataMember(Name = "data")]
        public string Data { get; set; }

        public bool Error {
            get { return Status < 1; }
        }

        public static OTPResult From(string json) {
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(json))) {
                return serializer.ReadObject(stream) as OTPResult;
            }
        }
    }
}
