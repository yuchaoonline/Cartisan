using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Cartisan.Config {
    public static class Configuration {
        public static readonly bool IsPersistanceMessage = GetAppConfig<bool>("PersistanceMessage");

        public static T GetAppConfig<T>(string key) {
            T value = default(T);
            try {
                string configValue = GetAppConfig(key);
                if(typeof(T).IsEquivalentTo(typeof(Guid))) {
                    value = (T)Convert.ChangeType(new Guid(configValue), typeof(T));
                }
                else {
                    value = (T)Convert.ChangeType(value, typeof(T));
                }
            }
            catch(Exception) {
                
            }
            return value;
        }

        public static string GetAppConfig(string keyname, string configPath = "Config") {
            string configValue = ConfigurationManager.AppSettings[keyname];

            try {
                if(string.IsNullOrWhiteSpace(configValue)) {
                    string filePath = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, configPath);
                    using(TextReader reader = new StreamReader(filePath)) {
                        XElement xml = XElement.Load(filePath);
                        if(xml != null) {
                            XElement element = xml.Elements().SingleOrDefault(
                                e => e.Attribute("key") != null && e.Attribute("key").Value.Equals(keyname));
                            if(element!=null) {
                                configValue = element.Attribute("value").Value;
                            }
                        }
                    }
                }
            }
            catch(Exception) {
                configValue = string.Empty;
            }
            return configValue;
        }
    }
}