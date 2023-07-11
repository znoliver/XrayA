using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Console = System.Console;

namespace XrayA.ViewModels;

public class SubscribeViewModel : ViewModelBase, IRoutableViewModel
{
    public string UrlPathSegment => nameof(SubscribeViewModel);

    public IScreen HostScreen { get; }

    public SubscribeViewModel(IScreen? hostScreen = null)
    {
        this.HostScreen = hostScreen ?? default!;
    }
}