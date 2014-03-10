namespace Cartisan.Web.Mvc.Tests {
    // WatiN.

//    public class SampleViewTests {
//        private Process webServer;
//        [TestInitialize]
//        public void Setup() {
//            webServer = new Process();
//            webServer.StartInfo.FileName = "WebDev.WebServer.exe";
//            string path = "";
//            webServer.StartInfo.Arguments = String.Format(
//            "/port:8080 /path: {0}", path);
//            webServer.Start();
//        }
//        [TestMethod]
//        public void CheckIfNicknameIsNotUsed() {
//            using (IE ie = new IE("http://localhost:8080/Samples/Datagrid")) {
//                // Select a particular customer ID
//                ie.SelectList("ddCustomerList").Option("1").Select();
//                // Check the resulting HTML on first row, second cell
//                Assert.AreEqual(
//                "A Bike Store",
//                ie.Table(Find.ById("gridOrders").TableRow[0].TableCells[1].InnerHtml));
//            }
//        }
//        [TestCleanup]
//        public void TearDown() {
//            webServer.Kill();
//        }
//    }
}
