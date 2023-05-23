using System.Collections.Concurrent;
using System.Xml;

using Microsoft.Extensions.Logging;

namespace WebStoreMVC.Logging.Log4Net;

public class Log4NetLoggerProvider : ILoggerProvider
{
    private readonly string _configurationFile;
    private readonly ConcurrentDictionary<string, Log4NetLogger> _loggers = new();

    public Log4NetLoggerProvider(string сonfigurationFile) => _configurationFile = сonfigurationFile;

    public ILogger CreateLogger(string categoryName) => _loggers.GetOrAdd(
        categoryName,
        (categoryName, config) =>
        {
            var xml = new XmlDocument();
            xml.Load(config);
            return new Log4NetLogger(categoryName, xml["log4net"]
                ?? throw new InvalidOperationException("Не удалось извлечь из xml-документа элемент log4net"));
        },
        _configurationFile);

    public void Dispose() => _loggers.Clear();
}