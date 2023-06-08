using Microsoft.AspNetCore.Mvc;

using WebStoreMVC.ViewModels;

namespace WebStoreMVC.Controllers;

public class AjaxTestController : Controller
{
    private readonly ILogger<AjaxTestController> _logger;

    public AjaxTestController(ILogger<AjaxTestController> Logger) => _logger = Logger;

    public IActionResult Index() => View();

    public async Task<IActionResult> GetJSON(int? id, string? msg, int delay = 2000)
    {
        _logger.LogInformation("Получен запрос к GetJSON - id:{0}, msg:{1}, delay:{2}", id, msg, delay);

        await Task.Delay(delay);

        _logger.LogInformation("Ответ на запрос к GetJSON - id:{0}, msg:{1}, delay:{2}", id, msg, delay);

        return Json(new
        {
            Message = $"Response (id:{id ?? -1}: {msg ?? "--null--"})",
            ServerTime = DateTime.Now,
        });
    }

    public async Task<IActionResult> GetHTML(int? id, string? msg, int delay = 2000)
    {
        _logger.LogInformation("Получен запрос к GetHTML - id:{0}, msg:{1}, delay:{2}", id, msg, delay);

        await Task.Delay(delay);

        _logger.LogInformation("Ответ на запрос к GetHTML - id:{0}, msg:{1}, delay:{2}", id, msg, delay);

        return PartialView("Partial/_DataView", new AjaxTestDataViewModel
        {
            Id = id ?? -1,
            Message = msg,
        });
    }

    public IActionResult Chat() => View();
}
