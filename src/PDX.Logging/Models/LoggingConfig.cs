namespace PDX.Logging.Models
{
    public class LoggingConfig
    {
        public string LoggingDirecory { get; set; }
    }

    public enum LogType
    {
        Info,
        Warning,
        Error
    }
}
