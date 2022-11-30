namespace ERSBusinessLayer;
using ERSModelsLayer;
using System.Text.RegularExpressions;

public class BusinessLayer : IBusinessLayer
{
    private IRepoLayer? repo;
    public Employee? CurrentUser { get; set; } //How to differentiate manager vs user?
    public Queue<Reimbursement>? PendingTicketQueue { get; set; } //3? Separate lists: employee own, manager pending, master ticket list for writing back to DB
    public List<Reimbursement>? PersonalTicketList { get; set; }
    private List<Reimbursement>? MasterTicketList { get; set; } //When / How to initialize list?

    public BusinessLayer(IRepoLayer iRepo)
    {
        repo = iRepo;
    }

    public void RegisterUser(LoginData loginD)
    {
        Regex emailValid = new Regex(@"[a-zA-Z]{4}@[a-zA-Z].[a-z]"); //Finish email matching pattern
    }

    public void UserLoginRequest(LoginData loginD)
    {
        if(repo.GetUserLoginInfo(loginD, CurrentUser))
        {
            //repo.GetTicketList(CurrentUser); If login is successful, populate ticket list
        }
        else
        {
            CurrentUser = null; //Check info in Repolayer => DB, create currentUser object for session
        }
        
    }

}
