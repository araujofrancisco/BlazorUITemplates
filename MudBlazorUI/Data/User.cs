using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MudBlazorUI.Data
{
    public enum Roles
    {
        Admin,
        PowerUser,
        Reports,
        Import
    }

    public class User
    {
        [Key]
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNbr { get; set; }
        public string Email { get; set; }
        public bool IsEnabled { get; set; }
        public IEnumerable<Roles> UserRoles { get; set; }
    }
}
