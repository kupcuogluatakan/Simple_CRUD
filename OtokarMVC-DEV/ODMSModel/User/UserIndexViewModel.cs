using System;
using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;
using ODMSCommon.Resources;

namespace ODMSModel.User
{
    [Validator(typeof(UserIndexViewModelValidator))]
    public class UserIndexViewModel : ModelBase
    {
        public UserIndexViewModel()
        {
        }
        public bool HideFormElements { get; set; }

        //UserId
        public int UserId { get; set; }

        //UserCode
        [Display(Name = "User_Display_UserCode", ResourceType = typeof (MessageResource))]
        public string UserCode { get; set; }

        //Password
        [Display(Name = "User_Display_Password", ResourceType = typeof(MessageResource))]
        public string Password { get; set; }

        //Dealer
        public string DealerId { get; set; }
        [Display(Name = "User_Display_DealerName", ResourceType = typeof (MessageResource))]
        public string DealerName { get; set; }
        
        //LanguageCode
        [Display(Name = "User_Display_LanguageCode", ResourceType = typeof (MessageResource))]
        public string LanguageCode { get; set; }

        //UserFirstName
        [Display(Name = "User_Display_UserFirstName", ResourceType = typeof (MessageResource))]
        public string UserFirstName { get; set; }

        //UserMidName
        [Display(Name = "User_Display_UserMidName", ResourceType = typeof(MessageResource))]
        public string UserMidName { get; set; }

        //UserLastName
        [Display(Name = "User_Display_UserLastName", ResourceType = typeof (MessageResource))]
        public string UserLastName { get; set; }

        //TCIdentitiyNo
        [Display(Name = "User_Display_IdentityNo", ResourceType = typeof (MessageResource))]
        public long? TCIdentityNo { get; set; }

        //IdentityNoControl
        [Display(Name = "User_Display_IdentityNo", ResourceType = typeof(MessageResource))]
        public string IdentityNoControl { get; set; }

        //PassportNo
        [Display(Name = "User_Display_PassportNo", ResourceType = typeof(MessageResource))]
        public string PassportNo { get; set; }
        
        //IdentityNo
        [Display(Name = "User_Display_IdentityNo", ResourceType = typeof(MessageResource))]
        public string IdentityNo { get; set; }

        //BirthDate
        [Display(Name = "User_Display_BirthDate", ResourceType = typeof(MessageResource))]
        public DateTime? BirthDate { get; set; }

        //Sex
        public int? SexId { get; set; }
        [Display(Name = "User_Display_Sex", ResourceType = typeof(MessageResource))]
        public string Sex { get; set; }
        
        //Marital Status
        public int? MaritalStatusId { get; set; }
        [Display(Name = "User_Display_MaritalStatus", ResourceType = typeof(MessageResource))]
        public string MaritalStatus { get; set; }

        //Photo Doc
        [Display(Name = "User_Display_PhotoDoc", ResourceType = typeof(MessageResource))]
        public int PhotoDocId { get; set; }

        //Phone
        [Display(Name = "User_Display_Phone", ResourceType = typeof(MessageResource))]
        public string Phone { get; set; }

        //Mobile
        [Display(Name = "User_Display_Mobile", ResourceType = typeof(MessageResource))]
        public string Mobile { get; set; }

        //Extension
        [Display(Name = "User_Display_Extension", ResourceType = typeof(MessageResource))]
        public string Extension { get; set; }

        //EMail
        [Display(Name = "User_Display_EMail", ResourceType = typeof(MessageResource))]
        public string EMail { get; set; }

        //Address
        [Display(Name = "User_Display_Address", ResourceType = typeof (MessageResource))]
        public string Address { get; set; }
        
        //Employment Date
        [Display(Name = "User_Display_EmploymentDate", ResourceType = typeof(MessageResource))]
        public DateTime? EmploymentDate { get; set; }

        //Unemployment Date
        [Display(Name = "User_Display_UnemploymentDate", ResourceType = typeof(MessageResource))]
        public DateTime? UnemploymentDate { get; set; }

        //IsActive
        [Display(Name = "Global_Display_Active", ResourceType = typeof(MessageResource))]
        public new bool IsActive { get; set; }
        [Display(Name = "Global_Display_Active", ResourceType = typeof(MessageResource))]
        public string IsActiveName { get; set; }
        
        //RoleType
        public int? RoleTypeId { get; set; }
        [Display(Name = "DealerUser_Display_RoleTypeName", ResourceType = typeof(MessageResource))]
        public string RoleTypeName { get; set; }

        public bool IsPartial { get; set; }
        public bool IsPasswordSet { get; set; }
        public bool IsTechnician { get; set; }
        public int Index { get; set; }
        public bool IsAutoPassword { get; set; }
    }
}
