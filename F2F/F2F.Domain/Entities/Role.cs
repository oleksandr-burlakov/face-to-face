using Microsoft.AspNetCore.Identity;

namespace F2F.DLL.Entities;

public class Role : IdentityRole<Guid>
{
    public DateTime LastModifiedDate { get; set; }
}
