namespace ODMSModel.DeliveryList
{
    public class DeliveryListServiceModel
    {
        public string VBELN { get; set; } // Teslimat numarası,
        public string MATNR { get; set; } // Malzeme numarası
        public string MATNR_UK { get; set; }
        // Teslimatın üst kalemi (sizin aşağıdaki  mailinizde sarı ile işaretlediğim yerde belirttiğiniz durum) var ise bu üst kaleme ait malzemeyi getiriyoruz. Eğer boş ise, teslimata ait siparişte bu bilginin dolu olup olmadığına bakıyoruz ve siparişe ait üst kalemin malzemesini getririyoruz. Eğer her iksinde de boş ise herhangi bir bilgi döndürmüyor.
        public string LFIMG { get; set; } // Miktar
        public string BSTKD { get; set; } // SAS numarası
        public string WADAT_IST { get; set; } // Teslimat Tarihi
        public string MATBUNO { get; set; } // İrsaliye üstündeki matbu no bilgisi
        public string NETWR { get; set; } // Teslimata ait siparişin KDV’siz Net fiyat değeri
        public string POSNR { get; set; } // Teslimata ait siparişin kalem numarası
        public string UEPOS { get; set; } // Teslimata ait siparişin üst kalem numarası   ( Yok ise sıfır dönmektedir. )
    }
}
