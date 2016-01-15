using Cleaver.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cleaver.Core;
using System.Net.Http;
using System.Net;
using Cleaver.Utils.Http;

namespace Cleaver.Core.Steps {

    public struct Result<TContext> {
        public bool IsSuccess { get; set; }
        public TContext Context { get; set; }
        public WorkerStatus Status { get; set; }

        public static Result<TContext> Success(TContext context) {
            return new Result<TContext> {
                IsSuccess = true,
                Context = context
            };
        }

        public static Result<TContext> Failure(string desc, int code) {
            return new Result<TContext> {
                IsSuccess = false,
                Status = new WorkerStatus {
                    Code = code,
                    Description = desc
                }
            };
        }
    }

    public delegate Result<TContext> Step<TContext>(TContext context);

    public class StepInfo {
        public int Retry = 1;
        public string Desc = string.Empty;
        public bool AfterSubmitTransfer = false;
        public bool Successful = false;
        public int DefaultError = WorkerStatus.UNKNOWN;

        public static StepInfo From<T>(Step<T> step) {
            var info = new StepInfo();
            var attrs = Attribute.GetCustomAttributes(step.Method).Where(a => a is BaseAttribute).Select(a => a as BaseAttribute);
            foreach (var attr in attrs) {
                attr.Apply(info);
            }
            if (info.DefaultError == 0) {
                info.DefaultError = info.AfterSubmitTransfer ? WorkerStatus.NETWORK_ERROR_AFTER_OTP : WorkerStatus.NETWORK_ERROR_BEFORE_OTP;
            }
            return info;
        }
    }

    public abstract class BaseAttribute : System.Attribute {
        public abstract void Apply(StepInfo info);
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class DefaultError : BaseAttribute {
        public int Code;
        public DefaultError(int code) {
            Code = code;
        }
        public override void Apply(StepInfo info) {
            info.DefaultError = Code;
        }
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class Retry : BaseAttribute {
        public const int DEFAULT_TIMES = 3;
        public int Times;
        public Retry() {
            Times = DEFAULT_TIMES;
        }
        public Retry(int times) {
            Times = times;
        }
        public override void Apply(StepInfo info) {
            info.Retry = Times;
        }
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class Description : BaseAttribute {
        public string Content;
        public Description(string content) {
            Content = content;
        }
        public override void Apply(StepInfo info) {
            info.Desc = Content;
        }
    }

    /// <summary>
    /// AfterSubmitTransfer attribute meaning we have submitted transfer to bank
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class AfterSubmitTransfer : BaseAttribute {
        public bool EnsureSuccessful = false;
        public AfterSubmitTransfer() { }
        public AfterSubmitTransfer(bool successful) {
            EnsureSuccessful = successful;
        }
        public override void Apply(StepInfo info) {
            info.AfterSubmitTransfer = true;
            info.Successful = EnsureSuccessful;
        }
    }

    public static class Helper {
        public static HttpClient CreateClient(CookieContainer cookies, int timeoutSec = 300) {
            var client = new HttpClient(new HttpClientHandler { CookieContainer = cookies, AllowAutoRedirect = true });
            client.Timeout = new TimeSpan(0, 0, timeoutSec);
            client.DefaultRequestHeaders.UserAgent.TryParseAdd(Request.UserAgents.Chrome);
            return client;
        }
    }
}
