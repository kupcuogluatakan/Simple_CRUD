using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSModel.ListModel;
using ODMSCommon.Resources;

namespace ODMSModel.GuaranteeCompLabourMargin
{

    public class GuaranteeCompLabourMarginListModel : BaseListWithPagingModel
    {
        public GuaranteeCompLabourMarginListModel()
        {

        }

        public GuaranteeCompLabourMarginListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                 {
                     {"IdGrntLabourMrgn","ID_GRNT_LABOUR_MRGN"},
                     {"CountryName", "CL.COUNTRY_NAME"}, // ülke adı
                     {"GrntRatio", "GRNT_RATIO"}, // marj oranı
                     {"GrntPrice", "GRNT_PRICE"}, // ödenecek tutar
                     {"MaxPrice", "ISNULL(MAX_PRICE,0)"}, // üst limit  tutar CurrencyCode şeklinde gösterilecek.
                     {"MaxPriceStr", "ISNULL(MAX_PRICE,0)"}, // üst limit  tutar CurrencyCode şeklinde gösterilecek.
                     {"CountryId","ID_COUNTRY"},
                     {"CurrencyCode","CURRENCY_CODE"},
                     {"CurrencyName","CURRENCY_NAME"},
                     {"GrntPriceAndCurrencyCode","ISNULL(GRNT_PRICE,0)"}
                 };
            SetMapper(dMapper);
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
        public int CountryId { get; set; }

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
        public string UpdateUser { get; set; }

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
        public DateTime? UpdateDate { get; set; }

        [Display(Name = "GuaranteeCompPartMargin_Display_CountryName", ResourceType = typeof(MessageResource))]
        public string CountryName { get; set; }

        [Display(Name = "GuaranteeCompPartMargin_Display_CurrencyName", ResourceType = typeof(MessageResource))]
        public string CurrencyName { get; set; }

        [Display(Name = "GuaranteeCompPartMargin_Display_CurrencyCode", ResourceType = typeof(MessageResource))]
        public string CurrencyCode { get; set; }

        [Display(Name = "GuaranteeCompPartMargin_Display_ShortCode", ResourceType = typeof(MessageResource))]
        public string ShortCode { get; set; }

        [Display(Name = "GuaranteeCompPartMargin_Display_GrntPrice", ResourceType = typeof(MessageResource))]
        public string GrntPriceAndCurrencyCode { get; set; }

        [Display(Name = "GuaranteeCompPartMargin_Display_MaxPrice", ResourceType = typeof(MessageResource))]
        public string MaxPriceStr { get; set; }
    }
}
