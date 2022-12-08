namespace ERSBusinessLayer;
using ERSModelsLayer;
using System.Text.RegularExpressions;

public class BusinessLayer : IBusinessLayer
{
    private IRepoLayer? repo;

    public BusinessLayer(IRepoLayer iRepo)
    {
        repo = iRepo;
    }

    public Employee? RegisterUser(string email, string password)
    {
        //Maybe separate into validation function for email and pass?
        Regex emailValid = new Regex(@"^[a-zA-Z0-9]{1,12}@[a-zA-Z]+.[a-zA-Z]{2,6}$"); //accepts up to 12 alphanumeric @ anynumber of alphabet . alpha 2-6 chars
        if (emailValid.IsMatch(email)) //if address is valid, send to repo layer
        {
            Employee? emp = repo.RegisterUser(email, password); //check if employee exists?
            return emp;
        }
        else
        {
            return null;
        }

    }

    public void UserLogin(string email, string password)
    {
        repo.UserLogin(email, password);
    }

}
