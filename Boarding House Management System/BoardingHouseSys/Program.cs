namespace BoardingHouseSys;

using System.Data;
using BoardingHouseSys.Forms;
using BoardingHouseSys.Data;
using MySql.Data.MySqlClient;

static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();

        Application.ThreadException += (s, e) => HandleException(e.Exception);
        AppDomain.CurrentDomain.UnhandledException += (s, e) => HandleException(e.ExceptionObject as Exception);

        string? connFromConfig = TryLoadConnectionFromConfig();

        if (connFromConfig == null)
        {
            using (var setup = new FormConnection())
            {
                setup.ShowDialog();
            }

            connFromConfig = TryLoadConnectionFromConfig();
            if (connFromConfig == null) return;
        }

        DatabaseHelper.SetConnectionString(connFromConfig);

        try
        {
            DatabaseBootstrap.Initialize();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error initializing database: {ex.Message}\n\nPlease ensure MySQL is running and your connection settings are correct.", "Startup Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        if (IsFirstRun(connFromConfig))
        {
            using (var setup = new FormConnection())
            {
                setup.ShowDialog();
            }
        }

        Application.Run(new FormLogin());
    }

    static string? TryLoadConnectionFromConfig()
    {
        string path = AppConfig.GetConfigPath();
        if (!File.Exists(path)) return null;

        try
        {
            string[] lines = File.ReadAllLines(path);
            if (lines.Length < 4) return null;

            string server = lines[0].Split('=')[1];
            string db = lines[1].Split('=')[1];
            string uid = lines[2].Split('=')[1];
            string pwd = lines[3].Split('=')[1];

            return $"Server={server};Database={db};Uid={uid};Pwd={pwd};";
        }
        catch
        {
            return null;
        }
    }

    static bool IsFirstRun(string connectionString)
    {
        try
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                var cmd = new MySqlCommand("SELECT COUNT(*) FROM Users", conn);
                long count = Convert.ToInt64(cmd.ExecuteScalar());
                return count == 0;
            }
        }
        catch
        {
            return false;
        }
    }

    static void HandleException(Exception? ex)
    {
        if (ex == null) return;
        MessageBox.Show($"An unexpected error occurred:\n{ex.Message}\n\n{ex.StackTrace}", "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}
