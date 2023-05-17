namespace WebStoreMVC.Dto.Base.Interfaces;

public interface INamedDto : IBaseDto
{
    string Name { get; set; }
}