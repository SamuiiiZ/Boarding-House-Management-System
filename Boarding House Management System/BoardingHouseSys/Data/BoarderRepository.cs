using System;
using System.Data;
using MySql.Data.MySqlClient;
using BoardingHouseSys.Models;

namespace BoardingHouseSys.Data
{
    public class BoarderRepository
    {
        private DatabaseHelper _dbHelper;

        public BoarderRepository()
        {
            _dbHelper = new DatabaseHelper();
        }

        public DataTable GetAllBoarders()
        {
            string query = @"
                SELECT 
                    b.Id, 
                    b.FullName, 
                    b.Address, 
                    b.Phone, 
                    r.RoomNumber, 
                    u.Username 
                FROM Boarders b
                LEFT JOIN Rooms r ON b.RoomId = r.Id
                LEFT JOIN Users u ON b.UserId = u.Id
                WHERE b.IsActive = 1";
            return _dbHelper.ExecuteQuery(query);
        }

        public void AddBoarder(Boarder boarder)
        {
            string query = @"
                INSERT INTO Boarders (UserId, FullName, Address, Phone, RoomId, IsActive) 
                VALUES (@UserId, @FullName, @Address, @Phone, @RoomId, 1)";
            
            MySqlParameter[] parameters = {
                new MySqlParameter("@UserId", boarder.UserId ?? (object)DBNull.Value),
                new MySqlParameter("@FullName", boarder.FullName),
                new MySqlParameter("@Address", boarder.Address),
                new MySqlParameter("@Phone", boarder.Phone),
                new MySqlParameter("@RoomId", boarder.RoomId ?? (object)DBNull.Value)
            };
            
            _dbHelper.ExecuteNonQuery(query, parameters);
        }
    }
}
