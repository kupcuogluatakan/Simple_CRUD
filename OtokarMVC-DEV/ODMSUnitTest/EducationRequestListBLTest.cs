using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSModel.EducationRequests;
using System;
using ODMSModel.Education;

namespace ODMSUnitTest
{

    [TestClass]
    public class EducationRequestListBLTest
    {

        EducationRequestListBL _EducationRequestListBL = new EducationRequestListBL();
        EducationBL _EducationBL = new EducationBL();


        [TestMethod]
        public void EducationRequestListBL_GetEducationRequests_GetModel()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new EducationViewModel();
            model.MultiLanguageContentAsText = "TR || TEST";
            model.EducationCode = guid;
            model.EducationTypeName = guid;
            model.EducationDurationDay = 1;
            model.EducationDurationHour = 1;
            model.AdminDesc = guid;
            model.VehicleModelCode = guid;
            model.VehicleModelName = guid;
            model.IsActive = true;
            model.IsMandatory = true;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _EducationBL.DMLEducation(UserManager.UserInfo, model);

            var filter = new EducationRequestsListModel();
            filter.EducationCode = guid;

            var count = 0;
            var resultGet = _EducationRequestListBL.GetEducationRequests(UserManager.UserInfo, filter, out count);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.CustomerName != string.Empty && resultGet.IsSuccess);
        }


    }

}

