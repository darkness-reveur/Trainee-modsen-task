using MeetupPlatformApi.Authentication.DependencyInjection;
using MeetupPlatformApi.Persistence.DependencyInjection;
using MeetupPlatformApi.Seedwork.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFiles(builder.Environment);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddDbContext();
builder.Services.AddJwtAuthentication(builder.Configuration);

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
