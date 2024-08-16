using MauiAccelerator.Features.Common.Pages;

namespace MauiAccelerator.Features.Login.Pages;

public partial class LoginPage : BaseContentPage
{
    public LoginPage(LoginPageViewModel viewModel) 
        : base(viewModel)
    {
        InitializeComponent();
    }
}