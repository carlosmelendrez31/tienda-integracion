using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

public class PedidosIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public PedidosIntegrationTests(WebApplicationFactory<Program> factory)
        => _client = factory.CreateClient();

    [Fact]
    public async Task CrearPedido_DescuentaStockDelInventario()
    {
        var r = await _client.PostAsJsonAsync("/api/pedidos", new NuevoPedido(1, 3));
        Assert.Equal(HttpStatusCode.Created, r.StatusCode);

        var pedido = await r.Content.ReadFromJsonAsync<Pedido>();
        Assert.Equal(3 * 899.50m, pedido!.Total);

        var producto = await _client.GetFromJsonAsync<Producto>("/api/productos/1");
        Assert.Equal(7, producto!.Stock);
    }

    [Fact]
    public async Task CrearPedido_SinStock_Devuelve400_YNoTocaElInventario()
    {
        var r = await _client.PostAsJsonAsync("/api/pedidos", new NuevoPedido(3, 5));
        Assert.Equal(HttpStatusCode.BadRequest, r.StatusCode);

        var producto = await _client.GetFromJsonAsync<Producto>("/api/productos/3");
        Assert.Equal(2, producto!.Stock);
    }

    [Fact]
    public async Task CrearPedido_ProductoInexistente_Devuelve404()
    {
        var r = await _client.PostAsJsonAsync("/api/pedidos", new NuevoPedido(999, 1));
        Assert.Equal(HttpStatusCode.NotFound, r.StatusCode);
    }
}
