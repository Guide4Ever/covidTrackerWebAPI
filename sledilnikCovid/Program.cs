using sledilnikCovid.Application;
using sledilnikCovid.Application.Contracts;
using sledilnikCovid.Infrastructure.Implementation;
using sledilnikCovid.Infrastructure.Interfaces;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddScoped<IRegionService, RegionService>();
builder.Services.AddSingleton<IFormatFetcher, FormatFetcher>();
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
