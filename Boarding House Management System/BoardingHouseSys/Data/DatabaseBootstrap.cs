using System;
using MySql.Data.MySqlClient;

namespace BoardingHouseSys.Data
{
    public static class DatabaseBootstrap
    {
        private static string _serverConnection = "Server=localhost;Uid=root;Pwd=root;";
        private static string _dbConnection = "Server=localhost;Database=BoardingHouseDB;Uid=root;Pwd=root;";

        public static void Initialize()
        {
            try
            {
                // 1. Create Database if it doesn't exist
                using (var conn = new MySqlConnection(_serverConnection))
                {
                    conn.Open();
                    var cmd = new MySqlCommand("CREATE DATABASE IF NOT EXISTS BoardingHouseDB;", conn);
                    cmd.ExecuteNonQuery();
                }

                // 2. Create Tables
                using (var conn = new MySqlConnection(_dbConnection))
                {
                    conn.Open();
                    
                    // Users Table
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

                    // Rooms Table
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

                    // Boarders Table
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

                    // Payments Table
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

                    // 3. Seed Data (Only if empty)
                    var checkCmd = new MySqlCommand("SELECT COUNT(*) FROM Users", conn);
                    long userCount = (long)checkCmd.ExecuteScalar();

                    if (userCount == 0)
                    {
                        string seedUsers = @"
                            INSERT INTO Users (Username, PasswordHash, Role) VALUES ('superadmin', '240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9', 'SuperAdmin');
                            INSERT INTO Users (Username, PasswordHash, Role) VALUES ('admin', '240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9', 'Admin');";
                        new MySqlCommand(seedUsers, conn).ExecuteNonQuery();

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
                // Re-throw to be caught in Program.cs and shown to user
                throw new Exception("Database Initialization Failed: " + ex.Message);
            }
        }
    }
}
