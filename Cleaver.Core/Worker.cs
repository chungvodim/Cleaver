using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cleaver.Core
{
    public interface Worker
    {
        void Run(TransferTask task);
        void Enquire(TransferTask task);
    }
}
