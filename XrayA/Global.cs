using System;
using System.IO;

namespace XrayA;

internal class Global
{
    public const string VmessProtocol = "vmess://";
    public const string VmessProtocolLite = "vmess";
    public const string SsProtocol = "ss://";
    public const string SsProtocolLite = "shadowsocks";
    public const string SocksProtocol = "socks://";
    public const string SocksProtocolLite = "socks";
    public const string HttpProtocol = "http://";
    public const string HttpsProtocol = "https://";
    public const string VlessProtocol = "vless://";
    public const string VlessProtocolLite = "vless";
    public const string TrojanProtocol = "trojan://";
    public const string TrojanProtocolLite = "trojan";

    public const string DataBaseName = "XDB.db";
}