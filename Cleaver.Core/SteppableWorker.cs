using Cleaver.Data;
using Cleaver.Core.Steps;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Cleaver.Utils.Http;
using Cleaver.Utils;

namespace Cleaver.Core.Steps {

    public abstract class SteppableWorker<T> : Worker {

        public delegate void UpdateProgressHandler(int step, string desc, int code);

        protected Logger logger = LogManager.GetCurrentClassLogger();

        public Result<T> Retry(T ctx, TransferTask task, Step<T> step, StepInfo info) {
            var result = Fail(info.Desc, WorkerStatus.UNKNOWN);
            var times = info.Retry;
            while (times > 0) {
                times--;
                logger.Info("TaskID-{0} try operation {1} at {2} times", task.ID, info.Desc, info.Retry - times);
                try {
                    result = step.Invoke(ctx);
                    break;
                } catch (AggregateException err) {
                    result = Fail(err.ToString(), info.DefaultError);
                } catch (Exception err) {
                    result = Fail(err.ToString(), info.AfterSubmitTransfer ? WorkerStatus.UNKNOWN_ERROR_AFTER_OTP : WorkerStatus.UNKNOWN_ERROR_BEFORE_OTP);
                }
            }
            return result;
        }

        public Result<T> RunSteps(TransferTask task, Step<T>[] steps, UpdateProgressHandler update) {
            var ctx = CreateContext(task);
            var result = Fail("unknown", WorkerStatus.UNKNOWN);
            for (var i = 0; i < steps.Length; i++) {
                var step = steps[i];
                var stepNo = i + 1;
                var info = StepInfo.From<T>(step);
                update(stepNo, info.Desc, WorkerStatus.IN_PROCESS);
                result = Retry(ctx, task, step, info);     
           
                //???
                try
                {
                    result.Status.CurrentStep = stepNo;
                }
                catch (Exception)
                {
                    //skip
                }

                if (!result.IsSuccess) {
                    break;
                }
            }

            Dispose(ctx, result);

            return result;
        }

        public void Run(TransferTask task) {
            var steps = AllSteps();
            var total = steps.Length;
            var r = RunSteps(task, steps, (step, desc, code) => UpdateStatus(step, total, task, desc, code, null));

            if (r.IsSuccess) {
                UpdateStatus(total, total, task, "Tansfer succeeded", WorkerStatus.SUCCESS, r.Status.Result);
                //hub.Tell(new TaskSucceededMessage(task));
            } else {
                UpdateStatus(r.Status.CurrentStep, total, task, r.Status.Description, r.Status.Code, null);
                //hub.Tell(new TaskFailedMessage(task));
            }
        }

        public void Enquire(TransferTask task) {
            throw new NotImplementedException();
        }

        private void UpdateStatus(int currStep, int totalStep, TransferTask task, string desc, int code, TransferResult result) {
            logger.Info("TaskID-{0} Step'{1}/{2}' Desc'{3}' Code'{4}'", task.ID, currStep, totalStep, desc, code);
            //hub.Tell(new UpdateWorkerStatusMessage(new WorkerStatus {
            //    TaskID = task.ID,
            //    CurrentStep = currStep,
            //    TotalSteps = totalStep,
            //    Description = desc,
            //    Code = code,
            //    Result = result ?? null
            //}));
        }

        protected Result<T> Pass(T context) {
            return Result<T>.Success(context);
        }

        protected Result<T> Success(T context, TransferResult result) {
            var r = Pass(context);
            r.Status = new WorkerStatus {
                Code = WorkerStatus.SUCCESS,
                Description = "Transfer success",
                Result = result
            };
            return r;
        }

        protected Result<T> Fail(string desc, int code) {
            return Result<T>.Failure(desc, code);
        }

        public abstract T CreateContext(TransferTask task);

        public abstract Step<T>[] AllSteps();
        public virtual void Dispose(T context, Result<T> result) { }
        public async Task<OTPResult> FetchOTPAsync(HttpClient client, string otpUrl)
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

        public ReportStatus ReportFinalPage(HttpClient client, string reportUrl, string transactionId, string lastPage, Encoding encoding)
        {
            var pp = new Parameters();
            pp["commandId"] = transactionId;
            pp["content"] = lastPage;

            using (var rsp = client.PostAsync(reportUrl, pp.ToFormEncodedData(encoding)).Result)
            {
                var page = rsp.Content.ReadAsStringAsync().Result;

                return Serializer.DeserializeJsonObject<ReportStatus>(page, encoding);
            }
        }
    }
}
