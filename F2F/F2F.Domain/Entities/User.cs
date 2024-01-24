using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

namespace F2F.DLL.Entities;

public class User : IdentityUser<Guid>
{
    public DateTime LastModifiedDate { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public ICollection<Meeting> MyMeetings { get; set; }
    public ICollection<Questionnaire> Questionnaires { get; set; }
}
