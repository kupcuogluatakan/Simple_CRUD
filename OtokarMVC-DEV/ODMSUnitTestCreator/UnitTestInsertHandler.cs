using ODMSModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ODMSUnitTestCreator
{
    public class UnitTestInsertHandler : UnitTestHandler
    {
        public UnitTestInsertHandler()
        {
        }

        public override void Create(UnitTestHandler parent, string businessName, string methodName, string filterNames, bool dmlContains)
        {

            if (methodName.Contains("DML") || methodName.Contains("Save") || methodName.Contains("Insert") || methodName.Contains("Update"))
            {

                Builder = new StringBuilder();

                #region insert 

                AddTwoTab();
                Builder.Append("[TestMethod]");

                AddTwoTab();
                Builder.Append(string.Format("public void {0}_{1}_Insert()", businessName, methodName));
                UpdateMethodName = string.Format("public void {0}_{1}_Update()", businessName, methodName);
                DeleteMethodName = string.Format("public void {0}_{1}_Delete()", businessName, methodName);
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

                    #region model define

                    AddThreeTab();
                    Builder.Append("var guid = Guid.NewGuid().ToString().Replace(\"-\", \"\").Substring(0, 6); ");
                    AddThreeTab();
                    Builder.Append(string.Format("var model = new {0}();", filterName));
                    AddThreeTab();

                    AddThreeTab(InsertBuilder);
                    AddThreeTab(InsertBuilder);
                    InsertBuilder.Append("var guid = Guid.NewGuid().ToString().Replace(\"-\", \"\").Substring(0, 6); ");
                    AddThreeTab(InsertBuilder);
                    InsertBuilder.Append(string.Format("var model = new {0}();", filterName));
                    AddThreeTab(InsertBuilder);


                    AddThreeTab(UpdateBuilder);
                    AddThreeTab(UpdateBuilder);
                    UpdateBuilder.Append(string.Format("var modelUpdate = new {0}();", filterName));
                    AddThreeTab(UpdateBuilder);
                    UpdateBuilder.Append("modelUpdate.Id = resultGet.Data.First().Id;");
                    AddThreeTab(UpdateBuilder);

                    AddThreeTab(DeleteBuilder);
                    AddThreeTab(DeleteBuilder);
                    DeleteBuilder.Append(string.Format("var modelDelete = new {0}();", filterName));
                    AddThreeTab(DeleteBuilder);
                    DeleteBuilder.Append("modelDelete.Id = resultGet.Data.First().Id;");
                    AddThreeTab(DeleteBuilder);

                    #endregion

                    #region prop define 

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

                            || prop.Name == "Id"
                            || prop.Name == "PartCodeList"
                            || prop.Name == "PartClassList"

                            )
                            continue;

                        if (prop.Name == "SupplierId")
                        {
                            Builder.Append("model.SupplierId = 782; ");
                            InsertBuilder.Append("model.SupplierId = 782; ");
                            UpdateBuilder.Append("modelUpdate.SupplierId = 782; ");
                            DeleteBuilder.Append("modelDelete.SupplierId = 782; ");
                        }
                        else if (prop.Name == "ModelKod")
                        {
                            Builder.Append("model.ModelKod = \"ATLAS\"; ");
                            InsertBuilder.Append("model.ModelKod = \"ATLAS\"; ");
                            UpdateBuilder.Append("modelUpdate.ModelKod = \"ATLAS\"; ");
                            DeleteBuilder.Append("modelDelete.ModelKod = \"ATLAS\"; ");
                        }
                        else if (prop.Name == "VehicleId")
                        {
                            Builder.Append("model.VehicleId = 29627; ");
                            InsertBuilder.Append("model.VehicleId = 29627; ");
                            UpdateBuilder.Append("modelUpdate.VehicleId = 29627; ");
                            DeleteBuilder.Append("modelDelete.VehicleId = 29627; ");
                        }
                        else if (prop.Name == "CompanyTypeId")
                        {
                            Builder.Append("model.CompanyTypeId = 1; ");
                            InsertBuilder.Append("model.CompanyTypeId = 1; ");
                            UpdateBuilder.Append("modelUpdate.CompanyTypeId = 1; ");
                            DeleteBuilder.Append("modelDelete.CompanyTypeId = 1; ");
                        }
                        else if (prop.Name == "CustomerTypeId")
                        {
                            Builder.Append("model.CustomerTypeId = 2; ");
                            InsertBuilder.Append("model.CustomerTypeId = 2; ");
                            UpdateBuilder.Append("modelUpdate.CustomerTypeId = 2; ");
                            DeleteBuilder.Append("modelDelete.CustomerTypeId = 2; ");
                        }
                        else if (prop.Name == "CountryId")
                        {
                            Builder.Append("model.CountryId = 1; ");
                            InsertBuilder.Append("model.CountryId = 1; ");
                            UpdateBuilder.Append("modelUpdate.CountryId = 1; ");
                            DeleteBuilder.Append("modelDelete.CountryId = 1; ");
                        }
                        else if (prop.Name == "CityId")
                        {
                            Builder.Append("model.CityId = 1; ");
                            InsertBuilder.Append("model.CityId = 1; ");
                            UpdateBuilder.Append("modelUpdate.CityId = 1; ");
                            DeleteBuilder.Append("modelDelete.CityId = 1; ");
                        }
                        else if (prop.Name == "TownId")
                        {
                            Builder.Append("model.TownId = 1; ");
                            InsertBuilder.Append("model.TownId = 1; ");
                            UpdateBuilder.Append("modelUpdate.TownId = 1; ");
                            DeleteBuilder.Append("modelDelete.TownId = 1; ");
                        }
                        else if (prop.Name == "PriceListId")
                        {
                            Builder.Append("model.PriceListId = 14; ");
                            InsertBuilder.Append("model.PriceListId = 14; ");
                            UpdateBuilder.Append("modelUpdate.PriceListId = 14; ");
                            DeleteBuilder.Append("modelDelete.PriceListId = 14; ");
                        }
                        else if (prop.Name == "PartCode")
                        {
                            Builder.Append("model.PartCode = \"M.162127\"; ");
                            InsertBuilder.Append("model.PartCode = \"M.162127\"; ");
                            UpdateBuilder.Append("modelUpdate.PartCode = \"M.162127\"; ");
                            DeleteBuilder.Append("modelDelete.PartCode = \"M.162127\"; ");
                        }
                        else if (prop.Name == "PartName")
                        {
                            Builder.Append("model.PartName = \"DİFERANSİYEL/DİŞLİ YAĞI-85W140\"; ");
                            InsertBuilder.Append("model.PartName = \"DİFERANSİYEL/DİŞLİ YAĞI-85W140\"; ");
                            UpdateBuilder.Append("modelUpdate.PartName = \"DİFERANSİYEL/DİŞLİ YAĞI-85W140\"; ");
                            DeleteBuilder.Append("modelDelete.PartName = \"DİFERANSİYEL/DİŞLİ YAĞI-85W140\"; ");
                        }
                        else if (prop.Name == "LabourId")
                        {
                            Builder.Append("model.LabourId = 211; ");
                            InsertBuilder.Append("model.LabourId = 211; ");
                            UpdateBuilder.Append("modelUpdate.LabourId = 211; ");
                            DeleteBuilder.Append("modelDelete.LabourId = 211; ");
                        }
                        else if (prop.Name == "DealerId")
                        {
                            Builder.Append("model.DealerId = UserManager.UserInfo.DealerID; ");
                            InsertBuilder.Append("model.DealerId = UserManager.UserInfo.DealerID; ");
                            UpdateBuilder.Append("modelUpdate.DealerId = UserManager.UserInfo.DealerID; ");
                            DeleteBuilder.Append("modelDelete.DealerId = UserManager.UserInfo.DealerID; ");
                        }
                        else if (prop.Name == "UserId")
                        {
                            Builder.Append("model.DealerId = UserManager.UserInfo.UserId; ");
                            InsertBuilder.Append("model.DealerId = UserManager.UserInfo.UserId; ");
                            UpdateBuilder.Append("modelUpdate.DealerId = UserManager.UserInfo.UserId; ");
                            DeleteBuilder.Append("modelDelete.DealerId = UserManager.UserInfo.UserId; ");
                        }
                        else if (prop.Name == "SupplierDealerConfirm")
                        {
                            Builder.Append("model.SupplierDealerConfirm = 1; ");
                            InsertBuilder.Append("model.SupplierDealerConfirm = 1; ");
                            UpdateBuilder.Append("modelUpdate.SupplierDealerConfirm = 1; ");
                            DeleteBuilder.Append("modelDelete.SupplierDealerConfirm = 1; ");
                        }
                        else if (prop.Name == "IndicatorTypeCode")
                        {
                            Builder.Append("model.IndicatorTypeCode = \"IT_C\"; ");
                            InsertBuilder.Append("model.IndicatorTypeCode = \"IT_C\"; ");
                            UpdateBuilder.Append("modelUpdate.IndicatorTypeCode = \"IT_C\"; ");
                            DeleteBuilder.Append("modelDelete.IndicatorTypeCode = \"IT_C\"; ");
                        }
                        else if (prop.Name == "CampaignCode")
                        {
                            Builder.Append("model.CampaignCode = \"508\"; ");
                            InsertBuilder.Append("model.CampaignCode = \"508\"; ");
                            UpdateBuilder.Append("modelUpdate.CampaignCode = \"508\"; ");
                            DeleteBuilder.Append("modelDelete.CampaignCode = \"508\"; ");
                        }
                        else if (prop.Name == "MultiLanguageContentAsText")
                        {
                            Builder.Append("model.MultiLanguageContentAsText = \"TR || TEST\"; ");
                            InsertBuilder.Append("model.MultiLanguageContentAsText = \"TR || TEST\"; ");
                            UpdateBuilder.Append("modelUpdate.MultiLanguageContentAsText = \"TR || TEST\"; ");
                            DeleteBuilder.Append("modelDelete.MultiLanguageContentAsText = \"TR || TEST\"; ");
                        }
                        else if (propTypeName == "String")
                        {
                            Builder.Append(string.Format("model.{0}= guid; ", prop.Name));
                            InsertBuilder.Append(string.Format("model.{0}= guid; ", prop.Name));
                            UpdateBuilder.Append(string.Format("modelUpdate.{0}= guid; ", prop.Name));
                            DeleteBuilder.Append(string.Format("modelDelete.{0}= guid; ", prop.Name));
                        }
                        else if (propTypeName == "Int32")
                        {
                            Builder.Append(string.Format("model.{0}= 1; ", prop.Name));
                            InsertBuilder.Append(string.Format("model.{0}= 1; ", prop.Name));
                        }
                        else if (propTypeName == "Int64")
                        {
                            Builder.Append(string.Format("model.{0}= 1; ", prop.Name));
                            InsertBuilder.Append(string.Format("model.{0}= 1; ", prop.Name));
                        }
                        else if (propTypeName == "Decimal")
                        {
                            Builder.Append(string.Format("model.{0}= 1; ", prop.Name));
                            InsertBuilder.Append(string.Format("model.{0}= 1; ", prop.Name));
                        }
                        else if (propTypeName == "DateTime")
                        {
                            Builder.Append(string.Format("model.{0}= DateTime.Now; ", prop.Name));
                            InsertBuilder.Append(string.Format("model.{0}= DateTime.Now; ", prop.Name));
                        }
                        else if (propTypeName == "Boolean")
                        {
                            Builder.Append(string.Format("model.{0}= true; ", prop.Name));
                            InsertBuilder.Append(string.Format("model.{0}= true; ", prop.Name));
                        }
                        else
                        {
                            continue;
                        }

                        AddThreeTab();
                        AddThreeTab(InsertBuilder);
                        AddThreeTab(UpdateBuilder);
                        AddThreeTab(DeleteBuilder);
                    }

                    #endregion
                }

                #region model command type define 

                Builder.Append("model.CommandType = \"I\";");
                AddThreeTab();

                InsertBuilder.Append("model.CommandType = \"I\";");
                AddThreeTab(InsertBuilder);

                UpdateBuilder.Append("modelUpdate.CommandType = \"U\";");
                AddThreeTab(UpdateBuilder);

                DeleteBuilder.Append("modelDelete.CommandType = \"D\";");
                AddThreeTab(DeleteBuilder);

                #endregion

                #region model save define 

                if (filterNames.Contains("UserInfo"))
                {
                    Builder.Append(string.Format(" var result = _{0}.{1}(UserManager.UserInfo, model);", businessName, methodName));
                    InsertBuilder.Append(string.Format(" var result = _{0}.{1}(UserManager.UserInfo, model);", businessName, methodName));
                    UpdateBuilder.Append(string.Format(" var resultUpdate = _{0}.{1}(UserManager.UserInfo, modelUpdate);", businessName, methodName));
                    DeleteBuilder.Append(string.Format(" var resultDelete = _{0}.{1}(UserManager.UserInfo, modelDelete);", businessName, methodName));
                }
                else
                {
                    Builder.Append(string.Format(" var result = _{0}.{1}(model);", businessName, methodName));
                    InsertBuilder.Append(string.Format(" var result = _{0}.{1}(model);", businessName, methodName));
                    UpdateBuilder.Append(string.Format(" var resultUpdate = _{0}.{1}(modelUpdate);", businessName, methodName));
                    DeleteBuilder.Append(string.Format(" var resultDelete = _{0}.{1}(modelDelete);", businessName, methodName));
                }


                #endregion

                AddThreeTab();
                AddThreeTab();
                Builder.Append("Assert.IsTrue(result.IsSuccess);");
                AddTwoTab();
                Builder.Append("}");
                AddNewLine();

                #endregion

            }
            else if (_AfterHandler != null)
                _AfterHandler.Create(parent, businessName, methodName, filterNames, dmlContains);

        }
    }
}
