using System;
using System.Web.Mvc;
using AutoMapper;

namespace Cartisan.Web.Mvc.Filters {
    public class AutoMapModelAttribute : ActionFilterAttribute{
        private readonly Type _sourceType;
        private readonly Type _destType;

        public AutoMapModelAttribute(Type sourceType, Type destType) {
            this._sourceType = sourceType;
            this._destType = destType;
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext) {
            object model = filterContext.Controller.ViewData.Model;
            object viewModel = Mapper.Map(model, _sourceType, _destType);
            filterContext.Controller.ViewData.Model = viewModel;
        }
    }
}