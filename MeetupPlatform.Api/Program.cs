using MeetupPlatform.Api.Authentication.DependencyInjection;
using MeetupPlatform.Api.Persistence.DependencyInjection;
using MeetupPlatform.Api.Swagger;
using MeetupPlatform.Api.Seedwork.Configuration;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.OverrideConfigurationSources();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddDbContext();
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddSwashbuckleSwagger(builder.Configuration);

var app = builder.Build();

app.UseSwashbuckleSwagger();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
