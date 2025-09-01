using System.Net.Http.Json;

namespace ArasDoner.Shared;

public class LocalizationService
{
    private Dictionary<string, string>? _translations;
    private readonly HttpClient _httpClient;
    private const string DefaultLanguage = "de";

    public LocalizationService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task InitializeAsync()
    {
        try
        {
            _translations = await _httpClient.GetFromJsonAsync<Dictionary<string, string>>($"Localization/{DefaultLanguage}.json");
            _translations ??= new Dictionary<string, string>();
        }
        catch
        {
            _translations = new Dictionary<string, string>();
        }
    }

    public string GetString(string key)
    {
        if (_translations == null || !_translations.TryGetValue(key, out var value))
        {
            return key; // Return key as fallback
        }
        return value;
    }
}