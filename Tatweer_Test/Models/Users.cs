using System.ComponentModel.DataAnnotations;

namespace Tatweer_Test.Models
{
    public class Users
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "The name field is required.")]
        [RegularExpression(@"^[^0-9]*$", ErrorMessage = "Numbers are not allowed in the name.")]
        public string Username { get; set; }

        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&#])[A-Za-z\d@$!%*?&#]{8,}$", ErrorMessage = "Password must have at least one uppercase, one lowercase, one number, and one special character.")]
        public string Password { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string Email { get; set; }

        [RegularExpression(@"^\d{10}$", ErrorMessage = "Mobile number must contain 10 numbers.")]
        public string Mobile { get; set; }

    }
}
