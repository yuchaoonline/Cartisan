using System;

namespace Cartisan.Config {
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class ConfigurationSectionNameAttribute: Attribute {
        public string Name { get; set; }

        public ConfigurationSectionNameAttribute(string name) {
            this.Name = name;
        }
    }
}