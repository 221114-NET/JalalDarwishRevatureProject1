using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERSModelsLayer
{
    public interface IBusinessLayer
    {
        public void UserLogin(string email, string password);
        public Employee? RegisterUser(string email, string password);
    }

    public interface IRepoLayer
    {
        public bool UserLogin(string email, string password); //consider int return for more scenarios?
        public Employee? RegisterUser(string email, string password);
    }
}