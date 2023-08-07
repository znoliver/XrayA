using System;
using System.Reactive.Disposables;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;
using XrayA.Models;
using XrayA.Models.DataBase;
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
            this.BindCommand(ViewModel, vm => vm.AddSubscribeCommand, v => v.BtnAddSubscribe)
                .DisposeWith(disposable);

            this.BindInteraction(ViewModel, vm => vm.AddSubscribeInteraction, AddSubscribeAsync)
                .DisposeWith(disposable);
        });
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private async Task AddSubscribeAsync(InteractionContext<AddSubscribeWindowViewModel, Subscribe?> interaction)
    {
        var dialog = new AddSubscribeWindowView();
        dialog.DataContext = interaction.Input;

        if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            if (desktop.MainWindow is null) return;
            var result = await dialog.ShowDialog<Subscribe?>(desktop.MainWindow);
            interaction.SetOutput(result);
        }
    }
}