namespace ODMSModel.ViewModel
{
    public class HomeViewModel
    {
        public string MultiLanguageContentAsText { get; set; }
        public string selectedItem { get; set; }
        public AutocompleteSearchViewModel searchModel { get; set; }
        public HomeViewModel()
        {
            searchModel = new AutocompleteSearchViewModel();
        }
    }
}
