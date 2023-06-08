using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

using WebStoreMVC.ViewModels;

namespace WebStoreMVC.TagHelpers;
public class Paging : TagHelper
{
	public PageViewModel PageModel { get; set; } = null!;

	public string PageAction { get; set; } = null!;

	[HtmlAttributeName(DictionaryAttributePrefix = "page-url-")]
	public Dictionary<string, object?> PageUrlValues { get; set; } = new(StringComparer.OrdinalIgnoreCase);

	public override void Process(TagHelperContext context, TagHelperOutput output)
	{
		var ul = new TagBuilder("ul");
		ul.AddCssClass("pagination");

		for (var i = 1; i <= PageModel.TotalPages; i++)
			ul.InnerHtml.AppendHtml(CreateElement(i));

		output.Content.AppendHtml(ul);
	}

	private TagBuilder CreateElement(int pageNumber)
	{
		var li = new TagBuilder("li");
		var a = new TagBuilder("a");

		a.InnerHtml.AppendHtml(pageNumber.ToString());

		if (pageNumber == PageModel.Page)
			li.AddCssClass("active");
		else
			a.Attributes["href"] = "#";

		PageUrlValues["PageNumber"] = pageNumber;

		foreach (var (key, value) in PageUrlValues.Select(v => (v.Key, Value: v.Value?.ToString())).Where(v => v.Value?.Length > 0))
			a.MergeAttribute($"data-{key}", value);

		li.InnerHtml.AppendHtml(a);

		return li;
	}
}
