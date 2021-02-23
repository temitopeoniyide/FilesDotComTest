using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TopeyPay.Shared.Contracts
{
   public interface ILogWriter
    {
        string LogWrite(string message);
    }
}
