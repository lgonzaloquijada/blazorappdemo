using System.Net.Http.Json;
using System.Text.Json;
using blazorappdemo.Models;

namespace blazorappdemo.Services;

public class ProductService : IProductService
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _options;

    public ProductService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true, PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
    }

    public async Task<IEnumerable<Product>?> GetProducts()
    {
        var response = await _httpClient.GetAsync("products");
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content);
        }

        var json = JsonSerializer.Deserialize<IEnumerable<Product>>(content, _options);
        json = json?.Select(p =>
        {
            if (p.Images?.Length > 0)
            {
                p.Images = [.. p.Images
                    .Where(img => img != null)
                    .Select(img =>
                    {
                        try
                        {
                            var cleanedImg = img.Trim('[', ']', '\"');
                            return cleanedImg;
                        }
                        catch
                        {
                            return string.Empty;
                        }
                    })];
            }
            return p;
        });
        return json;
    }

    public async Task<Product?> GetProduct(int id)
    {
        var response = await _httpClient.GetAsync($"products/{id}");
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content);
        }

        return JsonSerializer.Deserialize<Product>(content, _options);
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