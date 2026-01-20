using LibrosAPI.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// ----------------------------
// Registrar servicios
// ----------------------------
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer(); // Necesario para Swagger
builder.Services.AddSwaggerGen();           // Generar documentación
builder.Services.AddHttpClient();           // Para consumir API externa
builder.Services.AddSingleton<LibroService>(); // Registrar nuestro service

var app = builder.Build();

// ----------------------------
// Middleware para Swagger en desarrollo
// ----------------------------
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Libros API v1");
        c.RoutePrefix = string.Empty; // Swagger en la raíz: https://localhost:5001/
    });
}

// ----------------------------
// Middleware general
// ----------------------------
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
