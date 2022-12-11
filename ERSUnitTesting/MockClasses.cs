using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERSModelsLayer;

namespace ERSUnitTesting
{
    internal class fakeRepo : IRepoLayer
    {
        int IRepoLayer.ChangeTicketStatus(int userID, int reimbursmentID, ReimbursementStatus newStatus)
        {
            return 1; //success
        }

        int IRepoLayer.EditAccountInformation(int userID, string email, string password)
        {
            return 1; //success
        }

        List<Reimbursement>? IRepoLayer.GetPendingTickets(int managerID)
        {
            return new List<Reimbursement>();
        }

        List<Reimbursement>? IRepoLayer.GetReimbursements(int userID, ReimbursementStatus ticketFilter)
        {
            return new List<Reimbursement>();
        }

        Employee? IRepoLayer.RegisterUser(string email, string password)
        {
            return new Employee(email, 1);
        }

        Reimbursement? IRepoLayer.SubmitNewTicket(Reimbursement ticket)
        {
            return ticket;
        }

        int IRepoLayer.UserLogin(string email, string password)
        {
            return 1;
        }
    }

    internal class fakeLogger : ILogger
    {
        void ILogger.ErrorLog(Exception exc)
        {
        }

        void ILogger.LoginRecord(int userID, string email)
        {
        }
    }
}