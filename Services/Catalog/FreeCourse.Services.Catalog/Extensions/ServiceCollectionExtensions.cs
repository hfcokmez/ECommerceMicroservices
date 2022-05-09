using System;
using FreeCourse.Services.Catalog.Services.Abstract;
using FreeCourse.Services.Catalog.Services.Concrete;
using Microsoft.Extensions.DependencyInjection;

namespace FreeCourse.Services.Catalog.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection LoadServices(this IServiceCollection services)
        {
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICourseService, CourseService>();

            return services;
        }
    }
}
