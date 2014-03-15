using System.Collections.Generic;

namespace Cartisan.Infrastructure {
    public class PageInfo<T> {
        private int _total;
        private int _pageSize;
        private int _pageIndex;
        private IList<T> _items;

        public int Total {
            get { return this._total; }
            set { this._total = value; }
        }

        public int PageCount {
            get { return Total / PageSize + (Total % PageSize == 0 ? 0 : 1); }
        }

        public int PageSize {
            get { return this._pageSize; }
            set { this._pageSize = value; }
        }

        public int PageIndex {
            get { return this._pageIndex; }
            set { this._pageIndex = value; }
        }

        public IList<T> Items {
            get { return this._items; }
            set { this._items = value; }
        }
    }
}