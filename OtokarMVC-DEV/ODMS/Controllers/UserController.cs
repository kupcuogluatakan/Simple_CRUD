using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSModel.UserRole;
using ODMSModel.Common;
using ODMSModel.ListModel;
using ODMSModel.User;
using ODMS.Security.PasswordPolicyRules;
using ODMSModel.Title;
using ODMSBusiness.Business;
using ODMSModel.Dealer;
using ODMSModel.SystemAdministration;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class UserController : ControllerBase
    {
        #region General Methods
        public ActionResult Image(int id)
        {
            DocumentBL boDocument = new DocumentBL();
            DocumentInfo documentInfo = boDocument.GetDocumentById(id).Model;
            byte[] image = documentInfo.DocBinary;
            if (documentInfo.DocBinary == null)
                return null;
            else
                return File(image, "image/png");
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

        private void SetDefaults()
        {
            List<SelectListItem> sexList = CommonBL.ListLookup(UserManager.UserInfo, CommonValues.LookupKeys.SexLookup).Data;
            List<SelectListItem> maritalStatusList = CommonBL.ListLookup(UserManager.UserInfo, CommonValues.LookupKeys.MaritalStatusLookup).Data;
            List<SelectListItem> dealerList = DealerBL.ListDealerAsSelectListItem().Data;
            List<SelectListItem> languageList = LanguageBL.ListLanguageAsSelectListItem(UserManager.UserInfo).Data;
            List<SelectListItem> statusList = CommonBL.ListStatus().Data;

            ViewBag.SexList = sexList;
            ViewBag.MaritalStatusList = maritalStatusList;
            ViewBag.DealerList = dealerList;
            ViewBag.LanguageList = languageList;
            ViewBag.StatusList = statusList;

        }
        #endregion

        #region User Index
        [AuthorizationFilter(CommonValues.PermissionCodes.User.UserIndex)]
        [HttpGet]
        public ActionResult UserIndex(int? userId)
        {
            SetDefaults();
            List<SelectListItem> roleTypeList = RoleBL.ListRoleTypeComboByUserType(UserManager.UserInfo, false, false).Data;
            ViewBag.RoleTypeList = roleTypeList;
            var yesnoList = new List<SelectListItem>();
            yesnoList.Add(new SelectListItem() { Value = "True", Text = MessageResource.Global_Display_Active });
            yesnoList.Add(new SelectListItem() { Value = "False", Text = MessageResource.Global_Display_Passive });
            ViewBag.ActivePassiveList = yesnoList;

            UserIndexViewModel model = new UserIndexViewModel();
            if (userId != 0 && userId != null)
            {
                model.UserId = userId.GetValue<int>();
                model.IsActive = true;
            }
            return View(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.User.UserIndex, CommonValues.PermissionCodes.User.UserDetails)]
        public ActionResult ListUser([DataSourceRequest] DataSourceRequest request, UserListModel model)
        {
            var userBo = new UserBL();
            var v = new UserListModel(request);
            var totalCnt = 0;
            v.SearchIsActive = model.SearchIsActive;
            v.UserCode = model.UserCode;
            v.DealerId = model.DealerId;
            v.IdentityNo = model.IdentityNo;
            v.RoleTypeId = model.RoleTypeId;
            v.UserFirstName = model.UserFirstName;
            v.UserLastName = model.UserLastName;
            v.MaritalStatusId = model.MaritalStatusId;
            v.SexId = model.SexId;
            v.IsTechnician = 0;

            var returnValue = userBo.ListUsers(UserManager.UserInfo, v, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }

        #endregion

        #region User Create
        [AuthorizationFilter(CommonValues.PermissionCodes.User.UserIndex, CommonValues.PermissionCodes.User.UserCreate)]
        [HttpGet]
        public ActionResult UserCreate()
        {
            SetDefaults();
            List<SelectListItem> roleTypeList = RoleBL.ListRoleTypeComboByUserType(UserManager.UserInfo, false, false).Data;
            ViewBag.RoleTypeList = roleTypeList;
            UserIndexViewModel model = new UserIndexViewModel();
            model.IsActive = true;
            model.LanguageCode = "TR";
            return View(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.User.UserIndex, CommonValues.PermissionCodes.User.UserCreate)]
        [HttpPost]
        public ActionResult UserCreate(UserIndexViewModel viewModel, IEnumerable<HttpPostedFileBase> attachments)
        {
            SetDefaults();
            List<SelectListItem> roleTypeList = RoleBL.ListRoleTypeComboByUserType(UserManager.UserInfo, false, false).Data;
            ViewBag.RoleTypeList = roleTypeList;
            if (attachments == null)
            {
                SetMessage(MessageResource.User_Warning_Photo, CommonValues.MessageSeverity.Fail);
                return View(viewModel);
            }
            var userBo = new UserBL();
            // kullanıcı kodu kontrolü
            int totalCount = 0;
            UserListModel listModel = new UserListModel();
            listModel.UserCode = viewModel.UserCode;
            userBo.ListDealerUsers(UserManager.UserInfo, listModel, out totalCount);
            if (totalCount != 0)
            {
                SetMessage(MessageResource.User_Warning_DuplicateCode, CommonValues.MessageSeverity.Fail);
                return View(viewModel);
            }

            if (!string.IsNullOrWhiteSpace(viewModel.TCIdentityNo.ToString()))
            {
                UserIndexViewModel userInfo = userBo.GetUserByTCIdentityNo(UserManager.UserInfo, viewModel.TCIdentityNo.ToString()).Model;
                if (userInfo.TCIdentityNo != null)
                {
                    SetMessage(MessageResource.User_Warning_SameTCIdentityUserFound, CommonValues.MessageSeverity.Fail);
                    return View(viewModel);
                }
            }
            else
            {
                UserIndexViewModel userInfo = userBo.GetUserByPassportNo(UserManager.UserInfo, viewModel.PassportNo).Model;
                if (userInfo.PassportNo != null)
                {
                    SetMessage(MessageResource.User_Warning_SamePassportNoUserFound, CommonValues.MessageSeverity.Fail);
                    return View(viewModel);
                }
            }
            if (ModelState.IsValid)
            {
                if (viewModel.TCIdentityNo > 0 && !CommonUtility.ValidateTcIdentityNumber(viewModel.TCIdentityNo.ToString()))
                {
                    SetMessage(MessageResource.Validation_TCIdentityNo_Format, CommonValues.MessageSeverity.Fail);
                }

                viewModel.PhotoDocId = SaveAttachments(viewModel.PhotoDocId, attachments);
                viewModel.CommandType = viewModel.UserId > 0
                                            ? CommonValues.DMLType.Update
                                            : CommonValues.DMLType.Insert;
                viewModel.IsTechnician = false;

                string newPassword = string.Empty;
                if (viewModel.CommandType == "I")
                {
                    newPassword = CommonUtility.GeneratePassword();
                    viewModel.Password = new PasswordSecurityProvider().GenerateHashedPassword(newPassword);
                }
                userBo.DMLUser(UserManager.UserInfo, viewModel);

                SystemAdministrationBL sysBo = new SystemAdministrationBL();
                AccountRecoveryViewModel arModel = new AccountRecoveryViewModel();
                arModel.IdentityNo = viewModel.TCIdentityNo == null ? viewModel.PassportNo : viewModel.TCIdentityNo.ToString();
                arModel.UserName = viewModel.UserCode;
                arModel.Email = viewModel.EMail;
                arModel.Password = newPassword;
                sysBo.SetAccountRecovery(arModel);

                CheckErrorForMessage(viewModel, true);

                if (viewModel.ErrorNo == 0)
                {
                    UserTitleViewModel utModel = new UserTitleViewModel
                    {
                        UserId = viewModel.UserId,
                        TitleId = viewModel.RoleTypeId,
                        CommandType = CommonValues.DMLType.Insert
                    };

                    UserTitleBL bl = new UserTitleBL();
                    bl.DMLUserTitle(UserManager.UserInfo, utModel);
                    if (utModel.ErrorNo == 0)
                    {
                        SetMessage(MessageResource.Global_Display_Success, CommonValues.MessageSeverity.Success);
                        ModelState.Clear();
                    }
                    else
                    {
                        SetMessage(viewModel.ErrorMessage, CommonValues.MessageSeverity.Fail);
                    }
                }
                else
                {
                    SetMessage(viewModel.ErrorMessage, CommonValues.MessageSeverity.Fail);
                }

                ModelState.Clear();
                UserIndexViewModel newModel = new UserIndexViewModel();
                newModel.IsActive = true;
                newModel.LanguageCode = "TR";
                return View(newModel);

            }
            return View(viewModel);
        }

        #endregion

        #region User Update
        [AuthorizationFilter(CommonValues.PermissionCodes.User.UserIndex, CommonValues.PermissionCodes.User.UserUpdate)]
        [HttpGet]
        public ActionResult UserUpdate(int id = 0)
        {
            List<SelectListItem> roleTypeList = RoleBL.ListRoleTypeComboByUserType(UserManager.UserInfo, false, false).Data;
            ViewBag.RoleTypeList = roleTypeList;
            var v = new UserIndexViewModel();
            if (id > 0)
            {
                var userBo = new UserBL();
                SetDefaults();
                v.UserId = id;
                userBo.GetUserView(UserManager.UserInfo, v);
            }
            return View(v);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.User.UserIndex, CommonValues.PermissionCodes.User.UserUpdate)]
        [HttpPost]
        public ActionResult UserUpdate(UserIndexViewModel viewModel, IEnumerable<HttpPostedFileBase> attachments)
        {
            SetDefaults();
            List<SelectListItem> roleTypeList = RoleBL.ListRoleTypeComboByUserType(UserManager.UserInfo, false, false).Data;
            ViewBag.RoleTypeList = roleTypeList;
            if (attachments == null && viewModel.PhotoDocId == 0)
            {
                SetMessage(MessageResource.User_Warning_Photo, CommonValues.MessageSeverity.Fail);
                return View(viewModel);
            }
            var userBo = new UserBL();
            // kullanıcı kodu kontrolü
            int totalCount = 0;
            UserListModel listModel = new UserListModel();
            listModel.UserCode = viewModel.UserCode;
            List<UserListModel> userList = userBo.ListDealerUsers(UserManager.UserInfo, listModel, out totalCount).Data;
            var control = (from u in userList.AsEnumerable()
                           where u.UserId != viewModel.UserId
                           select u);
            if (control.Any())
            {
                SetMessage(MessageResource.User_Warning_DuplicateCode, CommonValues.MessageSeverity.Fail);
                return View(viewModel);
            }


            if (!string.IsNullOrWhiteSpace(viewModel.TCIdentityNo.ToString()))
            {
                var hasUser = userBo.GetAnyUserByTCIdentityNo(viewModel.UserId, viewModel.TCIdentityNo.ToString()).Model;
                if (hasUser)
                {
                    SetMessage(MessageResource.User_Warning_SameTCIdentityUserFound, CommonValues.MessageSeverity.Fail);
                    return View(viewModel);
                }
            }
            else
            {
                var hasUser = userBo.GetAnyUserByPassportNo(viewModel.UserId, viewModel.PassportNo).Model;
                if (hasUser)
                {
                    SetMessage(MessageResource.User_Warning_SamePassportNoUserFound, CommonValues.MessageSeverity.Fail);
                    return View(viewModel);
                }
            }


            if (ModelState.IsValid)
            {

                if (!string.IsNullOrWhiteSpace(viewModel.TCIdentityNo.ToString()) && !CommonUtility.ValidateTcIdentityNumber(viewModel.TCIdentityNo.ToString()))
                {
                    SetMessage(MessageResource.Validation_TCIdentityNo_Format, CommonValues.MessageSeverity.Fail);
                    return View(viewModel);
                }

                viewModel.PhotoDocId = SaveAttachments(viewModel.PhotoDocId, attachments);
                viewModel.CommandType = viewModel.UserId > 0
                                            ? CommonValues.DMLType.Update
                                            : CommonValues.DMLType.Insert;
                userBo.DMLUser(UserManager.UserInfo, viewModel);
                CheckErrorForMessage(viewModel, true);
                if (viewModel.ErrorNo == 0)
                {
                    UserTitleViewModel utModel = new UserTitleViewModel
                    {
                        UserId = viewModel.UserId,
                        TitleId = viewModel.RoleTypeId,
                        CommandType = CommonValues.DMLType.Update
                    };
                    UserTitleBL utBo = new UserTitleBL();
                    utBo.DMLUserTitle(UserManager.UserInfo, utModel);
                    if (utModel.ErrorNo == 0)
                    {
                        SetMessage(MessageResource.Global_Display_Success, CommonValues.MessageSeverity.Success);
                        ModelState.Clear();
                    }
                    else
                    {
                        SetMessage(viewModel.ErrorMessage, CommonValues.MessageSeverity.Fail);
                    }
                }
                else
                {
                    SetMessage(viewModel.ErrorMessage, CommonValues.MessageSeverity.Fail);
                }
            }
            return View(viewModel);
        }

        #endregion

        #region User Delete

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(CommonValues.PermissionCodes.User.UserIndex, CommonValues.PermissionCodes.User.UserDelete)]
        public ActionResult DeleteUser(int userId)
        {
            UserIndexViewModel viewModel = new UserIndexViewModel() { UserId = userId };
            var userBo = new UserBL();
            viewModel.CommandType = viewModel.UserId > 0 ? CommonValues.DMLType.Delete : string.Empty;
            userBo.DMLUser(UserManager.UserInfo, viewModel);

            ModelState.Clear();
            if (viewModel.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success,
                    MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error,
                viewModel.ErrorMessage);
        }
        #endregion

        #region User Details
        [AuthorizationFilter(CommonValues.PermissionCodes.User.UserIndex, CommonValues.PermissionCodes.User.UserDetails)]
        [HttpGet]
        public ActionResult UserDetails(int id = 0)
        {
            var v = new UserIndexViewModel();
            var userBo = new UserBL();

            v.UserId = id;
            userBo.GetUser(UserManager.UserInfo, v);

            return View(v);
        }

        #endregion

        #region User Change Password

        [HttpGet]
        [AuthorizationFilter]
        public ActionResult UserChangePassword(int? userId)
        {
            UserBL userBo = new UserBL();
            UserIndexViewModel model = new UserIndexViewModel();
            bool isPasswordSet = false;
            bool isPartial = userId != null && userId != 0;

            if (isPartial)
            {
                model.UserId = userId.GetValue<int>();
                userBo.GetUser(UserManager.UserInfo, model);
                isPasswordSet = model.IsPasswordSet;
            }
            else
            {
                UserInfo userInfo = UserManager.UserInfo;
                if (userInfo == null)
                    return RedirectToAction("Index", "Home");
                model.UserId = userInfo.UserId;
                isPasswordSet = userInfo.IsPasswordSet;
            }

            model.IsPartial = isPartial;
            ViewBag.PasswordValidationRules = new Security.PasswordPolicyManager(string.Empty);
            if (!isPasswordSet)
            {
                userBo.GetUser(UserManager.UserInfo, model);
                if (isPartial)
                    return PartialView(model);
                return View(model);
            }

            if (isPartial)
                return PartialView(model);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [AuthorizationFilter]
        public ActionResult UserChangePassword(int? userId, string password, bool? isPartial)
        {
            UserBL userBo = new UserBL();
            UserIndexViewModel model = new UserIndexViewModel();
            model.UserId = userId.GetValue<int>();
            userBo.GetUser(UserManager.UserInfo, model);
            model.Password = new PasswordSecurityProvider().GenerateHashedPassword(password);
            //Password Policy
            var manager = new Security.PasswordPolicyManager(password);

            foreach (var rule in manager.PasswordRuleValidators)
            {
                if (rule is AllowSpecialCharRule)
                {
                    if (!rule.Validate())
                        return Json(new { Status = 0, Message = MessageResource.PasswordPolicy_Validation_AllowSpecialCharRule });
                }
                if (rule is AlphaNumericCharCountRule)
                {
                    if (!rule.Validate())
                        return Json(new { Status = 0, Message = MessageResource.PasswordPolicy_Validation_AlphaNumericCharCountRule });
                }
                if (rule is NumericCharCountRule)
                {
                    if (!rule.Validate())
                        return Json(new { Status = 0, Message = MessageResource.PasswordPolicy_Validation_NumericCharCountRule });
                }
                if (rule is PasswordLengthRule)
                {
                    if (!rule.Validate())
                        return Json(new { Status = 0, Message = MessageResource.PasswordPolicy_Validation_PasswordLengthRule });
                }
                if (rule is OldPasswordCheckRule)
                {
                    if (!rule.Validate())
                        return Json(new { Status = 0, Message = MessageResource.PasswordPolicy_Validation_OldPasswordCheckRule });
                }
                if (rule is UpperCharCountRule)
                {
                    if (!rule.Validate())
                        return Json(new { Status = 0, Message = MessageResource.PasswordPolicy_Validation_UpperCharCountRule });
                }
            }


            userBo.DMLUserPassword(UserManager.UserInfo, model);

            if (!isPartial.GetValue<bool>())
            {
                if (!CheckErrorForMessage(model, false))
                {
                    var userInfoModified = UserManager.UserInfo;
                    userInfoModified.IsPasswordSet = true;
                    CommonUtility.CreateAuthenticationCookie(userInfoModified);
                    //return RedirectToAction("Index", "Home");
                    return Json(new { Status = 1, Message = MessageResource.Global_Display_Success });
                }
                else
                    return UserChangePassword(model.UserId);
            }
            else
            {
                SetMessage(MessageResource.Global_Display_Success, CommonValues.MessageSeverity.Success);
                return Json(new { Status = 1, Message = MessageResource.Global_Display_Success });
            }
        }

        #endregion

        #region Dealer User Index
        [AuthorizationFilter(CommonValues.PermissionCodes.User.DealerUserIndex)]
        [HttpGet]
        public ActionResult DealerUserIndex()
        {
            SetDefaults();
            List<SelectListItem> roleTypeList = RoleBL.ListRoleTypeComboByUserType(UserManager.UserInfo, false, true).Data;
            ViewBag.RoleTypeList = roleTypeList;
            var yesnoList = new List<SelectListItem>
                {
                    new SelectListItem() {Value = "True", Text = MessageResource.Global_Display_Active},
                    new SelectListItem() {Value = "False", Text = MessageResource.Global_Display_Passive}
                };
            ViewBag.ActivePassiveList = yesnoList;
            return View();
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.User.DealerUserIndex, CommonValues.PermissionCodes.User.DealerUserDetails)]
        public ActionResult ListDealerUser([DataSourceRequest] DataSourceRequest request, UserListModel model)
        {
            var userBo = new UserBL();
            var v = new UserListModel(request);
            var totalCnt = 0;
            v.SearchIsActive = model.SearchIsActive;
            v.UserCode = model.UserCode;
            v.IdentityNo = model.IdentityNo;
            v.UserFirstName = model.UserFirstName;
            v.UserLastName = model.UserLastName;
            v.DealerId = UserManager.UserInfo.GetUserDealerId();
            v.IsTechnician = 1;

            var returnValue = userBo.ListDealerUsers(UserManager.UserInfo, v, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }

        #endregion

        #region Dealer User Create
        [AuthorizationFilter(CommonValues.PermissionCodes.User.DealerUserIndex, CommonValues.PermissionCodes.User.DealerUserCreate)]
        [HttpGet]
        public ActionResult DealerUserCreate()
        {
            SetDefaults();
            List<SelectListItem> roleTypeList = RoleBL.ListRoleTypeComboByUserType(UserManager.UserInfo, false, true).Data;
            ViewBag.RoleTypeList = roleTypeList;
            UserIndexViewModel model = new UserIndexViewModel
            {
                DealerId = UserManager.UserInfo.GetUserDealerId().ToString(),
                IsActive = true,
                LanguageCode = "TR"
            };
            return View(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.User.DealerUserIndex, CommonValues.PermissionCodes.User.DealerUserCreate)]
        [HttpPost]
        public ActionResult DealerUserCreate(UserIndexViewModel viewModel, IEnumerable<HttpPostedFileBase> attachments)
        {
            SetDefaults();
            List<SelectListItem> roleTypeList = RoleBL.ListRoleTypeComboByUserType(UserManager.UserInfo, false, true).Data;
            ViewBag.RoleTypeList = roleTypeList;
            if (attachments == null)
            {
                SetMessage(MessageResource.User_Warning_Photo, CommonValues.MessageSeverity.Fail);
                return View(viewModel);
            }
            var userBo = new UserBL();
            // kullanıcı kodu kontrolü
            int totalCount = 0;
            UserListModel listModel = new UserListModel();
            listModel.UserCode = viewModel.UserCode;
            userBo.ListDealerUsers(UserManager.UserInfo, listModel, out totalCount);
            if (totalCount != 0)
            {
                SetMessage(MessageResource.User_Warning_DuplicateCode, CommonValues.MessageSeverity.Fail);
                return View(viewModel);
            }
            if (!string.IsNullOrWhiteSpace(viewModel.TCIdentityNo.ToString()))
            {
                var hasUser = userBo.GetAnyUserByTCIdentityNo(viewModel.UserId, viewModel.TCIdentityNo.ToString()).Model;
                if (hasUser)
                {
                    SetMessage(MessageResource.User_Warning_SameTCIdentityUserFound, CommonValues.MessageSeverity.Fail);
                    return View(viewModel);
                }
            }
            else
            {
                var hasUser = userBo.GetAnyUserByPassportNo(viewModel.UserId, viewModel.PassportNo).Model;
                if (hasUser)
                {
                    SetMessage(MessageResource.User_Warning_SamePassportNoUserFound, CommonValues.MessageSeverity.Fail);
                    return View(viewModel);
                }
            }
            if (ModelState.IsValid)
            {

                viewModel.PhotoDocId = SaveAttachments(viewModel.PhotoDocId, attachments);
                viewModel.CommandType = viewModel.UserId > 0
                                            ? CommonValues.DMLType.Update
                                            : CommonValues.DMLType.Insert;
                viewModel.IsTechnician = true;

                string newPassword = string.Empty;
                if (viewModel.CommandType == "I")
                {
                    newPassword = CommonUtility.GeneratePassword();
                    viewModel.Password = new PasswordSecurityProvider().GenerateHashedPassword(newPassword);
                }

                userBo.DMLUser(UserManager.UserInfo, viewModel);
                CheckErrorForMessage(viewModel, true);

                if (viewModel.ErrorNo == 0)
                {
                    //kullanıcı ünvan
                    UserTitleViewModel utModel = new UserTitleViewModel
                    {
                        UserId = viewModel.UserId,
                        TitleId = viewModel.RoleTypeId,
                        CommandType = CommonValues.DMLType.Insert
                    };
                    UserTitleBL utBo = new UserTitleBL();
                    utBo.DMLUserTitle(UserManager.UserInfo, utModel);
                    if (utModel.ErrorNo == 0)
                    {
                        SetMessage(MessageResource.Global_Display_Success, CommonValues.MessageSeverity.Success);
                        ModelState.Clear();
                        UserIndexViewModel newModel = new UserIndexViewModel
                        {
                            DealerId = UserManager.UserInfo.GetUserDealerId().ToString(),
                            IsActive = true,
                            LanguageCode = "TR"
                        };
                        return View(newModel);
                    }
                    else
                    {
                        SetMessage(viewModel.ErrorMessage, CommonValues.MessageSeverity.Fail);
                    }
                }
                else
                {
                    SetMessage(viewModel.ErrorMessage, CommonValues.MessageSeverity.Fail);
                }
            }
            return View(viewModel);
        }

        #endregion

        #region Dealer User Update
        [AuthorizationFilter(CommonValues.PermissionCodes.User.DealerUserIndex, CommonValues.PermissionCodes.User.DealerUserUpdate)]
        [HttpGet]
        public ActionResult DealerUserUpdate(int id = 0)
        {
            var v = new UserIndexViewModel();
            if (id > 0)
            {
                var userBo = new UserBL();
                SetDefaults();
                List<SelectListItem> roleTypeList = RoleBL.ListRoleTypeComboByUserType(UserManager.UserInfo, false, true).Data;
                ViewBag.RoleTypeList = roleTypeList;
                v.UserId = id;
                userBo.GetUser(UserManager.UserInfo, v);
            }
            return View(v);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.User.DealerUserIndex, CommonValues.PermissionCodes.User.DealerUserUpdate)]
        [HttpPost]
        public ActionResult DealerUserUpdate(UserIndexViewModel viewModel, IEnumerable<HttpPostedFileBase> attachments)
        {
            SetDefaults();
            List<SelectListItem> roleTypeList = RoleBL.ListRoleTypeComboByUserType(UserManager.UserInfo, false, true).Data;
            ViewBag.RoleTypeList = roleTypeList;
            if (attachments == null && viewModel.PhotoDocId == 0)
            {
                SetMessage(MessageResource.User_Warning_Photo, CommonValues.MessageSeverity.Fail);
                return View(viewModel);
            }

            var userBo = new UserBL();
            // kullanıcı kodu kontrolü
            int totalCount = 0;
            UserListModel listModel = new UserListModel();
            listModel.UserCode = viewModel.UserCode;
            List<UserListModel> userList = userBo.ListDealerUsers(UserManager.UserInfo, listModel, out totalCount).Data;
            var control = (from u in userList.AsEnumerable()
                           where u.UserId != viewModel.UserId
                           select u);
            if (control.Any())
            {
                SetMessage(MessageResource.User_Warning_DuplicateCode, CommonValues.MessageSeverity.Fail);
                return View(viewModel);
            }
            if (!string.IsNullOrWhiteSpace(viewModel.TCIdentityNo.ToString()))
            {
                var hasUser = userBo.GetAnyUserByTCIdentityNo(viewModel.UserId, viewModel.TCIdentityNo.ToString()).Model;
                if (hasUser)
                {
                    SetMessage(MessageResource.User_Warning_SameTCIdentityUserFound, CommonValues.MessageSeverity.Fail);
                    return View(viewModel);
                }
            }
            else
            {
                var hasUser = userBo.GetAnyUserByPassportNo(viewModel.UserId, viewModel.PassportNo).Model;
                if (hasUser)
                {
                    SetMessage(MessageResource.User_Warning_SamePassportNoUserFound, CommonValues.MessageSeverity.Fail);
                    return View(viewModel);
                }
            }
            if (ModelState.IsValid)
            {
                viewModel.PhotoDocId = SaveAttachments(viewModel.PhotoDocId, attachments);
                viewModel.CommandType = viewModel.UserId > 0
                                            ? CommonValues.DMLType.Update
                                            : CommonValues.DMLType.Insert;
                viewModel.IsTechnician = true;
                userBo.DMLUser(UserManager.UserInfo, viewModel);
                CheckErrorForMessage(viewModel, true);
                if (viewModel.ErrorNo == 0)
                {
                    UserTitleViewModel utModel = new UserTitleViewModel
                    {
                        UserId = viewModel.UserId,
                        TitleId = viewModel.RoleTypeId,
                        CommandType = CommonValues.DMLType.Update
                    };
                    UserTitleBL utBo = new UserTitleBL();
                    utBo.DMLUserTitle(UserManager.UserInfo, utModel);
                    if (utModel.ErrorNo == 0)
                    {
                        SetMessage(MessageResource.Global_Display_Success, CommonValues.MessageSeverity.Success);
                        ModelState.Clear();
                    }
                    else
                    {
                        SetMessage(viewModel.ErrorMessage, CommonValues.MessageSeverity.Fail);
                    }
                }
                else
                {
                    SetMessage(viewModel.ErrorMessage, CommonValues.MessageSeverity.Fail);
                }
            }
            return View(viewModel);
        }

        #endregion
        #region Dealer User Convert
        public ContentResult DealerUserConvert(int id)
        {
            var userBo = new UserBL();
            userBo.ConvertUser(id);
            SetMessage(MessageResource.Global_Display_Success, CommonValues.MessageSeverity.Success);
            return Content("<h3>İşlem Başarılı</h3>");
        }
        #endregion

        #region Dealer User Delete

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(CommonValues.PermissionCodes.User.DealerUserIndex, CommonValues.PermissionCodes.User.DealerUserDelete)]
        public ActionResult DeleteDealerUser(int userId)
        {
            UserIndexViewModel viewModel = new UserIndexViewModel() { UserId = userId };
            var userBo = new UserBL();
            viewModel.CommandType = viewModel.UserId > 0 ? CommonValues.DMLType.Delete : string.Empty;
            userBo.DMLUser(UserManager.UserInfo, viewModel);

            ModelState.Clear();
            if (viewModel.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success,
                    MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error,
                viewModel.ErrorMessage);
        }
        #endregion

        #region Dealer User Details
        [AuthorizationFilter(CommonValues.PermissionCodes.User.DealerUserIndex, CommonValues.PermissionCodes.User.DealerUserDetails)]
        [HttpGet]
        public ActionResult DealerUserDetails(int id = 0)
        {
            var v = new UserIndexViewModel();
            var userBo = new UserBL();

            v.UserId = id;
            userBo.GetUser(UserManager.UserInfo, v);

            return View(v);
        }

        #endregion

        #region User TC Identity Confirm
        [HttpGet]
        public ActionResult UserTCIdentityConfirm(int? userId, int? index)
        {
            var userBo = new UserBL();
            UserIndexViewModel model = new UserIndexViewModel();
            model.UserId = userId.GetValue<int>();
            model.Index = index.GetValue<int>();
            userBo.GetUser(UserManager.UserInfo, model);
            return View(model);
        }


        #endregion


        [AuthorizationFilter(CommonValues.PermissionCodes.User.UserIndex)]
        [HttpPost]
        public JsonResult CheckDealerIsForeign(int? dealerId)
        {
            if (!dealerId.HasValue)
                return Json(new { result = false });

            DealerBL dealerBl = new DealerBL();
            var dealer = dealerBl.GetDealer(UserManager.UserInfo, dealerId.Value).Model;

            if (dealer.Country == 1)
                return Json(new { result = false });

            return Json(new { result = true });
        }
    }
}
