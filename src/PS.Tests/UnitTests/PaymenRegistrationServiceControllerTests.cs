using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PS.Services.Interfaces;
using PS.Presentation.Controllers;
using PS.Domain.Common;
using Xunit;
using System;
using PS.Domain.DTOs;
using AutoMapper;

namespace PS.Tests.UnitTests
{

    public class PaymentRegistrationServiceControllerTests
    {
        [Fact]
        public async Task RegisterPayment_ShouldReturnCreated_WhenPaymentIsRegistered()
        {
            // Arrange
            var paymentIntent = new PaymentIntentDto
            {
                CustomerName = "testfact",
                Description = "testDescription",
                Amount = 200
            };

            var mockService = new Mock<IPaymentRegistrationService>();
            var mockMapper = new Mock<IMapper>();

            mockService.Setup(s => s.RegisterPaymentIntent(paymentIntent));

            var controller = new PaymentRegistrationServiceController(mockService.Object, mockMapper.Object);

            // Act
            var result = await controller.RegisterPaymentIntent(paymentIntent);

            // Assert
            result.Should().BeOfType<CreatedAtActionResult>();
        }

        [Fact]
        public async Task RegisterPayment_ShouldReturnBadRequest_WhenPaymentAmountIsZero()
        {
            // Arrange
            var paymentIntent = new PaymentIntentDto
            {
                CustomerName = "testfact",
                Description = "testDescription",
                Amount = 0
            };

            var mockService = new Mock<IPaymentRegistrationService>();
            var mockMapper = new Mock<IMapper>();

            mockService.Setup(s => s.RegisterPaymentIntent(paymentIntent));

            var controller = new PaymentRegistrationServiceController(mockService.Object, mockMapper.Object);

            // Act
            var result = await controller.RegisterPaymentIntent(paymentIntent);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }
    }

}