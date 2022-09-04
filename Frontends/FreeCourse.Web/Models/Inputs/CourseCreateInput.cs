using System.ComponentModel.DataAnnotations;
using FreeCourse.Web.Models.ViewModels.Catalogs;

namespace FreeCourse.Web.Models.Inputs
{
    public class CourseCreateInput
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        public string UserId { get; set; }
        public string Picture { get; set; }
        public FeatureViewModel Feature { get; set; }
        [Required]
        public string CategoryId { get; set; }
    }
}