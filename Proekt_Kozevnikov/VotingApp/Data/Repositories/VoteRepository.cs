using System.Data.OleDb;
using VotingApp.Models;
using VotingApp.Utilities;

namespace VotingApp.Data.Repositories;

internal sealed class VoteRepository
{
    private readonly string _connectionString = AccessDatabase.GetConnectionString();

    public bool HasUserVoted(int userId)
    {
        const string sql = "SELECT COUNT(*) FROM Votes WHERE UserId = ?";
        using var connection = new OleDbConnection(_connectionString);
        using var command = new OleDbCommand(sql, connection);
        command.Parameters.AddWithValue("@userId", userId);
        connection.Open();
        var count = Convert.ToInt32(command.ExecuteScalar());
        return count > 0;
    }

    public void SaveVote(int userId, int candidateId)
    {
        const string sql = "INSERT INTO Votes (UserId, CandidateId, VoteDate) VALUES (?, ?, ?)";
        using var connection = new OleDbConnection(_connectionString);
        using var command = new OleDbCommand(sql, connection);
        command.Parameters.AddWithValue("@userId", userId);
        command.Parameters.AddWithValue("@candidateId", candidateId);
        command.Parameters.Add("@voteDate", OleDbType.Date).Value = DateTime.Now;
        connection.Open();
        command.ExecuteNonQuery();
    }

    public IReadOnlyList<VoteResult> GetResults()
    {
        const string sql = """
            SELECT c.Id, c.Name, c.Description, COUNT(v.Id) AS Votes
            FROM Candidates c
            LEFT JOIN Votes v ON v.CandidateId = c.Id
            GROUP BY c.Id, c.Name, c.Description
            ORDER BY c.Id
            """;

        using var connection = new OleDbConnection(_connectionString);
        using var command = new OleDbCommand(sql, connection);
        connection.Open();

        var results = new List<VoteResult>();
        using var reader = command.ExecuteReader();
        while (reader != null && reader.Read())
        {
            var candidate = new Candidate
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                Description = reader.IsDBNull(2) ? string.Empty : reader.GetString(2)
            };

            var votes = reader.IsDBNull(3) ? 0 : Convert.ToInt32(reader[3]);
            results.Add(new VoteResult { Candidate = candidate, Votes = votes });
        }

        return results;
    }

    public void ClearAll()
    {
        const string sql = "DELETE FROM Votes";
        using var connection = new OleDbConnection(_connectionString);
        using var command = new OleDbCommand(sql, connection);
        connection.Open();
        command.ExecuteNonQuery();
    }
}

