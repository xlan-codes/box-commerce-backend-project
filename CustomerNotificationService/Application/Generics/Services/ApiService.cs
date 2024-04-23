using Application.Exceptions;
using Application.Generics.Dtos.Settings;
using Application.Generics.Interfaces;
using Application.Utils;
using System.Net.Mime;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using Application.Generics.Dtos.Auth.Cisl;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Application.Generics.Services
{
    public class ApiService<TSettings> : IApiService<TSettings> where TSettings : BaseApiGatewaySettings
    {
        #region private props
        private readonly IMemoryCache _cache;
        private readonly IHostEnvironment _hostEnv;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly TSettings _apiGatewayURL;
        private readonly ILogger<ApiService<TSettings>> _logger;
        #endregion

        #region Ctor
        public ApiService(IMemoryCache cache, IHostEnvironment hostEnv,
            IHttpClientFactory httpClientFactory, IOptions<TSettings> apiGatewayURL, ILogger<ApiService<TSettings>> logger)
        {
            _cache = cache;
            _hostEnv = hostEnv;
            _httpClientFactory = httpClientFactory;
            _apiGatewayURL = apiGatewayURL.Value;
            _logger = logger;
        }
        #endregion

        #region Authentication
        public async Task<string> GetAuthenticationToken(string username)
        {
            if (!_cache.TryGetValue(username, out string accessToken))
            {
                var client = _httpClientFactory.CreateClient();
                client.Timeout = TimeSpan.FromMinutes(5);

                // request form 
                Dictionary<string, string> requestForm = new();
                requestForm.Add("username", username);
                var req = new HttpRequestMessage(HttpMethod.Post, _apiGatewayURL.URL + "/oauth2/token") { Content = new FormUrlEncodedContent(requestForm) };

                foreach (var header in _apiGatewayURL.Headers)
                {
                    req.Headers.Add(header.Name, header.Value);
                }

                HttpResponseMessage response = await client.SendAsync(req);
                if (response.IsSuccessStatusCode)
                {
                    var text = await response.Content.ReadAsStringAsync();
                    var content = JsonConvert.DeserializeObject<AuthResponse>(text);
                    var options = new MemoryCacheEntryOptions();
                    options.SetAbsoluteExpiration(TimeSpan.FromSeconds(content.ExpiryTime - 10));

                    _cache.Set(username, content.AccessToken, options);
                    return content.AccessToken;
                }
                else
                {
                    throw new Exception(string.Concat("From the source Error Code:", response.StatusCode.ToString()));
                };
            }
            return accessToken;
        }

        #endregion

        #region Methods

        public async Task<TResponse> GetAsync<TResponse>(string path, string username) where TResponse : class
        {
            var client = _httpClientFactory.CreateClient();
            client.Timeout = TimeSpan.FromMinutes(5);
            var req = new HttpRequestMessage(HttpMethod.Get, _apiGatewayURL.URL + path);
            if (_apiGatewayURL.RequiresAuth)
            {
                string token = await GetAuthenticationToken(username);
                req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            foreach (var header in _apiGatewayURL.Headers)
            {
                req.Headers.Add(header.Name, header.Value);
            }

            HttpResponseMessage response = await client.SendAsync(req);

            return await BodyParser.ParseJson<TResponse>("GET", _apiGatewayURL.URL + path, null, response);
        }
        public async Task<string> GetAsync(string path, string username)
        {
            var client = _httpClientFactory.CreateClient();
            client.Timeout = TimeSpan.FromMinutes(5);
            var req = new HttpRequestMessage(HttpMethod.Get, _apiGatewayURL.URL + path);
            if (_apiGatewayURL.RequiresAuth)
            {
                string token = await GetAuthenticationToken(username);
                req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            foreach (var header in _apiGatewayURL.Headers)
            {
                req.Headers.Add(header.Name, header.Value);
            }

            HttpResponseMessage response = await client.SendAsync(req);

            return await BodyParser.ParseText("GET", _apiGatewayURL.URL + path, null, response);
        }
        
        public async Task<string> DeleteAsync<TRequest>(string path, string username)
        {
            var client = _httpClientFactory.CreateClient();
            client.Timeout = TimeSpan.FromMinutes(5);
            var req = new HttpRequestMessage(HttpMethod.Delete, _apiGatewayURL.URL + path);
            if (_apiGatewayURL.RequiresAuth)
            {
                string token = await GetAuthenticationToken(username);
                req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            foreach (var header in _apiGatewayURL.Headers)
            {
                req.Headers.Add(header.Name, header.Value);
            }

            HttpResponseMessage response = await client.SendAsync(req);

            return await BodyParser.ParseText("DELETE", _apiGatewayURL.URL + path, null, response);
        }


        public async Task<TResponse> PostAsync<TRequest, TResponse>(TRequest body, string path, string username) where TResponse : class
        {
            var client = _httpClientFactory.CreateClient();
            client.Timeout = TimeSpan.FromMinutes(5);
            var req = new HttpRequestMessage(HttpMethod.Post, _apiGatewayURL.URL + path);

            req.Content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, MediaTypeNames.Application.Json);
            if (_apiGatewayURL.RequiresAuth)
            {
                string token = await GetAuthenticationToken(username);
                req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            // headers
            foreach (var header in _apiGatewayURL.Headers)
            {
                req.Headers.Add(header.Name, header.Value);
            }

            HttpResponseMessage response = await client.SendAsync(req);
            return await BodyParser.ParseJson<TResponse>("POST", _apiGatewayURL.URL + path, await req.Content.ReadFromJsonAsync<dynamic>(), response);
        }

        public async Task<TResponse> PostFormAsync<TResponse>(MultipartFormDataContent form, string path, string username) where TResponse : class
        {
            var client = _httpClientFactory.CreateClient();
            client.Timeout = TimeSpan.FromMinutes(5);
            var req = new HttpRequestMessage(HttpMethod.Post, _apiGatewayURL.URL + path);
            req.Content = form;

            if (_apiGatewayURL.RequiresAuth)
            {
                string token = await GetAuthenticationToken(username);
                req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            // headers
            foreach (var header in _apiGatewayURL.Headers)
            {
                req.Headers.Add(header.Name, header.Value);
            }

            HttpResponseMessage response = await client.SendAsync(req);
            return await BodyParser.ParseJson<TResponse>("POST", _apiGatewayURL.URL + path, null, response);
        }

        public async Task<TResponse> PutAsync<TRequest, TResponse>(TRequest body, string path, string username) where TResponse : class
        {
            var client = _httpClientFactory.CreateClient();
            client.Timeout = TimeSpan.FromMinutes(5);
            var req = new HttpRequestMessage(HttpMethod.Put, _apiGatewayURL.URL + path);
            
            req.Content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, MediaTypeNames.Application.Json);
            if (_apiGatewayURL.RequiresAuth)
            {
                string token = await GetAuthenticationToken(username);
                req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            // headers
            foreach (var header in _apiGatewayURL.Headers)
            {
                req.Headers.Add(header.Name, header.Value);
            }

            HttpResponseMessage response = await client.SendAsync(req);
            return await BodyParser.ParseJson<TResponse>("PUT", _apiGatewayURL.URL + path, await req.Content.ReadFromJsonAsync<dynamic>(), response);
        }

        public async Task<string> PostAsync<TRequest>(TRequest body, string path, string username)
        {

            var client = _httpClientFactory.CreateClient();
            client.Timeout = TimeSpan.FromMinutes(5);
            var req = new HttpRequestMessage(HttpMethod.Post, _apiGatewayURL.URL + path);
            req.Content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, MediaTypeNames.Application.Json);
            if (_apiGatewayURL.RequiresAuth)
            {
                string token = await GetAuthenticationToken(username);
                req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            // headers
            foreach (var header in _apiGatewayURL.Headers)
            {
                req.Headers.Add(header.Name, header.Value);
            }

            HttpResponseMessage response = await client.SendAsync(req);
            return await BodyParser.ParseText("POST", _apiGatewayURL.URL + path, await req.Content.ReadFromJsonAsync<dynamic>(), response);
        }
        #endregion

    }
}
