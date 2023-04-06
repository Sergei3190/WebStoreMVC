namespace WebStoreMVC.ViewModels;

public class SectionViewModel : BaseViewModel
{
    public SectionViewModel()
    {
        ChildSections = new List<SectionViewModel>();
    }

    public IEnumerable<SectionViewModel> ChildSections { get; set; }
}