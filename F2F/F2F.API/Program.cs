using F2F.API;
using F2F.API.Middleware;
using FluentValidation;
using FluentValidation.AspNetCore;
using F2F.DLL;
using F2F.BLL;
using F2F.BLL.Models.Validators;
using F2F.API.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(config => config.Filters.Add(typeof(ValidateModelAttribute)));

builder.Services.AddHttpContextAccessor();
builder.Services.AddDataAccess(builder.Configuration).AddApplication();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining(typeof(IValidationMarker));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddJwt(builder.Configuration);

var app = builder.Build();

using var scope = app.Services.CreateScope();

await MigrationManager.MigrateAsync(scope.ServiceProvider);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "N-Tier V1");
    });
}

app.UseHttpsRedirection();

app.UseCors(
    corsPolicyBuilder => corsPolicyBuilder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
);

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseMiddleware<PerformanceMiddleware>();

app.UseMiddleware<TransactionMiddleware>();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();
