using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cleaver.Utils {
    public class RandomHelper {

        public static Random Generate() {
            return new Random((int)DateTime.Now.Ticks);
        }

        public static double Double() {
            return Generate().NextDouble();
        }

        public static bool Boolean() {
            return Generate().Next() % 2 == 0;
        }
    }
}
