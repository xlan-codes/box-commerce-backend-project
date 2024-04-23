using Newtonsoft.Json;

namespace Application.Generics.Dtos.Settings
{
    public abstract class Identity
    {
        [JsonIgnore]
        public string UserName { get; set; }
    }
}
