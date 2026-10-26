using Microsoft.EntityFrameworkCore;

public class Producto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = "";
    public decimal Precio { get; set; }
    public int Stock { get; set; }
}

public class Pedido
{
    public int Id { get; set; }
    public int ProductoId { get; set; }
    public int Cantidad { get; set; }
    public decimal Total { get; set; }
}

public record NuevoPedido(int ProductoId, int Cantidad);

public class TiendaDb(DbContextOptions<TiendaDb> options) : DbContext(options)
{
    public DbSet<Producto> Productos => Set<Producto>();
    public DbSet<Pedido> Pedidos => Set<Pedido>();
}
