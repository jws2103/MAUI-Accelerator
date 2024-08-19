using System.Diagnostics.CodeAnalysis;
using MauiAccelerator.UI.Controls;
using MauiAccelerator.UI.Font;

namespace MauiAccelerator.UI;

[ExcludeFromCodeCoverage]
public static class Icons
{
    public static readonly ExtendedFontImageSource BackGrayIcon = new ExtendedFontImageSource
    {
        Glyph = FAIconName.ArrowLeft,
        FontFamily = FontFamily.FASolid,
        Color = Colors.Grey3,
        iOSFontSize = 14,
        AndroidFontSize = 14
    };
    
    public static readonly ExtendedFontImageSource EyeIcon = new ExtendedFontImageSource
    {
        Glyph = FAIconName.Eye,
        FontFamily = FontFamily.FARegular,
        Color = Colors.Grey3,
        iOSFontSize = 14,
        AndroidFontSize = 14
    };
    
    public static readonly ExtendedFontImageSource EyeSlashIcon = new ExtendedFontImageSource
    {
        Glyph = FAIconName.EyeSlash,
        FontFamily = FontFamily.FARegular,
        Color = Colors.Grey3,
        iOSFontSize = 14,
        AndroidFontSize = 14
    };
}