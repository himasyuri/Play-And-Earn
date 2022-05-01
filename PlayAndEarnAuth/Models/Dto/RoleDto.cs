using System.ComponentModel.DataAnnotations;

namespace PlayAndEarnAuth.Models.Dto
{
    public class RoleDto
    {
        [Required]
        public string RoleName { get; set; } = string.Empty;
    }
}
