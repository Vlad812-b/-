#nullable enable

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace VotingApp.Forms;

partial class AdminLoginForm
{
    private IContainer? components = null!;
    private Label titleLabel = null!;
    private Label emailLabel = null!;
    private Label passwordLabel = null!;
    private TextBox emailTextBox = null!;
    private TextBox passwordTextBox = null!;
    private CheckBox showPasswordCheckBox = null!;
    private Button loginButton = null!;
    private Button closeButton = null!;
    private Label statusLabel = null!;
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
            Text = "Вход администратора",
            Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point),
            AutoSize = true,
            Location = new Point(32, 24)
        };

        emailLabel = new Label { Text = "E-mail", AutoSize = true, Location = new Point(34, 80) };
        emailTextBox = new TextBox { Location = new Point(34, 103), Width = 300 };

        passwordLabel = new Label { Text = "Пароль", AutoSize = true, Location = new Point(34, 150) };
        passwordTextBox = new TextBox { Location = new Point(34, 173), Width = 300, UseSystemPasswordChar = true };

        showPasswordCheckBox = new CheckBox { Text = "Показать пароль", AutoSize = true, Location = new Point(34, 205) };
        showPasswordCheckBox.CheckedChanged += TogglePasswordVisibility;

        loginButton = new Button { Text = "Войти", Location = new Point(34, 245), Size = new Size(140, 36) };
        loginButton.Click += LoginButton_Click;

        closeButton = new Button { Text = "Закрыть", Location = new Point(194, 245), Size = new Size(140, 36) };
        closeButton.Click += CloseButton_Click;

        autofillButton = new Button { Text = "Автозаполнение", Location = new Point(34, 290), Size = new Size(300, 32) };
        autofillButton.Click += AutofillButton_Click;

        statusLabel = new Label { AutoSize = true, ForeColor = Color.Firebrick, Location = new Point(34, 330) };

        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(370, 365);
        Controls.AddRange(new Control[]
        {
            titleLabel,
            emailLabel,
            emailTextBox,
            passwordLabel,
            passwordTextBox,
            showPasswordCheckBox,
            loginButton,
            closeButton,
            autofillButton,
            statusLabel
        });
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        StartPosition = FormStartPosition.CenterParent;
        Text = "Администратор";
        AcceptButton = loginButton;
        ResumeLayout(false);
        PerformLayout();
    }
}







