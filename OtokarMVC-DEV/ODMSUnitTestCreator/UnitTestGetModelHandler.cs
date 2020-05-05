using ODMSModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ODMSUnitTestCreator
{
    public class UnitTestGetModelHandler : UnitTestHandler
    {
        public UnitTestGetModelHandler()
        {

        }

        public override void Create(UnitTestHandler parent, string businessName, string methodName, string filterNames, bool dmlContains)
        {
            if (parent != null)
            {
                base.InsertBuilder = parent.InsertBuilder;
                base.UpdateBuilder = parent.UpdateBuilder;
                base.DeleteBuilder = parent.DeleteBuilder;
                base.UpdateMethodName = parent.UpdateMethodName;
                base.DeleteMethodName = parent.DeleteMethodName;
            }

            if (methodName.Contains("Get") && !methodName.Contains("List") && businessName != "CommonBL" && businessName != "ReportsBL")
            {
                Builder = new StringBuilder();

                AddTwoTab();
                Builder.Append("[TestMethod]");
                AddTwoTab();
                Builder.Append(string.Format("public void {0}_{1}_GetModel()", businessName, methodName));
                AddTwoTab();
                Builder.Append("{");

                if (filterNames.Contains("ODMSModel"))
                {
                    var filterList = filterNames.Split(new string[] { "###" }, StringSplitOptions.None);

                    var filterName = string.Empty;
                    filterList.ToList().ForEach(x =>
                    {
                        if (x.Contains("ODMSModel."))
                        {
                            filterName = x.Split(new string[] { "-" }, StringSplitOptions.None)[1];
                        }
                    });

                    var allModel = Assembly.Load("ODMSModel").GetTypes();
                    var model = allModel.FirstOrDefault(x => x.Name == filterName);
                    var propList = model.GetProperties();

                    #region add model define 

                    Builder.Append(InsertBuilder.ToString());

                    #endregion

                    #region filter define 

                    AddThreeTab();
                    AddThreeTab();
                    Builder.Append(string.Format("var filter = new {0}();", filterName));
                    AddThreeTab();

                    Builder.Append(string.Format("filter.Id = result.Model.Id;", filterName));
                    AddThreeTab();

                    #endregion

                    #region filter prop  define

                    foreach (var prop in propList)
                    {
                        var propTypeName = prop.PropertyType.Name;

                        if (!prop.CanWrite)
                            continue;

                        if (prop.Name == "ErrorNo"
                            || prop.Name == "ErrorMessage"
                            || prop.Name == "UpdateTrx"
                            || prop.Name == "StatusId"
                            || prop.Name == "Status"
                            || prop.Name == "SearchIsActive"
                            || prop.Name == "CommandType"
                            || prop.Name == "OperationTransactionId"
                            || prop.Name == "ConditionalCssClassForDealerCombo"
                            || prop.Name == "HideFormElements"

                            || prop.Name == "PartCodeList"
                            || prop.Name == "PartClassList"
                            )
                            continue;

                        if (prop.Name == "SupplierId")
                        {
                            Builder.Append("filter.SupplierId = 782; ");
                            AddThreeTab();
                        }
                        else if (prop.Name == "ModelKod")
                        {
                            Builder.Append("filter.ModelKod = \"ATLAS\"; ");
                            AddThreeTab();
                        }
                        else if (prop.Name == "Code")
                        {
                            Builder.Append("filter.Code = guid; ");
                            AddThreeTab();
                        }
                        else if (prop.Name == "VehicleId")
                        {
                            Builder.Append("filter.VehicleId = 29627; ");
                            AddThreeTab();
                        }
                        else if (prop.Name == "CompanyTypeId")
                        {
                            Builder.Append("filter.CompanyTypeId = 1; ");
                            AddThreeTab();
                        }
                        else if (prop.Name == "CustomerTypeId")
                        {
                            Builder.Append("filter.CustomerTypeId = 2; ");
                            AddThreeTab();
                        }
                        else if (prop.Name == "CountryId")
                        {
                            Builder.Append("filter.CountryId = 1; ");
                            AddThreeTab();
                        }
                        else if (prop.Name == "CityId")
                        {
                            Builder.Append("filter.CityId = 1; ");
                            AddThreeTab();
                        }
                        else if (prop.Name == "TownId")
                        {
                            Builder.Append("filter.TownId = 1; ");
                            AddThreeTab();
                        }
                        else if (prop.Name == "PartId")
                        {
                            Builder.Append("filter.PartId = 39399; ");
                            AddThreeTab();
                        }
                        else if (prop.Name == "PriceListId")
                        {
                            Builder.Append("filter.PriceListId = 14; ");
                            AddThreeTab();
                        }
                        else if (prop.Name == "PartCode")
                        {
                            Builder.Append("filter.PartCode = \"M.162127\"; ");
                            AddThreeTab();
                        }
                        else if (prop.Name == "PartName")
                        {
                            Builder.Append("filter.PartName = \"DİFERANSİYEL/DİŞLİ YAĞI-85W140\"; ");
                            AddThreeTab();
                        }
                        else if (prop.Name == "LabourId")
                        {
                            Builder.Append("filter.LabourId = 211; ");
                            AddThreeTab();
                        }
                        else if (prop.Name == "DealerId")
                        {
                            Builder.Append("filter.DealerId = UserManager.UserInfo.DealerID; ");
                            AddThreeTab();
                        }
                        else if (prop.Name == "UserId")
                        {
                            Builder.Append("filter.DealerId = UserManager.UserInfo.UserId; ");
                            AddThreeTab();
                        }
                        else if (prop.Name == "SupplierDealerConfirm")
                        {
                            Builder.Append("filter.SupplierDealerConfirm = 1; ");
                            AddThreeTab();
                        }
                        else if (prop.Name == "IndicatorTypeCode")
                        {
                            Builder.Append("filter.IndicatorTypeCode = \"IT_C\"; ");
                            AddThreeTab();
                        }
                        else if (prop.Name == "CampaignCode")
                        {
                            Builder.Append("filter.CampaignCode = \"508\"; ");
                            AddThreeTab();
                        }
                        else if (prop.Name == "MultiLanguageContentAsText")
                        {
                            Builder.Append("filter.MultiLanguageContentAsText = \"TR || TEST\"; ");
                            AddThreeTab();
                        }
                        else if (prop.Name.Contains("Code"))
                        {
                            Builder.Append(string.Format("filter.{0} = guid; ", prop.Name));
                            AddThreeTab();
                        }
                    }

                    #endregion
                }

                AddThreeTab();

                #region model get define 

                if (filterNames.Contains("UserInfo") && !filterNames.Contains("totalCnt"))
                    Builder.Append(string.Format(" var resultGet = _{0}.{1}(UserManager.UserInfo, filter);", businessName, methodName));
                else if (filterNames.Contains("UserInfo") && !filterNames.Contains("totalCount"))
                    Builder.Append(string.Format(" var resultGet = _{0}.{1}(UserManager.UserInfo, filter);", businessName, methodName));

                else if (filterNames.Length == 0)
                    Builder.Append(string.Format(" var resultGet = _{0}.{1}();", businessName, methodName));
                else
                    Builder.Append(string.Format(" var resultGet = _{0}.{1}(filter);", businessName, methodName));

                #endregion

                AddThreeTab();
                AddThreeTab();
                Builder.Append("Assert.IsTrue(resultGet.Model != null && resultGet.Model.Id > 0 && resultGet.IsSuccess);");
                AddTwoTab();
                Builder.Append("}");
                AddNewLine();
            }
            else if (_AfterHandler != null)
                _AfterHandler.Create(parent, businessName, methodName, filterNames, dmlContains);

        }
    }
}
