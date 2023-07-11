using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Disposables;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace XrayA.ViewModels;

public class MainViewModel : ViewModelBase
{
    public string Greeting => "Welcome to Avalonia!";

    /// <summary>
    /// 模块名称
    /// </summary>
    [Reactive]
    public List<string> ModuleList { get; set; } =
        new()
        {
            "服务节点",
            "节点管理",
            "设置",
            "关于"
        };
    
    [Reactive] public int SelectedModuleIndex { get; set; }

    public ReactiveCommand<int, Unit> ModuleChangeCommand { get; set; }

    public MainViewModel()
    {
        this.ModuleChangeCommand = ReactiveCommand.Create<int>(SwitchModule);
        
        this.WhenActivated(disposable =>
        {
            this.WhenAnyValue(x => x.SelectedModuleIndex)
                .InvokeCommand(ModuleChangeCommand)
                .DisposeWith(disposable);
        });
    }

    private void SwitchModule(int index)
    {
    }
}