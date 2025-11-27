using System.Data.OleDb;
using VotingApp.Models;
using VotingApp.Utilities;

namespace VotingApp.Data.Repositories;

internal sealed class CandidateRepository
{
    private readonly string _connectionString = AccessDatabase.GetConnectionString();

    public IReadOnlyList<Candidate> GetAll()
    {
        const string sql = "SELECT Id, Name, Description FROM Candidates ORDER BY Id";

        using var connection = new OleDbConnection(_connectionString);
        using var command = new OleDbCommand(sql, connection);
        connection.Open();

        var result = new List<Candidate>();
        using var reader = command.ExecuteReader();
        while (reader != null && reader.Read())
        {
            result.Add(MapCandidate(reader));
        }

        return result;
    }

    public Candidate? FindById(int id)
    {
        const string sql = "SELECT Id, Name, Description FROM Candidates WHERE Id = ?";
        using var connection = new OleDbConnection(_connectionString);
        using var command = new OleDbCommand(sql, connection);
        command.Parameters.AddWithValue("@id", id);
        connection.Open();
        using var reader = command.ExecuteReader();
        return reader != null && reader.Read() ? MapCandidate(reader) : null;
    }

    public void AddCandidate(string name, string description)
    {
        const string sql = "INSERT INTO Candidates (Name, Description) VALUES (?, ?)";
        using var connection = new OleDbConnection(_connectionString);
        using var command = new OleDbCommand(sql, connection);
        command.Parameters.AddWithValue("@name", name);
        command.Parameters.AddWithValue("@description", description);
        connection.Open();
        command.ExecuteNonQuery();
    }

    private static Candidate MapCandidate(OleDbDataReader reader) =>
        new()
        {
            Id = reader.GetInt32(0),
            Name = reader.GetString(1),
            Description = reader.IsDBNull(2) ? string.Empty : reader.GetString(2)
        };
}

