#nullable enable

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace VotingApp.Forms;

partial class VotingForm
{
    private IContainer? components = null!;
    private Label welcomeLabel = null!;
    private Label instructionLabel = null!;
    private ListBox candidateListBox = null!;
    private TextBox candidateDescriptionTextBox = null!;
    private Button voteButton = null!;
    private GroupBox resultsGroupBox = null!;
    private ListView resultsListView = null!;
    private ColumnHeader candidateColumn = null!;
    private ColumnHeader votesColumn = null!;
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

        welcomeLabel = new Label
        {
            Text = "Здравствуйте",
            Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point),
            AutoSize = true,
            Location = new Point(24, 20)
        };

        instructionLabel = new Label
        {
            Text = "Выберите кандидата и нажмите «Проголосовать».",
            AutoSize = true,
            Location = new Point(24, 55)
        };

        candidateListBox = new ListBox
        {
            Location = new Point(24, 88),
            Size = new Size(220, 184)
        };
        candidateListBox.SelectedIndexChanged += CandidateListBox_SelectedIndexChanged;

        candidateDescriptionTextBox = new TextBox
        {
            Location = new Point(260, 88),
            Size = new Size(240, 184),
            Multiline = true,
            ReadOnly = true,
            BorderStyle = BorderStyle.FixedSingle
        };

        voteButton = new Button
        {
            Text = "Проголосовать",
            Location = new Point(24, 290),
            Size = new Size(180, 40)
        };
        voteButton.Click += VoteButton_Click;

        statusLabel = new Label
        {
            AutoSize = true,
            Location = new Point(24, 340),
            ForeColor = Color.Firebrick
        };

        resultsGroupBox = new GroupBox
        {
            Text = "Промежуточные результаты",
            Location = new Point(24, 375),
            Size = new Size(476, 180)
        };

        resultsListView = new ListView
        {
            View = View.Details,
            FullRowSelect = true,
            GridLines = true,
            Location = new Point(12, 22),
            Size = new Size(452, 144)
        };

        candidateColumn = new ColumnHeader { Text = "Кандидат", Width = 280 };
        votesColumn = new ColumnHeader { Text = "Голоса", Width = 140 };
        resultsListView.Columns.AddRange(new[] { candidateColumn, votesColumn });

        resultsGroupBox.Controls.Add(resultsListView);

        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(530, 580);
        Controls.AddRange(new Control[]
        {
            welcomeLabel,
            instructionLabel,
            candidateListBox,
            candidateDescriptionTextBox,
            voteButton,
            statusLabel,
            resultsGroupBox
        });
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        StartPosition = FormStartPosition.CenterParent;
        Text = "Голосование";
        Load += VotingForm_Load;
        ResumeLayout(false);
        PerformLayout();
    }
}

