namespace BoardingHouseSys;

using BoardingHouseSys.Forms;

static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();

        // Initialize Database (Auto-fix for "Unknown Database" error)
        try
        {
            BoardingHouseSys.Data.DatabaseBootstrap.Initialize();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error initializing database: {ex.Message}\n\nPlease ensure XAMPP/MySQL is running.", "Startup Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return; // Exit if DB fails
        }

        Application.Run(new FormLogin());
    }    
}
