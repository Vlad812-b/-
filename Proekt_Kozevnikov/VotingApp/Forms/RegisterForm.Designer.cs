#nullable enable

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace VotingApp.Forms;

partial class RegisterForm
{
    private IContainer? components = null!;
    private Label titleLabel = null!;
    private Label fullNameLabel = null!;
    private Label emailLabel = null!;
    private Label passwordLabel = null!;
    private Label confirmPasswordLabel = null!;
    private TextBox fullNameTextBox = null!;
    private TextBox emailTextBox = null!;
    private TextBox passwordTextBox = null!;
    private TextBox confirmPasswordTextBox = null!;
    private Button registerButton = null!;
    private Button cancelButton = null!;
    private Label statusLabel = null!;

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
            Text = "Регистрация",
            Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point),
            AutoSize = true,
            Location = new Point(32, 24)
        };

        fullNameLabel = new Label { Text = "ФИО", AutoSize = true, Location = new Point(34, 75) };
        fullNameTextBox = new TextBox { Location = new Point(34, 98), Width = 360 };

        emailLabel = new Label { Text = "E-mail", AutoSize = true, Location = new Point(34, 138) };
        emailTextBox = new TextBox { Location = new Point(34, 161), Width = 360 };

        passwordLabel = new Label { Text = "Пароль", AutoSize = true, Location = new Point(34, 201) };
        passwordTextBox = new TextBox { Location = new Point(34, 224), Width = 360, UseSystemPasswordChar = true };

        confirmPasswordLabel = new Label { Text = "Повторите пароль", AutoSize = true, Location = new Point(34, 264) };
        confirmPasswordTextBox = new TextBox { Location = new Point(34, 287), Width = 360, UseSystemPasswordChar = true };

        registerButton = new Button { Text = "Создать", Location = new Point(34, 332), Width = 150, Height = 36 };
        registerButton.Click += RegisterButton_Click;

        cancelButton = new Button { Text = "Отмена", Location = new Point(244, 332), Width = 150, Height = 36 };
        cancelButton.Click += CancelButton_Click;

        statusLabel = new Label { AutoSize = true, ForeColor = Color.Firebrick, Location = new Point(34, 380) };

        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(430, 420);
        Controls.AddRange(new Control[]
        {
            titleLabel,
            fullNameLabel,
            fullNameTextBox,
            emailLabel,
            emailTextBox,
            passwordLabel,
            passwordTextBox,
            confirmPasswordLabel,
            confirmPasswordTextBox,
            registerButton,
            cancelButton,
            statusLabel
        });
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        StartPosition = FormStartPosition.CenterParent;
        Text = "Регистрация нового участника";
        AcceptButton = registerButton;
        ResumeLayout(false);
        PerformLayout();
    }
}

