using System;
namespace FreeCourse.Services.Catalog.Settings
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string CourseName { get; set; }
        public string CatalogName { get; set; } 
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
