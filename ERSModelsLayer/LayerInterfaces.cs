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
        public int ChangeTicketStatus(int userID, int reimbursmentID, ReimbursementStatus newStatus);
    }

    public interface IRepoLayer
    {
        public int UserLogin(string email, string password);
        public Employee? RegisterUser(string email, string password);
        public Reimbursement? SubmitNewTicket(Reimbursement ticket);
        
        /// <summary>
        /// Function to change pending ticket status
        /// </summary>
        /// <param name="userID">userID of a manager</param>
        /// <param name="reimbursmentID">ID of ticket to change</param>
        /// <param name="newStatus">new status of ticket from ReimbursmentStatus enum</param>
        /// <returns>-3 for unauthorized, -2 for invalid UserID, -1 for invalid reimbursment ticket ID or trying to change non pending ticket, 1 for success</returns>
        public int ChangeTicketStatus(int userID, int reimbursmentID, ReimbursementStatus newStatus);
    }
}