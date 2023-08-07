using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using DynamicData;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using XrayA.Helpers;
using XrayA.Models;
using XrayA.Models.DataBase;

namespace XrayA.ViewModels;

public class SubscribeViewModel : ViewModelBase, IRoutableViewModel
{
    public string UrlPathSegment => nameof(SubscribeViewModel);

    public IScreen HostScreen { get; }

    private ReadOnlyObservableCollection<SubscribeItem> _nodeInfos;
    public ReadOnlyObservableCollection<SubscribeItem> NodeInfoList => _nodeInfos;

    private SourceList<SubscribeItem> SourceNodeList { get; }

    /// <summary>
    /// 添加订阅命令
    /// </summary>
    public ReactiveCommand<Unit, Unit> AddSubscribeCommand;

    public ObservableCollection<Subscribe> Subscribes { get; set; }

    public Interaction<AddSubscribeWindowViewModel, Subscribe?> AddSubscribeInteraction { get; }

    [Reactive] public Guid SelectedId { get; set; }

    public SubscribeViewModel(IScreen? hostScreen = null)
    {
        this.HostScreen = hostScreen ?? default!;

        this.AddSubscribeCommand = ReactiveCommand.CreateFromTask(GetNodeBySubscribePath);

        SourceNodeList = new SourceList<SubscribeItem>();
        SourceNodeList.Connect().Bind(out _nodeInfos).Subscribe();

        Subscribes = new ObservableCollection<Subscribe>(DbHelper.Instance.Table<Subscribe>().ToList());
        SourceNodeList.AddRange(DbHelper.Instance.Table<SubscribeItem>().ToList());
        AddSubscribeInteraction = new Interaction<AddSubscribeWindowViewModel, Subscribe?>();

        this.SelectedId = Subscribes.First().Id;
    }

    private async Task GetNodeBySubscribePath()
    {
        var result = await AddSubscribeInteraction.Handle(new AddSubscribeWindowViewModel());
        if (result is null) return;

        this.Subscribes.Add(result);
    }
}