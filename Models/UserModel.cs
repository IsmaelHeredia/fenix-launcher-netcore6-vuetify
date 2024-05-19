using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace FenixLauncher.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Username is required")]
        public string? User { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string? Pwd { get; set; }
        public UserModel()
        {
        }
    }
}
