using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iBand.Common
{
    public class TraceLog
    {
        // Fields
        private static TraceSource _source = new TraceSource("ServiceTestSource");

        // Methods
        static TraceLog()
        {
            _source.Switch.Level = ~SourceLevels.Off;
            Trace.AutoFlush = true;
        }

        public static string GetAllErrMessages(Exception ex)
        {
            StringBuilder builder = new StringBuilder();
            for (Exception exception = ex; exception != null; exception = exception.InnerException)
            {
                builder.AppendLine(exception.Message + " at " + exception.Source + ", trace: " + exception.StackTrace);
            }
            return builder.ToString();
        }

        public static void SetSource(string name)
        {
            _source = new TraceSource(name);
        }

        public static void WriteToLog(string message)
        {
            Source.TraceInformation(message);
        }

        public static void WriteToLog(string message, Exception ex)
        {
            Source.TraceInformation(message + " - " + GetAllErrMessages(ex));
            Source.TraceEvent(TraceEventType.Error, 0, "{0}: {1}", new object[] { message, ex });
        }

        // Properties
        protected static TraceSource Source
        {
            get
            {
                return _source;
            }
            set
            {
                _source = value;
            }
        }
    }


}
