using MeetupPlatform.Api.Authentication.DependencyInjection;
using MeetupPlatform.Api.Persistence.DependencyInjection;
using MeetupPlatform.Api.Swagger;
using MeetupPlatform.Api.Seedwork.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.OverrideConfigurationSources();

builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddDbContext();
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddSwaggerSwashbuckleConfiguration();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        //Hides Schemas section from Swagger UI.
        options.DefaultModelsExpandDepth(-1);
    });
}

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
