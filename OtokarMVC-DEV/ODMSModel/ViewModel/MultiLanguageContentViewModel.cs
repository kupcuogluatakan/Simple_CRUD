using System;

namespace ODMSModel.ViewModel
{
    [Serializable]
    public class MultiLanguageContentViewModel
    {
        /// <summary>
        /// I = insert, U = update, D = delete;
        /// </summary>

        public string OperationType { get; set; }

        public string LanguageCode { get; set; }
        public string Content { get; set; }


        public MultiLanguageContentViewModel()
        {
        }

        public MultiLanguageContentViewModel(string languageCode, string content, string operationType)
        {
            LanguageCode = languageCode;
            Content = content;
            OperationType = operationType;
        }
    }
}
