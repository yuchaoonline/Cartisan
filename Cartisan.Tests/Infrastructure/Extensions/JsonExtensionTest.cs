using System;
using Cartisan.Infrastructure.Extensions;
using NUnit.Framework;

namespace Cartisan.Tests.Infrastructure.Extensions {
    [TestFixture]
    public class JsonExtensionTest {
        public class Parent {
            private string PrivateField = "This is private field.";
            public string PublicField = "This is public field.";

            private string PrivateProperty { get { return "This is private Property."; } }
            public string PublicProperty { get { return "This is private Property."; } }

            public Sub PublicSub { get { return new Sub(); } }
            private Sub PrivateSub { get { return new Sub(); } }
        }

        public class Sub {
            public string PublicName { get { return "Public Name"; } }
            private string PrivateName { get { return "Private Name"; } }
        }

        [Test]
        public void NonPublicSerialize() {
            Parent p = new Parent();
            Console.WriteLine(p.ToJson(true, false, true));
        }

        [Test]
        public void NonPublicCamelCasePropertyNameSerialize() {
            Parent p = new Parent();
            Console.WriteLine(p.ToJson(true, true, true));
        }

        [Test]
        public void PublicSerialize() {
            Parent p = new Parent();
            Console.WriteLine(p.ToJson(false, false, true));
        }

        [Test]
        public void PublicCamelCasePropertyNameSerialize() {
            Parent p = new Parent();
            Console.WriteLine(p.ToJson(false, true, true));
        }

        [Test]
        public void NormalUsing() {
            Parent p = new Parent();
            Console.WriteLine(p.ToJson());
        }
    }
}