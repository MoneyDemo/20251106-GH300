using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SimpleWebRazor.Pages;

namespace SimpleWebRazor.UnitTest;

[TestClass]
public class PrivacyPageTests
{
    [TestMethod]
    public void OnGet_ShouldExecuteSuccessfully()
    {
        // Arrange
        var mockLogger = new Mock<ILogger<PrivacyModel>>();
        var pageModel = new PrivacyModel(mockLogger.Object);

        // Act
        pageModel.OnGet();

        // Assert
        pageModel.Should().NotBeNull();
    }
}
