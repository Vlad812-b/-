using VotingApp.Data.Repositories;
using VotingApp.Models;

namespace VotingApp.Services;

internal sealed class AdminService
{
    private readonly CandidateRepository _candidates = new();
    private readonly VoteRepository _votes = new();

    public IReadOnlyList<VoteResult> GetResults() => _votes.GetResults();

    public IReadOnlyList<Candidate> GetCandidates() => _candidates.GetAll();

    public void AddCandidate(string name, string description)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Введите имя кандидата.", nameof(name));
        }

        _candidates.AddCandidate(name.Trim(), description?.Trim() ?? string.Empty);
    }

    public void ResetVotes() => _votes.ClearAll();
}







