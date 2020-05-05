using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSModel.FavoriteScreen;
using ODMSBusiness.Business;

namespace ODMSUnitTest
{

    [TestClass]
    public class FavoriteScreenBLTest
    {

        FavoriteScreenBL _FavoriteScreenBL = new FavoriteScreenBL();

        [TestMethod]
        public void FavoriteScreenBL_ListFavoriteScreen_GetAll()
        {
            var resultGet = _FavoriteScreenBL.ListFavoriteScreen(UserManager.UserInfo);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void FavoriteScreenBL_ListAllScreen_GetAll()
        {
            var resultGet = _FavoriteScreenBL.ListAllScreen(UserManager.UserInfo);

            Assert.IsTrue(resultGet.Total > 0);
        }


    }

}

