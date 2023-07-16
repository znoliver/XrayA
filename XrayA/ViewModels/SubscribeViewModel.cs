using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Reactive;
using System.Threading.Tasks;
using System.Web;
using DynamicData;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using XrayA.Models;
using XrayA.Utils;

namespace XrayA.ViewModels;

public class SubscribeViewModel : ViewModelBase, IRoutableViewModel
{
    public string UrlPathSegment => nameof(SubscribeViewModel);

    public IScreen HostScreen { get; }

    [Reactive] public SubscribePathModel SubscribePathModel { get; set; }

    private ReadOnlyObservableCollection<NodeInfo> _nodeInfos;

    public ReadOnlyObservableCollection<NodeInfo> NodeInfoList => _nodeInfos;

    public SourceList<NodeInfo> SourceNodeList { get; set; }

    /// <summary>
    /// 添加订阅命令
    /// </summary>
    public ReactiveCommand<string, Unit> AddSubscribeCommand;

    public SubscribeViewModel(IScreen? hostScreen = null)
    {
        this.HostScreen = hostScreen ?? default!;

        this.AddSubscribeCommand = ReactiveCommand.CreateFromTask<string>(AddSubscribe);
        this.SubscribePathModel = new SubscribePathModel();

        SourceNodeList = new SourceList<NodeInfo>();
        SourceNodeList.Connect().Bind(out _nodeInfos).Subscribe();
    }

    public Task AddSubscribe(string subscribePath) => GetNodeBySubscribePath(subscribePath);

    private async Task GetNodeBySubscribePath(string subscribePath)
    {
        var httpClient = new HttpClient();
        var resultString = await httpClient.GetStringAsync(subscribePath);

        if (resultString.IsBase64())
        {
            resultString = resultString.DecodeBase64();
        }

        var subscribeNodeLists = resultString.Split(Environment.NewLine)
            .Where(s => !string.IsNullOrWhiteSpace(s))
            .Select(ParseSubscribePath)
            .ToList();

        SourceNodeList.AddRange(subscribeNodeLists);
    }

    private NodeInfo ParseSubscribePath(string subscribePath)
    {
        var protocolType = subscribePath.GetProtocolType();
        var url = new Uri(subscribePath);

        var node = new NodeInfo
        {
            ProtocolType = protocolType,
            Address = url.IdnHost,
            Port = url.Port,
            RemoteId = url.UserInfo,
            Remark = HttpUtility.UrlDecode(url.GetComponents(UriComponents.Fragment, UriFormat.UriEscaped))
        };

        var queryParams = HttpUtility.ParseQueryString(url.Query);
        ResolveStdTransport(queryParams, ref node);
        return node;
    }

    private void ResolveStdTransport(NameValueCollection query, ref NodeInfo item)
    {
        item.VlessFlow = query["flow"] ?? "";
        item.StreamSecurity = query["security"] ?? "";
        item.Sni = query["sni"] ?? "";
        // item.alpn = Utils.UrlDecode(query["alpn"] ?? "");
        // item.fingerprint = Utils.UrlDecode(query["fp"] ?? "");
        // item.publicKey = Utils.UrlDecode(query["pbk"] ?? "");
        // item.shortId = Utils.UrlDecode(query["sid"] ?? "");
        // item.spiderX = Utils.UrlDecode(query["spx"] ?? "");

        var hasType = Enum.TryParse(query["type"], true, out NetworkType networkType);
        item.NetworkType = hasType ? networkType : NetworkType.Tcp;
    }
}