#nullable enable

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace VotingApp.Forms;

partial class StartForm
{
    private IContainer? components = null!;
    private Label titleLabel = null!;
    private Button userButton = null!;
    private Button adminButton = null!;
    private Button registerButton = null!;
    private Label descriptionLabel = null!;

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
            Text = "Выберите режим работы",
            Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point),
            AutoSize = true,
            Location = new Point(40, 30)
        };

        descriptionLabel = new Label
        {
            Text = "Пользователи голосуют, администратор управляет кандидатами и следит за результатами.",
            AutoSize = true,
            MaximumSize = new Size(420, 0),
            Location = new Point(40, 80)
        };

        userButton = new Button
        {
            Text = "Войти как участник",
            Location = new Point(40, 150),
            Size = new Size(200, 44)
        };
        userButton.Click += OpenUserLogin;

        adminButton = new Button
        {
            Text = "Войти как администратор",
            Location = new Point(260, 150),
            Size = new Size(200, 44)
        };
        adminButton.Click += OpenAdminLogin;

        registerButton = new Button
        {
            Text = "Зарегистрировать участника",
            Location = new Point(40, 210),
            Size = new Size(420, 42)
        };
        registerButton.Click += OpenRegistration;

        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(520, 280);
        Controls.AddRange(new Control[]
        {
            titleLabel,
            descriptionLabel,
            userButton,
            adminButton,
            registerButton
        });
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Голосование — выбор роли";
        ResumeLayout(false);
        PerformLayout();
    }
}







