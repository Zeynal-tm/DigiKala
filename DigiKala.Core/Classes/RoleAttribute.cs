using System;
using System.Collections.Generic;
using System.Text;
using DigiKala.Core.Interfaces;
using DigiKala.DataAccessLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DigiKala.Core.Classes
{
    public class RoleAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        int _permissionID;

        IUser _iuser;

        public RoleAttribute(int permissionID)
        {
            _permissionID = permissionID;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                string username = context.HttpContext.User.Identity.Name;

                _iuser = (IUser)context.HttpContext.RequestServices.GetService(typeof(IUser));

                int roleID = _iuser.GetUserRole(username);

                if (!_iuser.ExistsPermission(_permissionID, roleID))
                {
                    context.Result = new RedirectResult("/Accounts/Login");
                }

            }
            else
            {
                context.Result = new RedirectResult("/Accounts/Login");
            }
        }
    }
}
