using System;
using System.Collections.Generic;
using System.Reactive;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using XrayA.Helpers;

namespace XrayA.ViewModels;

public class MainViewModel : ViewModelBase, IScreen
{
    /// <summary>
    /// 模块名称
    /// </summary>
    [Reactive]
    public List<string> ModuleList { get; set; }

    [Reactive] public int SelectedModuleIndex { get; set; }

    public RoutingState Router { get; }

    private ReactiveCommand<int, Unit> ModuleChangeCommand { get; }

    public MainViewModel()
    {
        this.ModuleList = new() { "服务节点", "节点管理", "设置", "关于" };
        this.Router = new RoutingState();
        this.ModuleChangeCommand = ReactiveCommand.Create<int>(SwitchModule);
        
        LazyConfigHelper.Instance.Init();

        this.WhenAnyValue(x => x.SelectedModuleIndex)
            .InvokeCommand(ModuleChangeCommand);
    }

    private void SwitchModule(int index)
    {
        switch (index)
        {
            case 1:
                this.Router.Navigate.Execute(new SubscribeViewModel(this));
                break;
            default:
                break;
        }
    }
}