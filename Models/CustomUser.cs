using Microsoft.AspNetCore.Identity;

namespace FIsrtMVCapp.Models
{
    public class CustomUser:IdentityUser
    {
        public string? PreferredLanquaqe { get; set; }
    }
}
