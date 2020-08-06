using System;
using System.Collections.Generic;
using System.Text;

using DigiKala.DataAccessLayer.Entities;

namespace DigiKala.Core.Interfaces
{
    public interface IAccount
    {
        bool ExistsMobileNumber(string mobileNumber);

        bool ExistsMailAddress(string mailAddress);

        void AddUser(User user);

        int GetMaxRole();

        int GetStoreRole();

        int GetUserId(string mobileNumber);

        bool ActivateUser(string code);

        User LoginUser(string mobileNumber, string password);

        bool ResetPassword(string code, string password);

        string GetUserActiveCode(string mobileNumber);

        void AddStrore(Store store);

        void UpdateUserRole(string mobileNumber);
    }
}
