#nullable enable

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace VotingApp.Forms;

partial class LoginForm
{
    private IContainer? components = null!;
    private Label titleLabel = null!;
    private Label subtitleLabel = null!;
    private Label emailLabel = null!;
    private Label passwordLabel = null!;
    private TextBox emailTextBox = null!;
    private TextBox passwordTextBox = null!;
    private Button loginButton = null!;
    private Button registerButton = null!;
    private Label statusLabel = null!;
    private CheckBox showPasswordCheckBox = null!;
    private Button autofillButton = null!;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null)
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        SuspendLayout();
        components = new Container();

        titleLabel = new Label
        {
            Text = "Голосование",
            Font = new Font("Segoe UI", 20F, FontStyle.Bold, GraphicsUnit.Point),
            AutoSize = true,
            Location = new Point(36, 32)
        };

        subtitleLabel = new Label
        {
            Text = "Войдите или зарегистрируйтесь, чтобы проголосовать",
            Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point),
            AutoSize = true,
            Location = new Point(38, 85)
        };

        emailLabel = new Label
        {
            Text = "E-mail",
            Location = new Point(40, 135),
            AutoSize = true
        };

        emailTextBox = new TextBox
        {
            Location = new Point(40, 158),
            Width = 320,
            PlaceholderText = "example@mail.com"
        };

        passwordLabel = new Label
        {
            Text = "Пароль",
            Location = new Point(40, 208),
            AutoSize = true
        };

        passwordTextBox = new TextBox
        {
            Location = new Point(40, 231),
            Width = 320,
            UseSystemPasswordChar = true
        };

        showPasswordCheckBox = new CheckBox
        {
            Text = "Показать пароль",
            Location = new Point(40, 265),
            AutoSize = true
        };
        showPasswordCheckBox.CheckedChanged += TogglePasswordVisibility;

        loginButton = new Button
        {
            Text = "Войти",
            Location = new Point(40, 310),
            Width = 150,
            Height = 38
        };
        loginButton.Click += LoginButton_Click;

        registerButton = new Button
        {
            Text = "Регистрация",
            Location = new Point(210, 310),
            Width = 150,
            Height = 38
        };
        registerButton.Click += RegisterButton_Click;

        autofillButton = new Button
        {
            Text = "Автозаполнение",
            Location = new Point(40, 360),
            Width = 320,
            Height = 32
        };
        autofillButton.Click += AutofillButton_Click;

        statusLabel = new Label
        {
            AutoSize = true,
            ForeColor = Color.Firebrick,
            Location = new Point(40, 405)
        };

        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(420, 420);
        Controls.AddRange(new Control[]
        {
            titleLabel,
            subtitleLabel,
            emailLabel,
            emailTextBox,
            passwordLabel,
            passwordTextBox,
            showPasswordCheckBox,
            loginButton,
            registerButton,
            autofillButton,
            statusLabel
        });
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Вход";
        AcceptButton = loginButton;
        ResumeLayout(false);
        PerformLayout();
    }
}
