using Application;
using Infrastructure;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

#region Config

builder.Services.AddControllers();

builder.Services.AddHttpContextAccessor();

builder.Services.AddPersistence();
builder.Services.AddApplication();

#endregion

#region Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "My API",
        Version = "v1"
    });
});
#endregion

var app = builder.Build();

#region Swagger

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "IntenetShop_auth v1");
    });

}
#endregion

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
