using Application.Exceptions;
using Newtonsoft.Json;

namespace Application.Utils
{
    public static class BodyParser
    {
        public static async Task<TResponse> ParseJson<TResponse>(string verb, string path, dynamic req, HttpResponseMessage response)
        {
            bool isValidJson = false;
            string text = null;
            try
            {
                text = await response.Content.ReadAsStringAsync();
            }
            catch
            {
                throw new GatewayException(verb, path, req, "Could not read body as string", response.StatusCode);
            }
            dynamic json = null;
            try
            {
                json = JsonConvert.DeserializeObject<dynamic>(text);
                isValidJson = true;
            }
            catch
            {
                isValidJson = false;
            }

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    if (isValidJson)
                    {
                        return JsonConvert.DeserializeObject<TResponse>(text);
                    }
                    else
                    {
                        throw new SerializerException(verb, path, req, text, response.StatusCode);
                    }
                }
                catch
                {
                    throw new SerializerException(verb, path, req, text, response.StatusCode);
                }
            }
            else
            {
                if (isValidJson)
                {
                    throw new GatewayException(verb, path, req, json, response.StatusCode);
                }
                else
                {
                    throw new GatewayException(verb, path, req, text, response.StatusCode);
                }
            }
        }

        public static async Task<string> ParseText(string verb, string path, dynamic req, HttpResponseMessage response)
        {
            string text = null;
            try
            {
                text = await response.Content.ReadAsStringAsync();
            }
            catch
            {
                throw new GatewayException(verb, path, req, "Could not read body as string", response.StatusCode);
            }

            if (response.IsSuccessStatusCode)
            {
                return text;
            }
            else
            {
                throw new GatewayException(verb, path, req, text, response.StatusCode);
            }
        }
    }
}