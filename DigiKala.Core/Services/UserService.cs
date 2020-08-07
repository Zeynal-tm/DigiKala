using System;
using System.Collections.Generic;
using System.Text;
using DigiKala.DataAccessLayer.Context;
using DigiKala.DataAccessLayer.Entities;
using DigiKala.Core.Interfaces;
using Microsoft.EntityFrameworkCore.Internal;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace DigiKala.Core.Services
{
    public class UserService : IUser
    {
        private DatabaseContext _context;

        public UserService(DatabaseContext context)
        {
            _context = context;
        }

        public void ActiveMailAddress(string mailAddress)
        {
            Store store = _context.Store.FirstOrDefault(s => s.Mail == mailAddress);
            store.MailActivate = true;
            _context.SaveChanges();
        }

        public void ActiveMobileNumber(string mobileNumber)
        {
            Store store = _context.Store.Include(s=> s.User).FirstOrDefault(s => s.User.Mobile == mobileNumber);
            store.MobileActivate = true;
            _context.SaveChanges();
        }

        public bool ExistsMailActivate(string username, string code)
        {
            return _context.Store.Include(s => s.User).Any(s => s.User.Mobile == username && s.MailActivateCode == code);
        }

        public bool ExistsMobileActivate(string username, string code)
        {
            return _context.Users.Any(u => u.Mobile == username && u.ActiveCode == code);
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

        public Store GetUserStore(string username)
        {
            return _context.Store.Include(s => s.User).FirstOrDefault(s => s.User.Mobile == username); 
        }
    }
}
