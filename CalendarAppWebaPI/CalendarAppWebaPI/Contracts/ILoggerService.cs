using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalendarAppWebaPI.Contracts
{
    public interface ILoggerService
    {
        void LogInfo(string message);
        void LogDebug(string message);
        void LogError(string message);
        void LogWarn(string message);
    }
}
