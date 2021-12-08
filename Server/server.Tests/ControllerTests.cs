using System.Net.Http;
using Xunit;
using Server.Controllers;



namespace server.Tests;

public class TestPostController
{

    [Fact]
    public void Test()
    {
        // Arrange
        var controller = new PostController();
        controller.Request = new HttpRequestMessage();
        //controller.Configuration = new HttpConfiguration();

        // Act
        var response = controller.Get();

        // Assert
        Assert.Equal(null, response);

    }
}