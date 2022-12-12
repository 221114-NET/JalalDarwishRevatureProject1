namespace ERSUnitTesting;

using System.Collections.Generic;
using ERSBusinessLayer;
using ERSModelsLayer;
using ERSRepositoryLayer;

public class UnitTest1
{
    [Theory]
    [InlineData("valid@email.com", "password", true)]
    [InlineData("!nvalid@email.com", "password", false)]
    [InlineData("invalid@ema!l.com", "password", false)]
    [InlineData("invalid@email.commocs", "password", false)]
    [InlineData("valid@email.com", "!nvalidpassword", false)]
    public void TestUserRegistration(string eTest, string pwTest, bool result)
    {
        BusinessLayer bus = new BusinessLayer(new fakeRepo());

        bool testSuccess = bus.RegisterUser(eTest, pwTest) != null ? true : false;

        Assert.Equal(result, testSuccess);
    }

    [Theory]
    [InlineData("real@email.com", "password", 1)]
    [InlineData("", "password", -2)]
    [InlineData("real@email.com", "", -2)]
    public void TestUserLogin(string eTest, string pwTest, int result)
    {
        BusinessLayer bus = new BusinessLayer(new fakeRepo());

        int testSuccess = bus.UserLogin(eTest, pwTest);

        Assert.Equal(result, testSuccess);
    }

    [Theory]
    [InlineData(1, true)]
    [InlineData(-1, false)]
    public void TestTicketSubmissionForValidUserID(int testID, bool result)
    {
        Reimbursement fakeTick = new Reimbursement(userID: testID); //passing userID by name to constructor
        BusinessLayer bus = new BusinessLayer(new fakeRepo());

        bool testSuccess = bus.SubmitNewTicket(fakeTick) != null ? true : false;

        Assert.Equal(result, testSuccess);
    }

    [Theory]
    [InlineData(1, 1, 1)] //all valid inputs
    [InlineData(-1, 1, -4)] //invalid userID
    [InlineData(1, -1, -4)] //invalid reimbursementID
    public void TestChangeTicketStatusForValidUserIDAndReimbursementID(int userID, int reimbursementID, int result)
    {
        BusinessLayer bus = new BusinessLayer(new fakeRepo());

        int testSuccess = bus.ChangeTicketStatus(userID, reimbursementID, ReimbursementStatus.APPROVED);

        Assert.Equal(result, testSuccess);
    }

    [Theory]
    [InlineData(1, "valid@email.com", "validpw", 1)]
    [InlineData(-1, "valid@email.com", "validpw", -2)]
    [InlineData(1, "!nvalid@email.com", "validpw", -2)]
    [InlineData(1, "valid@email.com", "!nvalidpw", -2)]
    public void TestEditAccountInformationAgainstUserIDEmailAndPassword(int userID, string eTest, string pwTest, int result)
    {
        BusinessLayer bus = new BusinessLayer(new fakeRepo());

        int testSuccess = bus.EditAccountInformation(userID, eTest, pwTest);

        Assert.Equal(result, testSuccess);
    }

    [Theory]
    [InlineData("valid@email.com", true)]
    [InlineData("!nvalid@email.com", false)]
    [InlineData("invalid@3ma!l.com", false)]
    [InlineData("invalid@email.com7777", false)]
    public void TestEmailInputValidation(string eTest, bool result)
    {
        bool testSuccess = InputValidation.ValidateEmail(eTest);

        Assert.Equal(result, testSuccess);
    }

    [Theory]
    [InlineData("Validpass1", true)]
    [InlineData("!nvalidpass", false)]
    [InlineData("no", false)]
    public void TestPasswordInputValidation(string pwTest, bool result)
    {
        bool testSuccess = InputValidation.ValidatePassword(pwTest);

        Assert.Equal(result, testSuccess);
    }

    //Repo Layer Tests
    [Theory]
    [InlineData("UnitTest@test.test", "testing123", 11)]
    [InlineData("NotReal@fake.fake", "Fake123", -1)]
    public void TestRepoUserLogin(string eTest, string pwTest, int result)
    {
        RepoLayer repo = new RepoLayer(new fakeLogger());

        int testSuccess = repo.UserLogin(eTest, pwTest);

        Assert.Equal(result, testSuccess);
    }

    [Theory]
    [InlineData(11, ReimbursementStatus.PENDING)]
    public void TestRepoGetTickets(int userID, ReimbursementStatus ticketFilter)
    {
        RepoLayer repo = new RepoLayer(new fakeLogger());

        Assert.True(repo.GetReimbursements(userID, ticketFilter).Count > 0);
    }

    [Theory]
    [InlineData(11)]
    public void TestRepoUnitTestNonManagerAccountGetsEmptyListOfTickets(int managerID)
    {
        RepoLayer repo = new RepoLayer(new fakeLogger());

        Assert.True(repo.GetPendingTickets(11).Count == 0);
    }
}