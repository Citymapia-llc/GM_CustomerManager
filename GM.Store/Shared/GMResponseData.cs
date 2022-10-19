using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GM.Store.Shared
{
    public class GMResponseData
    {
        public int Status { get; set; }

        public string Message { get; set; }

        public bool Succeeded => Status == 200;
    }

    public class GMResponseListData<T> : GMResponseData
    {
        public GMList<T> Model { get; set; }

    }
    public class GMResponseData<T> : GMResponseData
    {
        public T Model { get; set; }

    }

    public class GMList<T>
    {
        public IEnumerable<T> List { get; set; }
        public GMPager Pager { get; set; }
    }
    public interface IGMPager
    {
        int PageSize { get; set; }
        int CurrentPage { get; set; }
    }
    public class GMPager : IGMPager
    {
        public GMPager()
        {
            PageSize = 10;
            CurrentPage = 1;
        }
        private int pageSize;
        private int pageIndex;

        public int PageSize { get => pageSize; set => pageSize = value; }
        /// <summary>
        /// Page index starts from 1
        /// </summary>
        public int CurrentPage { get => pageIndex; set => pageIndex = value; }

    }
}
