using System.Reactive.Disposables;
using Avalonia.ReactiveUI;
using ReactiveUI;
using XrayA.ViewModels;

namespace XrayA.Views;

public partial class MainView : ReactiveUserControl<MainViewModel>
{
    public MainView()
    {
        InitializeComponent();

        this.WhenActivated(disposable =>
        {
            this.OneWayBind(ViewModel, vm => vm.SelectedModuleIndex, v => v.lbModuleList.SelectedIndex)
                .DisposeWith(disposable);
            
        });
    }
}