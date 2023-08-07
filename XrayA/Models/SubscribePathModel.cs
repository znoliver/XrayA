using System.Collections.Generic;
using XrayA.Models.DataBase;

namespace XrayA.Models;

public class SubscribeResultModel
{
    public string SubscribeGroup { get; set; } = string.Empty;

    public string Path { get; set; } = string.Empty;

    public List<SubscribeItem> SubscribeNodeInfos { get; set; } = new();
}