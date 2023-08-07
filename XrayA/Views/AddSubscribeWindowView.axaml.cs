using System;
using System.Reactive.Disposables;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;
using XrayA.ViewModels;

namespace XrayA.Views;

public partial class AddSubscribeWindowView : ReactiveWindow<AddSubscribeWindowViewModel>
{
    public Button BtnParse => this.FindControl<Button>("btnParse") ?? new Button();
    public Button BtnSave => this.FindControl<Button>("btnSave") ?? new Button();
    
    public AddSubscribeWindowView()
    {
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif
        
        this.WhenActivated(disposable =>
        {
            this.BindCommand(ViewModel, vm => vm.ParseSubscribeCommand, v => v.BtnParse)
                .DisposeWith(disposable);
            this.BindCommand(ViewModel, vm => vm.SaveCommand, v => v.BtnSave)
                .DisposeWith(disposable);
            ViewModel?.SaveCommand.Subscribe(Close).DisposeWith(disposable);
        });
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}