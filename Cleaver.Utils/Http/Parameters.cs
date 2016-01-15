using Cleaver.Utils;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Cleaver.Utils.Http {
    public class Parameters : Dictionary<string, object> {
        public StringContent ToFormEncodedData() {
            return ToFormEncodedData(Encoding.UTF8);
        }

        public StringContent ToFormEncodedData(Encoding encoding)
        {
            return CreateFormEncodedData(this, encoding);
        }
        public static MultipartFormDataContent CreateMultipartContent(Stream stream, string name, string subfix)
        {
            var content = new MultipartFormDataContent();
            var image = new StreamContent(stream);
            image.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
            {
                Name = name,
                FileName = string.Format("\"{0}.{1}\"", name, subfix)
            };
            content.Add(image);
            return content;
        }

        public static StringContent CreateFormEncodedData(NameValueCollection keyValues)
        {
            return CreateFormEncodedData(keyValues, Encoding.UTF8);
        }

        public static StringContent CreateFormEncodedData(NameValueCollection keyValues, Encoding encoding)
        {
            return CreateFormEncodedData(keyValues.ToQueryString(encoding));
        }

        public static StringContent CreateFormEncodedData(string queryString)
        {
            return CreateFormEncodedData(queryString, Encoding.UTF8);
        }

        public static StringContent CreateFormEncodedData(string queryString, Encoding encoding)
        {
            return new StringContent(queryString, encoding, Request.MediaTypes.FormEncodedData);
        }

        internal static StringContent CreateFormEncodedData(Parameters parameters)
        {
            return CreateFormEncodedData(parameters, Encoding.UTF8);
        }

        internal static StringContent CreateFormEncodedData(Parameters parameters, Encoding encoding)
        {
            return CreateFormEncodedData(parameters.ToQueryString(encoding));
        }
    }
}
