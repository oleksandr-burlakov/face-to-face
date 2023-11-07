using System.Security.Claims;
using F2F.BLL.Exceptions;
using Microsoft.AspNetCore.Http;

namespace F2F.BLL.Services.Realization;

public class ClaimService : IClaimService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ClaimService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid GetUserId()
    {
        var isParsed = Guid.TryParse(GetClaim(ClaimTypes.NameIdentifier), out Guid result);
        if (isParsed)
            return result;
        else
            throw new BadFormatException("Id was not guid.");
    }

    public string GetClaim(string key)
    {
        return _httpContextAccessor.HttpContext?.User?.FindFirst(key)?.Value;
    }
}
