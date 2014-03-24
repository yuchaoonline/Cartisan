using Cartisan.Infrastructure.Extensions;
using NUnit.Framework;

namespace Cartisan.Tests.Infrastructure.Extensions {
    [TestFixture]
    public class StringExtensionTests {
        [Test]
        public void RepeatTest() {
            Assert.AreEqual("strstrstr", "str".Repeat(3));
            Assert.AreEqual("str,str,str", "str".Repeat(",", 3));
        }

        [Test]
        public void ToCamelCaseTest() {
            Assert.AreEqual("urlValue", "URLValue".ToCamelCase());
            Assert.AreEqual("url", "URL".ToCamelCase());
            Assert.AreEqual("id", "ID".ToCamelCase());
            Assert.AreEqual("i", "I".ToCamelCase());
            Assert.AreEqual("", "".ToCamelCase());
            string nullStr = null;
            Assert.AreEqual(null, nullStr.ToCamelCase());
            Assert.AreEqual("iPhone", "iPhone".ToCamelCase());
            Assert.AreEqual("person", "Person".ToCamelCase());
            Assert.AreEqual("iPhone", "IPhone".ToCamelCase());
            Assert.AreEqual("i Phone", "I Phone".ToCamelCase());
            Assert.AreEqual(" IPhone", " IPhone".ToCamelCase());
        }
    }
}