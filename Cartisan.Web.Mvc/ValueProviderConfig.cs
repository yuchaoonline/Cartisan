using System.Linq;
using System.Web.Mvc;
using Cartisan.Web.Mvc.Providers;

namespace Cartisan.Web.Mvc {
    public class ValueProviderConfig {
        public static void Initialize() {
            ValueProviderFactories.Factories.Remove(
                ValueProviderFactories.Factories.OfType<JsonValueProviderFactory>().FirstOrDefault());
            ValueProviderFactories.Factories.Add(new JsonNetValueProviderFactory());
        }
    }
}