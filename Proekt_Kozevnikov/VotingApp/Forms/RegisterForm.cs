using VotingApp.Services;

namespace VotingApp.Forms;

public partial class RegisterForm : Form
{
    private readonly AuthService _authService = new();

    public string RegisteredEmail { get; private set; } = string.Empty;

    public RegisterForm()
    {
        InitializeComponent();
    }

    private void RegisterButton_Click(object? sender, EventArgs e)
    {
        statusLabel.Text = string.Empty;

        try
        {
            if (passwordTextBox.Text != confirmPasswordTextBox.Text)
            {
                throw new InvalidOperationException("Пароли не совпадают.");
            }

            var user = _authService.Register(fullNameTextBox.Text, emailTextBox.Text, passwordTextBox.Text);
            RegisteredEmail = user.Email;
            DialogResult = DialogResult.OK;
            Close();
        }
        catch (Exception ex)
        {
            statusLabel.Text = ex.Message;
        }
    }

    private void CancelButton_Click(object? sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
        Close();
    }
}







