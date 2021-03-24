using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StorageLayer.Models
{
    public class BaseUser
    {
        [Required, Key, MinLength(1), MaxLength(50)]
        public string FirstName { get; set; }

        [Required, Key, MinLength(1), MaxLength(50)]
        public string LastName { get; set; }

        [Required, Key, MinLength(1), MaxLength(50)]
        public string EmailAddress { get; set; }

        [Required, MinLength(4), MaxLength(255)]
        public string Password { get; set; }
    }
}
