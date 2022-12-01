using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERSModelsLayer
{
    public interface IBusinessLayer
    {
        public void UserLoginRequest(LoginData loginD);
        public bool RegisterUser(LoginData loginD);
    }

    public interface IRepoLayer
    {
        public bool GetUserLoginInfo(LoginData loginD, Employee emp); //consider int return for more scenarios?
    }
}