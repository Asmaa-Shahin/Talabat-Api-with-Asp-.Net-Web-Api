﻿using System.ComponentModel.DataAnnotations;

namespace Talabat.DTO
{
    public class RegisterDto
    {
        [Required]
        public string DisplayName { get; set; }
       
        public string? FirstName { get; set; }
        

        public string? LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        public string? PhoneNumber { get; set; }
        [Required]
        [RegularExpression("(?=^.{6,10}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;'?/&gt;.&lt;,])(?!.*\\s).*$",
           ErrorMessage = "Password must have 1 Uppercase, 1 Lowercase, 1 number, 1 non alphanumeric and at least 6 characters")]

        public string Password { get; set; }
        

        public string? City { get; set; }
       

        public string? Country { get; set; }
      

        public string? Street { get; set; }
       

        public string? ZipCode { get; set; }
    }
}
