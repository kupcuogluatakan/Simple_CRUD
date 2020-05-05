using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.GuaranteeAuthorityGroupMembers;
using System;


namespace ODMSUnitTest
{

	[TestClass]
	public class GuaranteeAuthorityGroupMemberBLTest
	{

		GuaranteeAuthorityGroupMemberBL _GuaranteeAuthorityGroupMemberBL = new GuaranteeAuthorityGroupMemberBL();

		[TestMethod]
		public void GuaranteeAuthorityGroupMemberBL_Save_Insert()
		{
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new GuaranteeAuthorityGroupMembersModel();
			model.GroupId= 1; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _GuaranteeAuthorityGroupMemberBL.Save(UserManager.UserInfo, model);
			
			Assert.IsTrue(result.IsSuccess);
		}

		[TestMethod]
		public void GuaranteeAuthorityGroupMemberBL_ListGuaranteeAuthorityGroupMembersIncluded_GetAll()
		{
			 var resultGet = _GuaranteeAuthorityGroupMemberBL.ListGuaranteeAuthorityGroupMembersIncluded(1);
			
			Assert.IsTrue(resultGet.Total > 0);
		}

		[TestMethod]
		public void GuaranteeAuthorityGroupMemberBL_ListGuaranteeAuthorityGroupMembersExcluded_GetAll()
		{
			 var resultGet = _GuaranteeAuthorityGroupMemberBL.ListGuaranteeAuthorityGroupMembersExcluded(1);
			
			Assert.IsTrue(resultGet.Total > 0);
		}


	}

}

