using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FD.Blazor.Core;

namespace MudBlazorUI.Data
{
    public enum UserAction {
        Add = 0,
        Edit = 1,
        Delete = 2,
        List = 3
    }

    public class UserService
    {
        private static readonly string[] FirstNames = new[]
        {
            "Ana", "Edward", "John", "Paul", "Dawn", "Kyle", "Sheldon", "Douglas", "Martin", "Luke", "Phil", "Jane"
        };

        private static readonly string[] LastNames = new[]
{
            "Doe", "Perry", "Raussman", "Ford", "Thompson", "Smith", "Simpson", "Dunphy", "Brown", "Blake"
        };

        public Task<List<User>> GetUserAsync()
        {
            var rng = new Random();
            return Task.FromResult(Enumerable.Range(1, 500).Select(index => new User
            {
                FirstName = FirstNames[rng.Next(FirstNames.Length)],
                LastName = LastNames[rng.Next(LastNames.Length)],
                Email = $"user{rng.Next(10000,99999)}@gmail.com",
                MobileNbr = $"+{rng.Next(1,9)}{rng.Next(100,999)}{rng.Next(100,999)}{rng.Next(1000,9999)}",
                IsEnabled = (rng.Next(100) % 2) != 0,
                UserRoles = new List<Roles>() { (Roles)rng.Next(0, 3) }
            })
                .DistinctBy(u => u.UserName)
                .ToList()); 
        }

        public async Task ToggleSignIn(User user)
        {
            await Task.Run(() => user.IsEnabled = !user.IsEnabled);
        }
    }
}
