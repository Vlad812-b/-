using System.Data;
using System.Data.OleDb;
using System.Runtime.InteropServices;
using VotingApp.Models;

namespace VotingApp.Utilities;

internal static class AccessDatabase
{
    private const int SchemaVersion = 2;
    private static readonly string AppDataDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data");
    private static readonly string DatabasePath = Path.Combine(AppDataDirectory, "VotingApp.accdb");
    private static readonly string VersionFilePath = Path.Combine(AppDataDirectory, "schema.version");
    private static readonly string ConnectionString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={DatabasePath};Persist Security Info=False;";
    private static bool _initialized;

    public static string GetConnectionString()
    {
        EnsureDatabase();
        return ConnectionString;
    }

    public static void EnsureDatabase()
    {
        if (_initialized)
        {
            return;
        }

        Directory.CreateDirectory(AppDataDirectory);
        EnsureDatabaseFile();

        EnsureSchema();
        _initialized = true;
    }

    private static void CreateAccessFile()
    {
        var catalogType = Type.GetTypeFromProgID("ADOX.Catalog");

        if (catalogType == null)
        {
            throw new InvalidOperationException(
                "ACE OLEDB провайдер не установлен. Установите 'Microsoft Access Database Engine Redistributable'.");
        }

        dynamic catalog = Activator.CreateInstance(catalogType)!;

        try
        {
            catalog.Create($"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={DatabasePath};Jet OLEDB:Engine Type=5;");
        }
        finally
        {
            Marshal.FinalReleaseComObject(catalog);
        }

        WriteSchemaVersion(SchemaVersion);
    }

    private static void EnsureSchema()
    {
        using var connection = new OleDbConnection(ConnectionString);
        connection.Open();

        EnsureUsersTable(connection);
        EnsureCandidatesTable(connection);
        EnsureVotesTable(connection);
        EnsureDemoParticipant(connection);

        if (!HasAnyCandidates(connection))
        {
            SeedCandidates(connection);
        }
    }

    private static void EnsureDatabaseFile()
    {
        var currentVersion = ReadSchemaVersion();

        if (!File.Exists(DatabasePath))
        {
            CreateAccessFile();
            return;
        }

        if (currentVersion >= SchemaVersion)
        {
            return;
        }

        try
        {
            File.Delete(DatabasePath);
        }
        catch
        {
            // If file can't be deleted (e.g., open in Access), rethrow with helpful message.
            throw new InvalidOperationException("Закройте файл базы данных VotingApp.accdb и перезапустите приложение.");
        }

        CreateAccessFile();
    }

    private static int ReadSchemaVersion()
    {
        if (!File.Exists(VersionFilePath))
        {
            return 0;
        }

        var content = File.ReadAllText(VersionFilePath).Trim();
        return int.TryParse(content, out var version) ? version : 0;
    }

    private static void WriteSchemaVersion(int version)
    {
        File.WriteAllText(VersionFilePath, version.ToString());
    }

    private static void EnsureUsersTable(OleDbConnection connection)
    {
        if (TableExists(connection, "Users"))
        {
            return;
        }

        const string sql = """
            CREATE TABLE Users (
                Id AUTOINCREMENT PRIMARY KEY,
                FullName TEXT(255) NOT NULL,
                Email TEXT(255) NOT NULL UNIQUE,
                PasswordHash TEXT(255) NOT NULL,
                CreatedAt DATETIME DEFAULT NOW()
            )
            """;

        ExecuteNonQuery(connection, sql);
    }

    private static void EnsureCandidatesTable(OleDbConnection connection)
    {
        if (TableExists(connection, "Candidates"))
        {
            return;
        }

        const string sql = """
            CREATE TABLE Candidates (
                Id AUTOINCREMENT PRIMARY KEY,
                Name TEXT(255) NOT NULL,
                Description MEMO NULL
            )
            """;

        ExecuteNonQuery(connection, sql);
    }

    private static void EnsureVotesTable(OleDbConnection connection)
    {
        if (TableExists(connection, "Votes"))
        {
            return;
        }

        const string sql = """
            CREATE TABLE Votes (
                Id AUTOINCREMENT PRIMARY KEY,
                UserId LONG NOT NULL,
                CandidateId LONG NOT NULL,
                VoteDate DATETIME DEFAULT NOW(),
                CONSTRAINT FK_Votes_Users FOREIGN KEY (UserId) REFERENCES Users(Id),
                CONSTRAINT FK_Votes_Candidates FOREIGN KEY (CandidateId) REFERENCES Candidates(Id)
            )
            """;

        ExecuteNonQuery(connection, sql);

        const string uniqueSql = "CREATE UNIQUE INDEX UX_Votes_User ON Votes(UserId)";
        ExecuteNonQuery(connection, uniqueSql);
    }

    private static bool TableExists(OleDbConnection connection, string tableName)
    {
        var restrictions = new string[4];
        restrictions[2] = tableName;
        using DataTable? schema = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, restrictions);
        return schema is { Rows.Count: > 0 };
    }

    private static void ExecuteNonQuery(OleDbConnection connection, string sql)
    {
        using var command = new OleDbCommand(sql, connection);
        command.ExecuteNonQuery();
    }

    private static bool HasAnyCandidates(OleDbConnection connection)
    {
        using var command = new OleDbCommand("SELECT COUNT(*) FROM Candidates", connection);
        var count = Convert.ToInt32(command.ExecuteScalar());
        return count > 0;
    }

    private static void SeedCandidates(OleDbConnection connection)
    {
        var candidates = new[]
        {
            new Candidate { Name = "Андрей Иванов", Description = "За экологию и чистый город" },
            new Candidate { Name = "Мария Петрова", Description = "Поддержка образования и молодежи" },
            new Candidate { Name = "Сергей Смирнов", Description = "Развитие спорта и общественных пространств" }
        };

        foreach (var candidate in candidates)
        {
            using var command = new OleDbCommand("INSERT INTO Candidates (Name, Description) VALUES (@name, @description)", connection);
            command.Parameters.AddWithValue("@name", candidate.Name);
            command.Parameters.AddWithValue("@description", candidate.Description);
            command.ExecuteNonQuery();
        }
    }

    private static void EnsureDemoParticipant(OleDbConnection connection)
    {
        using var check = new OleDbCommand("SELECT COUNT(*) FROM Users WHERE Email = ?", connection);
        check.Parameters.AddWithValue("@email", DemoAccounts.ParticipantEmail);
        var count = Convert.ToInt32(check.ExecuteScalar());
        if (count > 0)
        {
            return;
        }

        using var insert = new OleDbCommand(
            "INSERT INTO Users (FullName, Email, PasswordHash, CreatedAt) VALUES (?, ?, ?, ?)",
            connection);
        insert.Parameters.AddWithValue("@fullName", "Демонстрационный участник");
        insert.Parameters.AddWithValue("@email", DemoAccounts.ParticipantEmail);
        insert.Parameters.AddWithValue("@passwordHash", PasswordHasher.Hash(DemoAccounts.ParticipantPassword));
        insert.Parameters.Add("@createdAt", OleDbType.Date).Value = DateTime.Now;
        insert.ExecuteNonQuery();
    }
}

