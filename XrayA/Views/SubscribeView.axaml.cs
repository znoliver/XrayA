using System.Reactive.Disposables;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using ReactiveUI;
using XrayA.ViewModels;

namespace XrayA.Views;

public partial class SubscribeView : ReactiveUserControl<SubscribeViewModel>
{
    public TextBox TbSubscribePath => this.FindControl<TextBox>("tbSubscribePath") ?? new TextBox();

    public Button BtnAddSubscribe => this.FindControl<Button>("btnAddSubscribe") ?? new Button();


    public SubscribeView()
    {
        this.WhenActivated(disposable =>
        {
            this.BindCommand(ViewModel, vm => vm.AddSubscribeCommand, v => v.BtnAddSubscribe,
                    v => v.SubscribePathModel.Path)
                .DisposeWith(disposable);
            this.Bind(ViewModel, vm => vm.SubscribePathModel.Path, v => v.TbSubscribePath.Text)
                .DisposeWith(disposable);
        });
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}