using Newtonsoft.Json;

namespace Application.Generics.Dtos
{
    public abstract class Identity
    {
        [JsonIgnore]
        public string UserName { get; set; }
    }
}
