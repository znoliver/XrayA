using XrayA.Models;

namespace XrayA.Utils;

public static class Url
{
    public static ProtocolType GetProtocolType(this string urlString)
    {
        var protocol = ProtocolType.None;
        if (urlString.StartsWith(Global.TrojanProtocol))
        {
            protocol = ProtocolType.Trojan;
        }

        return protocol;
    }
}