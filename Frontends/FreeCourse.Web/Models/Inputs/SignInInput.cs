using System.ComponentModel.DataAnnotations;

namespace FreeCourse.Web.Models.Inputs
{
    public class SignInInput
    {
        [Required]
        [Display(Name = "Your email address")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Your password")] 
        public string Password { get; set; }
        [Display(Name = "Remember me")] 
        public bool IsRemember { get; set; }
    }
}