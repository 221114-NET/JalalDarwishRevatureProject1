namespace ERSBusinessLayer;
using ERSModelsLayer;

public class BusinessLayer : IBusinessLayer
{
    private IRepoLayer? repo;

    public BusinessLayer(IRepoLayer iRepo)
    {
        repo = iRepo;
    }

    public Employee? RegisterUser(string email, string password)
    {
        if (InputValidation.ValidateEmail(email) && InputValidation.ValidatePassword(password)) //if address and pw are valid, send to repo layer
        {
            Employee? emp = repo!.RegisterUser(email, password);
            return emp;
        }
        else
        {
            return null;
        }

    }

    /// <summary>
    /// Function to log in based on email and password
    /// </summary>
    /// <param name="email"></param>
    /// <param name="password"></param>
    /// <returns>returns UserID on success, -1 for incorrect information, -2 for invalid input</returns>
    public int UserLogin(string email, string password)
    {
        if (InputValidation.ValidateEmail(email) && InputValidation.ValidatePassword(password))
        {
            return repo!.UserLogin(email, password);
        }
        else
        {
            return -2;
        }
    }

    public Reimbursement? SubmitNewTicket(Reimbursement? ticket)
    {
        if (ticket.UserID < 1) return null; //Quick simple input validation

        ticket.ReimburseStatus = ReimbursementStatus.PENDING; //Make sure status is pending
        return repo!.SubmitNewTicket(ticket);
    }

    int IBusinessLayer.ChangeTicketStatus(int userID, int reimbursmentID, ReimbursementStatus newStatus)
    {
        if (userID >= 1 && reimbursmentID >= 1)
        {
            return repo.ChangeTicketStatus(userID, reimbursmentID, newStatus);
        }
        else return -4;//Quick simple input validation to make sure UserID and ReimbursmentID are potentally valid values
    }
}
