using Cleaver.Core.Domains;
using Cleaver.Utils.Http;
using Cleaver.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Cleaver.Utils
{
    [DataContract]
    public class ReportStatus
    {
        [DataMember(Name="status")]
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

    public class CleaverUtils
    {
        public static ParseResult ParseValue(string pattern, string content)
        {
            var regex = new Regex(pattern);
            var match = regex.Match(content);

            if(match.Success)
            {
                return new ParseResult() { IsSuccessful = true, Value = match.Groups[1].Value };
            }
            else
                return new ParseResult() { IsSuccessful = false };
        }

        public static bool ParseHiddenFields(string pattern, string content, out Parameters p)
        {
            p = new Parameters();

            var regex = new Regex(pattern);
            foreach(Match match in regex.Matches(content))
            {
                if (!match.Success || match.Groups.Count != 3)
                    return false;

                p[match.Groups[1].Value] = match.Groups[2].Value;
            }

            return true;
        }

        public static T DeserializeJsonObject<T>(string content, Encoding encoding)
        {
            using (Stream stream = new MemoryStream())
            {
                byte[] data = encoding.GetBytes(content);
                stream.Write(data, 0, data.Length);
                stream.Position = 0;
                var deserializer = new DataContractJsonSerializer(typeof(T));
                return (T)deserializer.ReadObject(stream);
            }
        }

        public static async Task<OTPResult> FetchOTPAsync(HttpClient client, string otpUrl)
        {
            try
            {
                using (var rsp = await client.GetAsync(otpUrl))
                {
                    return OTPResult.From(rsp.Content.ReadAsStringAsync().Result);
                }
            }
            catch { }
            return new OTPResult();
        }

        public static ReportStatus ReportFinalPage(HttpClient client, string reportUrl, string transactionId, string lastPage, Encoding encoding)
        {
            var pp = new Parameters();
            pp["commandId"] = transactionId;
            pp["content"] = lastPage;

            using(var rsp = client.PostAsync(reportUrl, pp.ToFormEncodedData(encoding)).Result)
            {
                var page = rsp.Content.ReadAsStringAsync().Result;

                return DeserializeJsonObject<ReportStatus>(page, encoding);
            }
        }
    }
}
