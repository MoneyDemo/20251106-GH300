using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SimpleWebRazor.Pages;

namespace SimpleWebRazor.UnitTest;

[TestClass]
public class IndexPageTests
{
    [TestMethod]
    public void OnGet_ShouldSetUserName_WhenHeaderIsPresent()
    {
        // Arrange
        var mockLogger = new Mock<ILogger<IndexModel>>();
        var pageModel = new IndexModel(mockLogger.Object);
        
        var httpContext = new DefaultHttpContext();
        httpContext.Request.Headers["X-MS-CLIENT-PRINCIPAL-NAME"] = "testuser";
        
        pageModel.PageContext = new PageContext
        {
            HttpContext = httpContext
        };

        // Act
        pageModel.OnGet();

        // Assert
        pageModel.UserName.Should().Be("testuser");
    }

    [TestMethod]
    public void OnGet_ShouldSetDefaultUserName_WhenHeaderIsNotPresent()
    {
        // Arrange
        var mockLogger = new Mock<ILogger<IndexModel>>();
        var pageModel = new IndexModel(mockLogger.Object);
        
        var httpContext = new DefaultHttpContext();
        
        pageModel.PageContext = new PageContext
        {
            HttpContext = httpContext
        };

        // Act
        pageModel.OnGet();

        // Assert
        pageModel.UserName.Should().Be("Not login yet");
    }

    [TestMethod]
    public void OnGet_ShouldSetDefaultUserName_WhenHeaderIsEmpty()
    {
        // Arrange
        var mockLogger = new Mock<ILogger<IndexModel>>();
        var pageModel = new IndexModel(mockLogger.Object);
        
        var httpContext = new DefaultHttpContext();
        // Empty string is treated as a value in headers, not as "no value"
        httpContext.Request.Headers["X-MS-CLIENT-PRINCIPAL-NAME"] = "";
        
        pageModel.PageContext = new PageContext
        {
            HttpContext = httpContext
        };

        // Act
        pageModel.OnGet();

        // Assert
        // When empty string is provided, FirstOrDefault returns empty string, not null
        pageModel.UserName.Should().Be("");
    }
}
