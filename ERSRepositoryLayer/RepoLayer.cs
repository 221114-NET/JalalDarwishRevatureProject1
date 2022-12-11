﻿namespace ERSRepositoryLayer;

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

    Employee? IRepoLayer.RegisterUser(string email, string password)
    {
        //Maybe make connection in constructor instead, and reuse
        int userID = -1;
        SqlConnection conn = new SqlConnection(AzureConnectionString);
        try
        {

            SqlCommand comm = new SqlCommand("INSERT INTO registered_Users (Email, Passwords) VALUES(@email, @pword)", conn);

            conn.Open();
            comm.Parameters.AddWithValue("@email", email);
            comm.Parameters.AddWithValue("@pword", password);
            comm.ExecuteNonQuery();

            comm.CommandText = "SELECT UserID FROM registered_Users WHERE Email = @email";
            SqlDataReader read = comm.ExecuteReader();
            if (read.Read())
            {
                userID = read.GetInt32(0);
            }

        }
        catch (SqlException exc)
        {
            System.Console.WriteLine(exc.Message);
            return new Employee("user with that email already exists", userID, false);
        }
        finally
        {
            conn.Close();
        }
        return new Employee(email, userID, false);
    }

    //Session ID table if you have time, timestamped + SQLDatareader.GetGuid as Guid data type
    public int UserLogin(string email, string password)
    {
        int userID = -1;

        SqlConnection conn = new SqlConnection(AzureConnectionString);
        try
        {
            SqlCommand comm = new SqlCommand("SELECT UserID FROM registered_Users WHERE Email = @email AND Passwords = @password", conn);
            comm.Parameters.AddWithValue("@email", email);
            comm.Parameters.AddWithValue("@password", password);

            conn.Open();
            SqlDataReader read = comm.ExecuteReader();
            if (read.Read())
            {
                userID = read.GetInt32(0);
            }
        }
        finally
        {
            conn.Close();
        }
        return userID;
    }

    public Reimbursement? SubmitNewTicket(Reimbursement? ticket)
    {

        SqlConnection conn = new SqlConnection(AzureConnectionString);
        SqlConnection validationConn = new SqlConnection(AzureConnectionString);
        try
        {
            //Validating that the sent in UserID exists
            SqlCommand comm = new SqlCommand("SELECT UserID FROM registered_Users WHERE UserID = @user", validationConn);
            comm.Parameters.AddWithValue("@user", ticket!.UserID);
            validationConn.Open();
            try
            {
                using (SqlDataReader userValid = comm.ExecuteReader())
                {
                    if (!userValid.Read()) throw new InvalidDataException(); //If UserID doesn't exist, make return ticket null. Possible to do check as part of insert?
                }
            }
            finally
            {
                validationConn.Close();
            }

            //UserID is valid, so create new reimbursment to add
            comm = new SqlCommand("INSERT INTO reimbursment_Tickets (ReimbursmentType, DollarAmount, ReimbursmentDescription, TicketStatus, UserID) VALUES (@reimType, @dollars, @desc, @status, @user)", conn);
            comm.Parameters.AddWithValue("@reimType", ticket.ReimburseType);
            comm.Parameters.AddWithValue("@dollars", ticket.DollarAmount);
            comm.Parameters.AddWithValue("@desc", ticket.Description);
            comm.Parameters.AddWithValue("@status", ticket.ReimburseStatus);
            comm.Parameters.AddWithValue("@user", ticket.UserID);
            conn.Open();
            comm.ExecuteNonQuery();

        }
        catch
        {
            ticket = null;
        }
        finally
        {
            conn.Close();
        }

        return ticket;
    }

    /// <summary>
    /// Function to change pending ticket status
    /// </summary>
    /// <param name="userID">userID of a manager</param>
    /// <param name="reimbursmentID">ID of ticket to change</param>
    /// <param name="newStatus">new status of ticket from ReimbursmentStatus enum</param>
    /// <returns>-3 for unauthorized, -2 for invalid UserID, -1 for invalid reimbursment ticket ID or trying to change non pending ticket, 1 for success</returns>
    int IRepoLayer.ChangeTicketStatus(int userID, int reimbursmentID, ReimbursementStatus newStatus)
    {
        //Manager validation could be own function
        bool managerStatus = true;
        using (SqlConnection validationConn = new SqlConnection(AzureConnectionString))
        {
            using (SqlCommand validationComm = new SqlCommand("SELECT Manager FROM registered_users WHERE UserID = @user", validationConn))
            {
                validationComm.Parameters.AddWithValue("@user", userID);
                validationConn.Open();
                using (SqlDataReader manRead = validationComm.ExecuteReader())
                {
                    try
                    {
                        if (manRead.Read())
                        {
                            managerStatus = manRead.GetBoolean(0);
                        }
                        else
                        {
                            reimbursmentID = -2; //no matching UserID
                        }
                    }
                    finally
                    {
                        validationConn.Close();
                    }
                }

            }
        }
        if (managerStatus) //Specified user is a manager > execute ticket status change
        {
            using (SqlConnection conn = new SqlConnection(AzureConnectionString))
            {
                using (SqlCommand comm = new SqlCommand("UPDATE reimbursment_Tickets SET TicketStatus = @status WHERE ReimbursmentID = @ticketID AND TicketStatus = 1", conn))
                {
                    comm.Parameters.AddWithValue("@status", ((int)newStatus));
                    comm.Parameters.AddWithValue("@ticketID", reimbursmentID);
                    conn.Open();
                    try
                    {
                        if (comm.ExecuteNonQuery() != 0)
                        {
                            reimbursmentID = 1; //success
                        }
                        else
                        {
                            reimbursmentID = -1; //trying to change non pending, or invalid ticketID
                        }
                    }
                    finally
                    {
                        conn.Close();
                    }

                }
            }
        }
        else reimbursmentID = -3; //not a manager

        return reimbursmentID;
    }

    private int ManagerValidation(int userID)
    {
        
    }
}
