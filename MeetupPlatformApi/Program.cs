using MeetupPlatformApi.Authentication.DependencyInjection;
using MeetupPlatformApi.Persistence.DependencyInjection;
using MeetupPlatformApi.Swagger;
using MeetupPlatformApi.Seedwork.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFiles(builder.Environment);

builder.Services.AddControllers();
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
