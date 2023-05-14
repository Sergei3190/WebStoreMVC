using WebStoreMVC.ViewModels.Base;

namespace WebStoreMVC.ViewModels;

public class SectionViewModel : NamedViewModel
{
    public SectionViewModel()
    {
        ChildSections = new List<SectionViewModel>();
    }

    public IEnumerable<SectionViewModel> ChildSections { get; set; }
}