using System.Configuration;

namespace Cartisan.Config {
    public class HandlerElement: ConfigurationElement {
        [ConfigurationProperty("name", IsRequired = true, IsKey = true)]
        public string Name {
            get { return (string)base["name"]; }
            set { base["name"] = value; }
        } 
        
        [ConfigurationProperty("sourceType", IsRequired = true)]
        public HandlerSourceType SourceType {
            get { return (HandlerSourceType)base["sourceType"]; }
            set { base["sourceType"] = value; }
        }

        [ConfigurationProperty("source", IsRequired = true)]
        public string Source {
            get { return (string)base["source"]; }
            set { base["source"] = value; }
        }
    }
}