using System.ComponentModel.DataAnnotations;

namespace ODMSModel
{
    public class NumericTextBoxUIHintAttribute : UIHintAttribute
    {
        public int Min { get; set; }

        public bool Spinners { get; set; }

        public int MaxLength { get; set; }

        public string ControlName { get; set; }
        
        public int Decimals { get; set; }

        public NumericTextBoxUIHintAttribute(string uiHint, string controlName,int decimals, int min, int maxLength = 0, bool spinners = false)
            : base(uiHint)
        {
            Min = min;
            Spinners = spinners;
            MaxLength = maxLength;
            ControlName = controlName;
            Decimals = decimals;
        }
    }
}
