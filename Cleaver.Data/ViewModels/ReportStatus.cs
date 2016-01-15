using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Cleaver.Data
{
    [DataContract]
    public class ReportStatus
    {
        [DataMember(Name = "status")]
        public int StatusCode { get; set; }
        [DataMember(Name = "message")]
        public string Description { get; set; }

        public bool IsSuccessful
        {
            get
            {
                return StatusCode == 1;
            }
        }
    }
}
