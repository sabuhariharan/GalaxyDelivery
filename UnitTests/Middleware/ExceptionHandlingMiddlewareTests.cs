using System.IO;
using System.Text.Json;
using GalaxyDelivery.Middleware;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace UnitTests.Middleware;

[TestFixture]
public class ExceptionHandlingMiddlewareTests
{
    [Test]
    public async Task Invoke_WhenNextThrows_Returns500WithJson()
    {
        static Task Next(HttpContext _) => throw new InvalidOperationException("boom");

        var logger = new Mock<ILogger<ExceptionHandlingMiddleware>>();
        var middleware = new ExceptionHandlingMiddleware(Next, logger.Object);

        var context = new DefaultHttpContext();
        await using var body = new MemoryStream();
        context.Response.Body = body;

        await middleware.Invoke(context);

        Assert.That(context.Response.StatusCode, Is.EqualTo(StatusCodes.Status500InternalServerError));
        Assert.That(context.Response.ContentType, Is.EqualTo("application/json"));

        body.Position = 0;
        using var reader = new StreamReader(body);
        var json = await reader.ReadToEndAsync();

        using var doc = JsonDocument.Parse(json);
        Assert.That(doc.RootElement.GetProperty("error").GetString(), Is.EqualTo("An unexpected error occurred."));

        logger.Verify(
            x => x.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.IsAny<It.IsAnyType>(),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception, string>>()),
            Times.Once);
    }
}
