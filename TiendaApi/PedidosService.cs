public class PedidosService(TiendaDb db, InventarioService inventario)
{
    public async Task<(int Status, object Body)> Crear(NuevoPedido req)
    {
        var p = await inventario.Buscar(req.ProductoId);
        if (p is null)
            return (404, new { error = "Producto no existe" });

        if (req.Cantidad <= 0 || p.Stock < req.Cantidad)
            return (400, new { error = "Stock insuficiente" });

        var pedido = new Pedido
        {
            ProductoId = p.Id,
            Cantidad = req.Cantidad,
            Total = p.Precio * req.Cantidad
        };
        db.Pedidos.Add(pedido);

        await inventario.Descontar(p, req.Cantidad);
        return (201, pedido);
    }
}
