using System;
using MySql.Data.MySqlClient;
using BoardingHouseSys.Services;

namespace BoardingHouseSys.Data
{
    public static class DatabaseBootstrap
    {
        private static string _serverConnection = "Server=localhost;Uid=root;Pwd=root;";
        private static string _dbConnection = "Server=localhost;Database=BoardingHouseDB;Uid=root;Pwd=root;";

        public static void Initialize()
        {
            InitializeInternal(_serverConnection, _dbConnection, "superadmin", "password123", "admin", "password123");
        }

        public static void Install(string baseConnection, string databaseName, string superAdminUsername, string superAdminPassword, string? adminUsername = null, string? adminPassword = null)
        {
            string serverConn = baseConnection;
            string dbConn = baseConnection + $"Database={databaseName};";
            InitializeInternal(serverConn, dbConn, superAdminUsername, superAdminPassword, adminUsername, adminPassword);
        }

        private static void InitializeInternal(string serverConnection, string dbConnection, string superAdminUsername, string superAdminPassword, string? adminUsername, string? adminPassword)
        {
            try
            {
                using (var conn = new MySqlConnection(serverConnection))
                {
                    conn.Open();
                    var cmd = new MySqlCommand("CREATE DATABASE IF NOT EXISTS BoardingHouseDB;", conn);
                    cmd.ExecuteNonQuery();
                }

                using (var conn = new MySqlConnection(dbConnection))
                {
                    conn.Open();
                    
                    string sqlUsers = @"
                        CREATE TABLE IF NOT EXISTS Users (
                            Id INT PRIMARY KEY AUTO_INCREMENT,
                            Username VARCHAR(50) NOT NULL UNIQUE,
                            PasswordHash VARCHAR(255) NOT NULL,
                            Role VARCHAR(20) NOT NULL,
                            IsActive BIT DEFAULT 1,
                            CreatedAt DATETIME DEFAULT NOW()
                        );";
                    new MySqlCommand(sqlUsers, conn).ExecuteNonQuery();

                    string sqlRooms = @"
                        CREATE TABLE IF NOT EXISTS Rooms (
                            Id INT PRIMARY KEY AUTO_INCREMENT,
                            RoomNumber VARCHAR(20) NOT NULL UNIQUE,
                            Capacity INT NOT NULL,
                            MonthlyRate DECIMAL(10, 2) NOT NULL,
                            IsActive BIT DEFAULT 1,
                            CreatedAt DATETIME DEFAULT NOW()
                        );";
                    new MySqlCommand(sqlRooms, conn).ExecuteNonQuery();

                    string sqlBoarders = @"
                        CREATE TABLE IF NOT EXISTS Boarders (
                            Id INT PRIMARY KEY AUTO_INCREMENT,
                            UserId INT NULL,
                            FullName VARCHAR(100) NOT NULL,
                            Address VARCHAR(255),
                            Phone VARCHAR(20),
                            RoomId INT NULL,
                            IsActive BIT DEFAULT 1,
                            CreatedAt DATETIME DEFAULT NOW(),
                            FOREIGN KEY (UserId) REFERENCES Users(Id),
                            FOREIGN KEY (RoomId) REFERENCES Rooms(Id)
                        );";
                    new MySqlCommand(sqlBoarders, conn).ExecuteNonQuery();

                    string sqlPayments = @"
                        CREATE TABLE IF NOT EXISTS Payments (
                            Id INT PRIMARY KEY AUTO_INCREMENT,
                            BoarderId INT NOT NULL,
                            Amount DECIMAL(10, 2) NOT NULL,
                            PaymentDate DATETIME DEFAULT NOW(),
                            MonthPaid VARCHAR(20) NOT NULL,
                            YearPaid INT NOT NULL,
                            Status VARCHAR(20) DEFAULT 'Pending',
                            Notes VARCHAR(255),
                            FOREIGN KEY (BoarderId) REFERENCES Boarders(Id)
                        );";
                    new MySqlCommand(sqlPayments, conn).ExecuteNonQuery();

                    string sqlBH = @"
                        CREATE TABLE IF NOT EXISTS BoardingHouses (
                            Id INT PRIMARY KEY AUTO_INCREMENT,
                            OwnerId INT NOT NULL,
                            Name VARCHAR(100) NOT NULL,
                            Address VARCHAR(255),
                            Description TEXT,
                            Rules TEXT,
                            Amenities TEXT,
                            ImagePath1 VARCHAR(500) NULL,
                            ImagePath2 VARCHAR(500) NULL,
                            ImagePath3 VARCHAR(500) NULL,
                            IsActive BIT DEFAULT 1,
                            CreatedAt DATETIME DEFAULT NOW(),
                            FOREIGN KEY (OwnerId) REFERENCES Users(Id)
                        );";
                    new MySqlCommand(sqlBH, conn).ExecuteNonQuery();

                    try {
                        string alterRooms = "ALTER TABLE Rooms ADD COLUMN BoardingHouseId INT NULL;";
                        new MySqlCommand(alterRooms, conn).ExecuteNonQuery();
                        string fkRooms = "ALTER TABLE Rooms ADD FOREIGN KEY (BoardingHouseId) REFERENCES BoardingHouses(Id);";
                        new MySqlCommand(fkRooms, conn).ExecuteNonQuery();
                    } catch (Exception) { }

                    try {
                        string dropUniqueRoomNumber = "ALTER TABLE Rooms DROP INDEX RoomNumber;";
                        new MySqlCommand(dropUniqueRoomNumber, conn).ExecuteNonQuery();
                    } catch (Exception) { }

                    try {
                        string addCompositeUnique = "ALTER TABLE Rooms ADD UNIQUE KEY UX_Rooms_BH_Room (BoardingHouseId, RoomNumber);";
                        new MySqlCommand(addCompositeUnique, conn).ExecuteNonQuery();
                    } catch (Exception) { }

                    try {
                        string alterBoarders = "ALTER TABLE Boarders ADD COLUMN BoardingHouseId INT NULL;";
                        new MySqlCommand(alterBoarders, conn).ExecuteNonQuery();
                        string fkBoarders = "ALTER TABLE Boarders ADD FOREIGN KEY (BoardingHouseId) REFERENCES BoardingHouses(Id);";
                        new MySqlCommand(fkBoarders, conn).ExecuteNonQuery();
                    } catch (Exception) { /* Column likely exists */ }

                    try {
                        string alterBoardersPic = "ALTER TABLE Boarders ADD COLUMN ProfilePicturePath VARCHAR(500) NULL;";
                        new MySqlCommand(alterBoardersPic, conn).ExecuteNonQuery();
                    } catch (Exception) { /* Column likely exists */ }

                    try {
                        string alterBhPic1 = "ALTER TABLE BoardingHouses ADD COLUMN ImagePath1 VARCHAR(500) NULL;";
                        new MySqlCommand(alterBhPic1, conn).ExecuteNonQuery();
                    } catch (Exception) { /* Column likely exists */ }
                    try {
                        string alterBhPic2 = "ALTER TABLE BoardingHouses ADD COLUMN ImagePath2 VARCHAR(500) NULL;";
                        new MySqlCommand(alterBhPic2, conn).ExecuteNonQuery();
                    } catch (Exception) { /* Column likely exists */ }
                    try {
                        string alterBhPic3 = "ALTER TABLE BoardingHouses ADD COLUMN ImagePath3 VARCHAR(500) NULL;";
                        new MySqlCommand(alterBhPic3, conn).ExecuteNonQuery();
                    } catch (Exception) { /* Column likely exists */ }

                    var checkBH = new MySqlCommand("SELECT COUNT(*) FROM BoardingHouses", conn);
                    long bhCount = (long)checkBH.ExecuteScalar();

                    if (bhCount == 0)
                    {
                        // Get the first Admin ID
                        var cmdGetAdmin = new MySqlCommand("SELECT Id FROM Users WHERE Role IN ('Admin', 'SuperAdmin') LIMIT 1", conn);
                        object? adminIdObj = cmdGetAdmin.ExecuteScalar();
                        
                        if (adminIdObj != null)
                        {
                            int adminId = Convert.ToInt32(adminIdObj);
                            
                            string seedBH = @"
                                INSERT INTO BoardingHouses (OwnerId, Name, Address, Description) 
                                VALUES (@OwnerId, 'My Main Boarding House', 'Default Address', 'Main Property');
                                SELECT LAST_INSERT_ID();";
                            
                            var cmdSeed = new MySqlCommand(seedBH, conn);
                            cmdSeed.Parameters.AddWithValue("@OwnerId", adminId);
                            int newBhId = Convert.ToInt32(cmdSeed.ExecuteScalar());

                            new MySqlCommand($"UPDATE Rooms SET BoardingHouseId = {newBhId} WHERE BoardingHouseId IS NULL", conn).ExecuteNonQuery();
                            new MySqlCommand($"UPDATE Boarders SET BoardingHouseId = {newBhId} WHERE BoardingHouseId IS NULL", conn).ExecuteNonQuery();
                        }
                    }

                    var checkCmd = new MySqlCommand("SELECT COUNT(*) FROM Users", conn);
                    long userCount = (long)checkCmd.ExecuteScalar();

                    if (userCount == 0)
                    {
                        string superHash = SecurityHelper.HashPassword(superAdminPassword);
                        string seedUsers = @"
                            INSERT INTO Users (Username, PasswordHash, Role) VALUES (@SuperUser, @SuperHash, 'SuperAdmin');";

                        bool addAdmin = !string.IsNullOrWhiteSpace(adminUsername) && !string.IsNullOrWhiteSpace(adminPassword);
                        if (addAdmin)
                        {
                            seedUsers += @"
                            INSERT INTO Users (Username, PasswordHash, Role) VALUES (@AdminUser, @AdminHash, 'Admin');";
                        }

                        var cmdSeedUsers = new MySqlCommand(seedUsers, conn);
                        cmdSeedUsers.Parameters.AddWithValue("@SuperUser", superAdminUsername);
                        cmdSeedUsers.Parameters.AddWithValue("@SuperHash", superHash);
                        if (addAdmin)
                        {
                            string adminHash = SecurityHelper.HashPassword(adminPassword!);
                            cmdSeedUsers.Parameters.AddWithValue("@AdminUser", adminUsername);
                            cmdSeedUsers.Parameters.AddWithValue("@AdminHash", adminHash);
                        }
                        cmdSeedUsers.ExecuteNonQuery();

                        string seedRooms = @"
                            INSERT INTO Rooms (RoomNumber, Capacity, MonthlyRate) VALUES ('101', 2, 5000.00);
                            INSERT INTO Rooms (RoomNumber, Capacity, MonthlyRate) VALUES ('102', 4, 3500.00);
                            INSERT INTO Rooms (RoomNumber, Capacity, MonthlyRate) VALUES ('201', 1, 8000.00);";
                        new MySqlCommand(seedRooms, conn).ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Database Initialization Failed: " + ex.Message);
            }
        }
    }
}
