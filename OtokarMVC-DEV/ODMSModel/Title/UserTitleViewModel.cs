using FluentValidation.Attributes;
using ODMSModel.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ODMSModel.Title
{
    [Validator(typeof(UserTitleViewModel))]
    public class UserTitleViewModel : ModelBase
    {
        /// <summary>
        /// UserId for roles
        /// </summary>
        [Display(Name = "User_PageTitle_Index", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int UserId { get; set; }
        public int? TitleId { get; set; }
        public List<SelectListItem> UserList { get; set; }
        public List<ComboBoxModel> TitleList { get; set; }

        public UserTitleViewModel()
        {
            TitleList = new List<ComboBoxModel>();
        }
    }
}
