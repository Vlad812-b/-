using VotingApp.Forms;
using VotingApp.Utilities;

namespace VotingApp;

static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();
        AccessDatabase.EnsureDatabase();
        Application.Run(new StartForm());
    }
}