using Application.Generics.Dtos.Settings;

namespace Application.Generics.Interfaces
{
    public interface IApiService<TSettings> where TSettings : BaseApiGatewaySettings
    {
        Task<TResponse> GetAsync<TResponse>(string path, string username) where TResponse : class;
        Task<string> GetAsync(string path, string username);
        Task<string> DeleteAsync<TRequest>(string path, string username);
        Task<TResponse> PostAsync<TRequest, TResponse>(TRequest body, string path, string username) where TResponse : class;
        Task<TResponse> PutAsync<TRequest, TResponse>(TRequest body, string path, string username) where TResponse : class;
        Task<TResponse> PostFormAsync<TResponse>(MultipartFormDataContent body, string path, string username) where TResponse : class;
        Task<string> PostAsync<TRequest>(TRequest body, string path, string username);
        Task<string> GetAuthenticationToken(string username);
    }
}
