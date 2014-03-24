using System.Collections.Generic;
using System.Linq;
using Cartisan.Infrastructure;
using NUnit.Framework;

namespace Cartisan.Tests.Infrastructure {
    [TestFixture]
    public class PaginatedTest {
        [Test]
        public void Paginated() {
            Paginated<string> page = new Paginated<string>(Enumerable.Empty<string>(), 1, 10, 95);

            Assert.AreEqual(1, page.PageIndex);
            Assert.AreEqual(10, page.PageSize);
            Assert.AreEqual(95, page.Total);
            Assert.AreEqual(10, page.PageTotal);
        }
    }
}