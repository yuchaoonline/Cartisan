using System;
using System.Reflection;
using System.Web;
using System.Web.Routing;
using Moq;
using NUnit.Framework;

namespace Cartisan.Web.Mvc.UnitTest {
    public static class HttpUtility {
        public static HttpContextBase CreateHttpContext(string targetUrl = null, string httpMethod = "GET") {
            Mock<HttpRequestBase> mockRequest = new Mock<HttpRequestBase>();
            mockRequest.Setup(m => m.AppRelativeCurrentExecutionFilePath).Returns(targetUrl);
            mockRequest.Setup(m => m.HttpMethod).Returns(httpMethod);

            Mock<HttpResponseBase> mockResponse = new Mock<HttpResponseBase>();
            mockResponse.Setup(m => m.ApplyAppPathModifier(It.IsAny<string>())).Returns<string>(s => s);

            Mock<HttpContextBase> mockContext = new Mock<HttpContextBase>();
            mockContext.Setup(m => m.Request).Returns(mockRequest.Object);
            mockContext.Setup(m => m.Response).Returns(mockResponse.Object);

            return mockContext.Object;
        }

        public static void TestRouteFail(string url) {
            // Arrange
            RouteCollection routes = new RouteCollection();
            //RouteConfig.RegisterRoutes(routes);

            // Act
            RouteData result = routes.GetRouteData(CreateHttpContext(url));

            // Assert
            Assert.IsTrue(result == null || result.Route == null);
        }

        public static void TestRouteMatch(string url, string controller, string action, object routeProperties = null,
            string httpMethod = "GET") {
            // Arrange
            RouteCollection routes = new RouteCollection();
            //RouteConfig.RegisterRoutes(routes);

            // Act
            RouteData result = routes.GetRouteData(CreateHttpContext(url, httpMethod));

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(TestIncomingRouteResult(result, controller, action, routeProperties));
        }

        private static bool TestIncomingRouteResult(RouteData routeResult, string controller, string action, object propertySet = null) {
            Func<object, object, bool> valCompare =
                (v1, v2) => StringComparer.InvariantCultureIgnoreCase.Compare(v1, v2) == 0;

            bool result = valCompare(routeResult.Values["controller"], controller)
                && valCompare(routeResult.Values["action"], action);

            if(propertySet!=null) {
                PropertyInfo[] propertyInfos = propertySet.GetType().GetProperties();
                foreach(PropertyInfo propertyInfo in propertyInfos) {
                    if(!(routeResult.Values.ContainsKey(propertyInfo.Name)
                        && valCompare(routeResult.Values[propertyInfo.Name], propertyInfo.GetValue(propertySet, null)))) {
                        result = false;
                        break;
                    }
                }
            }
            return result;
        }
    }
}