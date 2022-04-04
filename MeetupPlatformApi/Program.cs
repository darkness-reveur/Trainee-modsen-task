using MeetupPlatformApi.Authentication;
using MeetupPlatformApi.Context;
using MeetupPlatformApi.Configuration;
using MeetupPlatformApi.Configuration.SwaggerDocConfiguration;

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
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
