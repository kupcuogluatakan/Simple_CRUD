using ODMSCommon.Security;
using ODMSData;
using ODMSService.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSService.Report
{
    public abstract class ReportBase
    {
        protected ReportsData _reportData;
        public ReportBase()
        {
            _reportData = new ReportsData();
        }

        /// <summary>
        /// Çıktıyı alacak kullanıcı bilgileri
        /// </summary>
        public UserInfo UserInfo { get; set; }

        /// <summary>
        /// FillData da data çekmek için kullanılan parametre
        /// </summary>
        public abstract object Filter
        {
            get;
        }

        /// <summary>
        /// Rapor adı
        /// </summary>
        public abstract string ReportName
        {
            get;
        }

        /// <summary>
        /// Data çekme
        /// </summary>
        /// <returns></returns>
        public abstract IEnumerable<dynamic> FillData();

        /// <summary>
        /// Excel oluşturma
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public IEnumerable<dynamic> CreateExcel(Dictionary<dynamic, dynamic> param)
        {

            foreach (var prop in Filter.GetType().GetProperties())
            {
                foreach (var item in param)
                {
                    if (prop.Name == item.Key)
                    {
                        try
                        {
                            Type type = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                            object safeValue = (item.Value == null) ? null : Convert.ChangeType(item.Value, type);
                            prop.SetValue(Filter, safeValue);
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                }
            }

            IEnumerable<dynamic> data = FillData();
            return data;
        }
    }
}
