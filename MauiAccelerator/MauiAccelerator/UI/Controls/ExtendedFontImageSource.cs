using System.Diagnostics.CodeAnalysis;

namespace MauiAccelerator.UI.Controls;

[ExcludeFromCodeCoverage]
public class ExtendedFontImageSource : FontImageSource
{
    public static readonly BindableProperty iOSFontSizeProperty = BindableProperty.Create(
        propertyName: nameof(iOSFontSize),
        returnType: typeof(double),
        declaringType: typeof(ExtendedFontImageSource),
        defaultValue: default(double),
        propertyChanged: (control, oldValue, newValue) =>
        {
            var runTimePlatform = DeviceInfo.Current.Platform;
            var fontSize = (double)newValue;
            if (control is ExtendedFontImageSource fontImagesSouce && runTimePlatform == DevicePlatform.iOS && fontSize > 0)
            {
                fontImagesSouce.Size = fontSize;
            }
        });

    public static readonly BindableProperty AndroidFontSizeProperty = BindableProperty.Create(
        propertyName: nameof(AndroidFontSize),
        returnType: typeof(double),
        declaringType: typeof(ExtendedFontImageSource),
        defaultValue: default(double),
        propertyChanged: (control, oldValue, newValue) =>
        {
            var runTimePlatform = DeviceInfo.Current.Platform;
            var fontSize = (double)newValue;
            if (control is ExtendedFontImageSource fontImagesSouce && runTimePlatform == DevicePlatform.Android && fontSize > 0)
            {
                fontImagesSouce.Size = (double)newValue;
            }
        });

    public double iOSFontSize
    {
        get => (double)GetValue(iOSFontSizeProperty);
        set => SetValue(iOSFontSizeProperty, value);
    }

    public double AndroidFontSize
    {
        get => (double)GetValue(AndroidFontSizeProperty);
        set => SetValue(AndroidFontSizeProperty, value);
    }
}