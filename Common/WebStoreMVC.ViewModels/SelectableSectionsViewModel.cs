namespace WebStoreMVC.ViewModels;
public class SelectableSectionsViewModel
{
	public IEnumerable<SectionViewModel> Sections { get; set; } = null!;

	public int? SectionId { get; set; }

	public int? ParentSectionId { get; set; }
}
