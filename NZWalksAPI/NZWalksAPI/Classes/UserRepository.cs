﻿using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Repositories;

namespace NZWalksAPI.Classes
{
    public class UserRepository : IUserRepository
    {
        private readonly NZWalksDbContext _nZWalksDbContext;

        public UserRepository(NZWalksDbContext nZWalksDbContext)
        {
            _nZWalksDbContext = nZWalksDbContext;
        }

        public async Task<User> AuthenticateAsync(string username, string password)
        {
            var user = await _nZWalksDbContext.Users
                .FirstOrDefaultAsync(x => x.Username.ToLower() == username && x.Password == password);

            if (user == null)
            {
                return null;
            }

            var userRoles = await _nZWalksDbContext.Users_Roles.Where(x => x.UserId== user.Id).ToListAsync();

            if(userRoles.Any())
            {
                user.Roles = new List<string>();
                foreach(var userRole in userRoles)
                {
                    var role = await _nZWalksDbContext.Roles.FirstOrDefaultAsync(x => x.Id == userRole.RoleId);
                    if (role != null)
                    {
                        user.Roles.Add(role.Name);
                    }
                }
            }

            user.Password = null;
            return user;
        }
    }
}
