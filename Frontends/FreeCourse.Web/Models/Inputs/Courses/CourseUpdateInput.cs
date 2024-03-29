using System.ComponentModel.DataAnnotations;
using FreeCourse.Web.Models.ViewModels.Catalogs;
using Microsoft.AspNetCore.Http;

namespace FreeCourse.Web.Models.Inputs.Courses
{
    public class CourseUpdateInput
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string UserId { get; set; }
        public string Picture { get; set; }
        public FeatureViewModel Feature { get; set; }
        [Display(Name = "Course Category")]
        public string CategoryId { get; set; }
        [Display(Name = "Course Photo")]
        public IFormFile PhotoFormFile { get; set; }
    }
}