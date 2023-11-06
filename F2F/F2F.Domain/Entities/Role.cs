using Microsoft.AspNetCore.Identity;

namespace F2F.Domain.Entities;

public class Role : IdentityRole<Guid>
{
    public DateTime LastModifiedDate { get; set; }
}
