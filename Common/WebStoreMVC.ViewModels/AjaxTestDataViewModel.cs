namespace WebStoreMVC.ViewModels;

public class AjaxTestDataViewModel
{
    public int Id { get; init; }

    public string? Message { get; init; }

    public DateTime ServerTime { get; init; } = DateTime.Now;
}
