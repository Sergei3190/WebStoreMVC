using System.Reflection;

using Microsoft.Extensions.Logging;

namespace WebStoreMVC.Logging.Log4Net;

public static class Log4NetLoggerFactoryExtensions
{
    private static string CheckPath(this string filePath)
    {
        if (filePath is not { Length: > 0 })
            throw new ArgumentException("Не указан файл конфигурации", nameof(filePath));

        if (Path.IsPathRooted(filePath))
            return filePath;

        var assembly = Assembly.GetEntryAssembly()!;
        var dir = Path.GetDirectoryName(assembly.Location)!;

        return Path.Combine(dir, filePath);
    }

    public static ILoggingBuilder AddLog4Net(this ILoggingBuilder builder, string configurationFile = "log4net.config")
    {
        builder.AddProvider(new Log4NetLoggerProvider(configurationFile.CheckPath()));
        return builder;
    }
}