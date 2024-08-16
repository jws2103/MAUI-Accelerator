using MauiAccelerator.Features.Popups.Loading.Services;
using Mopups.Interfaces;
using Mopups.Pages;
using Moq;

namespace MauiAccelerator.UnitTests.Features.Popups;

public class LoadingServiceTests : UnitTestBase<LoadingService>
{
    [Fact]
    public async Task Show_ShouldNavigateToPopupPage_WhenInvoked()
    {
        // Arrange
        var mockedPopupSvc = Mocker.GetMock<IPopupNavigation>();
        
        // Act
        await Sut.Show(It.IsAny<PopupPage>());
        
        // Assert
        mockedPopupSvc.Verify(p => p.PushAsync(It.IsAny<PopupPage>(), true), Times.Once);
    }
    
    [Fact]
    public void Dispose_ShouldPopPage_WhenInvoked() 
    {
        // Arrange
        var mockedPopupSvc = Mocker.GetMock<IPopupNavigation>();

        // Act
        Sut.Dispose();
        
        // Assert
		mockedPopupSvc.Verify(p => p.PopAsync(true), Times.Once);
    }
}