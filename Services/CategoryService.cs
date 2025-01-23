using System.Net.Http.Json;
using System.Text.Json;
using blazorappdemo.Models;

namespace blazorappdemo.Services;

public class CategoryService : ICategoryService
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _options;

    public CategoryService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    public async Task<IEnumerable<Category>?> GetCategories()
    {
        var response = await _httpClient.GetAsync("categories");
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content);
        }

        return JsonSerializer.Deserialize<IEnumerable<Category>>(content, _options);
    }

    public async Task<Category?> GetCategory(int id)
    {
        var response = await _httpClient.GetAsync($"categories/{id}");
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content);
        }

        return JsonSerializer.Deserialize<Category>(content, _options);
    }

    public async Task AddCategory(Category category)
    {
        var response = await _httpClient.PostAsync("categories", JsonContent.Create(category));
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content);
        }
    }

    public async Task UpdateCategory(Category category)
    {
        var response = await _httpClient.PutAsync($"categories/{category.Id}", JsonContent.Create(category));
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content);
        }
    }

    public async Task DeleteCategory(int id)
    {
        var response = await _httpClient.DeleteAsync($"categories/{id}");
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content);
        }
    }
}