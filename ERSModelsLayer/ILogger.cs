using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERSModelsLayer
{
    public interface ILogger
    {
        public void LoginRecord(int userID, string email);
        public void ErrorLog(Exception exc);
    }
}