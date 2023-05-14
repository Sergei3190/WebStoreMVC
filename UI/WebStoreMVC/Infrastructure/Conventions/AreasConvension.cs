
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace WebStoreMVC.Infrastructure.Conventions;

public class AreasConvension : IControllerModelConvention
{
    public void Apply(ControllerModel controller)
    {
        var typeNamespace = controller.ControllerType.Namespace;

        if (string.IsNullOrEmpty(typeNamespace)) return;

        const string areasNamespaceSuffix = "Areas.";
        const int areasNamespaceSuffixLength = 6;

        var areasIndex = typeNamespace.IndexOf(areasNamespaceSuffix, StringComparison.OrdinalIgnoreCase);   

        if(areasIndex < 0) return;

        areasIndex += areasNamespaceSuffixLength;

        var areaName = typeNamespace[areasIndex..typeNamespace.IndexOf('.', areasIndex)]; 

        if (string.IsNullOrEmpty(areaName)) return;

        if (controller.Attributes.OfType<AreaAttribute>().Any(a => a.RouteKey == "area" && a.RouteValue == areaName)) return;

        controller.RouteValues["area"] = areaName;
    }
}
