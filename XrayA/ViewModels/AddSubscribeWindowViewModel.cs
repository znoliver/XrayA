using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Reactive;
using System.Threading.Tasks;
using System.Web;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using XrayA.Helpers;
using XrayA.Models;
using XrayA.Models.DataBase;
using XrayA.Utils;

namespace XrayA.ViewModels;

public class AddSubscribeWindowViewModel : ViewModelBase
{
    [Reactive] public string SubscribePath { get; set; } = "";
    [Reactive] public string SubscribeGroup { get; set; } = "";

    [Reactive] public Subscribe? SubscribeResultModel { get; set; }

    private List<SubscribeItem> _subscribeItems = new();

    private Guid _subscribeId;
    
    public ReactiveCommand<Unit, Unit> ParseSubscribeCommand { get; }
    
    public ReactiveCommand<Unit, Subscribe?> SaveCommand { get; }

    public AddSubscribeWindowViewModel()
    {
        var canParseSubscribe = this.WhenAnyValue(
            x => x.SubscribePath, x => x.SubscribeGroup,
            (path, group) => !string.IsNullOrWhiteSpace(path) && !string.IsNullOrWhiteSpace(group));
        ParseSubscribeCommand = ReactiveCommand.CreateFromTask(GetNodeBySubscribePath, canParseSubscribe);

        var canSave = this.WhenAnyValue(
            x => x.SubscribeResultModel,
            selector: x => SubscribeResultModel != null);
        SaveCommand = ReactiveCommand.CreateFromTask(Save, canSave);

        _subscribeId = Guid.NewGuid();
    }

    private async Task GetNodeBySubscribePath()
    {
        var httpClient = new HttpClient();
        var resultString = await httpClient.GetStringAsync(SubscribePath);

        if (resultString.IsBase64())
        {
            resultString = resultString.DecodeBase64();
        }

        var subscribeNodeLists = resultString.Split(Environment.NewLine)
            .Where(s => !string.IsNullOrWhiteSpace(s))
            .Select(ParseSubscribePath)
            .ToList();


        _subscribeItems.AddRange(subscribeNodeLists);
        SubscribeResultModel = new Subscribe
        {
            GroupName = this.SubscribeGroup,
            Path = this.SubscribePath
        };
    }

    private async Task<Subscribe?> Save()
    {
        await DbHelper.Instance.InsertAsync(new Subscribe
        {
            GroupName = SubscribeGroup,
            Id = _subscribeId,
            Path = SubscribePath
        });

        await DbHelper.Instance.InsertAllAsync(_subscribeItems);
        
        return SubscribeResultModel;
    }

    private SubscribeItem ParseSubscribePath(string subscribePath)
    {
        var protocolType = subscribePath.GetProtocolType();
        var url = new Uri(subscribePath);

        var node = new SubscribeItem
        {
            ProtocolType = protocolType,
            SubscribeId = _subscribeId,
            Address = url.IdnHost,
            Port = url.Port,
            RemoteId = url.UserInfo,
            Remark = HttpUtility.UrlDecode(url.GetComponents(UriComponents.Fragment, UriFormat.UriEscaped))
        };

        var queryParams = HttpUtility.ParseQueryString(url.Query);
        ResolveStdTransport(queryParams, ref node);
        return node;
    }

    private void ResolveStdTransport(NameValueCollection query, ref SubscribeItem item)
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