namespace ERSBusinessLayer;
using ERSModelsLayer;

public interface IBusinessLayer
{
    public void UserLoginRequest();
}

public class BusinessLayer : IBusinessLayer
{
    public Employee? currentUser { get; set; }

    public void UserLoginRequest()
    {
        currentUser = null; //Check info in Repolayer => DB, create currentUser object for session
    }

}
