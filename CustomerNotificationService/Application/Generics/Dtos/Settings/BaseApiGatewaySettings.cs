namespace Application.Generics.Dtos.Settings
{
    public class BaseApiGatewaySettings
    {
        public class BaseAPIGatewayHeader
        {
            public string Name { get; set; }
            public string Value { get; set; }
        }

        public string URL { get; set; }
        public IEnumerable<BaseAPIGatewayHeader> Headers { get; set; }
        public bool RequiresAuth { get; set; }
    }

}
