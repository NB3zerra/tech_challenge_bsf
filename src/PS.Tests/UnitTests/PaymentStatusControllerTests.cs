using System;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Xunit;
using PS.Presentation.Controllers;
using Microsoft.AspNetCore.Mvc;
using PS.Domain.Common;
using PS.Services.Interfaces;

namespace PS.Tests.UnitTests
{
    public class PaymentProcessingServiceControllerTests
    {
        [Fact]
        public async Task GetPaymentStatus_ShouldReturnOk_WhenStatusIsFound()
        {
            // Arrange
            var paymentIntentId = Guid.NewGuid();
            var status = PaymentIntentStatus.OK;

            var mockService = new Mock<IPaymentProcessingService>();
            mockService.Setup(s => s.GetPaymentIntentStatusAsync(paymentIntentId))
                       .ReturnsAsync(status);

            var controller = new PaymentStatusController(mockService.Object);

            // Act
            var result = await controller.GetPaymentStatus(paymentIntentId);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var response = okResult.Value.Should().BeAssignableTo<object>().Subject;

            response.Should().BeEquivalentTo(new { PaymentIntentId = paymentIntentId, Status = status });
        }
    }
}