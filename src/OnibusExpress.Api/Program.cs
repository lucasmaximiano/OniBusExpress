using Microsoft.EntityFrameworkCore;
using OnibusExpress.Api.Middlewares;
using OnibusExpress.Application;
using OnibusExpress.Infrastructure;
using OnibusExpress.Infrastructure.Context;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

await ApplyMigrationsAsync(app);

app.UseValidationExceptionMiddleware();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.Run();

static async Task ApplyMigrationsAsync(WebApplication app)
{
    using IServiceScope scope = app.Services.CreateScope();

    OnibusExpressContext dbContext = scope.ServiceProvider
        .GetRequiredService<OnibusExpressContext>();

    await dbContext.Database.MigrateAsync();
}