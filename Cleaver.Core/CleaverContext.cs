using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Cleaver.Core {
    public class CleaverContext {
        public const string ACTOR_SYSTEM_NAME = "TransferServer";
        public const string ACTOR_HUB_NAME = "Hub";
        public static readonly string ACTOR_HUB_PATH = string.Format("akka://{0}/user/{1}", ACTOR_SYSTEM_NAME, ACTOR_HUB_NAME);

        public static string REMOTE_SERVER;
        public static string CAPTCHA_RECOGNIZE_SERVICE;
        public static string KEYPAD_RECOGNIZE_SERVICE;
        public static string LAST_PAGE_REPORT_SERVICE;
        public static string OTP_FETCH_SERVICE;
        public static string CARD_OTP_FETCH_SERVICE;
        public static string STATUS_REPORT;
        public static string UPDATE_BALANCE;
        public static string ENQUIRE_FAIL;

        public static string PORT;
        public static string USERNAME;
        public static string PASSWORD;
        public static string ACCOUNT_NUMBER;
        public static string OTP_PHONE_NUMBER;

        public static string WORKER_ASSEMBLY;
        public static string WORKER_CLASSNAME;

        private static Type _workerType;
        private static Dictionary<string, object> _cache;

        public static bool Initialize() {

            REMOTE_SERVER = ConfigurationManager.AppSettings["REMOTE_SERVER"];

            CAPTCHA_RECOGNIZE_SERVICE = string.Format("{0}/captcha/upload", REMOTE_SERVER);
            KEYPAD_RECOGNIZE_SERVICE = string.Format("{0}/keypad/upload", REMOTE_SERVER);
            LAST_PAGE_REPORT_SERVICE = string.Format("{0}/progress/updatelastpage", REMOTE_SERVER);

            STATUS_REPORT = REMOTE_SERVER + "/progress/update";
            UPDATE_BALANCE = REMOTE_SERVER + "/balance/update";
            ENQUIRE_FAIL = REMOTE_SERVER + "/balance/fail/";            

            PORT = ConfigurationManager.AppSettings["PORT"];
            USERNAME = ConfigurationManager.AppSettings["USERNAME"];
            PASSWORD = ConfigurationManager.AppSettings["PASSWORD"];
            ACCOUNT_NUMBER = ConfigurationManager.AppSettings["ACCOUNT_NUMBER"];

            OTP_PHONE_NUMBER = ConfigurationManager.AppSettings["PHONE_NUMBER"];
            OTP_FETCH_SERVICE = string.Format("{0}/sms/fetch/{1}", REMOTE_SERVER, OTP_PHONE_NUMBER);
            CARD_OTP_FETCH_SERVICE = string.Format("{0}/card/fetch/{1}", REMOTE_SERVER, ACCOUNT_NUMBER);

            WORKER_ASSEMBLY = ConfigurationManager.AppSettings["WORKER_ASSEMBLY"];
            WORKER_CLASSNAME = ConfigurationManager.AppSettings["WORKER_CLASSNAME"];

            _cache = new Dictionary<string, object>();

            return LoadWorker();
        }

        private static bool LoadWorker() {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, CleaverContext.WORKER_ASSEMBLY);
            if (!File.Exists(path)) {
                return false;
            }
            try {
                var assembly = Assembly.LoadFile(path);
                _workerType = (from type in assembly.GetExportedTypes()
                               where type.FullName == CleaverContext.WORKER_CLASSNAME
                               select type).FirstOrDefault();
                return _workerType != null;
            } catch (Exception err) {
                Console.Error.WriteLine(err.ToString());
            }
            return false;
        }

        public static Worker NewWorker() {
            if (_workerType == null)
                return null;
            return Activator.CreateInstance(_workerType) as Worker;
        }

        public static void SetCache(string key, object value) {
            _cache[key] = value;
        }

        public static T GetCache<T>(string key) {
            if (_cache.ContainsKey(key)) {
                return (T)_cache[key];
            }
            return default(T);
        }
    }
}
