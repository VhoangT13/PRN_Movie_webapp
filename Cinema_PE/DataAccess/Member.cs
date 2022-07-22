using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Cinema_PE.DataAccess
{
    public partial class Member
    {
        public int MemberId { get; set; }
        [Required(ErrorMessage = "Fullname is required")]
        public string Fullname { get; set; }
        [Required(ErrorMessage = "Gender is required")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Please enter valid email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public int Type { get; set; }
        public bool IsActive { get; set; }
    }
}
