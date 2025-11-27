using System.Drawing;
using VotingApp.Models;
using VotingApp.Services;

namespace VotingApp.Forms;

public partial class VotingForm : Form
{
    private readonly User _currentUser;
    private readonly VotingService _votingService = new();
    private IReadOnlyList<Candidate> _candidates = Array.Empty<Candidate>();

    public VotingForm(User currentUser)
    {
        _currentUser = currentUser;
        InitializeComponent();
        welcomeLabel.Text = $"Здравствуйте, {_currentUser.FullName}";
    }

    private void VotingForm_Load(object? sender, EventArgs e)
    {
        LoadCandidates();
        LoadResults();
        var alreadyVoted = _votingService.HasUserVoted(_currentUser);
        voteButton.Enabled = !alreadyVoted;

        if (alreadyVoted)
        {
            statusLabel.ForeColor = Color.SeaGreen;
            statusLabel.Text = "Вы уже проголосовали. Спасибо!";
        }
    }

    private void LoadCandidates()
    {
        _candidates = _votingService.GetCandidates();
        candidateListBox.Items.Clear();
        foreach (var candidate in _candidates)
        {
            candidateListBox.Items.Add(candidate.Name);
        }

        if (candidateListBox.Items.Count > 0)
        {
            candidateListBox.SelectedIndex = 0;
        }
    }

    private void CandidateListBox_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (candidateListBox.SelectedIndex < 0 || candidateListBox.SelectedIndex >= _candidates.Count)
        {
            candidateDescriptionTextBox.Text = string.Empty;
            return;
        }

        var candidate = _candidates[candidateListBox.SelectedIndex];
        candidateDescriptionTextBox.Text = candidate.Description;
    }

    private void VoteButton_Click(object? sender, EventArgs e)
    {
        if (candidateListBox.SelectedIndex < 0)
        {
            statusLabel.ForeColor = Color.Firebrick;
            statusLabel.Text = "Выберите кандидата.";
            return;
        }

        var candidate = _candidates[candidateListBox.SelectedIndex];

        try
        {
            _votingService.SubmitVote(_currentUser, candidate);
            statusLabel.ForeColor = Color.SeaGreen;
            statusLabel.Text = $"Голос за {candidate.Name} учтен. Спасибо!";
            voteButton.Enabled = false;
            LoadResults();
        }
        catch (Exception ex)
        {
            statusLabel.ForeColor = Color.Firebrick;
            statusLabel.Text = ex.Message;
        }
    }

    private void LoadResults()
    {
        resultsListView.Items.Clear();
        var results = _votingService.GetResults();
        foreach (var result in results)
        {
            var item = new ListViewItem(result.Candidate.Name)
            {
                SubItems = { result.Votes.ToString() }
            };
            resultsListView.Items.Add(item);
        }
    }
}

