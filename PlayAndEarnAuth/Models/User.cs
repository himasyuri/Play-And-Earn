using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace PlayAndEarnAuth.Models
{
    public class User : IdentityUser<Guid>
    {
        public DateTime CreatedOn { get; set; }

        //public IList<string> Roles { get; set; }

        //public User()
        //{
        //    Roles = new List<string>();
        //}
    }
}
