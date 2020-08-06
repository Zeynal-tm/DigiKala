using System;
using System.Collections.Generic;
using System.Text;
using DigiKala.DataAccessLayer.Context;
using DigiKala.DataAccessLayer.Entities;
using DigiKala.Core.Interfaces;
using Microsoft.EntityFrameworkCore.Internal;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DigiKala.Core.Services
{
    public class UserService : IUser
    {
        private DatabaseContext _context;

        public UserService(DatabaseContext context)
        {
            _context = context;
        }


        public bool ExistsPermission(int permissionId, int roleId)
        {
            return _context.RolePermissions.Any(r => r.RoleId == roleId && r.PermissionId == permissionId);
        }

        public int GetUserRole(string username)
        {
            return _context.Users.FirstOrDefault(u => u.Mobile == username).RoleId;
        }

        public string GetUserRoleName(string username)
        {
            return _context.Users.Include(u=> u.Role).FirstOrDefault(u => u.Mobile == username).Role.Name;
        }
    }
}
