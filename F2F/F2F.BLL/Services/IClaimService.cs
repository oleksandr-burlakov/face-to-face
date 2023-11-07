namespace F2F.BLL.Services;

public interface IClaimService
{
    Guid GetUserId();

    string GetClaim(string key);
}
