using CarvedRock_Shared.Data;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using System.Net.Http.Json;

namespace CarvedRock_BlazorWebAssembly.ApiServices;
public class ProductApiService : IProductApiService
{
    private readonly HttpClient httpClient;
    private HttpRequestMessage HttpRequestMessage { get; set; }

    public ProductApiService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
        HttpRequestMessage = new HttpRequestMessage() { RequestUri = new Uri("https://broodjes.sintmaartencampus.be:7273/product") };
    }
    public async Task<IEnumerable<Product>> GetAll()
    {
        HttpRequestMessage.Method = HttpMethod.Get;
        WebAssemblyHttpRequestMessageExtensions.SetBrowserRequestMode(HttpRequestMessage, BrowserRequestMode.NoCors);

        var response = await httpClient.SendAsync(HttpRequestMessage);
        // response.EnsureSuccessStatusCode();
        var products = await
            response.Content.ReadFromJsonAsync<IEnumerable<Product>>();

        if (products == null)
            return Enumerable.Empty<Product>();
        return products;

    }

    public async Task<Product?> Add(Product product)
    {
        HttpRequestMessage.Method = HttpMethod.Post;
        HttpRequestMessage.Content = JsonContent.Create(product);
        WebAssemblyHttpRequestMessageExtensions.SetBrowserRequestMode(HttpRequestMessage, BrowserRequestMode.NoCors);

        var response = await httpClient.SendAsync(HttpRequestMessage);
        // response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Product>();
    }
}
