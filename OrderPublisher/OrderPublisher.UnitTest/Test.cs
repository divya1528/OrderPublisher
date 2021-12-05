using ApplicationCore.Entities;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using OrderPublisher.Abstract;
using OrderPublisher.Controllers;
using System.Collections.Generic;

namespace OrderPublisher.UnitTest
{
    public class Tests
    {
        Mock<IOrderRepository> _mockRepository;
        Mock<ILogger<OrderController>> _mockLogger;
        Mock<IProducerWrapper> _mockProducer;
        [SetUp]
        public void Setup()
        {
            _mockLogger = new Mock<ILogger<OrderController>>();
            _mockProducer = new Mock<IProducerWrapper>();
            _mockRepository = new Mock<IOrderRepository>();
        }

        [Test]
        public void TestCreate()
        {
            Order order = new Order() { Products = new List<Product>() { new Product() { ProductID = "1", ProductPrice = 10, ProductQty = 2 } }, Status ="placed", UserID="user123" };
            OrderController controller = new OrderController(_mockRepository.Object, _mockProducer.Object, _mockLogger.Object);
            var response = (ObjectResult)controller.PostAsync(order).Result.Result;
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void TestCreate_BadRequest()
        {
            OrderController controller = new OrderController(_mockRepository.Object, _mockProducer.Object, _mockLogger.Object);
            var response = (BadRequestResult)controller.PostAsync(null).Result.Result;
            Assert.AreEqual(response.StatusCode, 400);
        }
    }
}