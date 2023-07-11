using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using XrayA.ViewModels;

namespace XrayA.Views;

public partial class SubscribeView : ReactiveUserControl<SubscribeViewModel>
{
    public SubscribeView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}