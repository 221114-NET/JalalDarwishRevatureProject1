using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERSModelsLayer
{
    public class Employee
    {
        public string? EmailAddress {get; set;}
        public int UserID {get; set;}
        public bool ManagerStatus {get; set;}

        public Employee(string? emailAddress, int userID, bool managerStatus = false)
        {
            EmailAddress = emailAddress;
            UserID = userID;
            ManagerStatus = managerStatus;
        }

    }
}