using MSConsumers.Api.Extensions;
using MSConsumers.Infrastructure.Extensions;
using MSConsumers.Application.Commands.Consumer;
using MSConsumers.Api.Configurations;
using MSConsumers.Infrastructure.Data;
using HealthChecks.UI.Client;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerConfiguration();

// Add Health Checks
builder.Services.AddHealthChecks()
    .AddCheck("self", () => HealthCheckResult.Healthy());

// Add Health Checks UI
builder.Services.AddHealthChecksUI(setup =>
{
    setup.SetEvaluationTimeInSeconds(5);
    setup.MaximumHistoryEntriesPerEndpoint(10);
    setup.SetApiMaxActiveRequests(1);
    setup.AddHealthCheckEndpoint("API", "/health");
})
.AddInMemoryStorage();

// Add MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateConsumerCommand).Assembly));

// Add Infrastructure
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "MSConsumers API V1");
        c.RoutePrefix = string.Empty; // Define a rota raiz para o Swagger
        c.DocumentTitle = "MSConsumers API Documentation";
        c.DefaultModelsExpandDepth(-1); // Oculta os schemas por padrão
        c.DisplayRequestDuration();
        c.EnableDeepLinking();
        c.EnableFilter();
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Add Health Check endpoints
app.MapHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.MapHealthChecks("/health-json", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

// Add Health Checks UI
app.UseHealthChecksUI(options =>
{
    options.UIPath = "/health-ui";
    options.ApiPath = "/health-ui-api";
});

// Add global exception handler
app.UseGlobalExceptionHandler();

app.Run();
