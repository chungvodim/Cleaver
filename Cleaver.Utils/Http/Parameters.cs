using Cleaver.Utils;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Cleaver.Utils.Http {
    public class Parameters : Dictionary<string, object> {
        public StringContent ToFormEncodedData() {
            return ToFormEncodedData(Encoding.UTF8);
        }

        public StringContent ToFormEncodedData(Encoding encoding)
        {
            return HttpUtils.CreateFormEncodedData(this, encoding);
        }
    }
}
