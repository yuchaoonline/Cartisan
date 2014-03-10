using System.Configuration;

namespace Cartisan.Config {
    [ConfigurationSectionName("cartisanConfiguration")]
    public class CartisanConfigurationSection: ConfigurationSection {
        [ConfigurationProperty("handlers", IsRequired = false)]
        public HandlerElementCollection Handlers {
            get { return (HandlerElementCollection)base["handlers"]; }
            set { base["handlers"] = value; }
        }
    }
}