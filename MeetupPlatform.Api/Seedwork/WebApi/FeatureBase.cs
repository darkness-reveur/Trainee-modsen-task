namespace MeetupPlatform.Api.Seedwork.WebApi;

using System.Net.Mime;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Consumes(MediaTypeNames.Application.Json)]
[Produces(MediaTypeNames.Application.Json)]
public abstract class FeatureBase : ControllerBase
{
    protected CurrentUserInfo CurrentUser => lazyCurrentUserInfo.Value;
    private readonly Lazy<CurrentUserInfo> lazyCurrentUserInfo;

    protected FeatureBase() =>
        lazyCurrentUserInfo = new Lazy<CurrentUserInfo>(GetCurrentUserInfo);
    
    protected IActionResult Created(object value) =>
        StatusCode(StatusCodes.Status201Created, value);
    
    private CurrentUserInfo GetCurrentUserInfo()
    {
        var idClaim = User.Claims.Single(claim => claim.Type == ClaimTypes.NameIdentifier);
        var id = Guid.Parse(idClaim.Value);

        return new CurrentUserInfo {UserId = id};
    }
}
