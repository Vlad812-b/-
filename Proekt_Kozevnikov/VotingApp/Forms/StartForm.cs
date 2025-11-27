using System.Drawing;
using System.Windows.Forms;

namespace VotingApp.Forms;

public partial class StartForm : Form
{
    public StartForm()
    {
        InitializeComponent();
    }

    private void OpenUserLogin(object? sender, EventArgs e)
    {
        using var loginForm = new LoginForm();
        Hide();
        loginForm.ShowDialog(this);
        Show();
    }

    private void OpenAdminLogin(object? sender, EventArgs e)
    {
        using var adminLogin = new AdminLoginForm();
        Hide();
        adminLogin.ShowDialog(this);
        Show();
    }

    private void OpenRegistration(object? sender, EventArgs e)
    {
        using var registerForm = new RegisterForm();
        registerForm.ShowDialog(this);
    }
}

