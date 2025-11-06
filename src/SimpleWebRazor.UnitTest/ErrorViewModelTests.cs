using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleWebRazor.Models;

namespace SimpleWebRazor.UnitTest;

[TestClass]
public class ErrorViewModelTests
{
    [TestMethod]
    public void ShowRequestId_ShouldBeTrue_WhenRequestIdHasValue()
    {
        // Arrange
        var model = new ErrorViewModel
        {
            RequestId = "TestRequestId"
        };

        // Act
        var result = model.ShowRequestId;

        // Assert
        result.Should().BeTrue();
    }

    [TestMethod]
    public void ShowRequestId_ShouldBeFalse_WhenRequestIdIsNull()
    {
        // Arrange
        var model = new ErrorViewModel
        {
            RequestId = null
        };

        // Act
        var result = model.ShowRequestId;

        // Assert
        result.Should().BeFalse();
    }

    [TestMethod]
    public void ShowRequestId_ShouldBeFalse_WhenRequestIdIsEmpty()
    {
        // Arrange
        var model = new ErrorViewModel
        {
            RequestId = string.Empty
        };

        // Act
        var result = model.ShowRequestId;

        // Assert
        result.Should().BeFalse();
    }

    [TestMethod]
    public void ShowRequestId_ShouldBeTrue_WhenRequestIdIsWhitespace()
    {
        // Arrange
        var model = new ErrorViewModel
        {
            RequestId = "   "
        };

        // Act
        var result = model.ShowRequestId;

        // Assert
        // The implementation uses !string.IsNullOrEmpty which returns true for whitespace
        result.Should().BeTrue();
    }
}
