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
    public class WorkerStatus {

        public const int IN_PROCESS = 100;
        public const int SUCCESS = 200;

        public const int UNKNOWN = 0;
        public const int NETWORK_ERROR_BEFORE_OTP = 400;
        public const int INVALID_USERNAME_PASSWORD = 401;
        public const int ACCOUNT_BLOCKED = 402;
        public const int TRANSFER_TARGET_ACCOUNT_ERROR = 403;
        public const int NOT_ENOUGH_BALANCE_ERROR = 404;
        public const int LOGIN_CAPTCHA_ERROR = 405;
        public const int UNKNOWN_ERROR_BEFORE_OTP = 406;
        public const int UNKNOWN_ERROR_AFTER_OTP = 407;
        public const int OTP_INCORRECT = 408;
        public const int OTP_TIMEOUT_ERROR = 409;
        public const int OTP_CAPTCHA_ERROR = 410;
        public const int TRANSFER_SOURCE_ACCOUNT_ERROR = 411;
        public const int PARSE_RESULT_PAGE_ERROR = 412;
        public const int LOGIN_CAPTCHA_TIMEOUT = 413;
        public const int OTP_CAPTCHA_TIMEOUT = 414;
        public const int AMOUNT_ERROR = 415;
        public const int CAN_NOT_CONTINUE_MAKE_TRANSFER = 416;
        public const int BANK_PAGE_CHANGED = 418;
        public const int DOWNLOAD_OTP_CAPTCHA_ERROR = 419;
        public const int DOWNLOAD_LOGIN_CAPTCHA_ERROR = 420;
        public const int NETWORK_ERROR_AFTER_SUCCEED = 422;
        public const int NETWORK_ERROR_AFTER_OTP = 423;
        public const int INCORRECT_ACCOUNT_NAME = 424;
        public const int SESSION_TIMEOUT_BEFORE_SUCCEED = 425;
        public const int SESSION_TIMEOUT_AFTER_OTP = 426;
        public const int SESSION_TIMEOUT_AFTER_SUCCEED = 427;
        public const int UNABLE_ENQUIRE_BALANCE_AFTER_SUCCEED = 428;

        private static readonly DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(WorkerStatus));

        [DataMember(Name = "task")]
        public string TaskID { get; set; }

        [DataMember(Name = "total")]
        public int TotalSteps { get; set; }

        [DataMember(Name = "current")]
        public int CurrentStep { get; set; }

        [DataMember(Name = "desc")]
        public string Description { get; set; }

        [DataMember(Name = "code")]
        public int Code { get; set; }

        [DataMember(Name = "jobs")]
        public int Jobs { get; set; }

        public TransferResult Result { get; set; }

        public bool Success {
            get { return Code == SUCCESS; }
        }

        public string JsonString() {
            using (var ms = new MemoryStream()) {
                serializer.WriteObject(ms, this);
                return Encoding.UTF8.GetString(ms.ToArray());
            }
        }
    }
}
