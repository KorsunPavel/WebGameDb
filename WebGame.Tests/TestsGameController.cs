using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebGame.Data.DAL;
using WebGame.Controllers;
using System.Web.Http;
using System.Web.Http.Results;
using System.Linq;
using WebGame.Data;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using GameStore.Web.Services.Interfaces;
using System.Net;

namespace WebGame.Test
{
    [TestClass]
    public class TestsGameController
    {
        [TestMethod]
        public void GetGamByKey_Ok()
        {
            // Arrange
            string correctKey = "correctKey";
            var expextedGame = new Mock<GameDto>().Object;
            var mockService = new Mock<IGameService>();
            mockService.Setup(s => s.GetGameByKey(correctKey)).Returns(expextedGame);


            var controller = new GamesController(mockService.Object);

            // Act
            var actionResult = controller.GetGame(correctKey);
            var createdResult = actionResult as OkNegotiatedContentResult<GameDto>;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkNegotiatedContentResult<GameDto>));
           
        }
        [TestMethod]
        public void GetGames_Ok()
        {
            // Arrange
            IQueryable<GameDto> listOfgames = new List<GameDto>() { new GameDto() }.AsQueryable();

            var expextedGame = new Mock<GameDto>().Object;
            var mockService = new Mock<IGameService>();
            mockService.Setup(s => s.GetAllGames()).Returns(() => listOfgames);

            var controller = new GamesController(mockService.Object);
            // Act
            var actionResult = controller.GetGames();
            var okResult = actionResult as OkNegotiatedContentResult<IQueryable<GameDto>>;
            // Assert
            // Assert
            Assert.IsNotNull(okResult);
            Assert.IsInstanceOfType(actionResult, typeof(OkNegotiatedContentResult<IQueryable<GameDto>>));

        }


        [TestMethod]
        public void GetGamByKeyReturnNotFoundWhenWronkKey()
        {
            // Arrange
            string wrongKey = "wrongKey";
            var mockService = new Mock<IGameService>();

            mockService.Setup(s => s.GetGameByKey(wrongKey)).Returns(It.IsAny<GameDto>());


            var controller = new GamesController(mockService.Object);

            // Act
            var actionResult = controller.GetGame(wrongKey);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public void ShouldReturnOkWhenRemoveGame()
        {
            // Arrange
            string correctKey = "correctKey";
            var mockService = new Mock<IGameService>();
            mockService.Setup(s => s.DeleteGame(correctKey)).Returns(true);

            var controller = new GamesController(mockService.Object);

            // Act
            var actionResult = controller.RemoveGames(correctKey);
            var okResult = actionResult as OkResult;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkResult));
        }

        [TestMethod]
        public void ShouldCreateGame()
        {
            // Arrange
            var mockGame = new Mock<GameDto>();
            var mockService = new Mock<IGameService>();
            mockService.Setup(s => s.CreateNewGame(mockGame.Object)).Returns(mockGame.Object);

            var controller = new GamesController(mockService.Object);

            // Act
            var actionResult = controller.NewGames(mockGame.Object);
            var contentResult = actionResult as NegotiatedContentResult<GameDto>;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NegotiatedContentResult<GameDto>));
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(HttpStatusCode.Created, contentResult.StatusCode);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(mockGame.Object.Key, contentResult.Content.Key);
        }
    }
}
