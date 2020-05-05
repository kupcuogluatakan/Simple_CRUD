using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSBusiness.Terminal.Common
{
    public class EnumerableResponse<T>:IEnumerable<T>,IResponse where T:class
    {
        private readonly List<T> _list;
        public GridPagingInfo PagingInfo { get; }

        public EnumerableResponse(List<T> list,GridPagingInfo pagingInfo=null)
        {
            _list = list;
            PagingInfo = pagingInfo;
        }

        public T this[int index]
        {
            get { return _list[index]; }
            set { _list.Insert(index, value); }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
