using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineTests.Models
{
    public class BaseUser
    {
        [Key]
        public int Id { get; set; }

        [Required, MinLength(1), MaxLength(50)]
        public string FirstName { get; set; }

        [Required, MinLength(1), MaxLength(50)]
        public string LastName { get; set; }

        [Required, MinLength(1), MaxLength(50)]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Invalid Email")]
        public string EmailAddress { get; set; }

        [Required, MinLength(4), MaxLength(255)]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Passwords are not same")]
        public string PasswordConfirm { set; get; }
    }
}
