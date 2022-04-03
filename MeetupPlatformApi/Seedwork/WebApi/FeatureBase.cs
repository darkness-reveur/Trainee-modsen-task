namespace MeetupPlatformApi.Seedwork.WebApi;

using Microsoft.AspNetCore.Mvc;

[ApiController]
public class FeatureBase : ControllerBase
{
    protected IActionResult Created(object value) =>
        StatusCode(StatusCodes.Status201Created, value);
}
