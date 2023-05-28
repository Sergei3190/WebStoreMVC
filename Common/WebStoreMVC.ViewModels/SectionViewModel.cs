using WebStoreMVC.ViewModels.Base;

namespace WebStoreMVC.ViewModels;

public class SectionViewModel : NamedViewModel
{
	public IEnumerable<SectionViewModel> ChildSections { get; set; } = null!;

	public int ProductsCount { get; set; }
}