﻿using System.Collections.Generic;
using System.Threading.Tasks;
using FreeCourse.Services.Catalog.Dtos;
using FreeCourse.Shared.Dtos;

namespace FreeCourse.Services.Catalog.Services.Abstract
{
    public interface ICourseService
    {
        Task<Response<List<CourseDto>>> GetAllAsync();
        Task<Response<CourseDto>> GetByIdAsync(string id);
        Task<Response<List<CourseDto>>> GetAllCoursesByUserIdAsync(string userId);
        Task<Response<CourseDto>> CreateAsync(CourseCreateDto courseCreate);
        Task<Response<NoContent>> UpdateAsync(CourseUpdateDto courseUpdate);
        Task<Response<NoContent>> DeleteAsync(string id);
    }
}