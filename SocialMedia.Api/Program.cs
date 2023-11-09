using SocialMedia.Application;
using SocialMedia.Infrastructure;
using SocialMedia.Domain;
using SocialMedia.Core;

using SocialMedia.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



//builder.Services.AddCache(builder.Configuration);
//builder.Services.ConfigureAppSettings();
builder.Services.AddInfrastructure();
builder.Services.AddMapperProfiles();
builder.Services.AddApplication();
builder.Services.AddSocialMediaContext();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
