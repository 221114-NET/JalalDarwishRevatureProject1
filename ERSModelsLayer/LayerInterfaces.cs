using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERSModelsLayer
{
    public interface IBusinessLayer
    {
        public int UserLogin(string email, string password);
        public Employee? RegisterUser(string email, string password);
        public Reimbursement? SubmitNewTicket(Reimbursement ticket);
    }

    public interface IRepoLayer
    {
        public int UserLogin(string email, string password);
        public Employee? RegisterUser(string email, string password);
        public Reimbursement? SubmitNewTicket(Reimbursement ticket);
    }
}