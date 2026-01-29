namespace BoardingHouseSys.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string? RoomNumber { get; set; }
        public int Capacity { get; set; }
        public decimal MonthlyRate { get; set; }
        public bool IsActive { get; set; }
    }
}
