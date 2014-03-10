using System.Collections;
using System.Configuration;
using Cartisan.Infrastructure.Extensions;

namespace Cartisan.Config {
    public class ConfigurationReader {
        private static readonly ConfigurationReader _instance = new ConfigurationReader();

        private ConfigurationReader() { }

        public static ConfigurationReader Instance { get { return _instance; } }

        private Hashtable _configs = new Hashtable();

        public TConfigurationSection GetConfigurationSection<TConfigurationSection>(string name = null) where TConfigurationSection: class {
            if(string.IsNullOrEmpty(name)) {
                ConfigurationSectionNameAttribute sectionNameAttribute =
                    typeof(TConfigurationSection).GetCustomAttribute<ConfigurationSectionNameAttribute>();
                if(sectionNameAttribute!=null) {
                    name = sectionNameAttribute.Name;
                }
                if(string.IsNullOrEmpty(name)) {
                    name = typeof(TConfigurationSection).Name;
                }
            }
            var configSection = _configs[name] as TConfigurationSection;
            if(configSection==null) {
                configSection = ConfigurationManager.GetSection(name) as TConfigurationSection;
                _configs[name] = configSection;
            }

            return configSection;
        }
    }
}