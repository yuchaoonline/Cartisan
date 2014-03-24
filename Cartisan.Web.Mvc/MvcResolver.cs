using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Mvc;
using Cartisan.Infrastructure;
using Cartisan.IoC;

namespace Cartisan.Web.Mvc {
    public class MvcResolver: IResolver {
        public object GetService(Type serviceType) {
            return DependencyResolver.Current.GetService(serviceType);
        }

        public TService GetService<TService>() {
            return DependencyResolver.Current.GetService<TService>();
        }

        public IEnumerable GetServices(Type serviceType) {
            return DependencyResolver.Current.GetServices(serviceType);
        }

        public IEnumerable<TService> GetServices<TService>() {
            return DependencyResolver.Current.GetServices<TService>();
        }
    }
}