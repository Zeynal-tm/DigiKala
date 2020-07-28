using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DigiKala.Core.Classes
{
    public class RoleAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        string _roleName;

        public RoleAttribute(string roleName)
        {
            _roleName = roleName;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                string username = context.HttpContext.User.Identity.Name;
            }
            else
            {
                //Go To Login
            }
        }
    }
}
