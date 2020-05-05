using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSBusiness.Business;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSModel.Common;
using ODMSModel.Contract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class ContractController : ControllerBase
    {
        private void SetDefaults()
        {
            //Status List
            List<SelectListItem> statusList = CommonBL.ListStatus().Data;
            ViewBag.StatusList = statusList;

            List<SelectListItem> languageList = LanguageBL.ListLanguageAsSelectListItem(UserManager.UserInfo).Data;
            ViewBag.LanguageList = languageList;

        }

        #region Contract Index

        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.Contract.ContractIndex)]
        [HttpGet]
        public ActionResult ContractIndex(int? idContract)
        {
            SetDefaults();
            ContractListModel model = new ContractListModel();
            if (idContract != null)
            {
                model.IdContract = idContract;
            }
            return View(model);
        }

        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.Contract.ContractIndex, ODMSCommon.CommonValues.PermissionCodes.Contract.ContractDetails)]
        public ActionResult ListContract([DataSourceRequest] DataSourceRequest request, ContractListModel model)
        {
            var contractBo = new ContractBL();
            var v = new ContractListModel(request);
            var totalCnt = 0;
            v.IdContract = model.IdContract;
            v.ContractName = model.ContractName;
            v.Duration = model.Duration;
            v.IsActive = model.IsActive;
            v.StartDate = model.StartDate;
            v.EndDate = model.EndDate;
            var returnValue = contractBo.ListContract(UserManager.UserInfo, v, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }

        #endregion

        #region Contract Create
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.Contract.ContractIndex, ODMSCommon.CommonValues.PermissionCodes.Contract.ContractCreate)]
        [HttpGet]
        public ActionResult ContractCreate()
        {
            var model = new ContractViewModel();
            model.StartDate = DateTime.Now;
            model.EndDate = DateTime.Now;
            model.IsActive = true;
            SetDefaults();
            return View(model);
        }

        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.Contract.ContractIndex, ODMSCommon.CommonValues.PermissionCodes.Contract.ContractCreate)]
        [HttpPost]
        public ActionResult ContractCreate(ContractViewModel viewModel, IEnumerable<HttpPostedFileBase> attachments)
        {
            SetDefaults();
            var contractBo = new ContractBL();

            if (ModelState.IsValid)
            {
                if (attachments != null)
                {
                    viewModel.DocId = SaveAttachments(viewModel.DocId, attachments);
                    viewModel.DocName = attachments.ElementAt(0).FileName;
                    viewModel.CommandType = CommonValues.DMLType.Insert;
                    contractBo.DMLContract(UserManager.UserInfo, viewModel);

                    CheckErrorForMessage(viewModel, true);

                    ModelState.Clear();
                    ContractViewModel model = new ContractViewModel
                    {
                        IdContract = viewModel.IdContract
                    };
                    return View(model);
                }
                else
                {
                    SetMessage(MessageResource.Global_Warning_ExcelNotSelected, CommonValues.MessageSeverity.Fail);
                }
            }
         
            return View(viewModel);
        }

        private int SaveAttachments(int photoDocId, IEnumerable<HttpPostedFileBase> attachments)
        {
            if (attachments != null)
            {
                MemoryStream target = new MemoryStream();
                attachments.ElementAt(0).InputStream.CopyTo(target);
                byte[] data = target.ToArray();

                DocumentInfo documentInfo = new DocumentInfo()
                {
                    DocId = photoDocId,
                    DocBinary = data,
                    DocMimeType = attachments.ElementAt(0).ContentType,
                    DocName = attachments.ElementAt(0).FileName,
                    CommandType = CommonValues.DMLType.Insert
                };
                DocumentBL documentBo = new DocumentBL();
                documentBo.DMLDocument(UserManager.UserInfo, documentInfo);
                photoDocId = documentInfo.DocId;
            }
            return photoDocId;
        }

        #endregion

        #region Contract Update

        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.Contract.ContractIndex, ODMSCommon.CommonValues.PermissionCodes.Contract.ContractUpdate)]
        [HttpGet]
        public ActionResult ContractUpdate(int? IdContract)
        {
            SetDefaults();
            var v = new ContractViewModel();
            if (IdContract != null && IdContract > 0)
            {
                var contractBo = new ContractBL();
                v.IdContract = IdContract;
                contractBo.GetContract(UserManager.UserInfo, v);
            }
            return View(v);
        }

        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.Contract.ContractIndex, ODMSCommon.CommonValues.PermissionCodes.Contract.ContractUpdate)]
        [HttpPost]
        public ActionResult ContractUpdate(ContractViewModel viewModel, IEnumerable<HttpPostedFileBase> attachments)
        {
            SetDefaults();
            var contractBo = new ContractBL();
            if (ModelState.IsValid)
            {
                if (attachments != null)
                {
                    viewModel.DocId = SaveAttachments(viewModel.DocId, attachments);
                    viewModel.DocName = attachments.ElementAt(0).FileName;
                }
                viewModel.CommandType = CommonValues.DMLType.Update;
                contractBo.DMLContract(UserManager.UserInfo, viewModel);
                CheckErrorForMessage(viewModel, true);
            }
            return View(viewModel);
        }

        #endregion

        #region Contract Delete

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.Contract.ContractIndex, ODMSCommon.CommonValues.PermissionCodes.Contract.ContractDelete)]
        public ActionResult DeleteContract(int idContract)
        {
            ContractViewModel viewModel = new ContractViewModel() { IdContract = idContract };
            var contractBo = new ContractBL();
            viewModel.CommandType = !string.IsNullOrEmpty(viewModel.IdContract.ToString()) ? CommonValues.DMLType.Delete : string.Empty;
            contractBo.DMLContract(UserManager.UserInfo, viewModel);

            ModelState.Clear();

            if (viewModel.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error, viewModel.ErrorMessage);
        }
        #endregion

        [HttpGet]
        public ActionResult ContractImage(int id)
        {
            DocumentBL docBl = new DocumentBL();
            DocumentInfo docInfo = docBl.GetDocumentById(id.GetValue<int>()).Model;
            return File(docInfo.DocBinary, docInfo.DocMimeType, docInfo.DocName);
        }
    }
}
