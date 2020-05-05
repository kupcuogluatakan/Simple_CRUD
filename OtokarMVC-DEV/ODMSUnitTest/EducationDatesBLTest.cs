using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSModel.EducationDates;


namespace ODMSUnitTest
{

    [TestClass]
    public class EducationDatesBLTest
    {

        EducationDatesBL _EducationDatesBL = new EducationDatesBL();

        [TestMethod]
        public void EducationDatesBL_DMLEducationDates_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new EducationDatesViewModel();
            model.IsRequestRoot = true;
            model.EducationCode = guid;
            model.RowNumber = 1;
            model.EducaitonTime = guid;
            model.EducationPlace = guid;
            model.Instructor = guid;
            model.Notes = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _EducationDatesBL.DMLEducationDates(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void EducationDatesBL_GetEducationDate_GetModel()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new EducationDatesViewModel();
            model.IsRequestRoot = true;
            model.EducationCode = guid;
            model.RowNumber = 1;
            model.EducaitonTime = guid;
            model.EducationPlace = guid;
            model.Instructor = guid;
            model.Notes = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _EducationDatesBL.DMLEducationDates(UserManager.UserInfo, model);

            var filter = new EducationDatesViewModel();
            filter.RowNumber = 1;
            filter.EducationCode = guid;

            var resultGet = _EducationDatesBL.GetEducationDate(UserManager.UserInfo, filter);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.EducationPlace != string.Empty && resultGet.IsSuccess);
        }

        [TestMethod]
        public void EducationDatesBL_GetEducationDatesList_GetAll()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new EducationDatesViewModel();
            model.IsRequestRoot = true;
            model.EducationCode = guid;
            model.RowNumber = 1;
            model.EducaitonTime = guid;
            model.EducationPlace = guid;
            model.Instructor = guid;
            model.Notes = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _EducationDatesBL.DMLEducationDates(UserManager.UserInfo, model);

            int count = 0;
            var filter = new EducationDatesListModel();
            filter.EducationCode = guid;

            var resultGet = _EducationDatesBL.GetEducationDatesList(UserManager.UserInfo, filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void EducationDatesBL_DMLEducationDates_Update()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new EducationDatesViewModel();
            model.IsRequestRoot = true;
            model.EducationCode = guid;
            model.RowNumber = 1;
            model.EducaitonTime = guid;
            model.EducationPlace = guid;
            model.Instructor = guid;
            model.Notes = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _EducationDatesBL.DMLEducationDates(UserManager.UserInfo, model);

            var filter = new EducationDatesListModel();
            filter.EducationCode = guid;

            int count = 0;
            var resultGet = _EducationDatesBL.GetEducationDatesList(UserManager.UserInfo, filter, out count);

            var modelUpdate = new EducationDatesViewModel();
            modelUpdate.RowNumber = 1;
            modelUpdate.EducationCode = guid;
            modelUpdate.EducaitonTime = guid;
            modelUpdate.EducationPlace = guid;
            modelUpdate.Instructor = guid;
            modelUpdate.Notes = guid;
            modelUpdate.CommandType = "U";
            var resultUpdate = _EducationDatesBL.DMLEducationDates(UserManager.UserInfo, modelUpdate);
            Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
        }

        [TestMethod]
        public void EducationDatesBL_DMLEducationDates_Delete()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new EducationDatesViewModel();
            model.IsRequestRoot = true;
            model.EducationCode = guid;
            model.RowNumber = 1;
            model.EducaitonTime = guid;
            model.EducationPlace = guid;
            model.Instructor = guid;
            model.Notes = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _EducationDatesBL.DMLEducationDates(UserManager.UserInfo, model);

            var filter = new EducationDatesListModel();
            filter.EducationCode = guid;

            int count = 0;
            var resultGet = _EducationDatesBL.GetEducationDatesList(UserManager.UserInfo, filter, out count);

            var modelDelete = new EducationDatesViewModel();
            modelDelete.RowNumber = 1;
            modelDelete.EducationCode = guid;
            modelDelete.EducaitonTime = guid;
            modelDelete.EducationPlace = guid;
            modelDelete.Instructor = guid;
            modelDelete.Notes = guid;
            modelDelete.CommandType = "D";
            var resultDelete = _EducationDatesBL.DMLEducationDates(UserManager.UserInfo, modelDelete);
            Assert.IsTrue(resultDelete.IsSuccess);
        }


    }

}

