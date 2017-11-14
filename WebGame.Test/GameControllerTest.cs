using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebGame.Data.DAL;
using WebGame.Controllers;
using System.Web.Http;
using System.Web.Http.Results;

namespace WebGame.Test
{
    [TestClass]
    public class GameControllerTest
    {
        [TestMethod]
        public void GetGamById_Ok()
        {
            // Arrange
            var mockRepository = new Mock<IUnitOfWork>();
            var controller = new GamesController(mockRepository.Object);

            // Act
            var actionResult = controller.GetGames();

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public void GetGamByKeyReturnNotFoundWhenWronkKey()
        {
            // Arrange
            var mockRepository = new Mock<IUnitOfWork>();
            var controller = new GamesController(mockRepository.Object);

            // Act
            var actionResult = controller.GetGame("wrongKey");

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }
    }
}
