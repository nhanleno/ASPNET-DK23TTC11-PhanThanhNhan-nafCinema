namespace nafCine.ViewModels
{
    /// <summary>
    /// ViewModel cho thông tin của một ghế khi hiển thị lên giao diện chọn ghế.
    /// </summary>
    public class SeatViewModel
    {
        // ID của ghế
        public int SeatId { get; set; }

        // Số hàng của ghế (ví dụ: "A", "B", "C"…)
        public string RowNumber { get; set; } = string.Empty;

        // Số ghế trong hàng (ví dụ: 5, 6, 7…)
        public int SeatNumber { get; set; }

        // Nhận diện ghế (ví dụ: "A5" được ghép từ RowNumber và SeatNumber)
        public string SeatIdentifier => $"{RowNumber}{SeatNumber}";

        // Trạng thái ghế: true nếu ghế đã được đặt, false nếu còn trống.
        public bool IsBooked { get; set; }
    }
}