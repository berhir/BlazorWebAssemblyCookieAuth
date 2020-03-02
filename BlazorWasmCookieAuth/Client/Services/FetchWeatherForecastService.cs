using BlazorWasmCookieAuth.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlazorWasmCookieAuth.Client.Services
{
    public class FetchWeatherForecastService
    {
        private readonly HttpClient _publicApiClient;
        private readonly HttpClient _protectedApiClient;

        public FetchWeatherForecastService(IHttpClientFactory clientFactory, NavigationManager navigationManager)
        {
            _publicApiClient = clientFactory.CreateClient();
            _protectedApiClient = clientFactory.CreateClient("authorizedClient");
            _publicApiClient.BaseAddress = _protectedApiClient.BaseAddress = new Uri(navigationManager.BaseUri);
        }

        public async Task<WeatherForecast[]> GetPublicWeatherForeacast()
        {
            return await _publicApiClient.GetJsonAsync<WeatherForecast[]>("api/WeatherForecast");
        }

        public async Task<WeatherForecast[]> GetProtectedWeatherForeacast()
        {
            return await _protectedApiClient.GetJsonAsync<WeatherForecast[]>("api/WeatherForecast/protected");
        }
    }
}
