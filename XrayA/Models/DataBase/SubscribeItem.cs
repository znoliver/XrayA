using System;
using Avalonia.Controls;
using SQLite;

namespace XrayA.Models.DataBase;

public class SubscribeItem
{
    public Guid SubscribeId { get; set; } = Guid.NewGuid();

    [PrimaryKey]
    public Guid Id { get; set; } = Guid.NewGuid();
    
    /// <summary>
    /// 协议类型
    /// </summary>
    public ProtocolType ProtocolType { get; set; }

    /// <summary>
    /// 远程地址
    /// </summary>
    public string Address { get; set; }

    /// <summary>
    /// 远程端口
    /// </summary>
    public int Port { get; set; }

    /// <summary>
    /// 远程唯一Id
    /// </summary>
    public string RemoteId { get; set; }

    /// <summary>
    /// 服务器类型
    /// </summary>
    public NetworkType NetworkType { get; set; }

    /// <summary>
    /// VLESS协议的Flow
    /// </summary>
    public string VlessFlow { get; set; }

    /// <summary>
    /// 本地安全策略
    /// </summary>
    public string Security { get; set; }
    
    /// <summary>
    /// 传输层安全
    /// </summary>
    public string StreamSecurity { get; set; }

    /// <summary>
    /// 是否允许不安全连接（用于客户端）
    /// </summary>
    public bool AllowInsecure { get; set; }
    
    /// <summary>
    /// 备注
    /// </summary>
    public string Remark { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string Sni { get; set; }

    public SubscribeItem()
    {
        this.Address = string.Empty;
        this.RemoteId = string.Empty;
        this.VlessFlow = string.Empty;
        this.Security = string.Empty;
        this.StreamSecurity = string.Empty;
        this.Remark = string.Empty;
        this.Sni = string.Empty;
    }
}