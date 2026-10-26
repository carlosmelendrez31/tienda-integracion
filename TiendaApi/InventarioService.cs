public class InventarioService(TiendaDb db)
{
    public async Task<Producto?> Buscar(int id) => await db.Productos.FindAsync(id);

    public async Task Descontar(Producto p, int cantidad)
    {
        p.Stock -= cantidad;
        await db.SaveChangesAsync();
    }
}
