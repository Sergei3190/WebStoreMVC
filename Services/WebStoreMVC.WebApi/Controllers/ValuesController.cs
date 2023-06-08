using Microsoft.AspNetCore.Mvc;

namespace WebStoreMVC.WebApi.Controllers;

[ApiController]
[Route("api/values")]
public class ValuesController : ControllerBase
{
    private static int _lastValue = 10;
    private static int _lastFreeId = ++_lastValue;

    private static readonly Dictionary<int, string> _value = Enumerable.Range(1,_lastValue)
        .Select(i => (key: i, value: $"value {i}"))
        .ToDictionary(i => i.key, i => i.value);


    private readonly ILogger<ValuesController> _logger;

    public ValuesController(ILogger<ValuesController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        if (!_value.Any())
            return NoContent();

        return Ok(_value.Values);
    }

    [HttpGet("{id:int}")]
    public IActionResult Get(int id)
    {
        if (_value.TryGetValue(id, out var value))
            return Ok(value);

        return NotFound(new { id });
    }

    [HttpPost]
    public IActionResult Add(string value)
    {
        var id = _lastFreeId;

        _value[id] = value;

        _logger.LogInformation("Успешно добавлено значение {0} с id = {1}", value, id);
        ++_lastFreeId;

        return CreatedAtAction(nameof(Get), new { id }, value);
    }

    [HttpPut("{id:int}")]
    public IActionResult Edit(int id, [FromBody] string value)
    {
        if (!_value.ContainsKey(id))
        {
            _logger.LogInformation("При попытке редактирования объект с id = {0} не найден", id);
            return NotFound(new { id });
        }

        var oldValue = _value[id];

        _value[id] = value;

        _logger.LogInformation("Объект с id = {0} успешно отредактирован, старое значение = {1}, новое значение = {2}", id, oldValue, value);

        return Ok(new { id, oldValue, value });
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        if (!_value.ContainsKey(id))
        {
            _logger.LogInformation("При попытке удаления объект с id = {0} не найден", id);
            return NotFound(new { id });
        }

        var value = _value[id];

        _value.Remove(id);

        _logger.LogInformation("Объект с id = {0} и значением {1} успешно удален", id, value);

        return Ok(new { id, value });
    }
}
