using Medevia.API.UI.ExtensionMethods;
using Medevia.Core.Infrastructure.Loggers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddInjections();
builder.Services.AddCustomOptions(builder.Configuration);
builder.Services.AddCustomSecurity(builder.Configuration);
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Logging.AddProvider(new CustomLoggerProvider());


var app = builder.Build();
;
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseCustomLogger();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseCors(SecurityMethods.DEFAULT_POLICY );
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
