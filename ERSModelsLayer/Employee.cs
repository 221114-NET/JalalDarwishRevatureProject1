using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERSModelsLayer
{
    /// <summary>
    /// Enum to keep track of role: Employee = 1, Manager = 2
    /// </summary>
    public enum RoleHeirarchy:int
    {
        EMPLOYEE = 1,
        MANAGER
    }
    public class Employee
    {
        public string? EmailAddress {get; set;}
        public string? Password {get; set;}
        //public List<Reimbursement>? ReimbursementTickets {get; set;}
        public RoleHeirarchy Role {get; set;}

        public Employee(string? emailAddress, string? password, RoleHeirarchy role)
        {
            EmailAddress = emailAddress;
            Password = password;
            Role = role;
        }

    }
}