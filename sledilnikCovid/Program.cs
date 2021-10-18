using sledilnikCovid.Application;
using sledilnikCovid.Application.Contracts;
using sledilnikCovid.Infrastructure.Implementation;
using sledilnikCovid.Infrastructure.Interfaces;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddScoped<IRegionService, RegionService>();
builder.Services.AddSingleton<IFormatFetcher, FormatFetcher>();
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() 
    { 
        Title = "sledilnikCovid.API",
        Version = "v1",
        Description = "API for retrieving Covid related data"
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});


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
