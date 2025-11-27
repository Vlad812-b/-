#nullable enable

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace VotingApp.Forms;

partial class AdminDashboardForm
{
    private IContainer? components = null!;
    private Label titleLabel = null!;
    private Label adminNameLabel = null!;
    private GroupBox resultsGroupBox = null!;
    private ListView resultsListView = null!;
    private ColumnHeader candidateColumn = null!;
    private ColumnHeader votesColumn = null!;
    private GroupBox managementGroupBox = null!;
    private ListBox candidatesListBox = null!;
    private TextBox candidateNameTextBox = null!;
    private TextBox candidateDescriptionTextBox = null!;
    private Button addCandidateButton = null!;
    private Button refreshButton = null!;
    private Button resetVotesButton = null!;
    private Label candidateNameLabel = null!;
    private Label candidateDescriptionLabel = null!;
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
            Text = "Панель администратора",
            Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point),
            AutoSize = true,
            Location = new Point(24, 20)
        };

        adminNameLabel = new Label
        {
            Text = "Администратор",
            AutoSize = true,
            Location = new Point(24, 60)
        };

        resultsGroupBox = new GroupBox
        {
            Text = "Текущие результаты",
            Location = new Point(24, 90),
            Size = new Size(360, 300)
        };

        resultsListView = new ListView
        {
            View = View.Details,
            FullRowSelect = true,
            GridLines = true,
            Location = new Point(12, 22),
            Size = new Size(330, 260)
        };
        candidateColumn = new ColumnHeader { Text = "Кандидат", Width = 220 };
        votesColumn = new ColumnHeader { Text = "Голоса", Width = 100 };
        resultsListView.Columns.AddRange(new[] { candidateColumn, votesColumn });
        resultsGroupBox.Controls.Add(resultsListView);

        managementGroupBox = new GroupBox
        {
            Text = "Управление кандидатами",
            Location = new Point(400, 90),
            Size = new Size(360, 300)
        };

        candidatesListBox = new ListBox { Location = new Point(12, 22), Size = new Size(330, 100) };
        candidateNameLabel = new Label { Text = "Имя кандидата", AutoSize = true, Location = new Point(12, 135) };
        candidateNameTextBox = new TextBox { Location = new Point(12, 158), Width = 330 };
        candidateDescriptionLabel = new Label { Text = "Описание", AutoSize = true, Location = new Point(12, 195) };
        candidateDescriptionTextBox = new TextBox
        {
            Location = new Point(12, 218),
            Size = new Size(330, 50),
            Multiline = true
        };
        addCandidateButton = new Button { Text = "Добавить", Location = new Point(12, 275), Size = new Size(150, 30) };
        addCandidateButton.Click += AddCandidateButton_Click;
        managementGroupBox.Controls.AddRange(new Control[]
        {
            candidatesListBox,
            candidateNameLabel,
            candidateNameTextBox,
            candidateDescriptionLabel,
            candidateDescriptionTextBox,
            addCandidateButton
        });

        refreshButton = new Button { Text = "Обновить данные", Location = new Point(24, 410), Size = new Size(170, 36) };
        refreshButton.Click += RefreshButton_Click;

        resetVotesButton = new Button { Text = "Сбросить голоса", Location = new Point(210, 410), Size = new Size(170, 36) };
        resetVotesButton.Click += ResetVotesButton_Click;

        statusLabel = new Label { AutoSize = true, ForeColor = Color.SeaGreen, Location = new Point(24, 460) };

        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(790, 500);
        Controls.AddRange(new Control[]
        {
            titleLabel,
            adminNameLabel,
            resultsGroupBox,
            managementGroupBox,
            refreshButton,
            resetVotesButton,
            statusLabel
        });
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        StartPosition = FormStartPosition.CenterParent;
        Text = "Администрирование голосования";
        Load += AdminDashboardForm_Load;
        ResumeLayout(false);
        PerformLayout();
    }
}







