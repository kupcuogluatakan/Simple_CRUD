using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Security.AccessControl;


namespace ODMSModel.Reports
{
    public class ReportCreateModel
    {
        public int Id { get; set; }
        public string FilePath { get; set; }
        public string Columns { get; set; }
        public int ReportType { get; set; }
        public string ParametersString { get; set; }
        public string Parameters
        {
            get
            {
                if (ParameterList.Count > 0)
                {
                    var param = new StringBuilder();
                    foreach (var item in ParameterList)
                    {
                        param.Append(string.Format("#!?!#{0}#=#{1}", item.Name, item.Value));
                    }
                    var paramReal = param.ToString().Substring(5, param.ToString().Length - 5);
                    return paramReal;
                }

                return string.Empty;
            }
        }

        private List<ReportCreateParameterModel> _parameterList;
        /// <summary>
        /// Rapor parametre listesi (filter property name,filter property value)
        /// </summary>
        public List<ReportCreateParameterModel> ParameterList
        {
            get
            {
                if (_parameterList == null)
                    _parameterList = new List<ReportCreateParameterModel>();
                return _parameterList;
            }
            set
            {
                _parameterList = value;
            }
        }
        public bool? IsComplete { get; set; }
        public bool? IsSuccess { get; set; }
       
        public int? CreateUserDealerId { get; set; }
        public string CreateUserLanguageCode { get; set; }
        public int? CreateUserId { get; set; }

        public string CreateUser { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string CommandType { get; set; }
        public int ErrorNo { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class ReportCreateParameterModel
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
