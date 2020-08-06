using System;
using System.Collections.Generic;
using System.Text;

using DigiKala.DataAccessLayer.Entities;
using DigiKala.DataAccessLayer.Context;

using DigiKala.Core.Classes;

using DigiKala.Core.Interfaces;
using Microsoft.EntityFrameworkCore.Internal;

using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DigiKala.Core.Services
{
    public class AccountService : IAccount
    {
        private DatabaseContext _context;

        public AccountService(DatabaseContext context)
        {
            _context = context;
        }

        public bool ActivateUser(string code)
        {
            User user = _context.Users.FirstOrDefault(u => u.IsActive == false && u.ActiveCode == code);

            if(user != null)
            {
                user.IsActive = true;
                user.ActiveCode = CodeGenerators.ActiveCode();

                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }

        }

        public void AddStrore(Store store)
        {
            _context.Store.Add(store);
            _context.SaveChanges();
        }

        public void AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public bool ExistsMailAddress(string mailAddress)
        {
            return _context.Store.Any(s => s.Mail == mailAddress);
        }

        public bool ExistsMobileNumber(string mobileNumber)
        {
            return _context.Users.Any(u => u.Mobile == mobileNumber); 
        }

        public int GetMaxRole()
        {
            return _context.Roles.Max(r => r.Id);
        }

        public int GetStoreRole()
        {
            return _context.Roles.FirstOrDefault(r => r.Name == "فروشگاه").Id;
        }

        public string GetUserActiveCode(string mobileNumber)
        {
            return _context.Users.FirstOrDefault(u => u.Mobile == mobileNumber).ActiveCode;
        }

        public int GetUserId(string mobileNumber)
        {
            return _context.Users.FirstOrDefault(u => u.Mobile == mobileNumber).Id;
        }

        public User LoginUser(string mobileNumber, string password)
        {
            return _context.Users.Include(u=> u.Role).FirstOrDefault(u => u.Mobile == mobileNumber && u.Password == password);
        }

        public bool ResetPassword(string code, string password)
        {
            User user = _context.Users.FirstOrDefault(u => u.ActiveCode == code);
            
            if (user != null)
            {
                user.Password = HashGenerators.MD5Encoding(password);
                user.ActiveCode = CodeGenerators.ActiveCode();
                _context.SaveChanges();

                return true;
            }
            else
            {
                return false;
            }
        }

        public void UpdateUserRole(string mobileNumber)
        {
            User user = _context.Users.FirstOrDefault(u => u.Mobile == mobileNumber);
            Role role = _context.Roles.FirstOrDefault(r => r.Name == "فروشگاه");

            user.RoleId = role.Id;
            user.IsActive = false;

            _context.SaveChanges();
        }
    }
}
