using System.Net;

namespace Application.Exceptions
{
    public class GatewayException : Exception
    {
        public string Verb { get; }
        public string Path { get; }
        public dynamic Request { get; }
        public dynamic Response { get; }
        public HttpStatusCode StatusCode { get; }
        public GatewayException(string verb, string path, dynamic req, dynamic res, HttpStatusCode code) : base()
        {
            Verb = verb;
            Path = path;
            Response = res;
            Request = req;
            StatusCode = code;
        }
    }
}
