using System;
using XrayA.Models.DataBase;

namespace XrayA.Helpers;

public sealed class LazyConfigHelper
{
    private static readonly Lazy<LazyConfigHelper> instance = new(() => new());

    public static LazyConfigHelper Instance => instance.Value;

    public LazyConfigHelper()
    {
        DbHelper.Instance.CreateTable<Subscribe>();
        DbHelper.Instance.CreateTable<SubscribeItem>();
    }

    public void Init()
    {
        
    }
}