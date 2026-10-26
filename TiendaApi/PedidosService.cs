public class PedidosService(TiendaDb db, InventarioService inventario)
{
    // PENDIENTE: lo implementa feature/pedidos
    public Task<(int Status, object Body)> Crear(NuevoPedido req) =>
        Task.FromResult<(int Status, object Body)>((501, new { error = "Pendiente" }));
}
