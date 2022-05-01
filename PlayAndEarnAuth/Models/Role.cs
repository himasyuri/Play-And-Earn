using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace PlayAndEarnAuth.Models
{
    public class Role : IdentityRole<Guid>
    {

        //public virtual ICollection<UserRole> Users { get; set; }

        //public virtual UserRole? UserRole { get; set; }

        //public Role()
        //{
        //    Users = new List<UserRole>();
        //}
    }
}
