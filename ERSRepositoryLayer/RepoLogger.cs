using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERSModelsLayer;

namespace ERSRepositoryLayer
{
    public class RepoLogger : ILogger
    {
        public void LoginRecord(int userID, string email)
        {
            File.AppendAllText("../Logs/LoginRecord.txt", $"UserID: {userID} Email: {email} logged in at {DateTime.UtcNow}\n");
        }

        public void ErrorLog(Exception exc)
        {
            File.AppendAllText("../Logs/ErrorLog.txt", $"{exc.ToString()}\n");
        }
    }
}