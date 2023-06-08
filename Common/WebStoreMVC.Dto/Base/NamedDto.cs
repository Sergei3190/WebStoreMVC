namespace WebStoreMVC.Dto.Base;

public abstract class NamedDto : BaseDto
{
    public NamedDto()
    {
        Name = null!;
    }

    public string Name { get; set; }
}