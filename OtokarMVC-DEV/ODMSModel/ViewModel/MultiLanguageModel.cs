using System;
using System.Collections.Generic;
using System.Linq;

namespace ODMSModel.ViewModel
{
    [Serializable]
    public class MultiLanguageModel
    {

        public MultiLanguageModel()
        {
            listOfInitialContent = new List<MultiLanguageContentViewModel>();
            listOfAddedContent = new List<MultiLanguageContentViewModel>();
            listOfAllContent = new List<MultiLanguageContentViewModel>();
            SetListOfAllContents();
        }
        //private string _multiLanguageContentAsText { get; set; }

        //public string MultiLanguageContentAsText
        //{
        //    get
        //    {
        //        if (string.IsNullOrWhiteSpace(_multiLanguageContentAsText))
        //        {
        //            _multiLanguageContentAsText = "TR||";
        //        }
        //        return _multiLanguageContentAsText;
        //    }
        //    set { _multiLanguageContentAsText = value; }
        //}

        public string MultiLanguageContentAsText { get; set; }

        public List<MultiLanguageContentViewModel> listOfInitialContent { get; set; }
        public List<MultiLanguageContentViewModel> listOfAddedContent { get; set; }
        public List<MultiLanguageContentViewModel> listOfAllContent { get; set; }
        public string txtLanguageContent { get; set; }
        public string txtSelectedLanguageCode { get; set; }
        public string txtLanguageContentAsString { get; set; }
        public bool isValid { get; set; }
        public string isValidText { get { return (isValid ? "1" : "0"); } }
        public string languageListChecker { get; set; }

        public void SetListOfAllContents()
        {
            if (listOfInitialContent.Count == 0 && listOfAddedContent.Count == 0 && listOfAllContent.Count == 0)
            {                               
                listOfAllContent.Add(new MultiLanguageContentViewModel
                {
                    Content = "",
                    LanguageCode = "EN",
                    OperationType = ""
                });
                listOfAllContent.Add(new MultiLanguageContentViewModel
                {
                    Content = "",
                    LanguageCode = "FR",
                    OperationType = ""
                });
                listOfAllContent.Add(new MultiLanguageContentViewModel
                {
                    Content = "",
                    LanguageCode = "RU",
                    OperationType = ""
                });
                return;
            }

            var all = listOfAllContent;
            var initial = listOfInitialContent;
            var finalAll = (from mlLast in all
                            let checkItem = initial.FirstOrDefault(v => v.LanguageCode == mlLast.LanguageCode)
                            where checkItem == null
                            select new MultiLanguageContentViewModel {LanguageCode = mlLast.LanguageCode}).ToList();

            //burdan sonrası artık initial doluysa yani ekran update için açıldıysa kısmını kapsıyor.
            //listenin son halindeki her item için dolaşıp, text değişmişmi diye bakmak gerek.

            listOfAllContent = finalAll;                    

        }
    }
}
