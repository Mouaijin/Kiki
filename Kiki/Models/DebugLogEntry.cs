using System;

namespace Kiki.Models
{
    public class DebugLogEntry
    {
        public Guid Id { get; set; }
        public string ErrorMessage { get; set; }
        public string ExceptionInfo { get; set; }
        public int? LineNumber { get; set; }
        public string Caller { get; set; }
    }
}