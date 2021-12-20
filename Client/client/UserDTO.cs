using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace client
{
    public class UserDTO
    {
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        public string? EmailAddress { get; set; }

        [Required(ErrorMessage = "Please enter a description of your project.")]
        [MinLength(2)]
        public string Password { get; set;  }
    }
}
