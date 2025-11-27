using System.Drawing;
using System.Windows.Forms;
using VotingApp.Models;
using VotingApp.Services;

namespace VotingApp.Forms;

public partial class AdminDashboardForm : Form
{
    private readonly User _admin;
    private readonly AdminService _adminService = new();

    public AdminDashboardForm(User admin)
    {
        _admin = admin;
        InitializeComponent();
        adminNameLabel.Text = $"Администратор: {_admin.FullName}";
    }

    private void AdminDashboardForm_Load(object? sender, EventArgs e)
    {
        LoadResults();
        LoadCandidates();
    }

    private void LoadResults()
    {
        resultsListView.Items.Clear();
        foreach (var result in _adminService.GetResults())
        {
            var item = new ListViewItem(result.Candidate.Name)
            {
                SubItems = { result.Votes.ToString() }
            };
            resultsListView.Items.Add(item);
        }
    }

    private void LoadCandidates()
    {
        candidatesListBox.Items.Clear();
        foreach (var candidate in _adminService.GetCandidates())
        {
            candidatesListBox.Items.Add(candidate.Name);
        }
    }

    private void AddCandidateButton_Click(object? sender, EventArgs e)
    {
        statusLabel.ForeColor = Color.Firebrick;
        statusLabel.Text = string.Empty;

        try
        {
            _adminService.AddCandidate(candidateNameTextBox.Text, candidateDescriptionTextBox.Text);
            candidateNameTextBox.Text = string.Empty;
            candidateDescriptionTextBox.Text = string.Empty;
            statusLabel.ForeColor = Color.SeaGreen;
            statusLabel.Text = "Кандидат добавлен.";
            LoadCandidates();
            LoadResults();
        }
        catch (Exception ex)
        {
            statusLabel.Text = ex.Message;
        }
    }

    private void RefreshButton_Click(object? sender, EventArgs e)
    {
        LoadResults();
        LoadCandidates();
        statusLabel.ForeColor = Color.SeaGreen;
        statusLabel.Text = "Данные обновлены.";
    }

    private void ResetVotesButton_Click(object? sender, EventArgs e)
    {
        if (MessageBox.Show(
                "Очистить все голоса? Это действие нельзя отменить.",
                "Подтверждение",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning) != DialogResult.Yes)
        {
            return;
        }

        _adminService.ResetVotes();
        LoadResults();
        statusLabel.ForeColor = Color.SeaGreen;
        statusLabel.Text = "Голоса очищены.";
    }
}

