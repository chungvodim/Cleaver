using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cleaver.Utils {
    public static class DateUtils {

        private const long offset = 621355968000000000L;

        public static DateTime FromUnixTimeStamp(long unix) {
            return new DateTime(offset + unix * TimeSpan.TicksPerMillisecond, DateTimeKind.Utc);
        }

    }
}
