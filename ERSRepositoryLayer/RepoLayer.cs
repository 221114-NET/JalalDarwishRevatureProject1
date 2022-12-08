namespace ERSRepositoryLayer;

using ERSModelsLayer;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

public class RepoLayer : IRepoLayer
{
    //Credit to Tyrel Marx for this method of how to keep connection string off GitHub
    private readonly string AzureConnectionString;

    public RepoLayer()
    {
        AzureConnectionString = new ConfigurationBuilder().AddJsonFile("appsettings.Development.json").Build().GetSection("ConnectionStrings")["ersDb"]!;
    }

    public bool UserLogin(string email, string password)
    {
        //Get info from DB and put into emp
        return false;
    }

    Employee? IRepoLayer.RegisterUser(string email, string password)
    {
        //Maybe make connection in constructor instead, and reuse
        SqlConnection conn = new SqlConnection(AzureConnectionString);
        try
        {

            SqlCommand comm = new SqlCommand("INSERT INTO registered_Users (Email, Passwords) VALUES(@email, @pword)", conn);

            conn.Open();
            comm.Parameters.AddWithValue("@email", email);
            comm.Parameters.AddWithValue("@pword", password);
            comm.ExecuteNonQuery();

            comm.CommandText = "SELECT UserID FROM registered_Users WHERE Email = @email";
            //comm.ExecuteReader()

        }
        catch (SqlException exc)
        {
            return null;
        }
        finally
        {

            conn.Close();
        }
        return new Employee("testing connection string", 1, false);
    }
}
