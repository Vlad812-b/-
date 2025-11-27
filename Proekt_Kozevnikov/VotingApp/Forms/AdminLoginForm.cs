using System.Drawing;
using System.Windows.Forms;
using VotingApp.Services;
using VotingApp.Utilities;

namespace VotingApp.Forms;

public partial class AdminLoginForm : Form
{
    private readonly AuthService _authService = new();

    public AdminLoginForm()
    {
        InitializeComponent();
    }

    private void LoginButton_Click(object? sender, EventArgs e)
    {
        statusLabel.ForeColor = Color.Firebrick;
        statusLabel.Text = string.Empty;

        try
        {
            var admin = _authService.LoginAdmin(emailTextBox.Text, passwordTextBox.Text);
            using var dashboard = new AdminDashboardForm(admin);
            Hide();
            dashboard.ShowDialog(this);
            Show();
        }
        catch (Exception ex)
        {
            statusLabel.Text = ex.Message;
        }
    }

    private void CloseButton_Click(object? sender, EventArgs e) => Close();

    private void TogglePasswordVisibility(object? sender, EventArgs e) =>
        passwordTextBox.UseSystemPasswordChar = !showPasswordCheckBox.Checked;

    private void AutofillButton_Click(object? sender, EventArgs e)
    {
        emailTextBox.Text = DemoAccounts.AdminEmail;
        passwordTextBox.Text = DemoAccounts.AdminPassword;
        statusLabel.ForeColor = Color.SeaGreen;
        statusLabel.Text = "Данные администратора заполнены автоматически.";
        passwordTextBox.UseSystemPasswordChar = !showPasswordCheckBox.Checked;
    }
}

