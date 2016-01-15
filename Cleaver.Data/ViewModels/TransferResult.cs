using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cleaver.Data {
    public interface TransferResult {
        string Balance { get; }
        string JsonString();
        bool Success { get; }
    }
}
