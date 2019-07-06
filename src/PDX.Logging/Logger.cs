using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Options;
using PDX.Logging.Models;
using File = System.IO.File;

namespace PDX.Logging
{
    public class Logger : ILogger
    {
        private readonly LoggingConfig _loggingConfig;
        private readonly string _loggingDirectory;
        private const string _globalErrorFile = "error_log.txt";

        public Logger(IOptions<LoggingConfig> loggingConfig)
        {
            _loggingConfig = loggingConfig.Value;
            _loggingDirectory = _loggingConfig.LoggingDirecory;
        }

        public bool Log(LogType logType, string message)
        {
            var fileName = $"{logType.ToString()}.txt";
            return Log(fileName, message);
        }


        public bool Log(LogType logType, Exception exception)
        {
            string source = string.Empty; 
            var trace = new System.Diagnostics.StackTrace(exception, true);
            if (trace != null && trace.GetFrames().Count() > 0)
            {
                var frame = trace.GetFrames().Last();
                var lineNumber = frame.GetFileLineNumber();
                var fileName = frame.GetFileName();
                source = $"File: {fileName}, Line: {lineNumber}";
            }

            source = GetExceptionInformation(exception, source);
            return Log(logType, source);
        }

        public bool Log(Exception exception)
        {
            return Log(LogType.Error, exception);
        }



        #region Helpers

        /// <summary>
        /// Helps to log any content to the specified file
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        private bool Log(string fileName, string content)
        {
            //Log content into a txt file
            string monthString = DateTime.Now.ToString("yyyy-MMM"), dateString = DateTime.Now.ToString("yyyy-MM-dd"), timeString = DateTime.Now.ToString("hh:mm:ss tt");
            var directory = $"{_loggingDirectory}{Path.DirectorySeparatorChar}{monthString}{Path.DirectorySeparatorChar}{dateString}";

            if (!Directory.Exists(directory))
            {
                // Try to create the directory.
                Directory.CreateDirectory(directory);
            }

            try
            {
                content = $"{Environment.NewLine}[{timeString}] {content}";
                File.AppendAllText(Path.Combine(directory, fileName), content);
            }
            catch (IOException ex)
            {
                return LogToGlobalFile(ex.Message);
            }

            return true;
        }

        /// <summary>
        /// Log global error here
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        private bool LogToGlobalFile(string content)
        {
            try
            {
                File.AppendAllText(_globalErrorFile, content);
            }
            catch (IOException ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Get all exception information
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        private string GetExceptionInformation(Exception exception, string source)
        {

            var sb = new StringBuilder();
            sb.AppendLine("********** " + $"{DateTime.Now:F}" + "**********");

            while (exception != null)
            {
                if (exception.InnerException != null)
                {
                    sb.AppendLine("Inner Exception Type: ");
                    sb.AppendLine(exception.InnerException.GetType().ToString());
                    sb.AppendLine("Inner Exception: ");
                    sb.AppendLine(exception.InnerException.Message);
                    sb.AppendLine("Inner Source: ");
                    sb.AppendLine(exception.InnerException.Source);
                    sb.AppendLine("Inner Stack Trace: ");
                    sb.AppendLine(exception.InnerException.StackTrace);
                }
                sb.AppendLine("Exception Type: ");
                sb.AppendLine(sb.GetType().ToString());
                sb.AppendLine("Exception: " + exception.Message);
                sb.AppendLine("Source: " + source);
                sb.AppendLine("Stack Trace: ");
                if (exception.StackTrace != null)
                {
                    sb.AppendLine(exception.StackTrace);
                    sb.AppendLine();
                }
                exception = exception.InnerException;
            }

            return sb.ToString();
        }

        #endregion
    }
}
