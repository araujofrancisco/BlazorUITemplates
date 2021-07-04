using System.Threading.Tasks;
using System.Collections.Generic;
using FD.SampleData.Models;
using FD.SampleData.Contexts;
using FD.SampleData.Services;
using System;
using System.Linq.Expressions;
using SortDirection = FD.Blazor.Core.SortDirection;

namespace MudBlazorUI.Services
{
    public interface IDataService
    {
        Task AddUser(User user);
        Task EditUser(User user);
        Task DeleteUser(User user);
        Task SetNewPassword(User user, string password);
        Task<User> SignIn(User user, bool enabled);
        Task<int> GetUsersCountAsync(Expression<Func<User, bool>>? filters);
        Task<List<User>> GetUsersAsync(Expression<Func<User, bool>>? filters, string sortColumn, SortDirection sortDirection, int startIndex, int numberOfRecords);
        Task<List<Role>> GetRolesAsync(Expression<Func<Role, bool>>? filters, string sortColumn, SortDirection sortDirection, int startIndex, int numberOfRecords);
    }

    public class DataService : IDataService
    {
        private UserDbContext _context;
        private UserService _service;

        public DataService(UserDbContext context)
        {
            _context = context;
            _ = Initialize();
        }

        private async Task Initialize()
        {
            // this will create the database and seed it with the number of records indicated
            // obtains a new context
            try
            {
                _service = new UserService(_context);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            await Task.CompletedTask;
        }

        public async Task<int> GetUsersCountAsync(Expression<Func<User, bool>>? filters)
        {
            return await _service.GetUsersCountAsync(filters);
        }

        public async Task<List<User>> GetUsersAsync(Expression<Func<User, bool>>? filters, string sortColumn, SortDirection sortDirection, 
            int startIndex, int numberOfRecords)
        {
            return await _service.GetUsersAsync(filters, sortColumn, sortDirection, startIndex, numberOfRecords);
        }

        public async Task<List<Role>> GetRolesAsync(Expression<Func<Role, bool>>? filters, string sortColumn, SortDirection sortDirection,
            int startIndex, int numberOfRecords)
        {
            return await _service.GetRolesAsync(filters, sortColumn, sortDirection, startIndex, numberOfRecords);
        }
public async Task AddUser(User user)
{
            await _service.AddUser(user);
        }

        public async Task EditUser(User user)
        {
            await _service.EditUser(user);
        }

        public async Task DeleteUser(User user)
        {
            await _service.DeleteUser(user);
        }

        public async Task SetNewPassword(User user, string password)
        {
            await _service.SetNewPassword(user, password);
        }
        
        public async Task<User> SignIn(User user, bool enabled)
        {
            return await _service.SignIn(user, enabled);
        }
    }
}
