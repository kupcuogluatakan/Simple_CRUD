using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using ODMSModel.Common;
using FluentValidation.Attributes;
namespace ODMSModel.UserRole
{
    [Validator(typeof(UserRoleViewModelValidator))]
    public class UserRoleViewModel : ModelBase
    {
        /// <summary>
        /// UserId for roles
        /// </summary>
        [Display(Name = "User_PageTitle_Index", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int UserId { get; set; }
        public int? RoleTypeId { get; set; }
        public List<SelectListItem> UserList { get; set; }
        public List<ComboBoxModel> PermissionList { get; set; }
        public List<ComboBoxModel> OtherPermissionList { get; set; }

        public UserRoleViewModel()
        {
            PermissionList=new List<ComboBoxModel>();
            OtherPermissionList= new List<ComboBoxModel>();
        }
    }
}
