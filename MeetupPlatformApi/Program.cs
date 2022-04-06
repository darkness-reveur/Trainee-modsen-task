using MeetupPlatformApi.Authentication.DependencyInjection;
using MeetupPlatformApi.Persistence.DependencyInjection;
using MeetupPlatformApi.Persistence.SwaggerDocConfiguration;
using MeetupPlatformApi.Seedwork.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFiles(builder.Environment);

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
        options.DefaultModelsExpandDepth(-1);
    });
}

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
