using VotingApp.Data.Repositories;
using VotingApp.Models;

namespace VotingApp.Services;

internal sealed class VotingService
{
    private readonly CandidateRepository _candidates = new();
    private readonly VoteRepository _votes = new();

    public IReadOnlyList<Candidate> GetCandidates() => _candidates.GetAll();

    public bool HasUserVoted(User user) => _votes.HasUserVoted(user.Id);

    public void SubmitVote(User user, Candidate candidate)
    {
        if (HasUserVoted(user))
        {
            throw new InvalidOperationException("Вы уже участвовали в голосовании.");
        }

        _votes.SaveVote(user.Id, candidate.Id);
    }

    public IReadOnlyList<VoteResult> GetResults() => _votes.GetResults();
}

