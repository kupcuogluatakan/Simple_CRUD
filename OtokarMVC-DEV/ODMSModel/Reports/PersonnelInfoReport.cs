namespace ODMSModel.Reports
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Web;

    public class PersonnelInfoReport
    {
        [Display(Name = "PersonnelInfoReport_Display_RegionName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string RegionName { get; set; }
        [Display(Name = "PurchaseOrderReport_Display_DealerName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DealerName { get; set; }
        [Display(Name = "WorkOrderDetailReport_Display_DealerSAP", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DealerSAPCode { get; set; }
        [Display(Name = "PersonnelInfoReport_Display_CreateDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? CreateDate { get; set; }
        [Display(Name = "PersonnelInfoReport_Display_CreateDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CreateDateToExcel
        {
            get
            {
                if (CreateDate != null)
                {
                    return CreateDate.Value.ToShortDateString();
                }
                return null;
            }
        }
        [Display(Name = "PersonnelInfoReport_Display_UserCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string UserCode { get; set; }
        [Display(Name = "PersonnelInfoReport_Display_FirstName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string FirstName { get; set; }
        [Display(Name = "PersonnelInfoReport_Display_MidName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string MidName { get; set; }
        [Display(Name = "PersonnelInfoReport_Display_LastName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string LastName { get; set; }
        //IdentityNo
        [Display(Name = "User_Display_IdentityNo", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string IdentityNo { get; set; }        
        [Display(Name = "PersonnelInfoReport_Display_IsActive", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string IsActive { get; set; }
        [Display(Name = "PersonnelInfoReport_Display_HireDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? HireDate { get; set; }
        [Display(Name = "PersonnelInfoReport_Display_HireDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string HireDateToExcel
        {
            get
            {
                if (HireDate != null)
                {
                    return HireDate.Value.ToShortDateString();
                }
                return null;
            }
        }
        [Display(Name = "PersonnelInfoReport_Display_LeaveDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? LeaveDate { get; set; }
        [Display(Name = "PersonnelInfoReport_Display_LeaveDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string LeaveDateToExcel
        {
            get
            {
                if (LeaveDate != null)
                {
                    return LeaveDate.Value.ToShortDateString();
                }
                return null;
            }
        }
        [Display(Name = "PersonnelInfoReport_Display_Gender", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Gender { get; set; }
        [Display(Name = "PersonnelInfoReport_Display_BirthDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? BirthDate { get; set; }
        [Display(Name = "PersonnelInfoReport_Display_BirthDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string BirthDateToExcel
        {
            get
            {
                if (BirthDate != null)
                {
                    return BirthDate.Value.ToShortDateString();
                }
                return null;
            }
        }
        [Display(Name = "PersonnelInfoReport_Display_Title", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Title { get; set; }
        [Display(Name = "PersonnelInfoReport_Display_EducationCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string EducationCode { get; set; }
        [Display(Name = "PersonnelInfoReport_Display_EducationName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string EducationName { get; set; }
        [Display(Name = "PersonnelInfoReport_Display_VehicleModel", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string VehicleModel { get; set; }
        [Display(Name = "PersonnelInfoReport_Display_EducationType", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string EducationType { get; set; }
        [Display(Name = "PersonnelInfoReport_Display_EducationDuration", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? EducationDuration { get; set; }
        [Display(Name = "PersonnelInfoReport_Display_EducationDuration", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string EducationDurationToExcel
        {
            get
            {
                if (EducationDuration != null)
                {
                    return EducationDuration.Value.ToShortDateString();
                }
                return null;
            }
        }
        [Display(Name = "PersonnelInfoReport_Display_EducationDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? EducationDate { get; set; }
        [Display(Name = "PersonnelInfoReport_Display_EducationDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string EducationDateToExcel
        {
            get
            {
                if (EducationDate != null)
                {
                    return EducationDate.Value.ToShortDateString();
                }
                return null;
            }
        }
        [Display(Name = "PersonnelInfoReport_Display_EducationGrade", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string EducationGrade { get; set; }
        [Display(Name = "PersonnelInfoReport_Display_CertificateNo", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CertificateNo { get; set; }

        [Display(Name = "PersonnelInfoReport_Display_Phone", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Phone { get; set; }
        [Display(Name = "PersonnelInfoReport_Display_Mobile", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Mobile { get; set; }

        [Display(Name = "PersonnelInfoReport_Display_Extension", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Extension { get; set; }

        [Display(Name = "PersonnelInfoReport_Display_Email", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Email { get; set; }

        [Display(Name = "PersonnelInfoReport_Display_MaritialStatus", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string MaritialStatus { get; set; }

        [Display(Name = "PersonnelInfoReport_Display_Address", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Address { get; set; }

        [Display(Name = "PersonnelInfoReport_Display_EducationDurationDay", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int? EducationDurationDay { get; set; }

        [Display(Name = "PersonnelInfoReport_Display_EducationDurationHour", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int? EducationDurationHour { get; set; }

        public int? UserId { get; set; }
    }
}
