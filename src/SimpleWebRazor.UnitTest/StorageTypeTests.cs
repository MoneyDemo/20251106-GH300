using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleWebRazor.Models;

namespace SimpleWebRazor.UnitTest;

[TestClass]
public class StorageTypeTests
{
    [TestMethod]
    public void StorageType_ShouldHaveLocalValue()
    {
        // Arrange & Act
        var local = StorageType.Local;

        // Assert
        ((int)local).Should().Be(1);
    }

    [TestMethod]
    public void StorageType_ShouldHaveAzureValue()
    {
        // Arrange & Act
        var azure = StorageType.Azure;

        // Assert
        ((int)azure).Should().Be(2);
    }

    [TestMethod]
    public void StorageType_ShouldConvertFromInt()
    {
        // Arrange & Act
        var local = (StorageType)1;
        var azure = (StorageType)2;

        // Assert
        local.Should().Be(StorageType.Local);
        azure.Should().Be(StorageType.Azure);
    }
}
