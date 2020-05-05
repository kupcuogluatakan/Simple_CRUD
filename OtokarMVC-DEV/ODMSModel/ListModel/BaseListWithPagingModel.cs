using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSCommon.Resources;

namespace ODMSModel.ListModel
{
    public class BaseListWithPagingModel
    {
        private Dictionary<string, string> _mappings;
        private readonly IList<Kendo.Mvc.SortDescriptor> _sortDescriptors;
        public BaseListWithPagingModel([DataSourceRequest] DataSourceRequest request)
        {
            _sortDescriptors = request.Sorts;
            StartPage = request.Page;
            PageSize = request.PageSize;            
        }

        protected void SetMapper(Dictionary<string, string> mappings)
        {
            _mappings = mappings;
            if (_sortDescriptors == null)
                return;
            if (_sortDescriptors.Count > 0)
            {
                SortColumn = GetDbColumnName(_sortDescriptors[0].Member);
                SortDirection = GetSortDirectionSQLSyntax(_sortDescriptors[0].SortDirection.ToString());
            }
        }

        private string GetSortDirectionSQLSyntax(string referenceSyntax)
        {
            if (referenceSyntax.Trim().ToLower().CompareTo("ascending") == 0)
                return "ASC";
            if (referenceSyntax.Trim().ToLower().CompareTo("descending") == 0)
                return "DESC";

            return string.Empty;
        }

        public BaseListWithPagingModel()
        {}
        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(MessageResource))]
        public bool? SearchIsActive { get; set; }
        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(MessageResource))]
        public string IsActiveString { get; set; }
        public int StartPage { get; set; }
        public int PageSize { get; set; }
        public int Offset { get { return  StartPage<=1 ? 0 : (StartPage - 1)*PageSize; } }
        public int TotalCount { get; set; }
        public int FilteredTotalCount { get; set; }      
        public string SortColumn { get; set; }
        public string SortDirection { get; set; }
        public int ErrorNo { get; set; }
        public string ErrorMessage { get; set; }
        public int FirstCall { get; set; }

        protected virtual string GetDbColumnName(string key)
        {
            var mapper = new DbColumnMapper(_mappings);
            return mapper.GetDbColumnName(key);
        }

        public class DbColumnMapper
        {
            private DbColumnMapper _instance;

            private  Dictionary<string, string> DMapper =
                new Dictionary<string, string>();

            public DbColumnMapper(Dictionary<string, string> mappings)
            {
                if (DMapper.Count != 0) return;
                DMapper = mappings;
            }

            public DbColumnMapper Instance(Dictionary<string, string> mappings)
            {
                return _instance = new DbColumnMapper(mappings); 
            }
            ///CREATE DATE_YOK ISE Burası saçmalıyor
            public string GetDbColumnName(string T)
            {
                return DMapper==null ?  (ODMSCommon.CommonValues.ConstantMissingMapperColumnName) : (DMapper.ContainsKey(T) ? DMapper[T] : ODMSCommon.CommonValues.ConstantMissingMapperColumnName);
            }
        }

    }
}
