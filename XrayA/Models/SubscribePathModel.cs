using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace XrayA.Models;

public class SubscribePathModel: ReactiveObject
{
    [Reactive]
    public string Path { get; set; } = string.Empty;
}