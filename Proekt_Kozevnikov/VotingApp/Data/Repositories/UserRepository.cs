using System.Data.OleDb;
using VotingApp.Models;
using VotingApp.Utilities;

namespace VotingApp.Data.Repositories;

internal sealed class UserRepository
{
    private readonly string _connectionString = AccessDatabase.GetConnectionString();

    public User? FindByEmail(string email)
    {
        const string sql = """
            SELECT Id, FullName, Email, PasswordHash, CreatedAt
            FROM Users
            WHERE Email = ?
            """;

        using var connection = new OleDbConnection(_connectionString);
        using var command = new OleDbCommand(sql, connection);
        command.Parameters.AddWithValue("@email", email);
        connection.Open();
        using var reader = command.ExecuteReader();
        return reader != null && reader.Read() ? MapUser(reader) : null;
    }

    public User? FindById(int id)
    {
        const string sql = """
            SELECT Id, FullName, Email, PasswordHash, CreatedAt
            FROM Users
            WHERE Id = ?
            """;

        using var connection = new OleDbConnection(_connectionString);
        using var command = new OleDbCommand(sql, connection);
        command.Parameters.AddWithValue("@id", id);
        connection.Open();
        using var reader = command.ExecuteReader();
        return reader != null && reader.Read() ? MapUser(reader) : null;
    }

    public bool EmailExists(string email) => FindByEmail(email) is not null;

    public User Create(string fullName, string email, string passwordHash)
    {
        const string sql = """
            INSERT INTO Users (FullName, Email, PasswordHash, CreatedAt)
            VALUES (?, ?, ?, ?)
            """;

        using var connection = new OleDbConnection(_connectionString);
        using var command = new OleDbCommand(sql, connection);
        command.Parameters.AddWithValue("@fullName", fullName);
        command.Parameters.AddWithValue("@email", email);
        command.Parameters.AddWithValue("@passwordHash", passwordHash);
        command.Parameters.Add("@createdAt", OleDbType.Date).Value = DateTime.Now;
        connection.Open();
        command.ExecuteNonQuery();

        command.CommandText = "SELECT @@IDENTITY";
        var newId = Convert.ToInt32(command.ExecuteScalar());
        return FindById(newId)!;
    }

    private static User MapUser(OleDbDataReader reader) =>
        new()
        {
            Id = reader.GetInt32(0),
            FullName = reader.GetString(1),
            Email = reader.GetString(2),
            PasswordHash = reader.GetString(3),
            CreatedAt = reader.GetDateTime(4),
            IsAdmin = false
        };
}

