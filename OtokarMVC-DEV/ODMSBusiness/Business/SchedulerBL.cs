using System;
using System.Collections.Generic;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSModel.Schedule;
using ODMSData;
using ODMSModel.Appointment;
using ODMSCommon.Security;

namespace ODMSBusiness
{
    public class SchedulerBL : BaseService<ScheduleViewModel>
    {
        private readonly ScheduleData data = new ScheduleData();
        private AppointmentBL _appointmentService = new AppointmentBL();

        public ResponseModel<ScheduleViewModel> List(UserInfo user,ScheduleViewModel filter, out int totalCnt)
        {
            var response = new ResponseModel<ScheduleViewModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.List(user,filter, out totalCnt);
                response.Total = totalCnt;
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false; 
 				 AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        public ResponseModel<ScheduleViewModel> Insert(UserInfo user,ScheduleViewModel model)
        {
            var response = new ResponseModel<ScheduleViewModel>();
            if (model.OptionValue == ((int)SchedulerOptionValue.Appointment).ToString())
            {
                var appModel = new AppointmentViewModel
                {
                    #region Mapping

                    AppointmentId = model.AppointmentId.GetValue<int>(),
                    CommandType = model.CommandType,
                    AppointmentDate = model.AppointmentDateFormatted.GetValue<DateTime>(),
                    AppointmentTime = TimeSpan.Parse(model.AppointmentTimeFormatted),
                    AppointmentTypeId = model.AppointmentTypeId,
                    AppointmentTypeName = model.AppointmentTypeName,
                    ComplaintDescription = model.ComplaintDescription,
                    ContactAddress = model.ContactAddress,
                    ContactLastName = model.ContactLastName,
                    ContactName = model.ContactName,
                    ContactFirstName = model.ContactFirstName,
                    ContactPhone = model.ContactPhone,
                    CustomerId = model.CustomerId,
                    CustomerName = model.CustomerName,
                    DealerId = model.DealerId,
                    DealerName = model.DealerName,
                    VehicleColor = model.VehicleColor,
                    VehicleId = model.VehicleId,
                    VehicleIdVehiclePlate = model.VehicleIdVehiclePlate,
                    VehicleModelCode = model.VehicleModelCode,
                    VehicleModelName = model.VehicleModelName,
                    VehiclePlate = model.VehiclePlate,
                    VehicleType = model.VehicleType,
                    VehicleTypeName = model.VehicleTypeName,
                    VehicleVin = model.VehicleVin,
                    DeliveryEstimateDate = model.DeliveryEstimateDate,
                    DeliveryEstimateTime = model.DeliveryEstimateTime,
                    StatusId = model.StatusId

                    #endregion
                };

                _appointmentService.DMLAppointment(user, appModel);
                model.AppointmentId = appModel.AppointmentId;
                model.ErrorNo = appModel.ErrorNo;
                model.ErrorMessage = appModel.ErrorMessage;
            }
            
            else
            {
                try
                {
                    data.Insert(user, model);
                    response.Model = model;
                    response.Message = MessageResource.Global_Display_Success;
                    if (model.ErrorNo > 0)
                        throw new System.Exception(model.ErrorMessage);
                }
                catch (System.Exception ex)
                {
                    response.IsSuccess = false; 
 				 AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                    response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
                }
                return response;
            }
            return response;
        }

        public ResponseModel<ScheduleViewModel> Update(UserInfo user,ScheduleViewModel model)
        {
            var response = new ResponseModel<ScheduleViewModel>();
            if (model.OptionValue == ((int)SchedulerOptionValue.Appointment).ToString())
            {
                var appModel = new AppointmentViewModel
                {
                    #region Mapping

                    AppointmentId = model.AppointmentId.GetValue<int>(),
                    CommandType = model.CommandType,
                    AppointmentDate = model.AppointmentDateFormatted.GetValue<DateTime>(),
                    AppointmentTime = TimeSpan.Parse(model.AppointmentTimeFormatted),
                    AppointmentTypeId = model.AppointmentTypeId,
                    AppointmentTypeName = model.AppointmentTypeName,
                    ComplaintDescription = model.ComplaintDescription,
                    ContactAddress = model.ContactAddress,
                    ContactLastName = model.ContactLastName,
                    ContactFirstName = model.ContactFirstName,
                    ContactName = model.ContactName,
                    ContactPhone = model.ContactPhone,
                    CustomerId = model.CustomerId,
                    CustomerName = model.CustomerName,
                    DealerId = model.DealerId,
                    DealerName = model.DealerName,
                    VehicleColor = model.VehicleColor,
                    VehicleId = model.VehicleId,
                    VehicleIdVehiclePlate = model.VehicleIdVehiclePlate,
                    VehicleModelCode = model.VehicleModelCode,
                    VehicleModelName = model.VehicleModelName,
                    VehiclePlate = model.VehiclePlate,
                    VehicleType = model.VehicleType,
                    VehicleTypeName = model.VehicleTypeName,
                    VehicleVin = model.VehicleVin,
                    DeliveryEstimateDate = model.DeliveryEstimateDate,
                    DeliveryEstimateTime = model.DeliveryEstimateTime,
                    StatusId = model.StatusId
                    #endregion
                };

                _appointmentService.DMLAppointment(user, appModel);
                model.ErrorNo = appModel.ErrorNo;
                model.ErrorMessage = appModel.ErrorMessage;
            }
            else
            {
                try
                {
                    data.Update(user, model);
                    response.Model = model;
                    response.Message = MessageResource.Global_Display_Success;
                    if (model.ErrorNo > 0)
                        throw new System.Exception(model.ErrorMessage);
                }
                catch (System.Exception ex)
                {
                    response.IsSuccess = false; 
 				 AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                    response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
                }
                return response;
            }
            return response;
        }

        public ResponseModel<ScheduleViewModel> Delete(UserInfo user,ScheduleViewModel model)
        {
            var response = new ResponseModel<ScheduleViewModel>();
            try
            {
                data.Delete(user, model);
                response.Model = model;
                response.Message = MessageResource.Global_Display_Success;
                if (model.ErrorNo > 0)
                    throw new System.Exception(model.ErrorMessage);
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false; 
 				 AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }
            return response;
        }

        public ResponseModel<ScheduleViewModel> Get(UserInfo user,ScheduleViewModel filter)
        {
            var response = new ResponseModel<ScheduleViewModel>();
            try
            {
                if (filter.OptionValue == ((int)SchedulerOptionValue.Appointment).ToString())
                {

                    #region Mapping

                    var appModel = _appointmentService.GetAppointment(user, filter.AppointmentId.GetValue<int>());
                    filter.AppointmentId = appModel.Model.AppointmentId;
                    filter.CommandType = appModel.Model.CommandType;
                    filter.AppointmentDate = appModel.Model.AppointmentDateFormatted.GetValue<DateTime>();
                    filter.AppointmentTime = TimeSpan.Parse(appModel.Model.AppointmentTimeFormatted);
                    filter.AppointmentTypeId = appModel.Model.AppointmentTypeId;
                    filter.AppointmentTypeName = appModel.Model.AppointmentTypeName;
                    filter.ComplaintDescription = appModel.Model.ComplaintDescription;
                    filter.ContactAddress = appModel.Model.ContactAddress;
                    filter.ContactLastName = appModel.Model.ContactLastName;
                    filter.ContactName = appModel.Model.ContactName;
                    filter.ContactPhone = appModel.Model.ContactPhone;
                    filter.CustomerId = appModel.Model.CustomerId;
                    filter.CustomerName = appModel.Model.CustomerName;
                    filter.DealerId = appModel.Model.DealerId;
                    filter.DealerName = appModel.Model.DealerName;
                    filter.VehicleColor = appModel.Model.VehicleColor;
                    filter.VehicleId = appModel.Model.VehicleId;
                    filter.VehicleIdVehiclePlate = appModel.Model.VehicleIdVehiclePlate;
                    filter.VehicleModelCode = appModel.Model.VehicleModelCode;
                    filter.VehicleModelName = appModel.Model.VehicleModelName;
                    filter.VehiclePlate = appModel.Model.VehiclePlate;
                    filter.VehicleType = appModel.Model.VehicleType;
                    filter.VehicleTypeName = appModel.Model.VehicleTypeName;
                    filter.VehicleVin = appModel.Model.VehicleVin;
                    filter.DeliveryEstimateDate = appModel.Model.DeliveryEstimateDate;
                    filter.DeliveryEstimateTime = appModel.Model.DeliveryEstimateTime;
                    filter.StatusId = appModel.Model.StatusId.Value;

                    #endregion

                }
                response.Model = data.Get(filter);
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false; 
 				 AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        public List<ScheduleViewModel> WorkHourList(UserInfo user,DateTime date, int dealerId)
        {

            if (user.LanguageCode == "TR" && date.DayOfWeek == DayOfWeek.Sunday)
            {
                date = date.AddDays(-7);
            }

            var listModel = new List<ScheduleViewModel>();
            var lastDayOfWeek = date.GetLastDayOfWeek().DayOfYear;
            var diffWeek = Math.Abs(date.DayOfYear - lastDayOfWeek);

            if (UserManager.LanguageCode == "TR")
            {
                diffWeek = diffWeek + 1;
            }

            var diffDate = date.AddDays(diffWeek);

            for (; date <= diffDate; date = date.AddDays(1))
            {
                var currentModelList = new List<ScheduleViewModel>();
                var workHourList = data.WorkHourList(date, dealerId);
                foreach (var item in workHourList)
                {
                    var model = new ScheduleViewModel();
                    bool isDay = false;

                    switch (date.DayOfWeek)
                    {
                        case DayOfWeek.Friday:
                            isDay = item.Friday;
                            break;
                        case DayOfWeek.Monday:
                            isDay = item.Monday;
                            break;
                        case DayOfWeek.Saturday:
                            isDay = item.Saturday;
                            break;
                        case DayOfWeek.Sunday:
                            isDay = item.Sunday;
                            break;
                        case DayOfWeek.Thursday:
                            isDay = item.Thursday;
                            break;
                        case DayOfWeek.Tuesday:
                            isDay = item.Tuesday;
                            break;
                        case DayOfWeek.Wednesday:
                            isDay = item.Wednesday;
                            break;
                    }

                    if (isDay && item.WorkStat)
                    {
                        model.Start = new DateTime(date.Year, date.Month, date.Day, item.LunchBreakStart.Hour, item.LunchBreakStart.Minute, 0, DateTimeKind.Utc);
                        model.End = new DateTime(date.Year, date.Month, date.Day, item.LunchBreakEnd.Hour, item.LunchBreakEnd.Minute, 0, DateTimeKind.Utc);
                        model.OptionValue = ((int)SchedulerOptionValue.UnAppoinment).ToString();

                        currentModelList.Add(model);

                        break;
                    }
                    if (isDay && !item.WorkStat)
                    {
                        model.Start = new DateTime(date.Year, date.Month, date.Day, item.WorkHourStart.Hour, item.WorkHourStart.Minute, 0, DateTimeKind.Utc);
                        model.End = new DateTime(date.Year, date.Month, date.Day, item.WorkHourEnd.Hour, item.WorkHourEnd.Minute, 0, DateTimeKind.Utc);
                        model.OptionValue = ((int)SchedulerOptionValue.UnAppoinment).ToString();

                        currentModelList.Add(model);

                        break;
                    }
                }

                if (currentModelList.Count == 0)
                {
                    var model = new ScheduleViewModel
                    {
                        Start = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0, DateTimeKind.Utc),
                        End = new DateTime(date.Year, date.Month, date.Day, 23, 59, 00, DateTimeKind.Utc),
                        OptionValue = ((int)SchedulerOptionValue.UnDefined).ToString()
                    };

                    currentModelList.Add(model);
                }

                listModel.AddRange(currentModelList);
            }


            return listModel;
        }

        public new bool Exists(ScheduleViewModel model)
        {
            bool resultWorkHours = false;

            DateTime fullDate = new DateTime();
            if (model.OptionValue == ((int)SchedulerOptionValue.Appointment).ToString())
                fullDate = new DateTime(model.AppointmentDate.Year, model.AppointmentDate.Month, model.AppointmentDate.Day, model.AppointmentTime.Value.Hours, model.AppointmentTime.Value.Minutes, 0);
            else if (model.OptionValue == ((int)SchedulerOptionValue.UnAppoinment).ToString())
                fullDate = new DateTime(model.AppointmentStartDate.Year, model.AppointmentStartDate.Month, model.AppointmentStartDate.Day, model.AppointmentStartTime.Value.Hours, model.AppointmentStartTime.Value.Minutes, 0);

            var workHourList = data.WorkHourList(fullDate, model.DealerId);

            int countCheck = 0;
            #region WorkHours Control
            foreach (var item in workHourList)
            {
                bool isDay = true;

                switch (fullDate.DayOfWeek)
                {
                    case DayOfWeek.Friday:
                        isDay = item.Friday;
                        break;
                    case DayOfWeek.Monday:
                        isDay = item.Monday;
                        break;
                    case DayOfWeek.Saturday:
                        isDay = item.Saturday;
                        break;
                    case DayOfWeek.Sunday:
                        isDay = item.Sunday;
                        break;
                    case DayOfWeek.Thursday:
                        isDay = item.Thursday;
                        break;
                    case DayOfWeek.Tuesday:
                        isDay = item.Tuesday;
                        break;
                    case DayOfWeek.Wednesday:
                        isDay = item.Wednesday;
                        break;
                }

                //eklenecek kayıt mutlaka workhoursta olması lazım yok ise kırmızıdır belirlenmemiş ise yeşildir. kırmızı ve yeşil alanlara kayıt eklenemez
                if (isDay && item.WorkStat)
                {
                    countCheck = countCheck + 1;
                    var dateWorkHourStart = new DateTime(fullDate.Year, fullDate.Month, fullDate.Day, item.LunchBreakStart.Hour, item.LunchBreakStart.Minute, 0);
                    var dateWorkHourEnd = new DateTime(fullDate.Year, fullDate.Month, fullDate.Day, item.LunchBreakEnd.Hour, item.LunchBreakEnd.Minute, 0);

                    if (fullDate >= dateWorkHourStart && fullDate <= dateWorkHourEnd)
                    {
                        resultWorkHours = true;
                        break;
                    }
                }
                else if (isDay && !item.WorkStat)
                {
                    countCheck = countCheck + 1;
                    var dateNoneWorkHourStart = new DateTime(fullDate.Year, fullDate.Month, fullDate.Day, item.WorkHourStart.Hour, item.WorkHourStart.Minute, 0);
                    var dateNoneWorkHourEnd = new DateTime(fullDate.Year, fullDate.Month, fullDate.Day, item.WorkHourEnd.Hour, item.WorkHourEnd.Minute, 0);

                    if (fullDate >= dateNoneWorkHourStart && fullDate <= dateNoneWorkHourEnd)
                    {
                        resultWorkHours = true;
                        break;
                    }
                }
            }

            if (countCheck == 0)
                resultWorkHours = true;


            #endregion

            #region NonWorkHours Control

            bool resultNoneWorkHours = data.Exists(model);

            #endregion

            if (!resultNoneWorkHours && !resultWorkHours)
                return false;
            return true;
        }
    }

    public static class DateTimeExtensions
    {
        public static DateTime GetLastDayOfWeek(this DateTime sourceDateTime)
        {
            var daysAhead = DayOfWeek.Saturday - (int)sourceDateTime.DayOfWeek;

            sourceDateTime = sourceDateTime.AddDays((int)daysAhead);

            return sourceDateTime;
        }

        public static DateTime GetFirstDayOfWeek(this DateTime sourceDateTime)
        {
            var daysAhead = (DayOfWeek.Sunday - (int)sourceDateTime.DayOfWeek);

            sourceDateTime = sourceDateTime.AddDays((int)daysAhead);

            return sourceDateTime;
        }
    }
}
