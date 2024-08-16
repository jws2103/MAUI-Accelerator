using System.Diagnostics.CodeAnalysis;
using System.Windows.Input;

namespace MauiAccelerator.UI.Controls;

[ExcludeFromCodeCoverage]
public partial class ButtonImage : Frame
{
    public ButtonImage()
    {
        InitializeComponent();
    }
    
    public static readonly BindableProperty CommandProperty = BindableProperty.Create(
        nameof(Command), 
        typeof(ICommand), 
        typeof(ButtonImage), 
        null);
    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    public static readonly BindableProperty ImageSourceProperty =
        BindableProperty.Create(
            nameof(ImageSource), 
            typeof(ImageSource), 
            typeof(ButtonImage), 
            null);
    public ImageSource ImageSource
    {
        get => (ImageSource)GetValue(ImageSourceProperty);
        set => SetValue(ImageSourceProperty, value);
    }
}