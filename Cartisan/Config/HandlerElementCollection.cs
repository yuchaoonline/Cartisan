using System.Configuration;

namespace Cartisan.Config {
    [ConfigurationCollection(typeof(HandlerElement), AddItemName = "handler",
        CollectionType = ConfigurationElementCollectionType.BasicMap)]
    public class HandlerElementCollection: BaseConfigurationCollection<HandlerElement> {}
}