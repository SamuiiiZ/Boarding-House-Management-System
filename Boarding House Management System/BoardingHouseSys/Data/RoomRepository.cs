using System;
using System.Data;
using MySql.Data.MySqlClient;
using BoardingHouseSys.Models;

namespace BoardingHouseSys.Data
{
    public class RoomRepository
    {
        private DatabaseHelper _dbHelper;

        public RoomRepository()
        {
            _dbHelper = new DatabaseHelper();
        }

        public DataTable GetAllRooms()
        {
            string query = "SELECT Id, RoomNumber, Capacity, MonthlyRate FROM Rooms WHERE IsActive = 1";
            return _dbHelper.ExecuteQuery(query);
        }

        public void AddRoom(Room room)
        {
            string query = "INSERT INTO Rooms (RoomNumber, Capacity, MonthlyRate, IsActive) VALUES (@RoomNumber, @Capacity, @MonthlyRate, 1)";
            MySqlParameter[] parameters = {
                new MySqlParameter("@RoomNumber", room.RoomNumber),
                new MySqlParameter("@Capacity", room.Capacity),
                new MySqlParameter("@MonthlyRate", room.MonthlyRate)
            };
            _dbHelper.ExecuteNonQuery(query, parameters);
        }
        
        // Add Update/Delete as needed
    }
}
