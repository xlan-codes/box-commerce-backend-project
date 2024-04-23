using Newtonsoft.Json;

namespace WebAPI.Models
{
    public class ErrorDetails
    {
        public int statusCode { get; set; }
        public dynamic response { get; set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
