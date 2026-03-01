using System;
using System.IO;

namespace BoardingHouseSys.Data
{
    public static class AppConfig
    {
        public static string GetConfigPath()
        {
            string folder = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
                "BoardingHouseSys");

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            return Path.Combine(folder, "db_config.txt");
        }
    }
}

