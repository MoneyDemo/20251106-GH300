using System.Diagnostics;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SimpleWebRazor.Pages;

namespace SimpleWebRazor.UnitTest;

[TestClass]
public class ErrorPageTests
{
    [TestMethod]
    public void OnGet_ShouldSetRequestId_FromActivity()
    {
        // Arrange
        var mockLogger = new Mock<ILogger<ErrorModel>>();
        var pageModel = new ErrorModel(mockLogger.Object);
        
        // Start an activity to test
        var activity = new Activity("TestActivity");
        activity.Start();
        
        try
        {
            var httpContext = new DefaultHttpContext();
            pageModel.PageContext = new PageContext
            {
                HttpContext = httpContext
            };

            // Act
            pageModel.OnGet();

            // Assert
            pageModel.RequestId.Should().NotBeNullOrEmpty();
            pageModel.ShowRequestId.Should().BeTrue();
        }
        finally
        {
            activity.Stop();
        }
    }

    [TestMethod]
    public void OnGet_ShouldSetRequestId_FromTraceIdentifier_WhenNoActivity()
    {
        // Arrange
        var mockLogger = new Mock<ILogger<ErrorModel>>();
        var pageModel = new ErrorModel(mockLogger.Object);
        
        var httpContext = new DefaultHttpContext();
        httpContext.TraceIdentifier = "TestTraceId";
        
        pageModel.PageContext = new PageContext
        {
            HttpContext = httpContext
        };

        // Act
        pageModel.OnGet();

        // Assert
        pageModel.RequestId.Should().Be("TestTraceId");
        pageModel.ShowRequestId.Should().BeTrue();
    }

    [TestMethod]
    public void ShowRequestId_ShouldBeFalse_WhenRequestIdIsNull()
    {
        // Arrange
        var mockLogger = new Mock<ILogger<ErrorModel>>();
        var pageModel = new ErrorModel(mockLogger.Object);

        // Act
        var result = pageModel.ShowRequestId;

        // Assert
        result.Should().BeFalse();
    }

    [TestMethod]
    public void ShowRequestId_ShouldBeFalse_WhenRequestIdIsEmpty()
    {
        // Arrange
        var mockLogger = new Mock<ILogger<ErrorModel>>();
        var pageModel = new ErrorModel(mockLogger.Object)
        {
            RequestId = string.Empty
        };

        // Act
        var result = pageModel.ShowRequestId;

        // Assert
        result.Should().BeFalse();
    }

    [TestMethod]
    public void ShowRequestId_ShouldBeTrue_WhenRequestIdHasValue()
    {
        // Arrange
        var mockLogger = new Mock<ILogger<ErrorModel>>();
        var pageModel = new ErrorModel(mockLogger.Object)
        {
            RequestId = "TestRequestId"
        };

        // Act
        var result = pageModel.ShowRequestId;

        // Assert
        result.Should().BeTrue();
    }
}
