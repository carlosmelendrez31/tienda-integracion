public class InventarioService(TiendaDb db)
{
    public async Task<Producto?> Buscar(int id) => await db.Productos.FindAsync(id);

    // PENDIENTE: lo implementa feature/inventario
    public Task Descontar(Producto p, int cantidad) => Task.CompletedTask;
}
