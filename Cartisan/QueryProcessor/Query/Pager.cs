namespace Cartisan.QueryProcessor.Query {
    public class Pager {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int Total { get; set; }

        public int PageCount {
            get {
                if(this.Total == 0) {
                    return 1;
                }
                return this.Total / this.PageSize + (this.Total % this.PageSize > 0 ? 1 : 0);
            }
        }
    }
}