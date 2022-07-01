using Amazon.DynamoDBv2.DataModel;
using LambdaWebApi.Controllers;
using LambdaWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Xunit;

namespace LambdaWebApi.Tests.Unit.Controllers
{
    public class TeamsControllerTests
    {
        private readonly TeamsController controller;
        private readonly IDynamoDBContext dynamoDBContext;

        public TeamsControllerTests()
        {
            dynamoDBContext = Substitute.For<IDynamoDBContext>();
            controller = new TeamsController(dynamoDBContext, Substitute.For<ILogger<TeamsController>>());
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task GetReturns500WhenQueryAsyncErrors()
        {
            // Arrange
            dynamoDBContext.QueryAsync<SportsTeam>(default).ReturnsForAnyArgs(x => { throw new Exception(); });

            // Act
            var result = (await controller.Get(default)) as ObjectResult;

            // Assert
            Assert.Equal(500, result.StatusCode);

        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task GetReturns200WhenNoErrors()
        {
            // Arrange
            // Act
            var result = await controller.Get(default);

            // Assert
            Assert.IsType<OkResult>(result);
        }
    }
}
