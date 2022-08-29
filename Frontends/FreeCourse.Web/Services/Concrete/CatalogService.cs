using System.Collections.Generic;
using System.Threading.Tasks;
using FreeCourse.Web.Models.Inputs;
using FreeCourse.Web.Models.ViewModels.Catalogs;
using FreeCourse.Web.Services.Interfaces;

namespace FreeCourse.Web.Services.Concrete
{
    public class CatalogService : ICatalogService
    {
        public Task<List<CourseViewModel>> GetAllCoursesAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<List<CategoryViewModel>> GetAllCategoriesAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<List<CourseViewModel>> GetAllCoursesByUserIdAsync(string userId)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<CourseViewModel>> GetAllCoursesByCourseIdAsync(string courseId)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> DeleteCourseAsync(string courseId)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> CreateCourseAsync(CourseCreateInput courseCreateInput)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> UpdateCourseAsync(CourseUpdateInput courseUpdateInput)
        {
            throw new System.NotImplementedException();
        }
    }
}