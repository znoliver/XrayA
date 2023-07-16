using System;
using System.Text;

namespace XrayA.Utils;

public static class Base64
{
    /// <summary>
    /// 判断字符串是否是Base64字符串
    /// </summary>
    public static bool IsBase64(this string plainText) => IsBase64String(plainText);

    /// <summary>
    /// 解析Base64字符串
    /// </summary>
    public static string DecodeBase64(this string plainText) => DecodeBase64String(plainText);

    /// <summary>
    /// 判断字符串是否是Base64字符串
    /// </summary>
    public static bool IsBase64String(string plainText)
    {
        var buffer = new Span<byte>(new byte[plainText.Length]);
        return Convert.TryFromBase64String(plainText, buffer, out int _);
    }

    /// <summary>
    /// 解析Base64字符串
    /// </summary>
    public static string DecodeBase64String(string plainText)
    {
        var resultArray = Convert.FromBase64String(plainText);
        return Encoding.UTF8.GetString(resultArray);
    }
}