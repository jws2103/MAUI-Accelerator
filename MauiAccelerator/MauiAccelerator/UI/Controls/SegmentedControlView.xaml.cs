using System.Diagnostics.CodeAnalysis;
using System.Windows.Input;
using Plugin.Maui.SegmentedControl;
using ValueChangedEventArgs = Plugin.Maui.SegmentedControl.ValueChangedEventArgs;

namespace MauiAccelerator.UI.Controls;

[ExcludeFromCodeCoverage]
public partial class SegmentedControlView : ContentView
{
    public static readonly BindableProperty TabItemTextsProperty = BindableProperty.Create(
        propertyName: nameof(TabItemTexts),
        returnType: typeof(IList<string>),
        declaringType: typeof(SegmentedControlView),
        defaultValue: default(IList<string>),
        propertyChanged: TabItemTextsChanged);

    public static readonly BindableProperty SelectedTabIndexProperty = BindableProperty.Create(
        propertyName: nameof(SelectedTabIndex),
        returnType: typeof(int),
        declaringType: typeof(SegmentedControlView),
        defaultValue: default(int), 
        BindingMode.TwoWay);
    
    public static readonly BindableProperty SelectedTabChangedCommandProperty = BindableProperty.Create(
        propertyName: nameof(SelectedTabChangedCommand),
        returnType: typeof(ICommand),
        declaringType: typeof(SegmentedControlView),
        defaultValue: default(ICommand));
    
    public static readonly BindableProperty FillColorProperty = BindableProperty.Create(
        propertyName: nameof(FillColor),
        returnType: typeof(Color),
        declaringType: typeof(SegmentedControlView),
        defaultValue: Colors.White);
    
    public static readonly BindableProperty TextColorProperty = BindableProperty.Create(
        propertyName: nameof(TextColor),
        returnType: typeof(Color),
        declaringType: typeof(SegmentedControlView),
        defaultValue: Colors.Grey1);
    
    public static readonly BindableProperty TintColorProperty = BindableProperty.Create(
        propertyName: nameof(TintColor),
        returnType: typeof(Color),
        declaringType: typeof(SegmentedControlView),
        defaultValue: Colors.OpteonBlue);
    
    public static readonly BindableProperty SelectedTextColorProperty = BindableProperty.Create(
        propertyName: nameof(SelectedTextColor),
        returnType: typeof(Color),
        declaringType: typeof(SegmentedControlView),
        defaultValue: Colors.White);
    
    public SegmentedControlView()
    {
        InitializeComponent();
    }

    public IList<SegmentedControlOption>? SegmentedControlOptions { get; set; }

    public IList<string> TabItemTexts
    {
        get => (IList<string>)GetValue(TabItemTextsProperty);
        set => SetValue(TabItemTextsProperty, value);
    }
    
    public int SelectedTabIndex
    {
        get => (int)GetValue(SelectedTabIndexProperty);
        set => SetValue(SelectedTabIndexProperty, value);
    }
    
    public ICommand SelectedTabChangedCommand
    {
        get => (ICommand)GetValue(SelectedTabChangedCommandProperty);
        set => SetValue(SelectedTabChangedCommandProperty, value);
    }
    
    public Color FillColor
    {
        get => (Color)GetValue(FillColorProperty);
        set => SetValue(FillColorProperty, value);
    }
    
    public Color TextColor
    {
        get => (Color)GetValue(TextColorProperty);
        set => SetValue(TextColorProperty, value);
    }
    
    public Color TintColor
    {
        get => (Color)GetValue(TintColorProperty);
        set => SetValue(TintColorProperty, value);
    }
    
    public Color SelectedTextColor
    {
        get => (Color)GetValue(SelectedTextColorProperty);
        set => SetValue(SelectedTextColorProperty, value);
    }
    
    private static void TabItemTextsChanged(BindableObject bindable, object oldvalue, object newvalue)
    {
        var tabItemTexts = newvalue as List<string>;
        if (bindable is not SegmentedControlView control || tabItemTexts!.Count == 0)
        {
            return;
        }
        
        var segmentedControlOptions = new List<SegmentedControlOption>();
        foreach (var tabItemText in tabItemTexts)
        {
            segmentedControlOptions.Add(new SegmentedControlOption
            {
                Text = tabItemText
            });
        }

        control.SegmentedControl.Children = new List<SegmentedControlOption>(segmentedControlOptions);
        if (DeviceInfo.Platform == DevicePlatform.iOS)
        {
            return;
        }
        
        var width = (DeviceDisplay.Current.MainDisplayInfo.Width / DeviceDisplay.Current.MainDisplayInfo.Density) - 20;
        control.SegmentedControl.WidthRequest = width;
    } 

    private void SegControl_OnValueChanged(object? sender, ValueChangedEventArgs e)
    {
        var selectedIndex = e.NewValue;
        SelectedTabChangedCommand?.Execute(selectedIndex);
    }
}