using F2F.DLL.Entities.Base;
using F2F.DLL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace F2F.DLL;

public class F2FContext : IdentityDbContext<User, Role, Guid>
{
    public F2FContext(DbContextOptions options)
        : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Meeting> Meetings { get; set; }
    public DbSet<MeetingParticipant> MeetingParticipants { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<Questionnaire> Questionnaires { get; set; }
    public DbSet<Role> Roles { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }

    public new async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            entry.Entity.LastModifiedDate = DateTime.Now;

        return await base.SaveChangesAsync(cancellationToken);
    }
}
