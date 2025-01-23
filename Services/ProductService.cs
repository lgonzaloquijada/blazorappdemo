using System.Net.Http.Json;
using System.Text.Json;
using blazorappdemo.Models;

namespace blazorappdemo.Services;

public class ProductService
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _options;

    public ProductService(HttpClient httpClient, JsonSerializerOptions options)
    {
        _httpClient = httpClient;
        _options = options;
    }

    public async Task<IEnumerable<Product>?> GetProducts()
    {
        var response = await _httpClient.GetAsync("products");
        return await JsonSerializer.DeserializeAsync<IEnumerable<Product>>(await response.Content.ReadAsStreamAsync());
    }

    public async Task<Product?> GetProduct(int id)
    {
        var response = await _httpClient.GetAsync($"products/{id}");
        return await JsonSerializer.DeserializeAsync<Product>(await response.Content.ReadAsStreamAsync());
    }

    public async Task AddProduct(Product product)
    {
        var response = await _httpClient.PostAsync("products", JsonContent.Create(product));
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content);
        }
    }

    public async Task UpdateProduct(Product product)
    {
        var response = await _httpClient.PutAsync($"products/{product.Id}", JsonContent.Create(product));
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content);
        }
    }

    public async Task DeleteProduct(int id)
    {
        var response = await _httpClient.DeleteAsync($"products/{id}");
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content);
        }
    }
}