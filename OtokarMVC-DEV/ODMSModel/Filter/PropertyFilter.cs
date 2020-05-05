using System.Collections.Generic;
using System.Linq;

namespace ODMSModel.Filter
{
    public class PropertyFilter<T>
    {
        public PropertyFilter()
        {

        }

        public PropertyFilter(T param)
        {
            if (param != null)
                this.Value = param;
        }

        public string Name { get; set; }

        private T _value;

        public T Value
        {
            get { return _value; }
            set { _value = value; IsActive = true; }
        }

        public bool IsActive { get; set; }

        public override string ToString()
        {
            return Name;
        }

        public PropertyFilterTypeEnum PropertyFilterType { get; set; }

    }

    public class PropertyListFilter<T>
    {
        public PropertyListFilter()
        {

        }

        public PropertyListFilter(T[] param)
        {
            if (param != null)
                this.Value = param.ToList();
        }

        public string Name { get; set; }

        private List<T> _value;

        public List<T> Value
        {
            get { return _value; }
            set { _value = value; }
        }


        public override string ToString()
        {
            return Name;
        }


        public bool FilterByNull { get; set; }

        public bool IsActive
        {
            get
            {
                return _value != null && _value.Count > 0;
            }
        }

        public PropertyFilterTypeEnum PropertyFilterType { get; set; }

        public string GetValues()
        {
            string l = "";
            Value.ForEach(x => { l += string.Format("{{Value:{0}}},", x); });
            return string.Format("[{0}]", l);
        }

    }



    public enum PropertyFilterTypeEnum
    {
        IsEqual,
        Like,
    }


}
