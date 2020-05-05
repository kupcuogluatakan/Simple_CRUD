using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSData;
using ODMSModel;
using ODMSModel.ContractPart;
using ODMSModel.SparePart;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSBusiness.Business
{
    public class ContractPartBL : BaseBusiness
    {
        private readonly ContractPartData data = new ContractPartData();
        private string partCode;

        public ContractPartBL()
        {
        }

        public ResponseModel<ContractPartListModel> ListContractPart(UserInfo user, ContractPartListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<ContractPartListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListContractPart(user, filter, out totalCnt);
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

        public ResponseModel<ContractPartViewModel> GetContractPart(UserInfo user, ContractPartViewModel filter)
        {
            var response = new ResponseModel<ContractPartViewModel>();
            try
            {
                response.Model = data.GetContractPart(user, filter);
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

        public ResponseModel<ContractPartViewModel> DMLContractPart(UserInfo user, ContractPartViewModel model)
        {
            var response = new ResponseModel<ContractPartViewModel>();
            try
            {
                data.DMLContractPart(user, model);
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

        public MemoryStream SetExcelReport(List<ContractPartViewModel> modelList, string errMsg)
        {
            string errorMessage = CommonValues.RowStart + errMsg + CommonValues.RowEnd;
            string preTable = CommonValues.TableStart + CommonValues.RowStart
                + CommonValues.ColumnStart + "ContractId" //+ MessageResource.ContractPart_Display_ContractIdGrp
                + CommonValues.ColumnEnd + CommonValues.ColumnStart + "Part Code"//MessageResource.Labour_Display_SubGrp
                + CommonValues.ColumnEnd;
            if (modelList != null)
            {
                preTable = preTable + ("<TD width='400px'>Log</TD>");
            }
            const string lastTable = CommonValues.RowEnd + CommonValues.TableEnd;
            MemoryStream ms = new MemoryStream();

            var sw = new StreamWriter(ms, Encoding.UTF8);

            StringBuilder sb = new StringBuilder();
            sb.Append(errorMessage);
            sb.Append(preTable);
            if (modelList != null)
            {
                foreach (var model in modelList)
                {
                    sb.AppendFormat(CommonValues.RowStart +
                                    CommonValues.ColumnStart + "{0}" + CommonValues.ColumnEnd +
                                    CommonValues.ColumnStart + "{1}" + CommonValues.ColumnEnd,
                                    model.IdContract, model.IdPart);
                    if (model.ErrorNo > 0)
                    {
                        sb.AppendFormat("<TD bgcolor='#FFCCCC'>{0}</TD>" + CommonValues.RowStart, model.ErrorMessage);
                    }
                    else
                    {
                        sb.AppendFormat(CommonValues.ColumnStart + CommonValues.ColumnEnd + CommonValues.RowEnd);
                    }
                }
            }
            sb.Append(lastTable);

            sw.WriteLine(sb.ToString());

            sw.Flush();
            ms.Seek(0, SeekOrigin.Begin);

            return ms;
        }

        public ResponseModel<ContractPartViewModel> ParseExcel(UserInfo user, ContractPartViewModel model, Stream s)
        {
            List<ContractPartViewModel> excelList = new List<ContractPartViewModel>();
            var spBo = new SparePartBL();

            var response = new ResponseModel<ContractPartViewModel>();
            try
            {
                DataSet ds = ExcelHelper.GetDataFromExcel(s);
                DataTable excelRows = ds.Tables[0];

                int totalCount = 0;
                ContractPartBL contractPartBo = new ContractPartBL();
                //VehicleCodeBL vehicleCodeBo = new VehicleCodeBL();
                //VehicleModelBL vehicleModelBo = new VehicleModelBL();
                //VehicleTypeBL vehicleTypeBo = new VehicleTypeBL();

                #region ColumnControl
                if (excelRows.Columns.Count != 2)
                {
                    model.ErrorNo = 1;
                    model.ErrorMessage = MessageResource.Labour_Warning_ColumnProblem;
                }
                #endregion

                foreach (DataRow excelRow in excelRows.Rows)
                {
                    ContractPartViewModel row = new ContractPartViewModel { };

                    #region IdContract

                    int idContract = excelRow[0].GetValue<int>();
                    if (idContract <= 0)
                    {
                        // exceldeki ilgili satırın yanına hata yazılır.
                        row.ErrorNo = 1;
                        row.ErrorMessage = MessageResource.ContractPart_Warning_EmtryContractId;
                    }
                    else
                    {
                        ContractPartViewModel contractPartModel = new ContractPartViewModel();
                        contractPartModel.IdContract = idContract;
                        contractPartBo.GetContractPart(user, contractPartModel);

                        if (contractPartModel.IdContract == 0)
                        {
                            // exceldeki ilgili satırın yanına hata yazılır.
                            row.ErrorNo = 1;
                            row.ErrorMessage = MessageResource.ContractPart_Warning_EmtryContractId;
                        }
                        else
                        {
                            row.IdContractPart = contractPartModel.IdContractPart;
                        }
                        row.IdContract = idContract;
                    }

                    #endregion

                    #region PartCode

                    string partCode = excelRow[1].GetValue<string>();
                    row.PartCode = partCode;
                    if (partCode == "")
                    {
                        // exceldeki ilgili satırın yanına hata yazılır.
                        row.ErrorNo = 1;
                        row.ErrorMessage = "PartCode Boş";//MessageResource.LabourDuration_Warning_EmptyVehicleType;
                    }
                    else
                    {
                        //ContractPartListModel contractPartListModel = new ContractPartListModel();
                        //contractPartListModel.PartCode = this.partCode;

                        SparePartIndexViewModel spModel = new SparePartIndexViewModel();
                        spModel.PartCode = partCode;
                        spBo.GetSparePart(user, spModel);


                        //List<ContractPartListModel> contractPartList = contractPartBo.ListContractPart(user, contractPartListModel, out totalCount).Data;
                        if (spModel.PartId == 0)
                        {
                            // exceldeki ilgili satırın yanına hata yazılır.
                            row.ErrorNo = 1;
                            row.ErrorMessage = string.Format(MessageResource.LabourDuration_Warning_CannotFindVehicleType, this.partCode);
                        }
                        else
                        {
                            row.IdPart = spModel.PartId;
                        }
                    }

                    #endregion


                    var existed = (from r in excelList.AsEnumerable()
                                   where r.IdContract == row.IdContract
                                   && r.IdPart == row.IdPart
                                   select r);
                    if (existed.Any())
                    {
                        // exceldeki ilgili satırın yanına hata yazılır.
                        row.ErrorNo = 1;
                        row.ErrorMessage = MessageResource.Labour_Warning_DuplicateValue;
                    }

                    excelList.Add(row);
                }


                var errorCount = (from r in excelList.AsEnumerable()
                                  where r.ErrorNo == 1
                                  select r);
                if (errorCount.Any())
                {
                    model.ErrorNo = 1;
                    model.ErrorMessage = MessageResource.Labour_Warning_FoundError;
                }
                if (excelRows.Rows.Count == 0)
                {
                    model.ErrorNo = 1;
                    model.ErrorMessage = MessageResource.DealerStartupInventoryLevel_Warning_EmptyExcel;
                }

                response.Data = excelList;
                response.Total = excelList.Count;
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

    }
}
