using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace TheApp.Model
{
    public class Base
    {
        public ObjectId? Id { get; set; }
        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }
        [Phone]
        public string? Phone { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        public Role Role { get; set; }
        public bool IsRemoved { get; set; } = false;
    }
}
