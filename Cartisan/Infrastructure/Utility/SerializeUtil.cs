using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Cartisan.Infrastructure.Utility {
    public class SerializeUtil {
        static public string Serialize(object xmlContent, Encoding encoding = null) {
            XmlSerializer serializer = new XmlSerializer(xmlContent.GetType());
            //StringBuilder builder = new System.Text.StringBuilder();
            //StringWriter writer = new StringWriterWithEncoding(Encoding.UTF8);
            //new System.IO.StringWriter(builder);
            //System.Xml.XmlTextWriter writer = new System.Xml.XmlTextWriter(@"c:\test.xml", System.Text.Encoding.UTF8);
            //serializer.Serialize(writer, xmlContent);
            //return builder.ToString();

            MemoryStream stream = new MemoryStream();
            XmlWriterSettings setting = new XmlWriterSettings();
            setting.Encoding = encoding ?? Encoding.GetEncoding("utf-8");
            setting.Indent = true;
            using (XmlWriter writer = XmlWriter.Create(stream, setting)) {
                serializer.Serialize(writer, xmlContent);
            }
            return System.Text.RegularExpressions.Regex.Replace(Encoding.GetEncoding("utf-8").GetString(stream.ToArray()), "^[^<]", "");
        }

        static public object DeSerialize<XmlType>(string xmlString) {

            XmlSerializer serializer = new XmlSerializer(typeof(XmlType));
            StringBuilder builder = new StringBuilder(xmlString);
            StringReader reader = new StringReader(builder.ToString());
            try {
                return serializer.Deserialize(reader);
            }
            catch (Exception) {
                return null;
            }
        }
    }
}