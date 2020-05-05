using System;
using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;

namespace ODMSModel.Announcement
{
    public class AppErrorViewModel : ModelBase
    {
        public AppErrorViewModel()
        {
        }

        public int AppErrorId { get; set; }
        public string Source { get; set; }
        public string BusinessName { get; set; }
        public string MethodName { get; set; }
        public string UserCode { get; set; }
        public bool IsResolved { get; set; }
        public DateTime ErrorTime { get; set; }
        public string SystemCode { get; set; }
        public string ErrorSource { get; set; }
        public string ErrorDesc { get; set; }
        public string DebugParameters { get; set; }
        public string DebugSection { get; set; }
        public string RefValue { get; set; }
        public bool IsFromUI { get; set; }
        public string SqlErrorNumber { get; set; }
        public string SqlErrorSeverity { get; set; }
        public string SqlErrorState { get; set; }
        public string SqlErrorProcedure { get; set; }
        public string SqlErrorLine { get; set; }
        public string SqlErrorMessage { get; set; }

    }
}
