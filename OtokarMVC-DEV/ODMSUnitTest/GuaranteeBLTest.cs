using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;


namespace ODMSUnitTest
{

    [TestClass]
    public class GuaranteeBLTest
    {

        GuaranteeBL _GuaranteeBL = new GuaranteeBL();

        //TODO : Yazýlacak
        [TestMethod]
        public void GuaranteeBL_UpdateCampaignPriceXml_Insert()
        {
            //model.CommandType = "I";
            //var result = _GuaranteeBL.UpdateCampaignPriceXml(model);

            Assert.IsTrue(false);
        }

        [TestMethod]
        public void GuaranteeBL_GetGuarantee_GetModel()
        {
            var resultGet = _GuaranteeBL.GetGuarantee(UserManager.UserInfo);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.CustomerName != string.Empty && resultGet.IsSuccess);
        }


    }

}

