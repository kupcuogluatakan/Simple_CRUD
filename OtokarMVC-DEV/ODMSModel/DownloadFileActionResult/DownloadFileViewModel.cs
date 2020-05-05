using ODMSCommon;
using ODMSCommon.Security;
using System;
using System.Collections.Generic;
using System.IO;

namespace ODMSModel.DownloadFileActionResult
{
    public class DownloadFileViewModel
    {
        public Guid Id { get; set; }

        public string FileName { get; set; }

        public string ContentType { get; set; }

        public MemoryStream MStream { get; set; }

        public Dictionary<Guid, DownloadFileViewModel> Add(DownloadFileViewModel model)
        {
            var list = new Dictionary<Guid, DownloadFileViewModel> { { Id, model } };

            return list;
        }
    }

    public interface IDownloadFile<T>
    {
        ResponseModel<T> ParseExcel(UserInfo user, T model, Stream s);
        MemoryStream SetExcelReport(List<T> listModel, string errorMessage);
    }

    public interface IExcelValidation<T>
    {
        T ExcelValidate(T model);
    }
}
