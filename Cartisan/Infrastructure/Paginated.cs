using System;
using System.Collections.Generic;

namespace Cartisan.Infrastructure {
    /// <summary>
    /// 分页
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Paginated<T> {
        private readonly IEnumerable<T> _datas;
        private readonly int _pageIndex;
        private readonly int _pageSize;
        private readonly int _total;
        private readonly int _pageTotal;

        public Paginated(IEnumerable<T> source, int pageIndex, int pageSize, int total) {
            if (source == null) {
                throw new ArgumentNullException("source");
            }
            this._datas = source;
            this._pageIndex = pageIndex;
            this._pageSize = pageSize;
            this._total = total;
            this._pageTotal = (int)Math.Ceiling(total / (double)pageSize);
        }

        /// <summary>
        /// 是否有上一页
        /// </summary>
        public bool HasPreviousPage {
            get {
                return this.PageIndex > 1;
            }
        }

        /// <summary>
        /// 是否有下一页
        /// </summary>
        public bool HasNextPage {
            get {
                return this.PageIndex < this.PageTotal;
            }
        }

        /// <summary>
        /// 当前页的数据
        /// </summary>
        public IEnumerable<T> Datas {
            get { return this._datas; }
        }

        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex {
            get { return this._pageIndex; }
        }

        /// <summary>
        /// 每页数据条数
        /// </summary>
        public int PageSize {
            get { return this._pageSize; }
        }

        /// <summary>
        /// 数据总条数
        /// </summary>
        public int Total {
            get { return this._total; }
        }

        /// <summary>
        /// 总页数
        /// </summary>
        public int PageTotal {
            get { return this._pageTotal; }
        }
    }
}