using System;
using SQLite;

namespace XrayA.Models.DataBase;

[Table("subscribe_path")]
public class Subscribe
{
    [PrimaryKey]
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// 订阅分组名称
    /// </summary>
    public string GroupName { get; set; } = string.Empty;
    
    /// <summary>
    /// 订阅路径
    /// </summary>
    public string Path { get; set; } = string.Empty;
}