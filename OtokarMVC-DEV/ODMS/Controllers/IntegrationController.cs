using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness.Business;
using ODMSCommon;
using ODMSModel.Integration;
using System.Web.Mvc;
using System.Linq;
using ODMSCommon.Security;

namespace ODMS.Controllers
{
    public class IntegrationController : ControllerBase
    {
        private readonly IIntegrationBL integrationBL_ = new IntegrationBL();

        public IntegrationController()
        {

        }

        /// <summary>
        /// Entegrasyon listesi sayfası
        /// </summary>
        [HttpGet]
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.Bank.BankIndex)]
        public ActionResult Index()
        {
            var filter = new IntegrationListModel();
            filter.IsActive = true;
            var list = integrationBL_.GetIntegrationList(UserManager.UserInfo, filter).Data;
            return View(list);
        }

        /// <summary>
        /// Hatalı entegrasyon sayısını verir
        /// </summary>
        public int IntegrationCountWithError()
        {
            var filter = new IntegrationListModel();
            filter.IsActive = true;
            var list = integrationBL_.GetIntegrationList(UserManager.UserInfo, filter).Data;
            return list.Where(x => x.LastSuccessDate < x.LastCallDate).Count();
        }

        /// <summary>
        /// Entegrasyon detay sayfası
        /// </summary>
        /// <param name="id">Entegrasyon tipi Id</param>
        [HttpGet]
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.Campaign.CampaignIndex)]
        public ActionResult Detail(int id)
        {
            var filter = new IntegrationListModel();
            ViewBag.IntegrationTypeList = integrationBL_.GetIntegrationList(UserManager.UserInfo, filter).Data;

            var model = new IntegrationDetailListModel();
            model.IntegrationTypeId = id;
            return View(model);
        }

        /// <summary>
        /// Entegrasyon detayları grid datası
        /// </summary>
        [AuthorizationFilter(CommonValues.PermissionCodes.Campaign.CampaignIndex)]
        public ActionResult DetailList([DataSourceRequest] DataSourceRequest request, IntegrationDetailListModel filterDetail)
        {
            var filter = new IntegrationDetailListModel(request);
            filter.IntegrationTypeId = filterDetail.IntegrationTypeId;

            var listDetail = integrationBL_.GetIntegrationDetailList(UserManager.UserInfo,filter).Data;

            return Json(new
            {
                Data = listDetail,
                Total = filter.TotalCount
            });
        }

        /// <summary>
        /// Entegrasyon istek parametreleri sayfası
        /// </summary>
        /// <param name="id">Log Id</param>
        [AuthorizationFilter(CommonValues.PermissionCodes.Campaign.CampaignIndex)]
        [HttpGet]
        public ActionResult RequestDetail(int id)
        {
            var filter = new IntegrationDetailListModel();
            filter.LogId = id;
            var detail =  integrationBL_.GetIntegrationDetailList(UserManager.UserInfo, filter).Data.FirstOrDefault();
            return View(detail);
        }

        /// <summary>
        /// Entegrasyon cevap sayfası
        /// </summary>
        /// <param name="id">Log Id</param>
        [AuthorizationFilter(CommonValues.PermissionCodes.Campaign.CampaignIndex)]
        [HttpGet]
        public ActionResult ResponseDetail(int id)
        {
            var filter = new IntegrationDetailListModel();
            filter.LogId = id;
            var detail = integrationBL_.GetIntegrationDetailList(UserManager.UserInfo, filter).Data.FirstOrDefault();
            return View(detail);
        }

        /// <summary>
        /// Entegrasyon hata detayı sayfası
        /// </summary>
        /// <param name="id">Log Id</param>
        [AuthorizationFilter(CommonValues.PermissionCodes.Campaign.CampaignIndex)]
        [HttpGet]
        public ActionResult ErrorDetail(int id)
        {
            var filter = new IntegrationDetailListModel();
            filter.LogId = id;
            var detail = integrationBL_.GetIntegrationDetailList(UserManager.UserInfo, filter).Data.FirstOrDefault();
            return View(detail);
        }
    }
}
