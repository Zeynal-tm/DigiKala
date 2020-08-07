using System;
using System.Collections.Generic;
using System.Text;
using DigiKala.Core.Interfaces;
using DigiKala.Core.Services;

namespace DigiKala.Core.Classes
{
    public class PanelLayoutScope
    {
        private IUser _user;

        public PanelLayoutScope(IUser user)
        {
            _user = user;
        }

        public string GetUserRoleName(string username)
        {
            return _user.GetUserRoleName(username);
        }

    }


}

