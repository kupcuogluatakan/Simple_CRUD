using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSModel.ListModel;
using ODMSModel.Announcement;
using ODMSModel.Common;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class AnnouncementController : ControllerBase
    {
        private void SetDefaults()
        {
            ViewBag.StatusList = CommonBL.ListStatus().Data;
            ViewBag.YesNoList = CommonBL.ListLookup(UserManager.UserInfo, CommonValues.LookupKeys.YesNo).Data;
            ViewBag.UserList = DealerRegionResponsibleBL.GetUserList().Data;
        }

        #region Announcement Index

        [AuthorizationFilter(CommonValues.PermissionCodes.Announcement.AnnouncementIndex)]
        [HttpGet]
        public ActionResult AnnouncementIndex()
        {
            SetDefaults();
            return View();
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.Announcement.AnnouncementIndex, CommonValues.PermissionCodes.Announcement.AnnouncementDetails)]
        public ActionResult ListAnnouncement([DataSourceRequest] DataSourceRequest request, AnnouncementListModel model)
        {
            var announcementBo = new AnnouncementBL();

            var filter = new AnnouncementListModel(request);
            filter.StartDate = model.StartDate;
            filter.EndDate = model.EndDate;
            filter.IsUrgent = model.IsUrgent;
            filter.IsActive = model.IsActive;
            filter.PublishDate = model.PublishDate;
            filter.PublishUser = model.PublishUser;
            var response = announcementBo.ListAnnouncements(UserManager.UserInfo, filter);

            return Json(response);
        }

        #endregion

        #region Announcement Create
        [AuthorizationFilter(CommonValues.PermissionCodes.Announcement.AnnouncementIndex, CommonValues.PermissionCodes.Announcement.AnnouncementCreate)]
        [HttpGet]
        public ActionResult AnnouncementCreate()
        {
            AnnouncementViewModel model = new AnnouncementViewModel();
            model.IsActive = 1;
            SetDefaults();
            return View(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.Announcement.AnnouncementIndex, CommonValues.PermissionCodes.Announcement.AnnouncementCreate)]
        [HttpPost]
        public ActionResult AnnouncementCreate(AnnouncementViewModel viewModel, IEnumerable<HttpPostedFileBase> attachments)
        {
            SetDefaults();
            var announcementBo = new AnnouncementBL();

            if (ModelState.IsValid)
            {
                if (attachments != null)
                {
                    viewModel.DocId = SaveAttachments(viewModel.DocId, attachments);
                    viewModel.DocName = attachments.ElementAt(0).FileName;
                }
                viewModel.CommandType = CommonValues.DMLType.Insert;
                announcementBo.DMLAnnouncement(UserManager.UserInfo, viewModel);

                CheckErrorForMessage(viewModel, true);

                ModelState.Clear();
                AnnouncementViewModel model = new AnnouncementViewModel();
                return View(model);
            }
            return View(viewModel);
        }
        private int SaveAttachments(int docId, IEnumerable<HttpPostedFileBase> attachments)
        {
            if (attachments != null)
            {
                MemoryStream target = new MemoryStream();
                attachments.ElementAt(0).InputStream.CopyTo(target);
                byte[] data = target.ToArray();

                DocumentInfo documentInfo = new DocumentInfo()
                {
                    DocId = docId,
                    DocBinary = data,
                    DocMimeType = attachments.ElementAt(0).ContentType,
                    DocName = attachments.ElementAt(0).FileName,
                    CommandType = CommonValues.DMLType.Insert
                };
                DocumentBL documentBo = new DocumentBL();
                documentBo.DMLDocument(UserManager.UserInfo, documentInfo);
                docId = documentInfo.DocId;
            }
            return docId;
        }
        #endregion

        #region Announcement Update
        [AuthorizationFilter(CommonValues.PermissionCodes.Announcement.AnnouncementIndex, CommonValues.PermissionCodes.Announcement.AnnouncementUpdate)]
        [HttpGet]
        public ActionResult AnnouncementUpdate(int announcementId)
        {
            SetDefaults();
            var filter = new AnnouncementViewModel();
            if (announcementId > 0)
            {
                var announcementBo = new AnnouncementBL();
                filter.AnnouncementId = announcementId;
                announcementBo.GetAnnouncement(UserManager.UserInfo, filter);
            }
            return View(filter);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.Announcement.AnnouncementIndex, CommonValues.PermissionCodes.Announcement.AnnouncementUpdate)]
        [HttpPost]
        public ActionResult AnnouncementUpdate(AnnouncementViewModel viewModel, IEnumerable<HttpPostedFileBase> attachments)
        {
            SetDefaults();
            var announcementBo = new AnnouncementBL();
            if (ModelState.IsValid)
            {
                if (viewModel.IsUrgent)
                    viewModel.SendMail = true;

                if (attachments != null)
                {
                    viewModel.DocId = SaveAttachments(viewModel.DocId, attachments);
                    viewModel.DocName = attachments.ElementAt(0).FileName;
                }
                viewModel.CommandType = CommonValues.DMLType.Update;
                announcementBo.DMLAnnouncement(UserManager.UserInfo, viewModel);
                CheckErrorForMessage(viewModel, true);
            }
            return View(viewModel);
        }

        #endregion

        #region Change Announcement Status

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(CommonValues.PermissionCodes.Announcement.AnnouncementIndex, CommonValues.PermissionCodes.Announcement.AnnouncementUpdate)]
        public ActionResult ChangeAnnouncementStatus(int announcementId)
        {
            AnnouncementViewModel viewModel = new AnnouncementViewModel() { AnnouncementId = announcementId };
            var announcementBo = new AnnouncementBL();
            announcementBo.GetAnnouncement(UserManager.UserInfo, viewModel);

            if (viewModel.SendMail)
            {
                bool sendMail = ConfigurationManager.AppSettings.Get("sendMail").GetValue<bool>();
                // is active 0 ise yayınlama yapılacak tüm aktif kullanıcılara mail atılacak.
                if (sendMail && viewModel.IsActive == 0)
                {
                    StringBuilder mailAddresses = new StringBuilder();

                    List<UserListModel> userList = announcementBo.ListMailUsers(announcementId).Data;

                    foreach (var userListModel in userList)
                    {
                        if (!mailAddresses.ToString().Contains(userListModel.EMail))
                        {
                            mailAddresses.Append(userListModel.EMail);
                            mailAddresses.Append(";");
                        }
                    }
                    string isUrgentS = viewModel.IsUrgent ? MessageResource.Mail_Display_TitleUrgent : "";
                    CommonBL.SendDbMail(mailAddresses.ToString(), MessageResource.Mail_Display_NewAnnounc + " " + isUrgentS + "   : " + viewModel.Title, viewModel.Body);
                }
            }
            viewModel.PublishDate = DateTime.Now;
            viewModel.PublishUser = UserManager.UserInfo.UserId;
            viewModel.CommandType = announcementId != 0 ? CommonValues.DMLType.Update : string.Empty;
            viewModel.IsActive = viewModel.IsActive == 0 ? 1 : 0;
            announcementBo.DMLAnnouncement(UserManager.UserInfo, viewModel);

            ModelState.Clear();
            if (viewModel.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success,
                    MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error,
                viewModel.ErrorMessage);
        }
        #endregion

        #region Announcement Delete

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(CommonValues.PermissionCodes.Announcement.AnnouncementIndex, CommonValues.PermissionCodes.Announcement.AnnouncementDelete)]
        public ActionResult DeleteAnnouncement(int announcementId)
        {
            AnnouncementViewModel viewModel = new AnnouncementViewModel() { AnnouncementId = announcementId };
            var announcementBo = new AnnouncementBL();
            viewModel.CommandType = announcementId != 0 ? CommonValues.DMLType.Delete : string.Empty;
            announcementBo.DMLAnnouncement(UserManager.UserInfo, viewModel);

            ModelState.Clear();
            if (viewModel.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success,
                    MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error,
                viewModel.ErrorMessage);
        }
        #endregion

        #region Announcement Details
        [AuthorizationFilter(CommonValues.PermissionCodes.Announcement.AnnouncementIndex, CommonValues.PermissionCodes.Announcement.AnnouncementDetails)]
        [HttpGet]
        public ActionResult AnnouncementDetails(int announcementId)
        {
            var filter = new AnnouncementViewModel();
            filter.AnnouncementId = announcementId;
            var announcementBo = new AnnouncementBL();
            announcementBo.GetAnnouncement(UserManager.UserInfo, filter);
            return View(filter);
        }

        #endregion
    }
}