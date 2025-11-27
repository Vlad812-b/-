namespace VotingApp.Models;

public sealed class VoteResult
{
    public Candidate Candidate { get; init; } = new();
    public int Votes { get; init; }
}







