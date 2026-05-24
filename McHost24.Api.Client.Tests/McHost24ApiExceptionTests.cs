using System.Net;

namespace McHost24.Api.Client.Tests
{
  public sealed class McHost24ApiExceptionTests
  {
    [Fact]
    public void Constructor_WithMessage_SetsMessage()
    {
      // Arrange
      var message = "API failed.";

      // Act
      var exception = new McHost24ApiException(message);

      // Assert
      Assert.Equal(message, exception.Message);
      Assert.Null(exception.StatusCode);
      Assert.Null(exception.ResponseContent);
    }

    [Fact]
    public void Constructor_WithInnerException_SetsInnerException()
    {
      // Arrange
      var innerException = new InvalidOperationException("Inner failure.");

      // Act
      var exception = new McHost24ApiException("API failed.", innerException);

      // Assert
      Assert.Equal(innerException, exception.InnerException);
      Assert.Null(exception.StatusCode);
      Assert.Null(exception.ResponseContent);
    }

    [Fact]
    public void Constructor_WithStatusCodeAndResponseContent_SetsApiDetails()
    {
      // Arrange
      var responseContent = "{\"error\":\"denied\"}";

      // Act
      var exception = new McHost24ApiException("API failed.", HttpStatusCode.Forbidden, responseContent);

      // Assert
      Assert.Equal(HttpStatusCode.Forbidden, exception.StatusCode);
      Assert.Equal(responseContent, exception.ResponseContent);
    }
  }
}
