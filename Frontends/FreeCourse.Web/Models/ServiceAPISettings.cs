namespace FreeCourse.Web.Models
{
    public class ServiceAPISettings
    {
        public string IdentityBaseUri { get; set; }
        public string GatewayBaseUri { get; set; }
        public string PhotoStockUri { get; set; }
        public ServiceApi Catalog { get; set; }
    }
    public class ServiceApi
    {
        public string Path { get; set; }
    }
}