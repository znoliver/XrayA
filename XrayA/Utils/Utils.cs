using System;
using System.IO;

namespace XrayA.Utils;

public static class Utils
{
    public static string GetDataFilePath(string fileName = "")
    {
        var tempPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
        if (!Directory.Exists(tempPath))
        {
            Directory.CreateDirectory(tempPath);
        }
        return string.IsNullOrEmpty(fileName) ? tempPath : Path.Combine(tempPath, fileName);
    }
}