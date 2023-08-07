using System;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using ReactiveUI;
using XrayA.ViewModels;

namespace XrayA;

public class ViewLocator : IViewLocator
{
    public IViewFor? ResolveView<T>(T? viewModel, string? contract = null)
    {
        if (viewModel is null)
        {
            throw new ArgumentOutOfRangeException(nameof(viewModel));
        }

        var name = viewModel.GetType().FullName!.Replace("ViewModel", "View");
        var type = Type.GetType(name);
        if (type == null) throw new ArgumentOutOfRangeException(nameof(viewModel));


        var view = (UserControl)Activator.CreateInstance(type)!;
        view.DataContext = viewModel;
        return (IViewFor)view;
    }
}