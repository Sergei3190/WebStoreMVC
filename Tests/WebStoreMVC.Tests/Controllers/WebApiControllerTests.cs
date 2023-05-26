using Microsoft.AspNetCore.Mvc;

using WebStoreMVC.Contollers;
using WebStoreMVC.Interfaces.TestApi;

namespace WebStoreMVC.Tests.Controllers;


public class WebApiControllerTests
{
	[Fact]
	public void Index_Returns_with_View_with_Values()
	{
		var expected_values = Enumerable.Range(1, 10).Select(i => $"Value-{i}");

		var values_service_mock = new Mock<IValueService>();

		values_service_mock.Setup(s => s.GetValues()).Returns(expected_values);

		var controller = new WebApiController(values_service_mock.Object);

		var result = controller.Index();

		var view_result = Assert.IsType<ViewResult>(result);

		var actual_values = Assert.IsAssignableFrom<IEnumerable<string>>(view_result.Model);

		Assert.Equal(expected_values, actual_values);

		values_service_mock.Verify(s => s.GetValues());
		values_service_mock.VerifyNoOtherCalls();
	}
}
