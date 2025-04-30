using MsConsumers.Api.Extensions;
using MsConsumers.Infrastructure.Extensions;
using MsConsumers.Application.Commands.Consumer;
using MsConsumers.Api.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerConfiguration();

// Add MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateConsumerCommand).Assembly));

// Add Infrastructure
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "MsConsumers API V1");
        c.RoutePrefix = string.Empty; // Define a rota raiz para o Swagger
        c.DocumentTitle = "MsConsumers API Documentation";
        c.DefaultModelsExpandDepth(-1); // Oculta os schemas por padrão
        c.DisplayRequestDuration();
        c.EnableDeepLinking();
        c.EnableFilter();
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Add global exception handler
app.UseGlobalExceptionHandler();

app.Run();
