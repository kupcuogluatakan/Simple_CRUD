using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Security;
using ODMSModel.LabourTechnician;

namespace ODMS.Controllers
{
    public class TechnicianOperationDetailController : Controller
    {
        public ActionResult TechnicianOperationDetailIndex(int userId)
        {
            LabourTechnicianViewModel model = new LabourTechnicianViewModel();
            model.UserID = userId;
            return PartialView(model);
        }
        public ActionResult ListWaitingLabours([DataSourceRequest] DataSourceRequest request, LabourTechnicianViewModel model)
        {
            var ltBo = new LabourTechnicianBL();

            var v = new LabourTechnicianListModel(request)
                {
                    UserID = model.UserID,
                    StatusId = (int) CommonValues.LabourTechnicianStatus.NotStarted
                };

            var totalCnt = 0;
            var returnValue = ltBo.ListLabourTechnicians(UserManager.UserInfo,v, out totalCnt).Data;

            v.StatusId = (int) CommonValues.LabourTechnicianStatus.Paused;
            returnValue.AddRange(ltBo.ListLabourTechnicians(UserManager.UserInfo, v, out totalCnt).Data);

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }
        public ActionResult ListContinuedLabours([DataSourceRequest] DataSourceRequest request, LabourTechnicianViewModel model)
        {
            var ltBo = new LabourTechnicianBL();

            var v = new LabourTechnicianListModel(request)
            {
                UserID = model.UserID,
                StatusId = (int) CommonValues.LabourTechnicianStatus.Continued
            };

            var totalCnt = 0;
            var returnValue = ltBo.ListLabourTechnicians(UserManager.UserInfo, v, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }

        [HttpPost]
        public ActionResult StartLabourTechnicianDetails(int userId, string idList)
        {
            string errMsg = string.Empty;
            /*
                * Çalışmaya başladım butonuna tıklandığında LABOUR_TECHNICIAN_START_FINISH tablosuna kayıt atılacak.
                * LABOUR_TECHNICIAN_START_FINISH  START_DATE Getdate ile güncellenecek. 
                * LABOUR_TECHNICIAN ın statüsü 1055 e 2 (Devam ediyor olarak set edilecek)
             */
            /*
             * TFS No : 26830 OYA 16.12.2014
                1- Başla denildiğinde ID_LABOUR_TECHNICIAN_START_FINISH =x ile bir kayıt yaratılır ve startdate get date atılır. (örn: 2014-12-16 14:18:21.630)
                2- Ara ver denildiğinde ID_LABOUR_TECHNICIAN_START_FINISH =x kaydının end_date alanına get date_atılır 
                3- ara verilen işe başla denildiğinde aynı id_labour_technician ile ID_LABOUR_TECHNICIAN_START_FINISH =x+1 olan yeni bir kayıt  yaratılır ve start_date e get date atılır.
                4- Bitir denildiğide ID_LABOUR_TECHNICIAN_START_FINISH =x+1 olan kaydın end_date alanına get_date atılır. 
 
                LABOUR_TECHNICIAN.LABOUR_WORK_TIME_REAL alanına x ve x+1 satırlarında ki  (end_date - start_date) sürelerinin toplamı atılır.*/
            LabourTechnicianBL ltBo = new LabourTechnicianBL();
            LabourTechnicianViewModel model = new LabourTechnicianViewModel();
            model.UserID = userId;
            model.LabourTechnicianId = idList.GetValue<int>();
            model.StartDate = DateTime.Now;
            model.EndDate = null;
            model.StatusId = (int) CommonValues.LabourTechnicianStatus.Continued;
            model.CommandType = CommonValues.DMLType.Insert;
            ltBo.DMLLabourTechnicianStartFinish(model);
            if (model.ErrorNo > 0)
                errMsg = model.ErrorMessage;
            //else
            //{
            //    model = ltBo.GetLabourTechnician(UserManager.UserInfo,model).Model;
            //    model.CommandType = CommonValues.DMLType.Update;
            //    model.StatusId = (int) CommonValues.LabourTechnicianStatus.Continued;
            //    ltBo.DMLLabourTechnician(UserManager.UserInfo,model);

            //    if (model.ErrorNo > 0)
            //        errMsg = model.ErrorMessage;
            //}
            return Json(errMsg);
        }

        [HttpPost]
        public ActionResult PauseLabourTechnicianDetails(int userId, string idList)
        {
            string errMsg = string.Empty;
            /*
                * Ara verdim butonuna tıklandığında  LABOUR_TECHNICIAN_START_FINISH  END_DATE Getdate ile güncellenecek. 
                * LABOUR_TECHNICIAN ın statüsü 1055 e 3 (ara verildi) olarak set edilecek.
             */
            /*
                * TFS No : 26830 OYA 16.12.2014
                 1- Başla denildiğinde ID_LABOUR_TECHNICIAN_START_FINISH =x ile bir kayıt yaratılır ve startdate get date atılır. (örn: 2014-12-16 14:18:21.630)
                 2- Ara ver denildiğinde ID_LABOUR_TECHNICIAN_START_FINISH =x kaydının end_date alanına get date_atılır 
                 3- ara verilen işe başla denildiğinde aynı id_labour_technician ile ID_LABOUR_TECHNICIAN_START_FINISH =x+1 olan yeni bir kayıt  yaratılır ve start_date e get date atılır.
                 4- Bitir denildiğide ID_LABOUR_TECHNICIAN_START_FINISH =x+1 olan kaydın end_date alanına get_date atılır. 
 
                 LABOUR_TECHNICIAN.LABOUR_WORK_TIME_REAL alanına x ve x+1 satırlarında ki  (end_date - start_date) sürelerinin toplamı atılır.*/
            LabourTechnicianViewModel model = new LabourTechnicianViewModel();
            model.UserID = userId;
            model.LabourTechnicianId = idList.GetValue<int>();
            model.EndDate = DateTime.Now;
            model.CommandType = CommonValues.DMLType.Update;
            model.StatusId = (int)CommonValues.LabourTechnicianStatus.Paused;
            LabourTechnicianBL ltBo = new LabourTechnicianBL();
            ltBo.DMLLabourTechnicianStartFinish(model);
            if (model.ErrorNo > 0)
                errMsg = model.ErrorMessage;
            //else
            //{
            //    model = ltBo.GetLabourTechnician(UserManager.UserInfo,model).Model;
            //    model.CommandType = CommonValues.DMLType.Update;
            //    model.StatusId = (int)CommonValues.LabourTechnicianStatus.Paused;
            //    ltBo.DMLLabourTechnician(UserManager.UserInfo, model);

            //    if (model.ErrorNo > 0)
            //        errMsg = model.ErrorMessage;
            //}
            return Json(errMsg);
        }

        [HttpPost]
        public ActionResult EndLabourTechnicianDetails(int userId, string idList)
        {
            string errMsg = string.Empty;
            /*
                * Çalışmayı Tamamladım butonuna tıklandığında  LABOUR_TECHNICIAN_START_FINISH  END_DATE Getdate ile güncellenecek. 
                * LABOUR_TECHNICIAN ın statüsü 1055 e 4 (tamamlandı) olarak set edilecek.
                * LABOUR_TECHNICIAN_START_FINISH tablosundan END_DATE - START_DATE yapılarak işin ne kadarda yapıldığı hesaplanacak ve 
                    * LABOUR_TECHNICIAN tablosunda ki  LABOUR_WORK_TIME_REAL alanına yazılacak.
             */
            /*
                * TFS No : 26830 OYA 16.12.2014
                 1- Başla denildiğinde ID_LABOUR_TECHNICIAN_START_FINISH =x ile bir kayıt yaratılır ve startdate get date atılır. (örn: 2014-12-16 14:18:21.630)
                 2- Ara ver denildiğinde ID_LABOUR_TECHNICIAN_START_FINISH =x kaydının end_date alanına get date_atılır 
                 3- ara verilen işe başla denildiğinde aynı id_labour_technician ile ID_LABOUR_TECHNICIAN_START_FINISH =x+1 olan yeni bir kayıt  yaratılır ve start_date e get date atılır.
                 4- Bitir denildiğide ID_LABOUR_TECHNICIAN_START_FINISH =x+1 olan kaydın end_date alanına get_date atılır. 
 
                 LABOUR_TECHNICIAN.LABOUR_WORK_TIME_REAL alanına x ve x+1 satırlarında ki  (end_date - start_date) sürelerinin toplamı atılır.*/
            LabourTechnicianViewModel model = new LabourTechnicianViewModel();
            model.UserID = userId;
            model.LabourTechnicianId = idList.GetValue<int>();
            model.EndDate = DateTime.Now;
            model.CommandType = CommonValues.DMLType.Update;
            model.StatusId = (int)CommonValues.LabourTechnicianStatus.Completed;
            LabourTechnicianBL ltBo = new LabourTechnicianBL();
            ltBo.DMLLabourTechnicianStartFinish(model);
            if (model.ErrorNo > 0)
                errMsg = model.ErrorMessage;
            //else
            //{
            //    LabourTechnicianViewModel sfModel = new LabourTechnicianViewModel();
            //    sfModel.LabourTechnicianId = model.LabourTechnicianId;
            //    sfModel.UserID = model.UserID;
            //    List<LabourTechnicianViewModel> list = ltBo.GetLabourTechnicianStartFinish(sfModel).Data;
            //    double totalHours = 0;
            //    foreach (var labourTechnicianViewModel in list)
            //    {
            //        totalHours +=
            //            (DateTime.Parse(labourTechnicianViewModel.EndDate.GetValue<string>())
            //                     .Subtract(DateTime.Parse(labourTechnicianViewModel.StartDate.GetValue<string>()))).TotalHours;
            //    }

            //    model = ltBo.GetLabourTechnician(UserManager.UserInfo, model).Model;
            //    model.WorkTimeReal = totalHours.GetValue<long>();
            //    model.CommandType = CommonValues.DMLType.Update;
            //    model.StatusId = (int)CommonValues.LabourTechnicianStatus.Completed;
            //    ltBo.DMLLabourTechnician(UserManager.UserInfo, model);

            //    if (model.ErrorNo > 0)
            //        errMsg = model.ErrorMessage;
            //}
            return Json(errMsg);
        }
    }
}
