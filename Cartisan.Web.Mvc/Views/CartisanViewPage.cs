using System.Web.Mvc;

namespace Cartisan.Web.Mvc.Views {
    public class CartisanViewPage: WebViewPage {
        public ParamBuilder Param { get { return new ParamBuilder();} }

        public override void Execute() {
            throw new System.NotImplementedException();
        }
    }
}