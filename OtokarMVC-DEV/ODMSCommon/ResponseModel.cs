using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSCommon
{
    public class ResponseModel<T>
    {
        public ResponseModel()
        {

        }
        public bool IsSuccess { get; set; } = true;
        public string Message { get; set; }
        public List<T> Data { get; set; }
        public int Total { get; set; }
        public T Model { get; set; }
        public int ResponseTime { get; set; }
    }

    public class ResponseModel<G,T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<T> Data { get; set; }
        public int Total { get; set; }
        public G Model { get; set; }
        public int ResponseTime { get; set; }
    }
}
