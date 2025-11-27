using System.Drawing;
using VotingApp.Services;
using VotingApp.Utilities;

namespace VotingApp.Forms;

public partial class LoginForm : Form
{
    private readonly AuthService _authService = new();

    public LoginForm()
    {
        InitializeComponent();
    }

    private void LoginButton_Click(object? sender, EventArgs e)
    {
        statusLabel.ForeColor = Color.Firebrick;
        statusLabel.Text = string.Empty;

        try
        {
            var user = _authService.Login(emailTextBox.Text, passwordTextBox.Text);
            using var votingForm = new VotingForm(user);
            Hide();
            votingForm.ShowDialog(this);
            Show();
        }
        catch (Exception ex)
        {
            statusLabel.Text = ex.Message;
        }
    }

    private void RegisterButton_Click(object? sender, EventArgs e)
    {
        using var registerForm = new RegisterForm();
        if (registerForm.ShowDialog(this) == DialogResult.OK)
        {
            emailTextBox.Text = registerForm.RegisteredEmail;
            passwordTextBox.Text = string.Empty;
            passwordTextBox.Focus();
        }
    }

    private void TogglePasswordVisibility(object? sender, EventArgs e) =>
        passwordTextBox.UseSystemPasswordChar = !showPasswordCheckBox.Checked;

    private void AutofillButton_Click(object? sender, EventArgs e)
    {
        emailTextBox.Text = DemoAccounts.ParticipantEmail;
        passwordTextBox.Text = DemoAccounts.ParticipantPassword;
        statusLabel.ForeColor = Color.SeaGreen;
        statusLabel.Text = "Данные демо-участника подставлены автоматически.";
        passwordTextBox.UseSystemPasswordChar = !showPasswordCheckBox.Checked;
    }
}
