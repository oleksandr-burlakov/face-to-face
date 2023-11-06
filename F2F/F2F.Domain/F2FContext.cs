using F2F.DLL.Entities.Base;
using F2F.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace F2F.DLL;

public class F2FContext : IdentityDbContext<User, Role, Guid>
{
    public F2FContext(DbContextOptions options)
        : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Assessment> Assessments { get; set; }
    public DbSet<AssessmentApply> AssessmentApplies { get; set; }
    public DbSet<Meeting> Meetings { get; set; }
    public DbSet<MeetingMessage> MeetingMessages { get; set; }
    public DbSet<MeetingParticipant> MeetingParticipants { get; set; }
    public DbSet<MeetingQuestionPoint> MeetingQuestionPoints { get; set; }
    public DbSet<OneWay> OneWays { get; set; }
    public DbSet<OneWayAnswer> OneWayAnswers { get; set; }
    public DbSet<OneWayAttempt> OneWayAttempts { get; set; }
    public DbSet<OneWayQuestion> OneWayQuestions { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<Questionnaire> Questionnaires { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<SuspendedBehaviour> SuspendedBehaviours { get; set; }
    public DbSet<Test> Tests { get; set; }
    public DbSet<TestAnswer> TestAnswers { get; set; }
    public DbSet<TestAttempt> TestAttempts { get; set; }
    public DbSet<TestQuestion> TestQuestions { get; set; }
    public DbSet<TestSection> TestSections { get; set; }
    public DbSet<TestUserAnswer> TestUserAnswers { get; set; }

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
