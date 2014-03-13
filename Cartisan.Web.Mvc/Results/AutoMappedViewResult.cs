using System;
using System.Web.Mvc;

namespace Cartisan.Web.Mvc.Results {
    public class AutoMappedViewResult: ViewResult {
        private readonly Type _destinationType;

        public static Func<object, Type, Type, object> Map = (a, b, c) => {
            throw new InvalidOperationException(@"The Mapping function must be set on the AutoMapperResult class");
        };

        public AutoMappedViewResult(Type destinationType) {
            this._destinationType = destinationType;
        }

        public Type ViewModelType { get; set; }

        public override void ExecuteResult(ControllerContext context) {
            ViewData.Model = Map(ViewData.Model, ViewData.Model.GetType(), this._destinationType);
        }
    }
}