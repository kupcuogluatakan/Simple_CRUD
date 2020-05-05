using System;
using System.ComponentModel.DataAnnotations;
using ODMSCommon.Resources;
using FluentValidation.Attributes;

namespace ODMSModel.GuaranteeCompLabourMargin
{
    [Validator(typeof(GuaranteeCompLabourMarginViewModelValidator))]
    public class GuaranteeCompLabourMarginViewModel : ModelBase
    {
        public GuaranteeCompLabourMarginViewModel()
        {
        }
        /// <summary>
        /// 	<columnInfo>
        /// 		<name>ID_GRNT_LABOUR_MRGN</name>
        /// 		<type>numeric</type>
        /// 		<nullable>False</nullable>
        /// 	</columnInfo>		
        /// 	<description>
        /// 				
        /// 	</description>
        /// </summary>
        public int IdGrntLabourMrgn { get; set; }

        /// <summary>
        /// 	<columnInfo>
        /// 		<name>ID_COUNTRY</name>
        /// 		<type>int</type>
        /// 		<nullable>False</nullable>
        /// 	</columnInfo>		
        /// 	<description>
        /// 		Marj bilgisinin geçerli oldugu ülke.		
        /// 	</description>
        /// </summary>
        public int? CountryId { get; set; }

        /// <summary>
        /// 	<columnInfo>
        /// 		<name>MAX_PRICE</name>
        /// 		<type>int</type>
        /// 		<nullable>True</nullable>
        /// 	</columnInfo>		
        /// 	<description>
        /// 		Üst Sinir bilgisi (0 dan büyük olmali)		
        /// 	</description>
        /// </summary>
        [Display(Name = "GuaranteeCompPartMargin_Display_MaxPrice", ResourceType = typeof(MessageResource))]
        public int? MaxPrice { get; set; }

        /// <summary>
        /// 	<columnInfo>
        /// 		<name>GRNT_RATIO</name>
        /// 		<type>numeric</type>
        /// 		<nullable>True</nullable>
        /// 	</columnInfo>		
        /// 	<description>
        /// 		Oran bilgisinin tutuldugu alan ( > 100 olmali)		
        /// 	</description>
        /// </summary>
        [Display(Name = "GuaranteeCompPartMargin_Display_GrntRatio", ResourceType = typeof(MessageResource))]
        public decimal? GrntRatio { get; set; }

        /// <summary>
        /// 	<columnInfo>
        /// 		<name>GRNT_PRICE</name>
        /// 		<type>numeric</type>
        /// 		<nullable>True</nullable>
        /// 	</columnInfo>		
        /// 	<description>
        /// 		Ödenecek tutar bilgisi > 0 olmali.		
        /// 	</description>
        /// </summary>

        [Display(Name = "GuaranteeCompPartMargin_Display_GrntPrice", ResourceType = typeof(MessageResource))]
        public decimal? GrntPrice { get; set; }

        /// <summary>
        /// 	<columnInfo>
        /// 		<name>CREATE_USER</name>
        /// 		<type>varchar</type>
        /// 		<nullable>False</nullable>
        /// 	</columnInfo>		
        /// 	<description>
        /// 				
        /// 	</description>
        /// </summary>
        public string CreateUser { get; set; }

        /// <summary>
        /// 	<columnInfo>
        /// 		<name>CREATE_DATE</name>
        /// 		<type>datetime</type>
        /// 		<nullable>False</nullable>
        /// 	</columnInfo>		
        /// 	<description>
        /// 				
        /// 	</description>
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 	<columnInfo>
        /// 		<name>UPDATE_USER</name>
        /// 		<type>varchar</type>
        /// 		<nullable>True</nullable>
        /// 	</columnInfo>		
        /// 	<description>
        /// 				
        /// 	</description>
        /// </summary>
        public new string UpdateUser { get; set; }

        /// <summary>
        /// 	<columnInfo>
        /// 		<name>UPDATE_DATE</name>
        /// 		<type>datetime</type>
        /// 		<nullable>True</nullable>
        /// 	</columnInfo>		
        /// 	<description>
        /// 				
        /// 	</description>
        /// </summary>
        public new DateTime? UpdateDate { get; set; }

        [Display(Name = "GuaranteeCompPartMargin_Display_CountryName", ResourceType = typeof(MessageResource))]
        public string CountryName { get; set; }

        [Display(Name = "GuaranteeCompPartMargin_Display_CurrencyName", ResourceType = typeof(MessageResource))]
        public string CurrencyName { get; set; }

        [Display(Name = "GuaranteeCompPartMargin_Display_CurrencyCode", ResourceType = typeof(MessageResource))]
        public string CurrencyCode { get; set; }

        [Display(Name = "GuaranteeCompPartMargin_Display_ShortCode", ResourceType = typeof(MessageResource))]
        public string ShortCode { get; set; }

    }
}
