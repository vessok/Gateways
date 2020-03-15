using Gateways.Mapping;
using NUnit.Framework;
using System.Collections.Generic;

namespace Gateways.Test {
    class TestGateways : TestsBase {
        [Test]
        public void Get_WhenCalled_ReturnsOkResult() {
            // Act
            var okResult = _gatewayController.ListGatewaysAsync(new QueryGatewayResource());

            // Assert
            Assert.IsAssignableFrom<List<GatewayResource>>(okResult.Result);
        }
    }
}
