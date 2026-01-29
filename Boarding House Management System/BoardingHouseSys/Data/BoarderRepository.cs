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
                    b.RoomId,
                    b.UserId,
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

        public void UpdateBoarder(Boarder boarder)
        {
            string query = @"
                UPDATE Boarders 
                SET FullName = @FullName, Address = @Address, Phone = @Phone, RoomId = @RoomId 
                WHERE Id = @Id";
            
            MySqlParameter[] parameters = {
                new MySqlParameter("@Id", boarder.Id),
                new MySqlParameter("@FullName", boarder.FullName),
                new MySqlParameter("@Address", boarder.Address),
                new MySqlParameter("@Phone", boarder.Phone),
                new MySqlParameter("@RoomId", boarder.RoomId ?? (object)DBNull.Value)
            };
            
            _dbHelper.ExecuteNonQuery(query, parameters);
        }

        public void DeleteBoarder(int id)
        {
            string query = "UPDATE Boarders SET IsActive = 0 WHERE Id = @Id";
            MySqlParameter[] parameters = {
                new MySqlParameter("@Id", id)
            };
            _dbHelper.ExecuteNonQuery(query, parameters);
        }

        public Boarder? GetBoarderByUserId(int userId)
        {
            string query = @"
                SELECT b.*, r.RoomNumber, r.MonthlyRate 
                FROM Boarders b 
                LEFT JOIN Rooms r ON b.RoomId = r.Id 
                WHERE b.UserId = @UserId AND b.IsActive = 1";

            MySqlParameter[] parameters = {
                new MySqlParameter("@UserId", userId)
            };

            DataTable dt = _dbHelper.ExecuteQuery(query, parameters);
            
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                return new Boarder
                {
                    Id = Convert.ToInt32(row["Id"]),
                    UserId = row["UserId"] != DBNull.Value ? Convert.ToInt32(row["UserId"]) : null,
                    FullName = row["FullName"].ToString()!,
                    Address = row["Address"].ToString(),
                    Phone = row["Phone"].ToString(),
                    RoomId = row["RoomId"] != DBNull.Value ? Convert.ToInt32(row["RoomId"]) : null,
                    // Store extra info if needed, or we can just return the basic boarder
                    // Ideally we'd have a DTO or extended properties, but for now basic is fine.
                    // We can expose RoomNumber via a separate property or just let the Form handle it.
                };
            }
            return null;
        }

        public DataTable GetBoarderDetailsByUserId(int userId)
        {
             string query = @"
                SELECT 
                    b.Id, 
                    b.FullName, 
                    b.Address, 
                    b.Phone, 
                    r.RoomNumber, 
                    r.MonthlyRate
                FROM Boarders b
                LEFT JOIN Rooms r ON b.RoomId = r.Id
                WHERE b.UserId = @UserId AND b.IsActive = 1";

            MySqlParameter[] parameters = {
                new MySqlParameter("@UserId", userId)
            };

            return _dbHelper.ExecuteQuery(query, parameters);
        }
    }
}
