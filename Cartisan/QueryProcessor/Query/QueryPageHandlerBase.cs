//using System.Linq;
//
//namespace Cartisan.QueryProcessor.Query {
//    public abstract class QueryPageHandlerBase<TQuery, TResult> : QueryHandlerBase<PageResult<TResult>>, IQueryPageHandler<TQuery, TResult> where TQuery : IQuery {
//        protected abstract string GetListSql { get; }
//        protected abstract string GetCountSql { get; }
//        protected abstract string GetAllSql { get; }
//
//        protected abstract object Parameters { get; }
//        
//        protected virtual TQuery Query { get; private set; }
//        protected virtual PageOption PageOption { get; private set; }
//
//        private IPageResult<TResult> Result { get; set; }
//
//        protected int Start {
//            get { return (this.PageOption.PageIndex - 1) * this.PageOption.PageSize + 1; }
//        }
//
//        protected int End {
//            get { return this.PageOption.PageIndex * this.PageOption.PageSize; }
//        }
//
//
//        public IPageResult<TResult> Execute(TQuery query, PageOption pageOption) {
//            return this.ExecuteResult(result => {
//                this.Query = query;
//                this.PageOption = pageOption;
//
//                this.Result = result;
//
//                if(pageOption.IsSelectAll) {
//                    this.GetByNoPager();
//                }
//                else {
//                    this.PageOption.PageIndex = this.PageOption.PageIndex == 0 ? 1 : this.PageOption.PageIndex;
//                    this.PageOption.PageSize = this.PageOption.PageSize == 0 ? 10 : this.PageOption.PageSize;
//
//                    this.GetByPager();
//                }
//            });
//        }
//
//        private void GetByNoPager() {
//            using(var connection = Database.GetConnection()) {
//                this.Result.Datas = connection.Query<TResult>(this.GetAllSql, this.Parameters).ToList();
//                var count = this.Result.Datas.Count();
//                this.Result.Pager = new Pager {
//                    PageIndex = 1,
//                    PageSize = count,
//                    Total = count
//                };
//            }
//        }
//
//        private void GetByPager() {
//            using(var connection = Database.GetConnection()) {
//                using(var reader = connection.QueryMultiple(this.GetCountSql + this.GetListSql, this.Parameters)) {
//                    this.Result.Pager = new Pager {
//                        PageIndex = this.PageOption.PageIndex,
//                        PageSize = this.PageOption.PageSize,
//                        Total = reader.Read<int>().Single()
//                    };
//                    this.Result.Datas = reader.Read<TResult>().ToList();
//                }
//            }
//        }
//    }
//}