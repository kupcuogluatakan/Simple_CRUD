using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ODMSWebService.Model
{
    public class ResponseModel<T>
    {
        public bool IsSuccess { get; set; }
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public List<T> Data { get; set; }
    }
}