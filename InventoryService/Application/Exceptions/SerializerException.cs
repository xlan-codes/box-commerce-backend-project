using System.Net;

namespace Application.Exceptions
{
    public class SerializerException : GatewayException
    {
        public SerializerException(string verb, string path, dynamic req, dynamic res, HttpStatusCode code) : base(verb, path, (object)req, (object)res, code) { }
    }
}
