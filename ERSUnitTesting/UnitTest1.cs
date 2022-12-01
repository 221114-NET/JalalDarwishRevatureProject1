namespace ERSUnitTesting;

using ERSBusinessLayer;
using ERSModelsLayer;

internal class fakeRepo : IRepoLayer
{
    public bool GetUserLoginInfo(LoginData loginD, Employee emp)
    {
        return true;
    }
}

public class UnitTest1
{
    [Theory]
    [InlineData("valid@email.com", "password", true)]
    [InlineData("!nvalid@email.com", "password", false)]
    [InlineData("invalid@ema!l.com", "password", false)]
    [InlineData("invalid@email.commocs", "password", false)]
    public void TestUserRegistrationValidation(string eTest, string pwTest, bool result)
    {
        BusinessLayer bus = new BusinessLayer(new fakeRepo());

        LoginData ld;
        ld.EmailAddress = eTest;
        ld.Password = pwTest;

        Assert.Equal(result, bus.RegisterUser(ld));
    }
}