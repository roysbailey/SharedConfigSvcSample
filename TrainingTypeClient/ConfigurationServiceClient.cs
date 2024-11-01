namespace TrainingTypeClient;

using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using TrainingTypeClient.Models;

public class ConfigurationServiceClient
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IMemoryCache _cache;

    public ConfigurationServiceClient()
    {
        _httpClientFactory = new DefaultHttpClientFactory();
        _cache = new MemoryCache(new MemoryCacheOptions());
    }

    public ConfigurationServiceClient(IHttpClientFactory httpClientFactory, IMemoryCache cache)
    {
        _httpClientFactory = httpClientFactory;
        _cache = cache;
    }

    public async Task<T?> GetConfigurationAsync<T>(Domains domain, string trainingType)
    {
        var cacheKey = $"{domain}:{trainingType}";

        if (_cache.TryGetValue(cacheKey, out T? config))
        {
            return config;
        }

        var client = _httpClientFactory.CreateClient("ConfigurationService");
        var url = $"/trainingtypeconfig/{domain.ToString()}/{trainingType}";
        var response = await client.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            config = JsonConvert.DeserializeObject<T>(json);;

            _cache.Set(cacheKey, config, TimeSpan.FromMinutes(30));
            return config;
        }

        throw new Exception($"Could not retrieve configuration for domain: {domain}, training type: {trainingType}");
    }
}

public class DefaultHttpClientFactory : IHttpClientFactory
{
    public HttpClient CreateClient(string name)
    {
        return new HttpClient
        {
            BaseAddress = new Uri("http://localhost:5291") // Default base address for testing
        };
    }
}

