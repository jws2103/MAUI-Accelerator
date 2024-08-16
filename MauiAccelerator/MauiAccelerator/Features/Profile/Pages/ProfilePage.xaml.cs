using MauiAccelerator.Features.Common.Pages;

namespace MauiAccelerator.Features.Profile.Pages;

public partial class ProfilePage : BaseContentPage
{
    public ProfilePage(ProfilePageViewModel viewModel)
        : base(viewModel)
    {
        InitializeComponent();
    }
}