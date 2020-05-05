using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSBusiness.Business;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSModel.Common;
using ODMSModel.Defect;
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
    public class DefectController : ControllerBase
    {
        private void SetDefaults()
        {
            //Status List
            List<SelectListItem> statusList = CommonBL.ListStatus().Data;
            ViewBag.StatusList = statusList;

            List<SelectListItem> languageList = LanguageBL.ListLanguageAsSelectListItem(UserManager.UserInfo).Data;
            ViewBag.LanguageList = languageList;

            List<SelectListItem> contractList = ContractBL.ListContractAsSelectListItem().Data;
            ViewBag.ContractList = contractList;

            List<SelectListItem> dealerList = DefectBL.GetDealerList().Data;
            ViewBag.DealerList = dealerList;
        }

        #region Defect Index

        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.Defect.DefectIndex)]
        [HttpGet]
        public ActionResult DefectIndex(int? idDefect)
        {
            SetDefaults();
            DefectListModel model = new DefectListModel();
            model.IsActive = 1;
            if (idDefect != null)
            {
                model.IdDefect = idDefect;
            }
            return View(model);
        }

        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.Defect.DefectIndex, ODMSCommon.CommonValues.PermissionCodes.Defect.DefectDetails)]
        public ActionResult ListDefect([DataSourceRequest] DataSourceRequest request, DefectListModel model)
        {
            var defectBo = new DefectBL();
            var v = new DefectListModel(request);
            var totalCnt = 0;
            v.IdDefect = model.IdDefect;
            v.IdContract = model.IdContract;
            v.IdDealer = model.IdDealer;
            v.IsActive = model.IsActive;
            v.DeclarationDate = model.DeclarationDate;
            v.DealerDeclarationDate = model.DealerDeclarationDate;
            v.DefectNo = model.DefectNo;
            v.DocName = model.DocName;
            v.VehicleVinNo = model.VehicleVinNo;
            var returnValue = defectBo.ListDefect(UserManager.UserInfo, v, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }

        #endregion

        #region Defect Create
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.Defect.DefectIndex, ODMSCommon.CommonValues.PermissionCodes.Defect.DefectCreate)]
        [HttpGet]
        public ActionResult DefectCreate()
        {
            var model = new DefectViewModel();
            model.DeclarationDate = DateTime.Now;
            model.DealerDeclarationDate = DateTime.Now;
            model.IsActive = true;
            SetDefaults();
            return View(model);
        }

        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.Defect.DefectIndex, ODMSCommon.CommonValues.PermissionCodes.Defect.DefectCreate)]
        [HttpPost]
        public ActionResult DefectCreate(DefectViewModel viewModel, IEnumerable<HttpPostedFileBase> attachments)
        {
            SetDefaults();
            var defectBo = new DefectBL();

            if (ModelState.IsValid)
            {
                if (attachments != null)
                {
                    viewModel.DocId = SaveAttachments(viewModel.DocId, attachments);
                    viewModel.DocName = attachments.ElementAt(0).FileName;
                    viewModel.CommandType = CommonValues.DMLType.Insert;
                    defectBo.DMLDefect(UserManager.UserInfo, viewModel);

                    CheckErrorForMessage(viewModel, true);

                    ModelState.Clear();
                    DefectViewModel model = new DefectViewModel
                    {
                        IdDefect = viewModel.IdDefect
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

        #region Defect Update

        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.Defect.DefectIndex, ODMSCommon.CommonValues.PermissionCodes.Defect.DefectUpdate)]
        [HttpGet]
        public ActionResult DefectUpdate(int? IdDefect)
        {
            SetDefaults();
            var v = new DefectViewModel();
            if (IdDefect != null && IdDefect > 0)
            {
                var contractBo = new DefectBL();
                v.IdDefect = IdDefect;
                contractBo.GetDefect(UserManager.UserInfo, v);
            }
            return View(v);
        }

        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.Defect.DefectIndex, ODMSCommon.CommonValues.PermissionCodes.Defect.DefectUpdate)]
        [HttpPost]
        public ActionResult DefectUpdate(DefectViewModel viewModel, IEnumerable<HttpPostedFileBase> attachments)
        {
            SetDefaults();
            var contractBo = new DefectBL();
            if (ModelState.IsValid)
            {
                if (attachments != null)
                {
                    viewModel.DocId = SaveAttachments(viewModel.DocId, attachments);
                    viewModel.DocName = attachments.ElementAt(0).FileName;
                }
                viewModel.CommandType = CommonValues.DMLType.Update;
                contractBo.DMLDefect(UserManager.UserInfo, viewModel);
                CheckErrorForMessage(viewModel, true);
            }
            return View(viewModel);
        }

        #endregion

        #region Defect Delete

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.Defect.DefectIndex, ODMSCommon.CommonValues.PermissionCodes.Defect.DefectDelete)]
        public ActionResult DeleteDefect(int? idDefect)
        {
            DefectViewModel viewModel = new DefectViewModel() { IdDefect = idDefect};
            var contractBo = new DefectBL();
            viewModel.CommandType = !string.IsNullOrEmpty(viewModel.IdDefect.ToString()) ? CommonValues.DMLType.Delete : string.Empty;
            contractBo.DMLDefect(UserManager.UserInfo, viewModel);

            ModelState.Clear();

            if (viewModel.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error, viewModel.ErrorMessage);
        }
        #endregion

        #region Defect Details
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.Defect.DefectIndex, ODMSCommon.CommonValues.PermissionCodes.Defect.DefectDetails)]
        [HttpGet]
        public ActionResult DefectDetails(int? idDefect)
        {
            var v = new DefectViewModel();
            var defectBo = new DefectBL();

            v.IdDefect = idDefect;
            defectBo.GetDefect(UserManager.UserInfo, v);

            return View(v);
        }

        #endregion

        [HttpGet]
        public ActionResult DefectImage(int id)
        {
            DocumentBL docBl = new DocumentBL();
            DocumentInfo docInfo = docBl.GetDocumentById(id.GetValue<int>()).Model;
            return File(docInfo.DocBinary, docInfo.DocMimeType, docInfo.DocName);
        }
    }
}
