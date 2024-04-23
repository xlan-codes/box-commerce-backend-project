using Newtonsoft.Json;

namespace Application.Generics.Dtos
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class AddressDto
    {
        public string Guid { get; set; }
        public int? Id { get; set; }
        public int? CountryId { get; set; }
        public string CountryName { get; set; }
        public int? CountyId { get; set; }
        public string CountyName { get; set; }
        public int? CityId { get; set; }
        public string CityName { get; set; }
        public int? District { get; set; }
        public string PostalCode { get; set; }
        public string StreetName { get; set; }
        public string StreetNumber { get; set; }
        public string Building { get; set; }
        public string Entrance { get; set; }
        public string Floor { get; set; }
        public string AppartamentNumber { get; set; }
        public string StreetType { get; set; }
    }
}
