using System.ComponentModel.DataAnnotations;

namespace TheApp.Model
{
    public class LoginReq
    {
        [EmailAddress]
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
