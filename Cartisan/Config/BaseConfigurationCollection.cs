using System.Configuration;
using System.Reflection;
using Cartisan.Infrastructure.Extensions;

namespace Cartisan.Config {
    public class BaseConfigurationCollection<TConfigurationElement>: ConfigurationElementCollection
        where TConfigurationElement: ConfigurationElement, new() {
        protected string ConfigurationElementKey { get; set; }

        public BaseConfigurationCollection() {
            foreach(PropertyInfo propertyInfo in typeof(TConfigurationElement).GetProperties()) {
                ConfigurationPropertyAttribute configurationProperty =
                    propertyInfo.GetCustomAttribute<ConfigurationPropertyAttribute>();
                if(configurationProperty != null && configurationProperty.IsKey) {
                    ConfigurationElementKey = propertyInfo.Name;
                    break;
                }
            }
        }

        public TConfigurationElement this[int index] {
            get { return base.BaseGet(index) as TConfigurationElement; }
            set {
                if(base.BaseGet(index) != null) {
                    base.BaseRemoveAt(index);
                }
                base.BaseAdd(index, value);
            }
        }

        public override ConfigurationElementCollectionType CollectionType {
            get { return ConfigurationElementCollectionType.BasicMap; }
        }

        protected override ConfigurationElement CreateNewElement() {
            return new TConfigurationElement();
        }

        protected override string ElementName {
            get { return this.GetCustomAttribute<ConfigurationCollectionAttribute>().AddItemName; }
        }

        protected override object GetElementKey(ConfigurationElement element) {
            return element.GetValueByKey(ConfigurationElementKey);
        }
    }
}