using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Cleaver.Utils.Http {

    public class Request : IDisposable {

        private HttpClient _client;
        private HttpRequestMessage _requestMessage;

        private Request(HttpRequestMessage requestMessage, CookieContainer cookieContainer, bool autoRedirect, IWebProxy proxy, TimeSpan timeout) {
            _requestMessage = requestMessage;
            _client = new HttpClient(new HttpClientHandler { CookieContainer = cookieContainer, AllowAutoRedirect = autoRedirect, Proxy = proxy, UseProxy = proxy != null });
        }

        public Task<HttpResponseMessage> SendAsync() {
            return _client.SendAsync(_requestMessage);
        }

        public void Dispose() {
            _requestMessage.Dispose();
            _client.Dispose();
        }

        public class MediaTypes {
            public const string TextualData = "text/plain";
            public const string FormEncodedData = "application/x-www-form-urlencoded";
            public const string JavaScriptObjectNotation = "application/json";
        }

        public class UserAgents {
            public const string Chrome = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/37.0.2062.120 Safari/537.36";
        }

        public class Builder {

            private HttpRequestMessage requestMessage;
            private CookieContainer cookieContainer;
            private bool autoRedirect = true;
            private string requestUri;
            private NameValueCollection queries;
            private IWebProxy proxy;
            private TimeSpan timeout;

            public Builder() {
                requestMessage = new HttpRequestMessage();
                requestMessage.Method = HttpMethod.Get;
                timeout = new TimeSpan(0, 0, 100);
                Header("User-Agent", Request.UserAgents.Chrome);
            }

            public Builder Timeout(TimeSpan timeout) {
                this.timeout = timeout;
                return this;
            }

            public Builder Timeout(int seconds) {
                return Timeout(new TimeSpan(0, 0, seconds));
            }

            public Builder Url(string url) {
                requestUri = url;
                return this;
            }

            public Builder Parameter(string name, object value) {
                if (queries == null) {
                    queries = new NameValueCollection();
                }
                queries[name] = value.ToString();
                return this;
            }

            public Builder Header(string name, string value) {
                if (!requestMessage.Headers.Contains(name))
                    requestMessage.Headers.Add(name, value);
                return this;
            }

            public Builder Header(string name, IEnumerable<string> values) {
                requestMessage.Headers.Add(name, values);
                return this;
            }

            public Builder Cookie(CookieContainer cookies) {
                cookieContainer = cookies;
                return this;
            }

            public Builder Get() {
                requestMessage.Method = HttpMethod.Get;
                return this;
            }

            public Builder Post() {
                if (queries == null)
                    return Post(string.Empty);
                return Post(queries.ToQueryString(Encoding.UTF8));
            }

            public Builder Post(object param) {
                return Post(param.ToQueryString(Encoding.UTF8));
            }

            public Builder Post(object param, Encoding encoding) {
                return Post(param.ToQueryString(Encoding.UTF8), encoding);
            }

            public Builder Post(object param, Encoding encoding, string mediaType) {
                return Post(param.ToQueryString(Encoding.UTF8), encoding, mediaType);
            }

            public Builder Post(byte[] body) {
                return Post(body, MediaTypes.TextualData);
            }

            public Builder Post(byte[] body, string mediaType) {
                return Post(body, mediaType, "utf-8");
            }

            public Builder Post(byte[] body, string mediaType, string charSet) {
                requestMessage.Method = HttpMethod.Post;
                requestMessage.Content = new ByteArrayContent(body);
                requestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue(mediaType) {
                    CharSet = charSet
                };
                return this;
            }

            public Builder Post(string body) {
                return Post(body, Encoding.UTF8);
            }

            public Builder Post(string body, Encoding encoding) {
                return Post(body, encoding, MediaTypes.FormEncodedData);
            }

            public Builder Post(string body, Encoding encoding, string mediaType) {
                requestMessage.Method = HttpMethod.Post;
                requestMessage.Content = new StringContent(body, encoding, mediaType);
                return this;
            }

            public Builder AutoRedirect(bool allow) {
                autoRedirect = allow;
                return this;
            }

            public Builder Proxy(IWebProxy proxy) {
                this.proxy = proxy;
                return this;
            }

            public Request Build() {
                if (cookieContainer == null)
                    cookieContainer = new CookieContainer();
                if (requestMessage.Method == HttpMethod.Get && queries != null) {
                    requestMessage.RequestUri = new Uri(requestUri + "?" + queries.ToQueryString(Encoding.UTF8));
                } else {
                    requestMessage.RequestUri = new Uri(requestUri);
                }
                return new Request(requestMessage, cookieContainer, autoRedirect, proxy, timeout);
            }
        }
    }
}
