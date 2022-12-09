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
}
