using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMJ_Application.Dtos
{
    public class RegisterDto
    {
        [Required, MaxLength(30)]
        public string FirstName { get; set; }
        [Required, MaxLength(20)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [StringLength(15, MinimumLength = 10, ErrorMessage = "Phone number must be between 10 and 15 characters.")]       
        [DataType(DataType.PhoneNumber, ErrorMessage = "Invalid phone number format.")]
        public string PhoneNumber { get; set; }
        [Required, MaxLength(30)]
        public string Email { get; set; }
        [Required, MaxLength(30)]
        public string Password { get; set; }
    }
}
