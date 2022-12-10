using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERSModelsLayer
{
    public interface IBusinessLayer
    {
        /// <summary>
        /// Function to log in based on email and password
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns>returns UserID on success, -1 for incorrect information, -2 for invalid input</returns>
        public int UserLogin(string email, string password);
        public Employee? RegisterUser(string email, string password);
        public Reimbursement? SubmitNewTicket(Reimbursement ticket);
        public int ChangeTicketStatus(int userID, int reimbursmentID);
    }

    public interface IRepoLayer
    {
        public int UserLogin(string email, string password);
        public Employee? RegisterUser(string email, string password);
        public Reimbursement? SubmitNewTicket(Reimbursement ticket);
        public int ChangeTicketStatus(int userID, int reimbursmentID);
    }
}