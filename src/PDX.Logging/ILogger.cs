using System;
using PDX.Logging.Models;

namespace PDX.Logging
{
    public interface ILogger
    {
        bool Log(LogType logType, string message);
        bool Log(LogType logType, Exception exception);    
        bool Log(Exception exception);        
    }
}
