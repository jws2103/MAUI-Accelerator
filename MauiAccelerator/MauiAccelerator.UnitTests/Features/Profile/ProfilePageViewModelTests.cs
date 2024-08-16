using FluentAssertions;
using MauiAccelerator.Features.Profile.Pages;
using MauiAccelerator.Features.Profile.Services;
using Moq;

namespace MauiAccelerator.UnitTests.Features.Profile;

public class ProfilePageViewModelTests : UnitTestBase<ProfilePageViewModel>
{
    [Fact]
    public void OnAppearing_ShouldLoadProfile_WhenInvoked()
    {
        // Arrange
        var mockedProfileService = Mocker.GetMock<IProfileService>();
        
        // Act
        Sut.OnAppearing();
        
        // Assert
        mockedProfileService.Verify(p => p.GetUserProfileAsync(), Times.Once);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    public void SelectedTabChanged_ShouldSetSelectedTabIndex_WhenInvoked(int index)
    {
        // Arrange
        // Act
        Sut.SelectedTabChangedCommand.Execute(index);
        
        // Assert
        Sut.SelectedTabIndex.Should().Be(index);
    }
}