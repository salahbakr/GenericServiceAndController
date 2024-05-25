
using Generics.ExtensionMethods;
using Microsoft.AspNetCore.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddApplicationServices();
builder.Services.AddControllers()
    .AddMvcOptions(o => o.Conventions.Add(
            new GenericControllerRouteConvention()
        ))
    .ConfigureApplicationPartManager(m =>
            m.FeatureProviders.Add(new GenericTypeControllerFeatureProvider()
        ));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContextConnection(builder);

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
