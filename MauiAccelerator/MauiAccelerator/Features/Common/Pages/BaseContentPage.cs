using Microsoft.Maui.Controls;

namespace MauiAccelerator.Features.Common.Pages;

public class BaseContentPage : ContentPage
{
    private readonly BasePageViewModel _viewModel;
    
    protected BaseContentPage(BasePageViewModel viewModel)
    {
        _viewModel = viewModel;
        BindingContext = viewModel;
    }
    
    public static readonly BindableProperty HasNavBarProperty =
        BindableProperty.Create(nameof(HasNavBar), typeof(bool), typeof(BaseContentPage), true, propertyChanged: OnHasNavBarChanged);


    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.OnAppearing();
    }
    
    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        _viewModel.OnDisappearing();
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        _viewModel.OnNavigatedTo();
    }

    protected override void OnNavigatingFrom(NavigatingFromEventArgs args)
    {
        _viewModel.OnNavigatingFrom();
        base.OnNavigatingFrom(args);
    }

    protected override void OnNavigatedFrom(NavigatedFromEventArgs args)
    {
        _viewModel.OnNavigatedFrom();
        base.OnNavigatedFrom(args);
    }

    public bool HasNavBar
    {
        get => (bool)GetValue(HasNavBarProperty);
        set => SetValue(HasNavBarProperty, value);
    }

    private static void OnHasNavBarChanged(BindableObject bindable, object value, object newValue)
    {
        Shell.SetNavBarIsVisible(bindable, (bool)newValue);
    }
}