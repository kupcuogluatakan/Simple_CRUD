using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Web.Mvc;
using ODMSCommon.Resources;
using FluentValidation.Attributes;


namespace ODMSModel.EducationRequest
{
    [Validator(typeof(EducationRequestDetailModelValidator))]
    public class EducationRequestDetailModel : ModelBase
    {
        public long Id { get; set; }

        [Display(Name = "Education_Display_Code", ResourceType = typeof(MessageResource))]
        public string EducationCode { get; set; }

        [Display(Name = "Education_Display_Name", ResourceType = typeof(MessageResource))]
        public string EducationName { get; set; }

        [Display(Name = "EducationRequest_Display_CreateDate", ResourceType = typeof(MessageResource))]
        public DateTime CreateDate { get; set; }

        [Display(Name = "EducationRequest_Display_CreateDate", ResourceType = typeof(MessageResource))]
        public string CreateDateString
        {
            get { return CreateDate.ToShortDateString(); }
        }

        /// <summary>
        /// WorkerId ve WorkerName Detay Ekranında, WorkerList ise Create ekranında kullanılmaktadır.
        /// </summary>
        public int WorkerId { get; set; }
        [Display(Name = "Global_Display_WorkerName", ResourceType = typeof(MessageResource))]
        public string WorkerName { get; set; }
        public List<SelectListItem> WorkerList { get; set; }

        public string SerializedWorkerIds
        {
            get
            {
                if (WorkerList == null || WorkerList.Count == 0)
                    return string.Empty;

                var builder = new StringBuilder();
                WorkerList.ForEach(x =>
                {
                    builder.Append(x.Value);
                    builder.Append(',');
                });
                return builder.ToString().Substring(0, builder.Length - 1);
            }
        }
    }
}
