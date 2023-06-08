using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace WebStoreMVC.TagHelpers;

[HtmlTargetElement(Attributes = AttributeName)]
public class ActiveRoute : TagHelper
{
	private const string AttributeName = "ws-is-active-route";
	private const string IgnoreAction = "ws-ignore-action";
	private const string IgnoreSecondaryRoute = "ws-ignore-secondary-route";

	private readonly string[] _secondaryRoutes = new[]
	{
		"Cart"
	};

	[HtmlAttributeName("asp-controller")]
	public string Controller { get; set; } = null!;

	[HtmlAttributeName("asp-action")]
	public string Action { get; set; } = null!;

	[HtmlAttributeName("asp-all-route-data", DictionaryAttributePrefix = "asp-route-")]
	public Dictionary<string, string> RouteValues { get; set; } = new(StringComparer.OrdinalIgnoreCase);

	[ViewContext, HtmlAttributeNotBound]
	public ViewContext ViewContext { get; set; } = null!;

	public override void Process(TagHelperContext context, TagHelperOutput output)
	{
		output.Attributes.RemoveAll(AttributeName);

		var isIgnoreAction = output.Attributes.RemoveAll(IgnoreAction);

		var isIgnoreSecondaryRoute = output.Attributes.RemoveAll(IgnoreSecondaryRoute);

		if (IsActive(isIgnoreAction, isIgnoreSecondaryRoute))
			MakeActive(output);
	}

	private bool IsActive(bool isIgnoreAction, bool isIgnoreSecondaryRoute)
	{
		var routeValues = ViewContext.RouteData.Values;

		var routeController = routeValues["controller"]?.ToString();
		var routeAction = routeValues["action"]?.ToString();

		if (!isIgnoreAction && Action is { Length: > 0 } action && !string.Equals(action, routeAction))
			return false;

		if (Controller is { Length: > 0 } controller && !string.Equals(controller, routeController))
		{
			if (isIgnoreSecondaryRoute && _secondaryRoutes.Contains(routeController))
				return true;
			else
				return false;
		}

		foreach (var (key, value) in RouteValues)
			if (!routeValues.ContainsKey(key) || routeValues[key]?.ToString() != value.ToString())
				return false;

		return true;
	}

	private static void MakeActive(TagHelperOutput output)
	{
		var classAttribute = output.Attributes.FirstOrDefault(attr => attr.Name == "class");

		if (classAttribute is null)
			output.Attributes.Add("class", "active");
		else
		{
			if (classAttribute.Value?.ToString()?.Contains("active") == true)
				return;

			output.Attributes.SetAttribute("class", $"{classAttribute.Value} active");
		}
	}
}