using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<TiendaDb>(o => o.UseInMemoryDatabase("tienda"));
builder.Services.AddScoped<InventarioService>();
builder.Services.AddScoped<PedidosService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<TiendaDb>();
    db.Productos.AddRange(
        new Producto { Id = 1, Nombre = "Perfume", Precio = 899.50m, Stock = 10 },
        new Producto { Id = 2, Nombre = "Gorra", Precio = 250m, Stock = 5 },
        new Producto { Id = 3, Nombre = "Playera", Precio = 300m, Stock = 2 }
    );
    db.SaveChanges();
}

app.MapGet("/api/productos/{id:int}", async (int id, TiendaDb db) =>
    await db.Productos.FindAsync(id) is Producto p ? Results.Ok(p) : Results.NotFound());

app.MapPost("/api/pedidos", async (NuevoPedido req, PedidosService svc) =>
{
    var (status, body) = await svc.Crear(req);
    return Results.Json(body, statusCode: status);
});

app.Run();

public partial class Program { }
