using System;
using System.Collections.Generic;
using System.Text;
using DigiKala.DataAccessLayer.Entities;

namespace DigiKala.Core.Interfaces
{
    public interface IUser
    {
        bool ExistsPermission(int permissionId, int roleId);

        int GetUserRole(string username);

        string GetUserRoleName(string username);

        Store GetUserStore(string username);

        bool ExistsMailActivate(string username, string code);
        bool ExistsMobileActivate(string username, string code);

        void ActiveMailAddress(string mailAddress);
        void ActiveMobileNumber(string mobileNumber);
    }
}
